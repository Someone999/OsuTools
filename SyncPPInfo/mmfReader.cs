using System.Text;
namespace osuTools
{
    partial class SyncPPInfo
    {
        ORTDP ot;
        byte[] RawData = new byte[4097];
        double stars;
        string rawstr;
        int a = 0;
        string PPILine, PPLine, HitLine, TimeLine, ComboLine;
        string[] strarr;
        System.IO.MemoryMappedFiles.MemoryMappedViewStream PPInfomfsStream;


        public void GetInfo(object sender, System.EventArgs e)
        {
            long size = System.IO.File.Open("C:\\Users\\add.DESKTOP-RQ91ME4\\Desktop\\sync app\\plugins\\formatconfig.ini", System.IO.FileMode.Open).Length;
            #region 从mmf获取信息
            try
            {

                PPInfomfsStream = PPInfomfs.CreateViewStream();
                // System.Windows.Forms.MessageBox.Show(PPInfomfsStream.Length.ToString());
                PPInfomfsStream.Read(RawData, 0, (int)PPInfomfsStream.Length);
                rawstr = Encoding.Default.GetString(RawData).Split('\0')[0];
                #endregion
                #region 获取星星数
                strarr = rawstr.Split('\n');
                if (ot.GameMode.CurrentMode == OsuGameMode.Mania)
                {
                    double.TryParse(System.IO.File.ReadAllText("D:\\osu\\stars.txt"), out stars);
                }
                else
                {

                }
                #endregion
                #region PP的组成
                PPILine = strarr[0];
                string[] ppitemp = PPILine.Split('/');
                double.TryParse(ppitemp[0], out accpp);
                double.TryParse(ppitemp[1], out aimpp);
                double.TryParse(ppitemp[2], out speedpp);
                double.TryParse(ppitemp[3], out fcaim);
                double.TryParse(ppitemp[4], out fcacc);
                double.TryParse(ppitemp[5], out fcspeed);
                double.TryParse(ppitemp[6], out maccpp);
                double.TryParse(ppitemp[7], out maimpp);
                double.TryParse(ppitemp[8], out mspeedpp);
                #endregion
                #region PP
                PPLine = strarr[1];
                string[] pptemp = PPLine.Split('/');
                double.TryParse(pptemp[0], out rpp);
                double.TryParse(pptemp[1], out fpp);
                double.TryParse(pptemp[2], out mpp);
                #endregion
                #region 时间操作(以毫秒为单位)
                TimeLine = strarr[2];
                string[] timetemp = TimeLine.Split('/');
                double.TryParse(timetemp[0], out du);
                double.TryParse(timetemp[1], out pt);
                if (pt > du) { du = pt; }
                d = System.TimeSpan.FromMilliseconds(du);
                s = System.TimeSpan.FromMilliseconds(pt);
                timep = pt / du;
                #endregion
                #region 判定结果
                HitLine = strarr[3];
                string[] hittemp = HitLine.Split('/');
                int.TryParse(hittemp[0], out C300g);
                int.TryParse(hittemp[1], out C300);
                int.TryParse(hittemp[2], out C200);
                int.TryParse(hittemp[3], out C100);
                int.TryParse(hittemp[4], out C50);
                int.TryParse(hittemp[5], out Cmiss);
                #endregion
                #region 连击与note数量
                ComboLine = strarr[4];
                string[] comtemp = ComboLine.Split('/');
                int.TryParse(comtemp[0], out fc);
                int.TryParse(comtemp[1], out objcount);
                int.TryParse(comtemp[2], out maxc);
                int.TryParse(comtemp[3], out pmaxc);
                int.TryParse(comtemp[4], out cc);
                string sqq = "shit!";
                System.IO.MemoryMappedFiles.MemoryMappedFile.CreateOrOpen("Shit", 100).CreateViewStream().Write(Encoding.Default.GetBytes(sqq), 0, sqq.Length);
                #endregion
                #region 对字符串进行格式化
                forhit = $"{c300}x300 {c100}x100 {c50}x50 {cMiss}xMiss\n{c300g}x300g({(c300g / (c300g + c300)).ToString("p")}) {c200}x200";

                fortime = $"{CurrentTime.Minutes.ToString("d2")}:{CurrentTime.Seconds.ToString("d2")}/{SongDuration.Minutes.ToString("d2")}:{SongDuration.Seconds.ToString("d2")}";
                forpp = $"{CurrentPP.ToString("f2")}pp / {FcPP.ToString("f2")}pp => {MaxPP.ToString("f2")}pp";
                var f = System.IO.MemoryMappedFiles.MemoryMappedFile.CreateOrOpen("Fuck", 999);
                string c = "Fucked";
                f.CreateViewStream().Write(Encoding.Default.GetBytes(c), 0, c.Length);
                long size2 = System.IO.File.Open("C:\\Users\\add.DESKTOP-RQ91ME4\\Desktop\\sync app\\plugins\\formatconfig.ini", System.IO.FileMode.Open).Length;
                if (size != size2)
                {

                }
                #endregion
            }
            catch (System.Exception x)
            {
                System.IO.File.AppendAllText("D:\\osu\\osuTools\\log\\0.txt", x.ToString());
            }
        }
    }
    /*我刚刚试了一下32767->32768的经验值*/
}