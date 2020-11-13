namespace osuTools
{
    using osuTools.OsuDB;
    using System;
    using System.Collections.Generic;
    using System.IO;
    namespace Replay
    {
        /// <summary>
        /// 未将FileInfo初始化却使用了FileInfo时触发的异常
        /// </summary>
        public class FileInfoHasNoValue : osuToolsException.osuToolsExceptionBase 
        {
            /// <summary>
            /// 使用指定的信息构建一个FileInfoHasNoValue异常
            /// </summary>
            /// <param name="m"></param>
            public FileInfoHasNoValue(string m) : base(m) { } 
        }
        /// <summary>
        /// 生命值图像的集合
        /// </summary>
        public class LifeBarGraphCollection
        {
            List<LifeBarGraph> gr = new List<LifeBarGraph>();
            /// <summary>
            /// 存储了生命值图像的集合
            /// </summary>
            public IReadOnlyList<LifeBarGraph> Data { get => gr; }
            /// <summary>
            /// 将字符串解析成<see cref="LifeBarGraph"/>
            /// </summary>
            /// <param name="s"></param>
            public LifeBarGraphCollection(string s)
            {
                GetData(s);
            }
            /// <summary>
            /// 构造一个空的LifeBarGraphCollection对象
            /// </summary>
            public LifeBarGraphCollection()
            {
            }
            void GetData(string str)
            {
                string[] pair = str.Split('|');
                foreach (var value in pair)
                {
                    gr.Add(new LifeBarGraph(value));
                }
            }

        }
        /// <summary>
        /// 生命值图像，一个时间，生命值的键值对
        /// </summary>
        public class LifeBarGraph
        {
            string orgstr;
            double hp = -1;
            int offset = -1;
            /// <summary>
            /// 构造一个空的LifeBarGraph对象
            /// </summary>
            public LifeBarGraph() { }
            /// <summary>
            /// 将字符串解析成一个LifeBarGraph对象
            /// </summary>
            /// <param name="pair"></param>
            public LifeBarGraph(string pair)
            {
                orgstr = pair;
                string[] data = orgstr.Split(',');
                if (data.Length < 2) return;
                string HP = data[0];
                string Offset = data[1];
                double.TryParse(HP, out hp);
                int.TryParse(Offset, out offset);
            }
        }
        /// <summary>
        /// 表示额外的录像数据
        /// </summary>
        public class AdditionalRepalyData
        {
            MemoryStream m = new MemoryStream();
            LifeBarGraphCollection l;
            /// <summary>
            /// 生命值图像的列表
            /// </summary>
            public IReadOnlyList<LifeBarGraph> LifeBarGraphData { get => l.Data; }
            byte[] LZMAstream;
            /// <summary>
            /// 游玩回放的数据
            /// </summary>
            public MemoryStream LZMAStream 
            {
                get => m;
            }
            int len;
            public int ReplayDataLength { get => len; }
            /// <summary>
            /// 使用回放数据，数据长度和表示生命值图像的字符串构造一个AdditionalRepalyData对象
            /// </summary>
            /// <param name="data"></param>
            /// <param name="len"></param>
            /// <param name="lifebargraphstr"></param>
            public AdditionalRepalyData(byte[] data, int len, string lifebargraphstr)
            {
                LZMAstream = data;
                m.Write(data, 0, len);
                this.len = len;
                l = new LifeBarGraphCollection(lifebargraphstr);
            }
        }
        public partial class Replay
        {
            short C300g, C300, C200, C100, C50, Cmiss, maxco;
            int sco, ver;
            List<OsuGameMod> mods = new List<OsuGameMod>();
            /// <summary>
            /// 录像对应的游玩记录使用Mods
            /// </summary>
            public IReadOnlyList<OsuGameMod> Mods { get => mods.AsReadOnly(); }
            double acc;
            string accstr;
            byte mode, per, flag;
            bool perb;
            string md5, beatmapmd5, playern;
            OsuGameMode osumode;
            bool infonovalue;
            BinaryReader r;
            FileInfo Info;
            DateTime dt;
            AdditionalRepalyData adata;
            /// <summary>
            /// 附加的录像数据
            /// </summary>
            public AdditionalRepalyData AdditionalData { get => adata; }
            /// <summary>
            /// 游玩时间
            /// </summary>
            public DateTime PlayTime { get => dt; }
            string d;
            /// <summary>
            /// 录像文件的文件信息
            /// </summary>
            public FileInfo ReplayFile
            {
                get
                {
                    if (infonovalue)
                        throw new FileInfoHasNoValue("当前的构造函数没有为FileInfo赋值");
                    else
                        return Info;

                }
            }
            /// <summary>
            /// 使用录像文件构造一个Replay对象
            /// </summary>
            /// <param name="Dir"></param>
            public Replay(string Dir)
            {

                {
                    r = new BinaryReader(File.OpenRead(Dir));
                }
                Info = new FileInfo(Dir);
                Read();
            }
            /// <summary>
            /// 使用录像文件的二进制流构造一个Replay对象，并指定录像的全路径
            /// </summary>
            /// <param name="b"></param>
            /// <param name="Dir"></param>
            public Replay(BinaryReader b, string Dir)
            {
                if (b is null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    r = b;
                }
                Info = new FileInfo(Dir);
                Read();
            }
            /// <summary>
            /// 使用录像文件的二进制流构造一个Replay对象
            /// </summary>
            /// <param name="b"></param>
            public Replay(BinaryReader b)
            {
                if (b is null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    r = b;
                }
                Info = null;
                infonovalue = true;
                Read();
            }

            void Read()
            {
                string lfbar = "";
                mode = r.ReadByte();
                osumode = (OsuGameMode)(mode);
                ver = r.ReadInt32();
                flag = r.ReadByte();
                beatmapmd5 = r.ReadString();
                flag = r.ReadByte();
                playern = r.ReadString();
                flag = r.ReadByte();
                md5 = r.ReadString();
                C300 = r.ReadInt16();
                C100 = r.ReadInt16();
                C50 = r.ReadInt16();
                C300g = r.ReadInt16();
                C200 = r.ReadInt16();
                Cmiss = r.ReadInt16();
                acc = AccCalculater(osumode);
                accstr = acc.ToString("p");
                sco = r.ReadInt32();
                maxco = r.ReadInt16();
                per = r.ReadByte();
                if (per == 1)
                {
                    perb = true;
                }
                else
                {
                    perb = false;
                }
                mods = Beatmaps.HitObject.HitObjectTools.GetGenericTypesByInt<OsuGameMod>(r.ReadInt32());
                if (r.ReadByte() == 0x0b)
                    lfbar = r.ReadString();
                dt = new DateTime(r.ReadInt64());
                var datalen = r.ReadInt32();
                byte[] data = r.ReadBytes(datalen);
                adata = new AdditionalRepalyData(data, datalen, lfbar);
                r.ReadDouble();
            }
            public static bool operator ==(Replay replay,OsuScoreInfo score) => replay.ReplayMD5 == score.ReplayMD5;
            public static bool operator !=(Replay replay, OsuScoreInfo score) => replay.ReplayMD5 != score.ReplayMD5;
            public static bool operator ==(OsuScoreInfo score,Replay replay) => replay.ReplayMD5 == score.ReplayMD5;
            public static bool operator !=(OsuScoreInfo score,Replay replay) => replay.ReplayMD5 != score.ReplayMD5;
            double AccCalculater(OsuGameMode mode)
            {
                double a300 = 1, a200 = 2.0 / 3, a100 = 1.0 / 3, a50 = 1.0 / 6, a150 = 1.0 / 2;
                int ManiaAllHit = C300g + C300 + C200 + C100 + C50 + cMiss;
                int OsuAllHit = C300 + C100 + C50 + Cmiss;
                int CtbAllHit = C50 + C100 + C300 + Cmiss + C200;
                int TaikoAllHit = C300 + C100 + Cmiss;
                double ma300c = c300 + c300g,
                       a300c = c300,
                       a200c = c200 * a200,
                       a100c = c100 * a100,
                       a50c = c50 * a50,
                       a150c = c100 * a150;
                double OsuValue = a300c + a100c + a50c,
                       TaikoValue = a300c + a150c,
                       CtbValue = c300 + c50 + c100,
                       ManiaValue = ma300c + a200c + a100c + a50c,
                       InvalidValue = -1;
                if (Mode == OsuGameMode.Osu)
                {
                    return OsuValue / OsuAllHit;
                }
                if (Mode == OsuGameMode.Catch)
                {
                    return CtbValue / CtbAllHit;
                }
                if (Mode == OsuGameMode.Taiko)
                {
                    return TaikoValue / TaikoAllHit;
                }
                if (Mode == OsuGameMode.Mania)
                {

                    return ManiaValue / ManiaAllHit;
                }
                if (Mode == OsuGameMode.Unkonwn)
                {
                    return InvalidValue;
                }
                return InvalidValue;
            }

        }
    }
}