﻿namespace osuTools
{
    namespace Online.ApiV1
    {
        
        using Newtonsoft.Json;
        using Newtonsoft.Json.Linq;
        using OsuRTDataProvider.BeatmapInfo;
        using Sync.Tools;
        using System;
        using System.Collections.Generic;
        using Online.ApiV1.Querier;
        using System.Text;
        using System.IO;
        using System.Diagnostics.Contracts;

        /// <summary>
        /// 存储最高PP榜指定数量的记录，最多100个。
        /// </summary>
        public class OnlineBestRecordCollection : OnlineInfo<OnlineBestRecord>
        {
            List<OnlineBestRecord> records = new List<OnlineBestRecord>();
            /// <summary>
            /// 存储的记录
            /// </summary>
            public List<OnlineBestRecord> Records { get => records; }
            /// <summary>
            /// 指示此次查询是否失败
            /// </summary>
            public bool Failed { get; internal set; }
            /// <summary>
            /// 使用整数索引从列表获取BestRecord
            /// </summary>
            /// <param name="x"></param>
            /// <returns></returns>
            public OnlineBestRecord this[int x]
            {
                get => records[x];
            }
            /// <summary>
            /// 初始化OnlineBestRecordCollection的新实例
            /// </summary>
            public OnlineBestRecordCollection()
            {
            }
            /// <summary>
            /// 获取成绩列表的枚举器
            /// </summary>
            /// <returns></returns>
            public IEnumerator<OnlineBestRecord> GetEnumerator()
            {
                return records.GetEnumerator();
            }
        }
        /// <summary>
        /// 最高PP榜中的记录
        /// </summary>
        [Serializable]
        public partial class OnlineBestRecord :PPSorted, IFormattable
        {

            bool per;
            bool findInit = false;
            Beatmaps.BeatmapCollection BC;
            DateTime d;
            /// <summary>
            /// 使用了的Mod
            /// </summary>
            public List<OsuGameMod> Mods { get; } = new List<OsuGameMod>();
            /// <summary>
            /// 准度
            /// </summary>
            public double Accuracy { get; private set; }
            /// <summary>
            /// 游戏模式
            /// </summary>
            public OsuGameMode Mode { get; private set; }
            int mods;
            int
            beatmap_id,
            score_id;
            double pp,wpp;
            int
            score,
            maxcombo,
            count50,
            count100,
            count300,
            countmiss,
            countkatu,
            countgeki,
            perfect,
            user_id;
            string
            date,
            rank;
            /// <summary>
            /// 获取该成绩对应的谱面
            /// </summary>
            /// <returns></returns>
            public OnlineBeatmap GetOnlineBeatmap()
            {
                OnlineBeatmapQuery q = new OnlineBeatmapQuery();
                string osuApiKey = "fa2748650422c84d59e0e1d5021340b6c418f62f";
                q.OsuApiKey = osuApiKey;
                q.BeatmapID = beatmap_id;
                var beatmap = q.Beatmaps[0];
                return beatmap;
            }
            /// <summary>
            /// 获取游玩该谱面的玩家的信息
            /// </summary>
            /// <returns></returns>
            public OnlineUser GetUser()
            {
               
                OnlineUser user = new OnlineUser();
                return user;

            }
            double AccCalc(OsuGameMode mode)
            {
                double c3g = c300g, c3 = c300, c2 = c200, c1 = c100, c5 = c50, cm = cMiss;
                double a2 = (2.0 / 3), a1 = (1.0 / 3), a5 = (1.0 / 6);
                double mall = c3 + c3g + c2 + c1 + c5 + cm;
                double sall = c3 + c1 + c5 + cm;
                double call = c3 + c1 + c2 + c5 + cm;
                double tall = c3 + c3g + c1 + c2 + cm;
                switch (mode)
                {
                    case OsuGameMode.Catch: return (c3 + c1 + c5) / call;
                    case OsuGameMode.Osu: return (c3 + c1 * a1 + c5 * a5) / sall;
                    case OsuGameMode.Taiko: return (c3 + c3g + (c1 + c2) * a1) / tall;
                    case OsuGameMode.Mania: return (c3 + c3g + c2 * a2 + c1 * a1 + c5 * a5) / mall;
                    default: return 0;
                }
            }
            /// <summary>
            /// 使用指定的格式构建字符串
            /// </summary>
            /// <param name="format">格式</param>
            /// <param name="formatProvider"></param>
            /// <returns></returns>
            public string ToString(string format, IFormatProvider formatProvider)
            {
                
                StringBuilder b = new StringBuilder(format);
                b.Replace("perfect", Perfect.ToString());
                b.Replace("pp", PP.ToString());
                b.Replace("c300g", c300g.ToString());
                b.Replace("c300", c300.ToString());
                b.Replace("c200", c200.ToString());
                b.Replace("c100", c100.ToString());
                b.Replace("c50", c50.ToString());
                b.Replace("cMiss", cMiss.ToString());
                b.Replace("userid", UserID.ToString());
                b.Replace("rank", Rank);
                b.Replace("playtime", d.ToString("yyyy/MM/dd HH:mm:ss"));
                b.Replace("score", Score.ToString());
                b.Replace("beatmapid", BeatmapID.ToString());
                b.Replace("maxcombo", MaxCombo.ToString());
                b.Replace("acc", Accuracy.ToString("p2"));
                return b.ToString();
            }
            /// <summary>
            /// 使用指定的格式构建字符串
            /// </summary>
            /// <param name="format">格式</param>
            /// <returns></returns>
            public string ToString(string format)
            {
                return ToString(format, null);
            }
            /// <summary>
            /// 初始化一个新的OnlineBestRecord实例
            /// </summary>
            public OnlineBestRecord()
            {
                per = false;
                d = DateTime.MinValue;
                beatmap_id = 0;
                score_id = 0;
                score = 0;
                pp = 0.0;
                wpp = 0.0;
                maxcombo = 0;
                count300 = 0;
                count100 = 0;
                count50 = 0;
                countgeki = 0;
                countkatu = 0;
                countmiss = 0;
                perfect = 0;
                user_id = 0;
                date = "0-0-0 0:0:0";
                Mods = Beatmaps.HitObject.HitObjectTools.GetGenericTypesByInt<OsuGameMod>(mods);
                rank = "?";
            }
            internal void CalcWeight(int index)
            {
                wpp = pp * (Math.Pow(0.95, index));
            }
            /// <summary>
            /// 使用json填充一个OnlineBestRecord对象,并指定模式
            /// </summary>
            /// <param name="json"></param>
            /// <param name="mode"></param>
            public OnlineBestRecord(string json,OsuGameMode mode)
            {
                Mode = mode;
                var jobj = (JObject)(JsonConvert.DeserializeObject(json));
                int.TryParse(jobj["countgeki"].ToString(), out countgeki);
                int.TryParse(jobj["countkatu"].ToString(), out countkatu);
                int.TryParse(jobj["count300"].ToString(), out count300);
                int.TryParse(jobj["count100"].ToString(), out count100);
                int.TryParse(jobj["count50"].ToString(), out count50);
                int.TryParse(jobj["countmiss"].ToString(), out countmiss);
                int.TryParse(jobj["maxcombo"].ToString(), out maxcombo);
                int.TryParse(jobj["score"].ToString(), out score);
                int.TryParse(jobj["user_id"].ToString(), out user_id);
                int.TryParse(jobj["perfect"].ToString(), out perfect);
                int.TryParse(jobj["enabled_mods"].ToString(), out mods);
                int.TryParse(jobj["beatmap_id"].ToString(), out beatmap_id);
                int.TryParse(jobj["score_id"].ToString(), out score_id);
                double.TryParse(jobj["pp"].ToString(), out pp);
                date = jobj["date"].ToString();
                rank = jobj["rank"].ToString();
                Mods = Beatmaps.HitObject.HitObjectTools.GetGenericTypesByInt<OsuGameMod>(mods);
                Accuracy = AccCalc(mode);
                DateTime.TryParse(date, out d);
                if (perfect == 1)
                {
                    per = true;
                }
                else if (perfect == 0)
                {
                    per = false;
                }
            }
            /// <summary>
            /// 使用osu!api获得相应谱面的信息并转换成Beatmap
            /// </summary>
            /// <returns>返回一个<seealso cref="Beatmaps.Beatmap"/>对象</returns>
            public Beatmaps.Beatmap GetBeatmap()
            {
                Beatmaps.Beatmap b = new Beatmaps.Beatmap();
                OnlineBeatmapQuery query = new OnlineBeatmapQuery();
                string osuApiKey = "fa2748650422c84d59e0e1d5021340b6c418f62f";
                query.BeatmapID = beatmap_id;
                query.OsuApiKey = osuApiKey;
                var bms = query.Beatmaps;
                b = new Beatmaps.Beatmap(bms[0]);
                if (b.BeatmapID == -2048)
                    b = null;
                return b;
            }
            /*
            /// <summary>
            /// 使用osu!api获得相应谱面的信息
            /// </summary>
            /// <returns>返回一个<seealso cref="OnlineBeatmap"/>对象</returns>
            public OnlineBeatmap GetOnlineBeatmap()
            {
                OnlineBeatmapCollection bms = new OnlineBeatmapCollection();
                OsuApiQuery q = new OsuApiQuery();
                string osuApiKey = "fa2748650422c84d59e0e1d5021340b6c418f62f";
                q.QueryType = OsuApiQueryType.Beatmaps;
                q.ApiKey = osuApiKey;
                q.BeatmapID = (int)beatmap_id;
                bms.AllParse(q);
                return bms[0];
            }*/
            System.Threading.Tasks.Task findbeatmaptask;
            /// <summary>
            /// 在本地的谱面集合中寻找对应谱面
            /// </summary>
            /// <param name="bc">谱面集合</param>
            /// <returns>查找到的谱面</returns>
            public Beatmaps.Beatmap FindInBeatmapCollection(Beatmaps.BeatmapCollection bc)
            {

                if (bc is null) throw new NullReferenceException();
                BC = bc;
                return bc.Find((int)beatmap_id);
            }
            /// <summary>
            /// 将查询到的数据转换成一定格式的字符串
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                try
                {
                    if (BC == null)
                        return $"{beatmap_id}\nScore:{Score} PP:{PP}\nc300g:{c300g} c300:{c300} c200:{c200} c100:{c100} c50:{c50} cMiss:{cMiss} MaxCombo:{MaxCombo}\nPerfect:{Perfect}";
                    else
                    {
                        var v = FindInBeatmapCollection(BC);
                        if (v == null) throw new NullReferenceException();
                        return $"{v.ToString()}\nScore:{Score} PP:{PP}\nc300g:{c300g} c300:{c300} c200:{c200} c100:{c100} c50:{c50} cMiss:{cMiss} MaxCombo:{MaxCombo}\nPerfect:{Perfect}";
                    }
                }
                catch
                {

                    return "操作失败";
                }
            }                      
        }
    }
}