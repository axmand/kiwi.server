﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Engine.NLP;
using Engine.NLP.Analysis;

namespace Host.UI.Jobs
{
    class JobAnnotationText:IJob
    {
        public double Process { get; private set; } = 0.0;

        public string Name { get; private set; } = "AnnotationTextTask";

        public string Summary { get; private set; } = "";

        public DateTime StartTime { get; private set; } = DateTime.Now;

        public bool Complete { get; private set; } = false;

        public event OnTaskCompleteHandler OnTaskComplete;

        public event OnStateChangedHandler OnStateChanged;

        Thread _t;

        public JobAnnotationText(string rawText)
        {
            _t = new Thread(() =>
            {
                //1. select annotator type and process text
                IAnnotation annotator = new TimeMarkupAnnotation();
                annotator.Process(rawText);
                //
                Complete = true;
                OnTaskComplete?.Invoke(Name);
            });
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
