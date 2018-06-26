﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.RL.DQN
{
    /// <summary>
    /// action枚举
    /// </summary>
    public enum EAction
    {
        /// <summary>
        /// 水稻
        /// </summary>
        RICE = 1,
        /// <summary>
        /// 小麦
        /// </summary>
        WHEAT=2,
        /// <summary>
        /// 烟草
        /// </summary>
        TOBACCO=3
    }

    /// <summary>
    /// 输入样本通过多次cnn卷积，输出一个相对简单的特征向量，用于计算(s)
    /// 任务：
    /// 1.观测并读取训练样本
    /// 2.多层cnn，降维生成样本特征向量
    /// 3.计算reward(给出reward的量化方法）
    /// </summary>
    public class Environment
    {

        string _dir;

        /// <summary>
        /// 构建环境
        /// 样本存放格式形如：
        /// sampleDirectory
        ///              |
        ///          /       \
        ///    分类1    分类2 ...
        ///        |           |
        ///    /       \       \
        /// 样本a 样本b  样本c
        /// </summary>
        public Environment(string sampleDirectory)
        {
            //样本根目录
            _dir = sampleDirectory;
            //获取样本分类目录
            string[] categories = Directory.GetDirectories(sampleDirectory);
            //样本集合
            Dictionary<string, string[]> sampleDictionary = new Dictionary<string, string[]>();
            //初始化环境
            InitEnv(categories, sampleDictionary);
        }
        /// <summary>
        /// 构建样本字典
        /// </summary>
        private void InitEnv(string[] categories, Dictionary<string, string[]> sampleDictionary)
        {
            //构建dictory目录树
            Array.ForEach(categories, p => {
                //1.获取样本目录
                string dir = _dir + @"\" + p;
                //2.获取样本全集
                string[] samples = Directory.GetFiles(dir);
                //3.载入字典
                sampleDictionary.Add(p, samples);
            });
        }
        /// <summary>
        /// 执行下一步操作
        /// 返回：操作后的环境s'和当前的reward
        /// 1. 根据当前的s计算reward
        /// 2. 计算下一个s（s'）
        /// </summary>
        public double[] Step(EAction action)
        {
            //1.

            //2.

            return null;
        }
        /// <summary>
        /// 重设环境
        /// </summary>
        public void Reset()
        {

        }

    }


}
