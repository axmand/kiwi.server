﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using Engine.Brain.Method;
using Engine.Brain.Method.DeepQNet;
using Engine.Brain.Method.DeepQNet.Env;
using Engine.Brain.Method.DeepQNet.Net;
using Engine.Brain.Utils;
using Engine.GIS.GLayer.GRasterLayer;
using Engine.GIS.GOperation.Tools;

namespace Host.UI.Jobs
{
    /// <summary>
    /// image classification based on dqn
    /// </summary>
    public class JobDQNClassify : IJob
    {
        /// <summary>
        /// background thread
        /// </summary>
        Thread _t;

        /// <summary>
        /// 
        /// </summary>
        DQN _dqn;

        /// <summary>
        /// 
        /// </summary>
        float _gamma = 0.0f;

        /// <summary>
        /// task name
        /// </summary>
        public string Name => "DqnClassificationTask";

        /// <summary>
        /// run process
        /// </summary>
        public double Process { get; private set; } = 0.0;

        /// <summary>
        /// task start time
        /// </summary>
        public DateTime StartTime { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Summary { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Complete { get; private set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public event OnTaskCompleteHandler OnTaskComplete;

        /// <summary>
        /// 
        /// </summary>
        public event OnStateChangedHandler OnStateChanged;

        /// <summary>
        /// 
        /// </summary>
        IEnv _env;

        /// <summary>
        /// classification tast by DQN
        /// </summary>
        /// <param name="featureRasterLayer"></param>
        /// <param name="envSampleFilename"></param>
        /// <param name="epochs"></param>
        public JobDQNClassify(GRasterLayer featureRasterLayer, string envSampleFilename, string supportNetName,string deviceName, int width, int height, int depth, int epochs = 3000)
        {
            _t = new Thread(() =>
            {
                List<List<float>> inputList = new List<List<float>>();
                List<int> outputList = new List<int>();
                List<int> keys = new List<int>();
                using (StreamReader sr = new StreamReader(envSampleFilename))
                {
                    string text = sr.ReadLine().Replace("\t", ",");
                    do
                    {
                        string[] rawdatas = text.Split(',');
                        int key = Convert.ToInt32(rawdatas.Last());
                        outputList.Add(key);
                        if (!keys.Contains(key))
                            keys.Add(key);
                        List<float> inputItem = new List<float>();
                        for (int i = 0; i < rawdatas.Length - 1; i++)
                            inputItem.Add(float.Parse(rawdatas[i]));
                        inputList.Add(inputItem);
                        text = sr.ReadLine();
                    } while (text != null);
                    //转换成指定类型
                    int count = inputList.Count;
                    float[][] x = new float[count][];
                    int[] y = outputList.ToArray();
                    for (int i = 0; i < count; i++)
                        x[i] = inputList[i].ToArray();
                    //构造DQN样本环境
                    _env = new SamplesEnv(x, y);
                }
                Summary = "模型训练中";
                //create actor and critic
                IDNet actor = null, critic = null;
                if(supportNetName == typeof(DNetDNN).Name)
                {
                    actor = new DNetDNN(new int[] { width, height, depth }, _env.ActionNum);
                    critic = new DNetDNN(new int[] { width, height, depth }, _env.ActionNum);
                }
                else if(supportNetName == typeof(DNetCNN).Name)
                {
                    actor = new DNetCNN(deviceName, width, height, depth, _env.ActionNum);
                    critic = new DNetCNN(deviceName, width, height, depth, _env.ActionNum);
                }
                _dqn = new DQN(_env, actor, critic ,epochs: epochs, gamma: _gamma);
                _dqn.OnLearningLossEventHandler += _dqn_OnLearningLossEventHandler;
                _dqn.Learn();
                //classification
                Summary = "分类应用中";
                IRasterLayerCursorTool pRasterLayerCursorTool = new GRasterLayerCursorTool();
                pRasterLayerCursorTool.Visit(featureRasterLayer);
                Bitmap classificationBitmap = new Bitmap(featureRasterLayer.XSize, featureRasterLayer.YSize);
                Graphics g = Graphics.FromImage(classificationBitmap);
                int seed = 0;
                int totalPixels = featureRasterLayer.XSize * featureRasterLayer.YSize;
                for (int i = 0; i < featureRasterLayer.XSize; i++)
                    for (int j = 0; j < featureRasterLayer.YSize; j++)
                    {
                        //get normalized input raw value
                        float[] normal = pRasterLayerCursorTool.PickRagneNormalValue(i, j, width, height);
                        var (action, q) = _dqn.ChooseAction(normal);
                        //convert action to raw byte value
                        int gray = _dqn.ActionToRawValue(NP.Argmax(action));
                        Color c = Color.FromArgb(gray, gray, gray);
                        Pen p = new Pen(c);
                        SolidBrush brush = new SolidBrush(c);
                        g.FillRectangle(brush, new Rectangle(i, j, 1, 1));
                        //report progress
                        Process = (double)(seed++) / totalPixels;
                    }
                //save result
                string fullFileName = Directory.GetCurrentDirectory() + @"\tmp\" + DateTime.Now.ToFileTimeUtc() + ".png";
                classificationBitmap.Save(fullFileName);
                //complete
                Summary = "DQN分类完成";
                Complete = true;
                OnTaskComplete?.Invoke(Name, fullFileName);
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loss"></param>
        /// <param name="totalReward"></param>
        /// <param name="accuracy"></param>
        /// <param name="progress"></param>
        /// <param name="epochesTime"></param>
        private void _dqn_OnLearningLossEventHandler(double loss, double totalReward, double accuracy, double progress, string epochesTime)
        {
            Process = progress;
            Summary = string.Format("accuracy: {0:P}, loss:{1:0.000}, reward:{2}", accuracy, loss, totalReward);
        }
        /// <summary>
        /// example samples
        /// </summary>
        /// <param name="fullFilename"></param>
        public void Export(string fullFilename)
        {
            _env.Export(fullFilename);
        }
        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            StartTime = DateTime.Now;
            _t.IsBackground = true;
            _t.Start();
        }
    }
}
