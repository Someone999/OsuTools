namespace osuTools
{
    using System;
   /// <summary>
   /// 请使用osuTools.ORTDP类
   /// </summary>
    [Obsolete]
    public partial class SyncPPInfo
    {

        int C300g, C300, C200, C100, C50, Cmiss, fc, objcount, maxc, pmaxc, cc;
        double accpp, aimpp, speedpp, fcaim, fcacc, fcspeed, maccpp, maimpp, mspeedpp, rpp, fpp, mpp;
        double pt, du, timep;
        System.TimeSpan durat;
        string forhit = "", fortime = "", forpp = "", SyncDir, osuDir;
        System.IO.MemoryMappedFiles.MemoryMappedFile PPInfomfs;
        double tmper = 0;
        System.TimeSpan d, s;
        InfoReaderPlugin.RtppdInfo rtppi;
        RealTimePPDisplayer.RealTimePPDisplayerPlugin rpg;
        /// <summary>
        /// 使用内存映射的名称，<see cref="RealTimePPDisplayer.RealTimePPDisplayerPlugin"/>，<see cref="RtppdInfo"/>及<see cref="ORTDP"/>构造一个SyncPPInfo对象
        /// </summary>
        /// <param name="mmfName"></param>
        /// <param name="rp"></param>
        /// <param name="rti"></param>
        /// <param name="or"></param>
        public SyncPPInfo(string mmfName = "rtpp", RealTimePPDisplayer.RealTimePPDisplayerPlugin rp = null, InfoReaderPlugin.RtppdInfo rti = null, ORTDP or = null)
        {
            try
            {

                /*if (Sync != "" && System.IO.File.Exists(Sync))
                {
                    if (System.Diagnostics.Process.GetProcessesByName("Sync").Length == 0)
                    {
                        System.Diagnostics.Process.Start(Sync);
                    }
                }
                else
                {
                    if (System.Diagnostics.Process.GetProcessesByName("Sync").Length == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("请启动Sync");
                    }
                }*/
                if (or != null)
                {
                    ot = or;
                }
                if (!(rti is null) && !(rp is null))
                {
                    rpg = rp;
                    rtppi = rti;
                }
                else
                {
                    //ot = new ORTDP();
                }
                PPInfomfs = System.IO.MemoryMappedFiles.MemoryMappedFile.OpenExisting(mmfName);
                Sync.Tools.IO.CurrentIO.Write("SyncPPInfo初始化完成");
            }

            catch (Exception x)
            {

            }
        }
    }
}