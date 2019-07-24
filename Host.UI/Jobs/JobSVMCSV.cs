﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Engine.Brain.Model.ML;

namespace Host.UI.Jobs
{
    public class JobSVMCSV:IJob
    {
        public bool Complete { get; private set; } = false;

        public string Name => "SVMCSVClassificationTask";

        public string Summary { get; private set; } = "";

        public double Process { get; private set; } = 0.0;

        public DateTime StartTime { get; private set; } = DateTime.Now;

        public event OnTaskCompleteHandler OnTaskComplete;

        public event OnStateChangedHandler OnStateChanged;

        Thread _t;

        public JobSVMCSV(string sampleFullFilename, string waitFullFilename, string saveFullFilename)
        {
            _t = new Thread(() =>
            {
                Summary = "SVM训练中";
                L2SVM svm;
                List<int> outputKey = new List<int>();
                using (StreamReader sr = new StreamReader(sampleFullFilename))
                {
                    List<List<float>> inputList = new List<List<float>>();
                    List<int> outputList = new List<int>();
                    string text = sr.ReadLine().Replace("\t", ",").Replace("N/A", "0");
                    do
                    {
                        text = text.Replace("N/A", "0");
                        string[] rawdatas = text.Split(',');
                        //make sure the label classes from 0
                        int output = Convert.ToInt32(rawdatas.Last());
                        outputList.Add(output);
                        if (!outputKey.Contains(output)) outputKey.Add(output);
                        List<float> inputItem = new List<float>();
                        for (int i = 0; i < rawdatas.Length - 1; i++)
                            inputItem.Add(float.Parse(rawdatas[i]));
                        inputList.Add(inputItem);
                        text = sr.ReadLine();
                    } while (text != null);
                    float[][] inputs = new float[inputList.Count][];
                    int[] outputs = new int[inputList.Count];
                    for (int i = 0; i < inputList.Count; i++)
                    {
                        inputs[i] = inputList[i].ToArray();
                    }
                    for (int i = 0; i < inputList.Count; i++)
                    {
                        outputs[i] = outputKey.IndexOf(outputList[i]);
                    }
                    int inputDiminsion = inputs[0].Length;
                    int outputDiminsion = outputKey.Count;
                    svm = new L2SVM(inputDiminsion, outputDiminsion);
                    svm.Train(inputs, outputs);
                }
                Summary = "分类应用中";
                using (StreamReader sr = new StreamReader(waitFullFilename))
                {   
                    using(StreamWriter sw = new StreamWriter(saveFullFilename))
                    {
                        Process = 0.0;
                        string text = sr.ReadLine().Replace("\t", ",").Replace("N/A","0");
                        while (text != null)
                        {
                            text = text.Replace("N/A", "0");
                            string[] rawdatas = text.Split(',');
                            float[][] inputs = new float[1][];
                            List<float> inputItem = new List<float>();
                            for (int i = 0; i < rawdatas.Length; i++)
                                inputItem.Add(float.Parse(rawdatas[i]));
                            inputs[0] = inputItem.ToArray();
                            int[] ouputs = svm.Predict(inputs);
                            int classtype = outputKey[ouputs[0]];
                            sw.WriteLine(classtype);
                            text = sr.ReadLine();
                            Process++;
                        }
                    }
                }
                //rf complete
                Summary = "SVM训练和分类完成";
                Complete = true;
                OnTaskComplete?.Invoke(Name, "");
            });
        }


        public void Export(string fullFilename)
        {

        }

        public void Start()
        {
            StartTime = DateTime.Now;
            _t.IsBackground = true;
            _t.Start();
        }
    }
}
