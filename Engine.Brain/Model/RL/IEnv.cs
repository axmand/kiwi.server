﻿using System.Collections.Generic;
using System.Drawing;

namespace Engine.Brain.AI.RL
{
    /// <summary>
    /// 定义基本环境编写接口
    /// </summary>
    public interface IEnv
    {
        /// <summary>
        /// exprot the sample
        /// </summary>
        void Export(string fullFilename, int row =1, int col =1);
        /// <summary>
        /// indicate the action can be assigned by multi-action value
        /// e.g. 
        /// only support [0001] while the value is true, indicate the agent only do one-kind action at once
        /// support [1001] while the value is false, indicate the agent do two types action at once
        /// </summary>
        bool SingleAction { get; }
        /// <summary>
        /// get action - rawValue dictionary map
        /// </summary>
        int[] RandomSeedKeys { get; }
        /// <summary>
        /// number of actions
        /// </summary>
        int ActionNum { get; }
        /// <summary>
        /// number of features
        /// FeatureNum[0] represent Channel
        /// FeatureNum[1] represent image Width if possible
        /// FeatureNum[2] represent image Height if possible
        /// </summary>
        int[] FeatureNum { get; }
        /// <summary>
        /// 验证数据集
        /// </summary>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        (List<double[]> states, double[][] labels) RandomEval(int pickWidth=5, int pickHight=5,int batchSize = 64);
        /// <summary>
        /// get sate/reward/q/sate next(state_) (one hot)
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        (double[] state, double reward) Step(double[] action);
        /// <summary>
        /// crate an action located in action range
        /// </summary>
        /// <returns></returns>
        double[] RandomAction();
        /// <summary>
        /// 重置环境
        /// </summary>
        double[] Reset();
    }
}