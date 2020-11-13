using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using osuTools.Beatmaps;
namespace osuTools.OsuDB
{
    /// <summary>
    /// 谱面的状态
    /// </summary>
    public enum OsuBeatmapStatus 
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown, 
        /// <summary>
        /// 未提交
        /// </summary>
        NotSubmitted,
       /// <summary>
       /// 未过审
       /// </summary>
        Pending,
       /// <summary>
       /// 已上架并计算分数，pp
       /// </summary>
        Ranked=4,
        /// <summary>
        /// 已上架并计算分数，pp
        /// </summary>
        Approved,
        /// <summary>
        /// 已上架并计算分数，不计算pp
        /// </summary>
        Qualified,
        /// <summary>
        /// 已上架并计算分数，不计算pp
        /// </summary>
        Loved,
        /// <summary>
        /// 谱面正在从文件加载，与osu!制定的谱面状态无关
        /// </summary>
        Loading
    }
    /// <summary>
    /// 存储<see cref="OsuBeatmap"/>的集合
    /// </summary>
    public class OsuBeatmapCollection
    {
        List<OsuBeatmap> beatmaps { get; set; } = new List<OsuBeatmap>();
        /// <summary>
        /// 存储的<seealso cref="OsuBeatmap"/>
        /// </summary>
        public IReadOnlyList<OsuBeatmap> Beatmaps { get => beatmaps.AsReadOnly(); }
        /// <summary>
        /// 检测指定谱面是否在列表中
        /// </summary>
        /// <param name="b">要检测的谱面</param>
        /// <returns>布尔值，指示谱面是否在列表中</returns>
        public bool Contains(OsuBeatmap b) => beatmaps.Contains(b);
        /// <summary>
        /// 谱面的数量
        /// </summary>
        public int Count { get => beatmaps.Count; }
        internal void Add(OsuBeatmap b) => beatmaps.Add(b);
        /// <summary>
        /// 使用整数索引从列表中获取OsuBeatmap
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public OsuBeatmap this[int x]
        {
            get => beatmaps[x];
        }
        /// <summary>
        /// 使用关键词搜索，可指定包含或不包含
        /// </summary>
        /// <param name="KeyWord">关键词</param>
        /// <param name="option">是否包含关键词</param>
        /// <returns>包含搜索结果的谱面集合</returns>
        public OsuBeatmapCollection Find(string KeyWord, Beatmaps.BeatmapCollection.BeatmapFindOption option = BeatmapCollection.BeatmapFindOption.Contains)
        {
            OsuBeatmapCollection b = new OsuBeatmapCollection();
            string keyword = KeyWord.ToUpper();
            foreach (var beatmap in Beatmaps)
            {

                string allinfo = beatmap.ToString().ToUpper() + " " + beatmap.Source.ToUpper() + " " + beatmap.Tags.ToUpper() + " " + beatmap.Creator.ToUpper();
                if (option == BeatmapCollection.BeatmapFindOption.Contains)
                {
                    if (keyword.StartsWith("${") && keyword.EndsWith("}"))
                    {
                        string newkeyw = keyword.Trim('$', '}', '{');
                        if (beatmap.Title.ToUpper() == newkeyw || beatmap.TitleUnicode.ToUpper() == newkeyw || beatmap.Artist.ToUpper() == newkeyw || beatmap.ArtistUnicode.ToUpper() == newkeyw ||
                            beatmap.Creator.ToUpper() == newkeyw || beatmap.Tags.ToUpper() == newkeyw || beatmap.Source.ToUpper() == newkeyw ||
                            beatmap.Difficulty.ToUpper() == newkeyw)
                        {
                            if (!b.Contains(beatmap))
                                b.Add(beatmap);
                        }
                    }
                    else
                    if (allinfo.Contains(keyword))
                        b.Add(beatmap);
                }
                if (option == BeatmapCollection.BeatmapFindOption.NotContains)
                {
                    if (keyword.StartsWith("${") && keyword.EndsWith("}"))
                    {
                        string newkeyw = keyword.Trim('$', '}', '{');
                        if (beatmap.Title.ToUpper() != newkeyw && beatmap.TitleUnicode.ToUpper() != newkeyw && beatmap.Artist.ToUpper() != newkeyw && beatmap.ArtistUnicode.ToUpper() != newkeyw &&
                             beatmap.Creator.ToUpper() != newkeyw && beatmap.Tags.ToUpper() != newkeyw && beatmap.Source.ToUpper() != newkeyw &&
                            beatmap.Difficulty.ToUpper() != newkeyw)
                        {
                            if (!b.Contains(beatmap))
                                b.Add(beatmap);
                        }
                    }
                    else
                    if (!allinfo.Contains(keyword))
                        b.Add(beatmap);
                }
            }
            if (b.Count == 0)
            {
                throw new osuTools.osuToolsException.BeatmapNotFoundException("找不到指定的谱面");
            }
            return b;
        }
        /// <summary>
        /// 谱面的ID的种类
        /// </summary>
        public enum BeatmapIDType 
        {
            /// <summary>
            /// 谱面ID
            /// </summary>
            BeatmapID,
            /// <summary>
            /// 谱面集ID
            /// </summary>
            BeatmapSetID 
        }
        /// <summary>
        /// 根据谱面的ID查找谱面
        /// </summary>
        /// <param name="ID">BeatmapID或BeatmapSetID</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<OsuBeatmap> Find(int ID,BeatmapIDType type=BeatmapIDType.BeatmapID)
        {
            List<OsuBeatmap> lst = new List<OsuBeatmap>();
            if (ID != -1)
                if(type==BeatmapIDType.BeatmapID)
                foreach (var beatmap in Beatmaps)
                {
                    if (beatmap.BeatmapID == ID)
                    {
                            lst.Add(beatmap);
                    }
                }
            if(type==BeatmapIDType.BeatmapSetID)
                foreach (var beatmap in Beatmaps)
                {
                    if (beatmap.BeatmapSetID == ID)
                    {
                        lst.Add(beatmap);
                    }
                }
            return lst;
        }
        /// <summary>
        /// 使用MD5在谱面列表里搜索
        /// </summary>
        /// <param name="md5"></param>
        /// <returns></returns>
        public OsuBeatmap FindByMD5(string md5)
        {
            foreach (var beatmap in Beatmaps)
            {
                if (beatmap.MD5 == md5)
                {
                    return beatmap;
                }
            }
            throw new osuToolsException.BeatmapNotFoundException($"找不到MD5为{md5}的谱面。");
        }
        /// <summary>
        /// 使用游戏模式来搜索谱面，可指定包括或不包括
        /// </summary>
        /// <param name="Mode"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public OsuBeatmapCollection Find(OsuGameMode Mode, BeatmapCollection.BeatmapFindOption option = BeatmapCollection.BeatmapFindOption.Contains)
        {
            OsuBeatmapCollection bc = new OsuBeatmapCollection();
            foreach (var b in beatmaps)
            {
                if (option == BeatmapCollection.BeatmapFindOption.Contains)
                    if (b.Mode == Mode)
                    {
                        if (!bc.Contains(b))
                            bc.Add(b);
                    }
                if (option == BeatmapCollection.BeatmapFindOption.NotContains)
                    if (b.Mode != Mode)
                    {
                        if (!bc.Contains(b))
                            bc.Add(b);
                    }
            }
            return bc;
        }
        /// <summary>
        /// 获取列表的枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<OsuBeatmap> GetEnumerator() => beatmaps.GetEnumerator();

    }
    /// <summary>
    /// 一个变速点或时间标志。
    /// </summary>
    public class OsuBeatmapTimePoint
    {
        internal OsuBeatmapTimePoint(double bpm, double offset, bool inherit)
        {
            BPM = 1/bpm*1000*60;
            Offset = offset;
            Inherit = inherit;
        }
        /// <summary>
        /// 该时间点对应的BPM
        /// </summary>
        public double BPM { get; internal set; }
        /// <summary>
        /// 该时间点相对于开始的偏移量
        /// </summary>
        public double Offset { get; internal set; }
        /// <summary>
        /// 是否为继承时间线(是不是绿线)
        /// </summary>
        public bool Inherit { get; internal set; }
    }
    /// <summary>
    /// 谱面，存储的信息多于Beatmap类
    /// </summary>
    public class OsuBeatmap : IOsuDBData
    {
        /// <summary>
        /// 将OsuBeatmap转换成字符串形式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Artist} - {Title} [{Difficulty}]";
        }
        /// <summary>
        /// 将OsuBeatmap转化成<seealso cref="Beatmap"/>
        /// </summary>
        /// <returns></returns>
        public  Beatmap ToBeatmap()
        {
            return new Beatmap(this);
        }
        /// <summary>
        /// 使用MD5判断两个OsuBeatmap是否相同
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(OsuBeatmap a, OsuBeatmap b)
        {
            if ((object)a == null && (object)b != null)
                return false;
            if ((object)a != null && (object)b == null)
                return false;
            if ((object)a != null && (object)b != null || ((object)a == null && (object)b == null))
                return true;
            return a.MD5 == b.MD5;
        }
        /// <summary>
        /// 使用MD5判断两个OsuBeatmap是否相同
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static bool operator !=(OsuBeatmap a, OsuBeatmap b)
        {
            if ((object)a == null && (object)b != null)
                return true;
            if ((object)a != null && (object)b == null)
                return true;
            if ((object)a != null && (object)b != null || ((object)a == null && (object)b == null))
                return false;
            return a.MD5 != b.MD5;
           
        }
        /// <summary>
        /// 创建一个空的OsuBeatmap对象
        /// </summary>
        public OsuBeatmap()
        {
        }
        /// <summary>
        /// 谱面的难度星级
        /// </summary>
        public double Stars { get; internal set; }
        /// <summary>
        /// 存放谱面的文件夹的名称
        /// </summary>
        public string FolderName { get; internal set; }
        /// <summary>
        /// 谱面的游戏模式
        /// </summary>
        public OsuGameMode Mode { get; internal set; } = OsuGameMode.Unkonwn;
        /// <summary>
        /// 谱面的音乐文件名称
        /// </summary>
        public string AudioFileName { get; internal set; }
        /// <summary>
        /// 艺术家
        /// </summary>
        public string Artist { get; internal set; }
        /// <summary>
        /// 艺术家
        /// </summary>
        public string ArtistUnicode { get; internal set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; internal set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string TitleUnicode { get; internal set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; internal set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tags { get; internal set; }
        /// <summary>
        /// 谱面的创造者
        /// </summary>
        public string Creator { get; internal set; }
        /// <summary>
        /// 谱面的难度标签
        /// </summary>
        public string Difficulty { get; internal set; }
        /// <summary>
        /// 谱面的MD5
        /// </summary>
        public string MD5 { get; internal set; }
        /// <summary>
        /// 谱面文件的文件名
        /// </summary>
        public string FileName { get; internal set; }
        /// <summary>
        /// 谱面的状态
        /// </summary>
        public OsuBeatmapStatus BeatmapStatus { get; internal set; } = OsuBeatmapStatus.Unknown;
        /// <summary>
        /// 谱面中圈圈的数量
        /// </summary>
        public short HitCircle { get; internal set; }
        /// <summary>
        /// 谱面中滑条的数量
        /// </summary>
        public short Slider { get; internal set; }
        /// <summary>
        /// 谱面中转盘的数量
        /// </summary>
        public short Spinner { get; internal set; }
        internal long ModifucationTime;
        /// <summary>
        /// 谱面上次修改的时间
        /// </summary>
        public DateTime LastModificationTime { get; internal set; }
        /// <summary>
        /// 谱面的长度
        /// </summary>
        public TimeSpan DrainTime { get; internal set; }
        /// <summary>
        /// 音频的长度
        /// </summary>
        public TimeSpan TotalTime { get; internal set; }
        /// <summary>
        /// 音频的预览点
        /// </summary>
        public TimeSpan PreviewPoint { get; internal set; }
        /// <summary>
        /// 缩圈速度
        /// </summary>
        public double AR { get; internal set; }
        /// <summary>
        /// 综合难度
        /// </summary>
        public double OD { get; internal set; }
        /// <summary>
        /// 圈圈大小
        /// </summary>
        public double CS { get; internal set; }
        /// <summary>
        /// 掉血、回血速度
        /// </summary>
        public double HPDrain { get; internal set; }
        internal List<OsuBeatmapTimePoint> timepoints = new List<OsuBeatmapTimePoint>();
        /// <summary>
        /// 谱面的时间点
        /// </summary>
        public IReadOnlyList<OsuBeatmapTimePoint> TimePoints { get => timepoints.AsReadOnly(); }
        /// <summary>
        /// 谱面ID
        /// </summary>
        public int BeatmapID { get; internal set; }
        /// <summary>
        /// 谱面集的ID
        /// </summary>
        public int BeatmapSetID { get; internal set; }
        /// <summary>
        /// 包含部分Mod与难度星级的字典
        /// </summary>
        public DifficultyRate ModStarPair { get; internal set; } = new DifficultyRate();
        /// <summary>
        /// 
        /// </summary>
        public int ThreadID { get; internal set; }

    }
    /// <summary>
    /// 包含指定模式的指定Mods与Star的键值对。
    /// </summary>
    public class DifficultyRate
    {
        internal Dictionary<OsuGameMode, Dictionary<int, double>> Difficuties = new Dictionary<OsuGameMode, Dictionary<int, double>>();
        internal void Add(OsuGameMode mode, int modCombine, double stars)
        {
            Difficuties[mode].Add(modCombine, stars);
        }
        /// <summary>
        /// 构建一个空白的DifficultyRate对象
        /// </summary>
        public DifficultyRate() { }
        /// <summary>
        /// 使用游戏模式获取难度字典
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public Dictionary<int, double> this[OsuGameMode mode] {
            get
            {
                try
                {
                    var ret = Difficuties[mode];
                    return ret;
                }
                catch
                {
                   var d= new Dictionary<int, double>();
                   d.Add(0, 0);
                   return d;
                }
            }
        }
        /// <summary>
        /// 获取指定Mod在指定模式下对应的星星数
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="modCombine"></param>
        /// <returns></returns>
        public double GetStars(OsuGameMode mode, int modCombine)
        {
            try
            {
                return Difficuties[mode][modCombine];
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 设置指定Mod在指定模式下对应的星星数
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="modCombine"></param>
        /// <param name="stars"/>
        /// <returns></returns>
        public void SetStar(OsuGameMode mode, int modCombine, double stars)
        {
            try
            {
                Difficuties[mode][modCombine] = stars;
                return;
            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// 将模式的难度字典更改为指定字典
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="dict"></param>
        public void SetModeDict(OsuGameMode mode, Dictionary<int, double> dict)
        {
            Difficuties[mode] = dict;
        }
    }
}
namespace osuTools.OsuDB
{
    /// <summary>
    /// osu!中的部分基础信息。
    /// </summary>
    public class OsuManifest
    {
        /// <summary>
        /// 当前登录用户所拥有的权限。
        /// </summary>
        public enum UserPermission 
        {
            /// <summary>
            /// 无
            /// </summary>
            None, 
            /// <summary>
            /// 普通身份
            /// </summary>
            Normal, 
            /// <summary>
            /// 主持人
            /// </summary>
            Moderator, 
            /// <summary>
            /// 支持者
            /// </summary>
            Supporter = 4, 
            /// <summary>
            /// 好友
            /// </summary>
            Friend = 8, 
            /// <summary>
            /// 官方
            /// </summary>
            peppy = 16, 
            /// <summary>
            /// 世界杯解说人员
            /// </summary>
            WorldCupstaff = 32
        }
        /// <summary>
        /// 当前游戏的版本。
        /// </summary>
        public int Version { get; internal set; }
        /// <summary>
        /// 当前谱面目录下文件夹的数目
        /// </summary>
        public int FolderCount { get; internal set; }
        /// <summary>
        /// 账号是否处于未封禁的状态。
        /// </summary>
        public bool AccountUnlocked { get; internal set; }
        /// <summary>
        /// 账号解封的时间。
        /// </summary>
        public DateTime AccountUnlockTime { get; internal set; }
        /// <summary>
        /// 当前登录的用户的用户名。
        /// </summary>
        public string PlayerName { get; internal set; }
        /// <summary>
        /// 谱面的数目。
        /// </summary>
        public int NumberOfBeatmap { get; internal set; }
        /// <summary>
        /// 当前登录用户所拥有的权限/
        /// </summary>
        public UserPermission Permission { get; internal set; }
    }
    /// <summary>
    /// 通过读取osu!.db获取所有的谱面以及一些游戏相关的信息。
    /// </summary>
    public class OsuBeatmapDB : IOsuDB
    {

        BinaryReader reader;
        /// <summary>
        /// osu!的一些信息
        /// </summary>
        public OsuManifest Manifest { get; internal set; }
        /// <summary>
        /// 从osu!.db读取到的谱面
        /// </summary>
        public OsuBeatmapCollection Beatmaps { get; internal set; }
        MD5String GetMD5()
        {
            System.Security.Cryptography.MD5CryptoServiceProvider provider = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var data = File.ReadAllBytes(f);
            provider.ComputeHash(data);
            return new MD5String(provider);
        }
        /// <summary>
        /// osu!.db的MD5
        /// </summary>
        public MD5String MD5 { get; internal set; }
        string f;
        bool readmanifest = false;
       /// <summary>
       /// 初始化一个OsuBeatmapDB对象
       /// </summary>
        public OsuBeatmapDB()
        {
            OsuInfo info=new OsuInfo();
            string file = info.OsuDirectory + "osu!.db";
            var stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            reader = new BinaryReader(stream);
            
            f = file;
            //System.Windows.Forms.MessageBox.Show(f);
            MD5 = GetMD5();
            //Sync.Tools.IO.CurrentIO.Write(or.CurrentMode.ToString());
            Read();
        }
        /// <summary>
        /// 手动从osu!.db读取信息，这将重新写入所有信息
        /// </summary>
        public void Read()
        {
            Manifest = new OsuManifest();
            Beatmaps = new OsuBeatmapCollection();
            if (!readmanifest)
            {
                ReadManifest();
            }
            GetAllBeatmaps();
        }
        short GetInt16()
        {
            var v = reader.ReadInt16();
            return v;
        }
        int GetInt32()
        {
            var v = reader.ReadInt32();
            return v;
        }
        long GetInt64()
        {
            var v = reader.ReadInt64();
            return v;
        }
        double GetDouble()
        {
            var v = reader.ReadDouble();
            return v;
        }
        float GetSingle()
        {
            var v = reader.ReadSingle();
            return v;
        }
        byte GetByte()
        {
            var v = reader.ReadByte();
            return v;
        }
        bool GetBoolean()
        {
            var v = reader.ReadBoolean();
            return v;
        }
        string GetString()
        {
            if (reader.ReadByte() == 0x0b)
            {
                string v = reader.ReadString();
                return v;
            }
            else return string.Empty;
        }
        void ReadManifest()
        {
            Manifest.Version = GetInt32();
            Manifest.FolderCount = GetInt32();
            Manifest.AccountUnlocked = GetBoolean();
            Manifest.AccountUnlockTime = new DateTime(GetInt64());
            Manifest.PlayerName = GetString();
            Manifest.NumberOfBeatmap = GetInt32();
            readmanifest = true;
        }
        OsuBeatmap ReadBeatmap()
        {
            Dictionary<int, double> osustars = new Dictionary<int, double>();
            Dictionary<int, double> taikostars = new Dictionary<int, double>();
            Dictionary<int, double> ctbstars = new Dictionary<int, double>();
            Dictionary<int, double> maniastars = new Dictionary<int, double>();
            OsuBeatmap Beatmap = new OsuBeatmap();
            Beatmap.Artist = GetString();
            Beatmap.ArtistUnicode = GetString();
            Beatmap.Title = GetString();
            Beatmap.TitleUnicode = GetString();
            Beatmap.Creator = GetString();
            Beatmap.Difficulty = GetString();
            Beatmap.AudioFileName = GetString();
            Beatmap.MD5 = GetString();
            Beatmap.FileName = GetString();
            try
            {
                Beatmap.BeatmapStatus = (OsuBeatmapStatus)Enum.Parse(typeof(OsuBeatmapStatus), GetByte().ToString());
            }
            catch
            {
                Beatmap.BeatmapStatus = OsuBeatmapStatus.Unknown;
            }
            Beatmap.HitCircle = GetInt16();
            Beatmap.Slider = GetInt16();
            Beatmap.Spinner = GetInt16();
            Beatmap.LastModificationTime = new DateTime(GetInt64());
            Beatmap.AR = GetSingle();
            Beatmap.CS = GetSingle();
            Beatmap.HPDrain = GetSingle();
            Beatmap.OD = GetSingle();
            GetDouble();
            var pac = GetInt32();
            List<int> intlst = new List<int>();
            for (int i = 0; i < pac; i++)
            {
                int intflag = 0; double doubleflag = 0;
                GetByte();
                intflag = GetInt32();
                GetByte();
                doubleflag = GetDouble();
                osustars.Add((intflag), doubleflag);               
            }
            pac = GetInt32();
            for (int i = 0; i < pac; i++)
            {
                int intflag = 0; double doubleflag = 0;
                GetByte();
                intflag = GetInt32();
                GetByte();
                doubleflag = GetDouble();
                taikostars.Add((intflag), doubleflag);               
            }
            pac = GetInt32();
            for (int i = 0; i < pac; i++)
            {
                int intflag = 0; double doubleflag = 0;
                GetByte();
                intflag = GetInt32();
                GetByte();
                doubleflag = GetDouble();
                ctbstars.Add((intflag), doubleflag);
            }
            pac = GetInt32();
            for (int i = 0; i < pac; i++)
            {
                int intflag = 0; double doubleflag = 0;
                GetByte();
                intflag = GetInt32();
                GetByte();
                doubleflag = GetDouble();
                if (intlst.Contains(intflag)) throw new Exception();
                intlst.Add(intflag);
                maniastars.Add(intflag, doubleflag);
                
            }
            Beatmap.DrainTime = TimeSpan.FromSeconds(GetInt32());
            Beatmap.TotalTime = TimeSpan.FromMilliseconds(GetInt32());
            Beatmap.PreviewPoint = TimeSpan.FromMilliseconds(GetInt32());
            pac = GetInt32();
            for (int i = 0; i < pac; i++)
            {
                double BPM = GetDouble();
                double Offset = GetDouble();
                bool Inherit = GetBoolean();
                Beatmap.timepoints.Add(new OsuBeatmapTimePoint(BPM, Offset, Inherit));
            }
            Beatmap.BeatmapID = GetInt32();
            Beatmap.BeatmapSetID = GetInt32();
            Beatmap.ThreadID = GetInt32();
            GetByte();
            GetByte();
            GetByte();
            GetByte();
            GetInt16();
            GetSingle();
            Beatmap.Mode = (OsuGameMode)GetByte();
            if (osustars.Count == 0)
                osustars.Add(0, 0);
            if (taikostars.Count == 0)
                taikostars.Add(0, 0);
            if (ctbstars.Count == 0)
                ctbstars.Add(0, 0);
            if (maniastars.Count == 0)
                maniastars.Add(0, 0);
            Beatmap.ModStarPair.SetModeDict(OsuGameMode.Osu, osustars);
            Beatmap.ModStarPair.SetModeDict(OsuGameMode.Taiko, taikostars);
            Beatmap.ModStarPair.SetModeDict(OsuGameMode.Catch, ctbstars);
            Beatmap.ModStarPair.SetModeDict(OsuGameMode.Mania, maniastars);
            
            Beatmap.Source = GetString();
            Beatmap.Tags = GetString();
            GetInt16();
            GetString();
            GetBoolean();
            GetInt64();
            GetBoolean();
            Beatmap.FolderName = GetString();
            GetInt64();
            GetBoolean();
            GetBoolean();
            GetBoolean();
            GetBoolean();
            GetBoolean();
            GetInt32();
            GetByte();
            try { Beatmap.Stars = Beatmap.ModStarPair[Beatmap.Mode][0]; } 
            catch { Beatmap.Stars = 0; Debug.WriteLine("Error when reading stars return beatmap with 0 star."); return Beatmap; };
            return Beatmap;

        }
        void GetAllBeatmaps()
        {
            int i = Manifest.NumberOfBeatmap;
            OsuBeatmapCollection beatmaps = new OsuBeatmapCollection();
            for (int j = 0; j < i; j++)
            {
                var newbeatmap = ReadBeatmap();
                if(newbeatmap.Title!=""&&newbeatmap.Artist!="")
                beatmaps.Add(newbeatmap);
            }
            Beatmaps = beatmaps;
            reader.Close();
        }
    }
}


