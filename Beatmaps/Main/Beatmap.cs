namespace osuTools
{
    namespace Beatmaps
    {
        using Online.ApiV1.Querier;
        using osuTools.Online.ApiV1;
        using osuTools.Skins.Settings.Mania;
        using System;
        using System.Collections.Generic;

        /// <summary>
        /// 谱面类，其中包含谱面的信息。
        /// </summary>
        [System.Serializable]
        public partial class Beatmap
        {

            /// <summary>
            /// 谱面的游戏模式
            /// </summary>
            public OsuGameMode Mode { get => mode; }
            /// <summary>
            /// 谱面有无倒计时
            /// </summary>
            public bool HasCountdown { get; internal set; } = false;
            /// <summary>
            /// 谱面的
            /// </summary>
            public double AudioLeadIn { get; set; } = 0;
            /// <summary>
            /// 谱面的预览时间点
            /// </summary>
            public double PreviewTime { get; set; } = -1;
            /// <summary>
            /// 谱面的音效集
            /// </summary>
            public SampleSets SampleSet { get; set; } = SampleSets.Default;
            /// <summary>
            /// 堆叠系数
            /// </summary>
            public double StackLeniency { get; set; } = 0;
            /// <summary>
            /// 暂未知
            /// </summary>
            public bool LetterboxInBreaks { get; set; } = false;
            /// <summary>
            /// 特殊样式
            /// </summary>
            public SpecialStyles SpecialStyle { get; set; } = SpecialStyles.None;
            /// <summary>
            /// 暂未知
            /// </summary>
            public bool StoryFireInFront { get; set; } = false;
            /// <summary>
            /// 可能诱发癫痫的警告
            /// </summary>
            public bool EpilepsyWarning { get; set; } = false;
            /// <summary>
            /// 倒计时的时间便宜
            /// </summary>
            public double CountdownOffset { get; set; } = 0;
            /// <summary>
            /// 暂未知
            /// </summary>
            public bool WidescreenStoryboard { get; set; } = false;
            /// <summary>
            /// 编辑器中时间线上的书签，以毫秒为标记
            /// </summary>
            public List<int> Bookmarks { get; set; } = new List<int>();
            /// <summary>
            /// 间隔空间
            /// </summary>
            public double DistanceSpacing { get; set; } = 0;
            /// <summary>
            /// 节拍
            /// </summary>
            public double BeatDivisor { get; set; } = 4;
            /// <summary>
            /// 网格大小
            /// </summary>
            public double GridSize { get; set; } = 32;
            /// <summary>
            /// 时间线的缩放比
            /// </summary>
            public double TimelineZoom { get; set; } = 1;
            /// <summary>
            /// 滑条的速度倍数
            /// </summary>
            public double SliderMultiplier { get; set; } = 0;
            /// <summary>
            /// 每拍的滑条点的个数
            /// </summary>
            public double SliderTickRate { get; set; } = 0;
            /// <summary>
            /// 谱面集ID
            /// </summary>
            public int BeatmapSetID { get { return setid; }set=>setid=value; }
            /// <summary>
            /// 谱面文件的<see cref="System.IO.FileInfo"/>
            /// </summary>
            public System.IO.FileInfo BeatmapFile { get { if (FileStreamAvalable) { return info; } else throw new System.NotSupportedException(); } }
            /// <summary>
            /// 谱面是否包含视频
            /// </summary>
            public bool HasVideo { get => hasvideo; }
            internal void SetBeatmapID(int beatmap_id)
            {
                id = beatmap_id;
            }
            internal void SetBeatmapSetID(int beatmapset_id)
            {
                setid = beatmapset_id;
            }

            /// <summary>
            /// 使用osu!api在线查询谱面信息
            /// </summary>
            /// <returns></returns>
            public OnlineBeatmap GetOnlineBeatmap()
            {
                OnlineBeatmapQuery q = new OnlineBeatmapQuery();
                q.OsuApiKey = OnlineQueryTools.DefaultOsuApiKey;
                q.BeatmapID = BeatmapID;
                return q.Beatmaps[0];
            }
            /// <summary>
            /// 将该谱面转换成OsuBeatmap
            /// </summary>
            /// <returns></returns>
            public OsuDB.OsuBeatmap ToOsuBeatmap()
            {
                OsuInfo info = new OsuInfo();
                OsuDB.OsuBeatmapDB baseDB = new OsuDB.OsuBeatmapDB();
                return baseDB.Beatmaps.FindByMD5(MD5.ToString());
            }

            /// <summary>
            /// 使用MD5判断两个谱面是否相同
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            public static bool operator ==(Beatmap a, Beatmap b)
            {
                if (((object)a == null && (object)b != null) || ((object)a != null && b == null)) return false;
                if ((object)a == null && (object)b == null) return true;
                try
                {
                    return a.MD5 == b.MD5;
                }
                catch (NullReferenceException)
                {
                    return false;
                }
            }
            /// <summary>
            /// 使用MD5判断两个谱面是否相同
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            public static bool operator !=(Beatmap a, Beatmap b)
            {
                if (((object)a == null && (object)b != null) || ((object)a != null && b == null)) return true;
                if ((object)a == null && (object)b == null) return false;
                try
                {
                    return a.MD5 != b.MD5;
                }
                catch (NullReferenceException)
                {
                    return false;
                }

            }
            public Beatmap()
            {
                fuau = "";
                au = "";
                t = "";
                ut = "";
                a = "";
                ua = "";
                c = "";
                dif = "";
                ver = "";
                fn = "";
                fullfn = "";
                dlnk = "";
                bgf = "";
                mak = "";
                sou = "";
                tag = "";
                md = new MD5String("");
                id = 0;
            }
            internal bool notv = false;
            public override string ToString()
            {
                return $"{Artist} - {Title} [{Version}]";
            }

        }
    }
}