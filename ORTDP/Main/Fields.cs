namespace osuTools
{
    using osuTools.Beatmaps.HitObject;
    using osuTools.Game.Modes;
    using osuTools.Game.Mods;
    using osuTools.OsuDB;
    using osuTools.OtherTools;
    using System;
    
    partial class ORTDP
    {
        int dmp = 0;
        double tmper = 0;
        int bests;
        double bestp;
        TimeSpan dur, cur;
        
        bool ManiaRanked = false;
        bool unRanked = false;
        OsuInfo info = new OsuInfo();
        string file;
        OsuDB.OsuBeatmapDB bd;
        bool Ranked;
        [NonSerialized]
        SyncPPInfo ppinfo;
        double stars;
        GMMode gm;
        //GMMod mo;
        GMStatus gs;
        int rt;
        string Ranking = "Unknown";
        HitObjectCollection hitObjects = null;
        Beatmaps.Beatmap b;
        [NonSerialized]
        InfoReaderPlugin.RtppdInfo rtppi;
        [NonSerialized]
        OsuRTDataProvider.Listen.OsuListenerManager lm;
        [NonSerialized]
        OsuRTDataProvider.OsuRTDataProviderPlugin p;
        int C300g = 0, C300 = 0, C200 = 0, C100 = 0, C50 = 0, Cmiss = 0, combo = 0, score = 0, time = 0, mco = 0, ppc = 0;
        /// <summary>
        /// 读取谱面的方式
        /// </summary>
        public BeatmapReadMethods BeatmapReadMethod=BeatmapReadMethods.OsuRTDataProvider;
        string pn = "";
        RealTimePPDisplayer.RealTimePPDisplayerPlugin arp;
        double acc, hp, maxpp, fcpp, scc = 0;
        OsuBeatmapStatus b_status=OsuBeatmapStatus.Unknown;
        ModList mod=new ModList();string np = "";
        OsuRTDataProvider.Mods.ModsInfo modsinfo;
        bool canfail = false;
        /// <summary>
        /// 是否处于调试模式
        /// </summary>
        public bool DebugMode { get; set; }
        /// <summary>
        /// 指定读取谱面信息的插件
        /// </summary>
        public enum BeatmapReadMethods 
        {
            /// <summary>
            /// 通过<see cref="OsuRTDataProvider.OsuRTDataProviderPlugin"/>获取谱面信息
            /// </summary>
            OsuRTDataProvider,
            /// <summary>
            /// 通过<see cref="osuTools.OsuDB.OsuBeatmapDB"/>获取谱面信息
            /// </summary>
            OsuDB
        }
    }
}