using osuTools.osuToolsException;
using System;

namespace osuTools
{
    [Serializable]
    public partial class ORTDP
    {
        void Failed(ORTDP current)
        {
            string con =
                $"Date:{SysTime.ToString("yyyy/MM/dd HH:mm:ss")}\n" +
                $"Song:{current.NowPlaying}(RetryCount:{current.RetryCount})\n" +
                $"Stars:{ current.StarsStr} " +
                $"Mode: { current.GameMode.CurrentMode}\n" +
                $"FailedTime:{CurrentTimeStr}\\{SongDurationStr}({TimePercentStr})\n"+
                $"Accuracy:{current.Accuracy}% Score:{current.Score}\n" +               
                $"PP: {CurrentPPStr}pp \\ {FcPPStr}pp -> {MaxPPStr}pp({current.PPpercent})\n" +
                $"MaxCombo:{current.PlayerMaxCombo}\n" +               
                $"Mod:{(string.IsNullOrEmpty(current.ModShortNames)?"None":current.ModShortNames)}\n" +
                $"Rank:{current.CurrentRank}\n" +
                $"{current.c300}xc300 {current.c100}xc100 {current.c50}x50 {current.cMiss}xMiss\n" +
                $"{current.c300g}x300g {current.c200}x200\n\n";
            System.IO.File.AppendAllText("D:\\osu\\Failed\\Failed.txt", con);
        }
        /// <summary>
        /// 不使用额外类型构建ORTDP
        /// </summary>
        public ORTDP()
        {
            bd = new OsuDB.OsuBeatmapDB();
            InitLisenter();          
            System.Windows.Forms.Application.ThreadException += Application_ThreadException;
            System.AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Sync.Tools.IO.CurrentIO.Write("ORTDP初始化完成");
            ppinfo = new SyncPPInfo("rtpp", null, null, this);
            OnFail += Failed;
            System.Windows.Forms.Application.ThreadException += Application_ThreadException;
        }
        /// <summary>
        /// 使用<see cref="OsuRTDataProvider.OsuRTDataProviderPlugin"/>辅助构建ORTDP类
        /// </summary>
        public ORTDP(OsuRTDataProvider.OsuRTDataProviderPlugin p)
        {
            bd = new OsuDB.OsuBeatmapDB();
            InitLisenter(p);           
            System.Windows.Forms.Application.ThreadException += Application_ThreadException;
            Sync.Tools.IO.CurrentIO.Write("ORTDP初始化完成");
            ppinfo = new SyncPPInfo("rtpp", null, null, this);
            OnFail += Failed;
            System.Windows.Forms.Application.ThreadException += Application_ThreadException;
        }
        /// <summary>
        /// 使用<see cref="OsuRTDataProvider.OsuRTDataProviderPlugin"/>及<see cref="RealTimePPDisplayer.RealTimePPDisplayerPlugin"/>辅助构建ORTDP类
        /// </summary>
        public ORTDP(OsuRTDataProvider.OsuRTDataProviderPlugin p, RealTimePPDisplayer.RealTimePPDisplayerPlugin rp, InfoReaderPlugin.RtppdInfo d)
        {
            bd = new OsuDB.OsuBeatmapDB();
            InitLisenter(p);            
            if (rp != null && d != null)
            {
                
                arp = rp;
                arp.RegisterDisplayer("my", (id) =>
                  {
                      d = new InfoReaderPlugin.RtppdInfo();
                      rtppi = d;
                      ppinfo = new SyncPPInfo("rtpp", arp, rtppi, this);
                      if (d is null)
                          throw new InitializationFailedException("未能在RTPPD中注册新的Displayer。");
                      return d;
                  });
            }
            Sync.Tools.IO.CurrentIO.Write("ORTDP初始化完成");
            OnFail += Failed;
            System.Windows.Forms.Application.ThreadException += Application_ThreadException;


        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            System.IO.File.AppendAllText("osuToolsEx.txt", ((Exception)(e.ExceptionObject)).ToString());
        }
        private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            System.IO.File.AppendAllText("osuToolsEx.txt", e.Exception.ToString());
        }
    }
}
