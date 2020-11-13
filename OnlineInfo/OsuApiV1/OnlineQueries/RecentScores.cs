namespace osuTools
{
    namespace Online.ApiV1
    {
        using Newtonsoft.Json;
        using Newtonsoft.Json.Linq;
        using Online.ApiV1.Querier;
        using System;
        using System.Collections.Generic;
        /// <summary>
        /// 存储最近24小时打出所有成绩
        /// </summary>
        public class OnlineRecentResultCollection : OnlineInfo<RecentOnlineResult>
        {
            int x = 0;
            List<RecentOnlineResult> results = new List<RecentOnlineResult>();
            int p = -1;
            /// <summary>
            /// 指示本次查询是否成功
            /// </summary>
            public bool Failed { get; internal set; } = false;
            /// <summary>
            /// 返回循环访问List的枚举数。
            /// </summary>
            /// <returns></returns>
            public IEnumerator<RecentOnlineResult> GetEnumerator()
            {
                return results.GetEnumerator();
            }
            /// <summary>
            /// 使用整数索引从列表中获取记录
            /// </summary>
            /// <param name="x"></param>
            /// <returns></returns>
            public RecentOnlineResult this[int x]
            {
                get => results[x];
            }
            /// <summary>
            /// 存储最近24小时内的记录的列表
            /// </summary>
            public List<RecentOnlineResult> RecentResults { get => results; }
            /// <summary>
            /// 初始化一个RecentOnlineResultCollection类
            /// </summary>
            public OnlineRecentResultCollection()
            {
                
            }
            
        }
        /// <summary>
        /// 最近24小时打出的成绩
        /// </summary>
        [Serializable]
        public partial class RecentOnlineResult:ScoreSorted,IComparable<RecentOnlineResult>,IFormattable
        {
            bool per;
            DateTime d;
            /// <summary>
            /// 本次游戏使用的Mods
            /// </summary>
            public List<OsuGameMod> Mods { get; } = new List<OsuGameMod>();
            int
            beatmap_id;
            int score;
            int mod;
            int
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
            double acc;
            /// <summary>
            /// 游戏模式
            /// </summary>
            public OsuGameMode Mode { get; private set; }
            /// <summary>
            /// 准度
            /// </summary>
            public double Accuracy { get; private set; }
            /// <summary>
            /// 与另一个RecentOnlineResult的分数进行比较
            /// </summary>
            /// <param name="r"></param>
            /// <returns></returns>
            public int CompareTo(RecentOnlineResult r)
            {
                if (score > r.score) return -1;
                if (score == r.score) return 0;
                if (score < r.score) return 1;
                return 0;
            }
            /// <summary>
            /// 创造一个空白的RecentOnlineResult对象
            /// </summary>
            public RecentOnlineResult()
            {
                per = false;
                d = DateTime.MinValue;
                beatmap_id = 0;
                score = 0;
                mod = 0;
                maxcombo = 0;
                Mods = Beatmaps.HitObject.HitObjectTools.GetGenericTypesByInt<OsuGameMod>(mod);
                count300 = 0;
                count100 = 0;
                count50 = 0;
                countgeki = 0;
                countkatu = 0;
                countmiss = 0;
                perfect = 0;
                user_id = 0;
                date = "0-0-0 0:0:0";
                rank = "?";
            }
            /// <summary>
            /// 获取该成绩对应的谱面
            /// </summary>
            /// <returns></returns>
            public OnlineBeatmap GetOnlineBeatmap()
            {
                OnlineBeatmapQuery q = new OnlineBeatmapQuery();
                q.OsuApiKey = OnlineQueryTools.DefaultOsuApiKey;
                q.BeatmapID = beatmap_id;
                OnlineBeatmap beatmap = q.Beatmaps[0];
                return beatmap;
            }
            /// <summary>
            /// 获取游玩该谱面的玩家的信息
            /// </summary>
            /// <returns></returns>
            public OnlineUser GetUser()
            {

                OnlineUserQuery q = new OnlineUserQuery();
                q.UserID = user_id;
                q.OsuApiKey = OnlineQueryTools.DefaultOsuApiKey;
                return q.UserInfo;
                

            }
            double AccCalc(OsuGameMode mode)
            {
                double c3g = c300g,c3=c300,c2=c200,c1=c100,c5=c50,cm=cMiss;
                double a3g = 1, a3 = 1, a2 = (2.0 / 3), a1 = (1.0 / 3), a5 = (1.0 / 6);
                double mall = c3 + c3g + c2 + c1 + c5 + cm;
                double sall = c3 + c1 + c5 + cm;
                double call = c3 + c1 + c2 + c5 + cm;
                double tall = c3 + c3g + c1 + c2 + cm;
                switch(mode)
                {
                    case OsuGameMode.Catch:return (c3 + c1 + c5) / call;
                    case OsuGameMode.Osu:return (c3 + c1*a1 + c5*a5) / sall;
                    case OsuGameMode.Taiko:return (c3 + c3g + (c1 + c2) * a1) / tall;
                    case OsuGameMode.Mania:return (c3 + c3g + c2 * a2 + c1 * a1 + c5 * a5) / mall;
                    default: return 0;
                }
            }
            /// <summary>
            /// 使用json字符串和游戏模式初始化一个RecentOnlineResult
            /// </summary>
            /// <param name="json"></param>
            /// <param name="mode"></param>
            public RecentOnlineResult(string json,OsuGameMode mode)
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
                int.TryParse(jobj["enabled_mods"].ToString(), out mod);
                Mods = Beatmaps.HitObject.HitObjectTools.GetGenericTypesByInt<OsuGameMod>(mod);
                int.TryParse(jobj["beatmap_id"].ToString(), out beatmap_id);                
                date = jobj["date"].ToString();
                rank = jobj["rank"].ToString();
                DateTime.TryParse(date, out d);
                DateTime e;
                e=TimeZone.CurrentTimeZone.ToLocalTime(d);
                d = e;
                if (perfect == 1)
                {
                    per = true;
                }
                else if (perfect == 0)
                {
                    per = false;
                }
                Accuracy = AccCalc(Mode);
            }
            /// <summary>
            /// 使用指定的格式创建字符串
            /// </summary>
            /// <param name="format">格式</param>
            /// <param name="formatProvider"></param>
            /// <returns></returns>
            public string ToString(string format, IFormatProvider formatProvider)
            {
                System.Text.StringBuilder b = new System.Text.StringBuilder(format);
                b.Replace("perfect", Perfect.ToString());
                b.Replace("c300g", c300g.ToString());
                b.Replace("c300", c300.ToString());
                b.Replace("c200", c200.ToString());
                b.Replace("c100", c100.ToString());
                b.Replace("c50", c50.ToString());
                b.Replace("cMiss", cMiss.ToString());
                b.Replace("maxcombo", MaxCombo.ToString());
                b.Replace("userid", UserID.ToString());
                b.Replace("rank", Rank);
                b.Replace("playtime", d.ToString("yyyy/MM/dd HH:mm:ss"));
                b.Replace("score", Score.ToString());
                b.Replace("beatmapid", BeatmapID.ToString());
                b.Replace("acc", Accuracy.ToString("p2"));
                return b.ToString();
            }
            /// <summary>
            /// 使用指定的格式创建字符串
            /// </summary>
            /// <param name="format">格式</param>
            /// <returns></returns>
            public string ToString(string format)
            {
                return ToString(format, null);
            }
        }
    }
}