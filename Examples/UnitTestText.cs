﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Engine.NLP;
using Engine.NLP.Annotation;
using Engine.NLP.Entity;
using Engine.NLP.Utils;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Recognizers.Text;
using Microsoft.Recognizers.Text.DateTime;
using Microsoft.Recognizers.Text.Matcher;
using Microsoft.Recognizers.Text.Number;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples
{
    /// <summary>
    /// https://github.com/microsoft/Recognizers-Text/tree/master/.NET/Samples
    /// </summary>
    [TestClass]
    public class RecognizersText
    {

        string rawTexten = "TASMAN SPIRIT, Pakistan,Incident. The Maltese tanker TASMAN SPIRIT grounded at the entrance to Karachi Port, Pakistan in the early hours on 27th July 2003. The vessel was carrying 67,800 tonnes of Iranian Light crude oil destined for the national refinery in Karachi.There were also 440 tonnes of heavy fuel oil in aft bunker tanks.The condition of the grounded tanker deteriorated as she was subjected to continuous stress from the heavy swell of the prevailing south-west monsoon and the vessel subsequently broke in two.In total, it is estimated that some 30,000 tonnes of oil was spilled from the TASMAN SPIRIT. In the course of inspections on board the TASMAN SPIRIT it became apparent that most of the cargo tanks had been ruptured, whilst the bunker tanks remained intact.The owners' appointed salvors and also hired a succession of small tankers and barges for the purpose of shuttling and storing oil lightered from the casualty. During the next few weeks roughly half of the crude oil cargo and most of the bunker fuel was successfully transferred from the casualty.On 11 August the tanker began to show signs of breaking up and eventually broke in two overnight on 13/14 August, spilling several thousand tonnes of crude oil.Much of the spilled oil quickly stranded on Clifton Beach, the main tourist beach in Karachi, but significant quantities remained afloat both inside and outside Karachi port.Dispersants were applied offshore from a Hercules C-130 aircraft equipped with an aerial dispersant spraying system(ADDS Pack) in response to two distinct pollution events involving the progressive break-up of the tanker.Approval for large scale dispersant use was given by the Karachi Port Trust (KPT) and the Pakistan Environment Protection Agency.Oil entering the port of Karachi was confined by deploying booms at suitable collection sites, and in total some 140 tonnes of oil were recovered by skimmers.KPT also deployed vessels to apply dispersant on oil drifting through the port entrance.The severe pollution of Clifton Beach created very strong oil vapours causing considerable discomfort to local residents and clean-up personnel. Local hospitals reported many cases of headaches, nausea and dizziness and seventeen schools in the vicinity were closed for about a week.The beach was cleaned by a combination of manual and mechanical means, but work was hampered by a lack of suitable disposal sites for collected oily waste.Agreement was eventually reached for disposal at one of the municipal waste sites serving Karachi City.Clifton Beach was re-opened to the public in the middle of October.Given the low persistence of Iranian Light crude oil and the high mixing energy in the many damaged cargo tanks generated by the incessant heavy swell, it is likely that most of the spilled oil dispersed naturally.Field surveys conducted showed little or no impact on mangroves, salt pans and other sensitive resources in the vicinity. The geographical extent of shoreline oiling was limited to a ten-mile radius around the grounded tanker.Whilst there were few reports of impacts of the oil on fisheries, a three-month fishing ban was imposed by the Marine Fisheries Department along the coastline directly affected by oil, extending five nautical miles offshore.";

        string rawTextzh = "2010年7月15日Tanker Pacific Management (Singapore) Pte Ltd（新加坡太平洋油轮公司）所属利比里亚籍（最初误报道为巴拿马籍[1]）“COSMIC JEWEL”(“宇宙宝石”)号30万吨级VLCC油轮在大连新港向国际储运公司原油灌区卸送中石油控股的中油燃料油股份有限公司委托中国联合石油有限责任公司进口的委内瑞拉祖阿塔原油15.3万吨，卸载入中国联合石油有限责任公司租赁的国际储运公司原油灌区304、401、403号罐。由于该原油H2S含量较高，中油燃料油股份有限公司委托天津辉盛达石化技术有限公司负责加入原油脱硫剂作业。辉盛达公司委托上海祥诚商品检验技术服务有限公司大连分公司在国际储运公司原油罐区输油管道上进行现场作业。所添加的“HD-硫化氢脱除剂”原油脱硫剂由辉盛达公司生产。卸油作业于7月15日15时30分开始，在两条输油管道同时进行。7月15日20时，油轮开始用2号输油管线向国际储运公司的原油罐区卸送，祥诚公司作业人员开始通过原油罐区内一套内径90厘米输油管道上的排空阀向输油管道内注入脱硫剂。加剂过程中由于由于输油管内压力高，加注软管多处出现超压鼓泡，连接处脱落造成脱硫化剂泄漏等情况，致使加注作业多次中断共计约4个小时，以致未能按计划在17小时卸油作业中加入全部的脱硫剂。7月16日13时，油轮进行原油洗舱集油作业，停止向岸上卸油并关闭船岸间控制阀。此时，中石油大连石化公司石油储运公司生产调度通知上海祥诚大连分公司经理“船上停止卸油了”，但注入脱硫剂的作业没有停止，又继续加入了22.6t脱硫剂。18时，在注入了全部的88立方米脱硫剂后，现场作业人员用消防泵房（位于103号油罐东侧）内的消防水对脱硫剂管路和泵进行冲洗，冲洗液0.1t直接经加剂口入该输油管线。18时02分，靠近脱硫剂注入部位的输油管道突然发生爆炸，引发火灾，造成部分输油管道、附近储罐阀门、输油泵房和电力系统损坏和大量原油泄漏。事故导致储罐阀门无法及时关闭，火灾不断扩大。原油顺地下管沟流淌，形成地面流淌火，火势蔓延。";

        TextAnalyticsClient _client;

        public RecognizersText()
        {
            ApiKeyServiceClientCredentials credentials = new ApiKeyServiceClientCredentials(NLPConfiguration.SubscriptionKey);
            _client = new TextAnalyticsClient(credentials) { Endpoint = NLPConfiguration.Endpoint };
        }

        /// <summary>
        /// 命名实体识别
        /// </summary>
        /// <param name="client"></param>
        /// <param name="rawText"></param>
        /// <returns></returns>
        private static async Task<EntitiesBatchResult> RecognizeNamedEntity(TextAnalyticsClient client, List<string> sentences)
        {
            List<MultiLanguageInput> list = new List<MultiLanguageInput>() {
                new MultiLanguageInput(language:"en",id: "0",text: string.Join(" ", sentences.ToArray()))
            };
            MultiLanguageBatchInput inputDocuments = new MultiLanguageBatchInput(list);
            EntitiesBatchResult entitiesResult = await client.EntitiesAsync(false, inputDocuments);
            return entitiesResult;
        }

        private static async Task<KeyPhraseBatchResult> RecognizeKeyPhrase(TextAnalyticsClient client, List<string> sentences)
        {
            List<MultiLanguageInput> list = new List<MultiLanguageInput>() {
                new MultiLanguageInput(language:"en",id: "0",text: string.Join(" ", sentences.ToArray()))
            };
            MultiLanguageBatchInput inputDocuments = new MultiLanguageBatchInput(list);
            KeyPhraseBatchResult keyPhraseResult = await client.KeyPhrasesAsync(multiLanguageBatchInput: inputDocuments);
            return keyPhraseResult;
        }

        private static async Task<KeyPhraseBatchResult> RecognizeEntityMention(TextAnalyticsClient client, List<string> sentences)
        {
            List<MultiLanguageInput> list = new List<MultiLanguageInput>() {
                new MultiLanguageInput(language:"en",id: "0",text: string.Join(" ", sentences.ToArray()))
            };
            MultiLanguageBatchInput inputDocuments = new MultiLanguageBatchInput(list);
            KeyPhraseBatchResult keyPhraseResult = await client.KeyPhrasesAsync(multiLanguageBatchInput: inputDocuments);
            return keyPhraseResult;
        }

        [TestMethod]
        public void RecognizeTextNumber()
        {
            List<ModelResult> result = NumberRecognizer.RecognizeNumber(rawTexten, Culture.English);
        }

        [TestMethod]
        public void RecognizeTextDatetime()
        {
            DateTimeRecognizer recognizer = new DateTimeRecognizer(Culture.English);
            DateTimeModel model = recognizer.GetDateTimeModel();
            List<ModelResult> result = model.Parse(rawTexten);
        }

        [TestMethod]
        public void RecognizeTextTokenize()
        {
            var result = new SimpleTokenizer().Tokenize(rawTexten);
        }

        [TestMethod]
        public void RecognizeScenario()
        {
            //1. timeline group
            SentenceGroup sGroup = new SentenceGroup(rawTextzh);
            sGroup.RegroupByTimeline(Culture.Chinese);
            //2. annotation
            IAnnotation annotation = new DepenencyParseAnnotation();
            foreach (var group in sGroup.Groups)
            {
                string rawText = string.Join(".", group.Value.ToArray());
                annotation.Process(rawText);
            }

            //2. key parse for each group
            //foreach (var group in sGroup.Groups)
            //{
            //    //1. entity
            //    EntitiesBatchResult entityResult = RecognizeNamedEntity(_client, group.Value).Result;
            //    KeyPhraseBatchResult keyPhraseResult = RecognizeKeyPhrase(_client, group.Value).Result;
            //    //2. get location and time as scenario 
            //    for(int i=0;i<sGroup.Groups.Count;i++)
            //    {
            //        EntitiesBatchResultItem entityDoc = entityResult.Documents.FirstOrDefault();
            //        KeyPhraseBatchResultItem keyPhraseDoc = keyPhraseResult.Documents.FirstOrDefault();
            //        //3. 根据keyphrase提取的关键词，处理entity

                //    }
                //}

                //3. number with unit parse

                //4. GolVe embedding

                //5. property
        }

        [TestMethod]
        public void RegroupTextByTimeline()
        {
            // split into sentences
            SentenceGroup group = new SentenceGroup(rawTexten);
            group.RegroupByTimeline();
        }

        [TestMethod]
        public void AnalysisTextEntity()
        {
            //EntitiesBatchResult entitiesResult = RecognizeNamedEntity(_client, rawText).Result;
            //KeyPhraseBatchResult keyPhraseResult = RecognizeKeyPhrase(_client, rawText).Result;
        }


    }
}
