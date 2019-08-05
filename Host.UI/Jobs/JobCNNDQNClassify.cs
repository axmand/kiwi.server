﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using Engine.Brain.Method;
using Engine.Brain.Method.Convolution;
using Engine.Brain.Method.DeepQNet;
using Engine.Brain.Method.DeepQNet.Env;
using Engine.Brain.Utils;
using Engine.GIS.GEntity;
using Engine.GIS.GLayer.GRasterLayer;
using Engine.GIS.GOperation.Tools;

namespace Host.UI.Jobs
{
    public class JobCNNDQNClassify:IJob
    {
        public double Process { get; private set; } = 0.0;

        public string Name => "CnnDqnClassificationTask";

        public string Summary { get; private set; } = "";

        public DateTime StartTime { get; private set; } = DateTime.Now;

        public bool Complete { get; private set; } = false;

        public event OnTaskCompleteHandler OnTaskComplete;

        public event OnStateChangedHandler OnStateChanged;

        Thread _t;

        public JobCNNDQNClassify(GRasterLayer featureRasterLayer, string sampleFilename, int epochs, int model, int width, int height, int channel, string deviceName)
        {
            _t = new Thread(() =>
            {
                //input list
                List<List<float>> inputList = new List<List<float>>();
                List<int> outputList = new List<int>();
                List<int> outputKey = new List<int>();
                using (StreamReader sr = new StreamReader(sampleFilename))
                {
                    string text = sr.ReadLine().Replace("\t", ",");
                    do
                    {
                        string[] rawdatas = text.Split(',');
                        int key = Convert.ToInt32(rawdatas.Last());
                        outputList.Add(key);
                        if (!outputKey.Contains(key))
                            outputKey.Add(key);
                        List<float> inputItem = new List<float>();
                        for (int i = 0; i < rawdatas.Length - 1; i++)
                            inputItem.Add(float.Parse(rawdatas[i]));
                        inputList.Add(inputItem);
                        text = sr.ReadLine();
                    } while (text != null);
                }
                //create cnn model
                Summary = "模型训练中";
                int smapleSize = outputList.Count;
                int classNum = outputKey.Count;
                int[] keysArray = outputKey.ToArray();
                int batchSize = 19;
                //LeNet CNN 
                IConvNet cnn = new FullyChannelNet9(width, height, channel, classNum, deviceName);
                //train model
                for (int i = 0; i < epochs; i++)
                {
                    float[][] inputs = new float[batchSize][];
                    float[][] labels = new float[batchSize][];
                    for (int k = 0; k < batchSize; k++)
                    {
                        int index = NP.Random(smapleSize);
                        inputs[k] = inputList[index].ToArray();
                        labels[k] = NP.ToOneHot(Array.IndexOf(keysArray, outputList[index]), classNum);
                    }
                    double loss = cnn.Train(inputs, labels);
                    Process = (double)i / epochs;
                    Summary = string.Format("loss:{0}", loss);
                }
                //训练DQN
                Summary = "DQN训练中";
                cnn.ConvertToExtractNetwork();
                float[][] svmInputs = new float[inputList.Count][];
                int[] svmOutputs = new int[inputList.Count];
                //2.recalcute smaples
                for (int i = 0; i < inputList.Count; i++)
                {
                    svmInputs[i] = cnn.Predict(inputList[i].ToArray());
                }
                for (int i = 0; i < inputList.Count; i++)
                {
                    svmOutputs[i] = outputKey.IndexOf(outputList[i]);
                }
                SamplesEnv _env = new SamplesEnv(svmInputs, svmOutputs);
                DQN dqn = new DQN(_env);
                dqn.OnLearningLossEventHandler += Dqn_OnLearningLossEventHandler;
                dqn.Learn();
                //classify
                Summary = "分类应用中";
                IRasterLayerCursorTool pRasterLayerCursorTool = new GRasterLayerCursorTool();
                pRasterLayerCursorTool.Visit(featureRasterLayer);
                int seed = 0;
                int totalPixels = featureRasterLayer.XSize * featureRasterLayer.YSize;
                byte[] buffer = new byte[totalPixels];
                //应用dqn对图像分类
                for (int i = 0; i < featureRasterLayer.XSize; i++)
                    for (int j = 0; j < featureRasterLayer.YSize; j++)
                    {
                        //get normalized input raw value
                        float[] normal = pRasterLayerCursorTool.PickRagneNormalValue(i, j, width, height);
                        //}{debug
                        var (action, q) = dqn.ChooseAction(normal);
                        int gray = dqn.ActionToRawValue(NP.Argmax(action));
                        //convert action to raw byte value
                        buffer[i + featureRasterLayer.XSize + featureRasterLayer.YSize] = Convert.ToByte(gray);
                        //report progress
                        Process = (double)(seed++) / totalPixels;
                    }
                //保存结果至tmp
                string fullFileName = Directory.GetCurrentDirectory() + @"\tmp\" + DateTime.Now.ToFileTimeUtc() + ".png";
                Bitmap classificationBitmap = GBitmap.ToGrayBitmap(buffer, featureRasterLayer.XSize, featureRasterLayer.YSize);
                classificationBitmap.Save(fullFileName);
                //complete
                Summary = "CNN训练分类完成";
                Complete = true;
                OnTaskComplete?.Invoke(Name, fullFileName);
            });
        }

        private void Dqn_OnLearningLossEventHandler(double loss, double totalReward, double accuracy, double progress, string epochesTime)
        {
            Process = progress;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullFilename"></param>
        public void Export(string fullFilename)
        {

        }
        /// <summary>
        /// start task
        /// </summary>
        public void Start()
        {
            StartTime = DateTime.Now;
            _t.IsBackground = true;
            _t.Start();
        }
    }
}
