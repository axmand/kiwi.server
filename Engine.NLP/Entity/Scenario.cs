﻿using System.Collections.Generic;

namespace Engine.NLP.Entity
{
    /// <summary>
    /// 
    /// 定义一个情景是由多个包含动态趋势定义的
    /// </summary>
    public class Scenario
    {
        readonly List<Pipline> _piplines = new List<Pipline>();

        public void MergePipline(Pipline p)
        {
            _piplines.Add(p);
        }

        public void MergePipline(List<Pipline> plines)
        {
           _piplines.AddRange(plines);
        }

    }
}
