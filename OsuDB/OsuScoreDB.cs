using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Linq.Expressions;

namespace osuTools.OsuDB
{
    /// <summary>
    /// 分数文件起始部位的数据
    /// </summary>
    public class ScoreManifest
    {
        /// <summary>
        /// 使用游戏版本构造一个ScoreManifest
        /// </summary>
        /// <param name="ver"></param>
        public ScoreManifest(int ver)
        {
            Version = ver;
        }
        /// <summary>
        /// 游戏版本
        /// </summary>
        public int Version { get; internal set; }
    }
    /// <summary>
    /// scores.db中存储的成绩
    /// </summary>
    public class OsuScoreInfo : Online.ScoreSorted,IOsuDBData
    {
        /// <summary>
        /// 确定指定的对象是否等于当前对象。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        /// <summary>
        /// 默认哈希函数
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        double AccCalc(OsuGameMode mode)
        {
            switch (mode)
            {
                case OsuGameMode.Osu: return (c300 + c100 * (1.0 / 3.0) + c50 * (1.0 / 6)) / (c300 + c100 + c50 + cMiss);
                case OsuGameMode.Taiko: return (c300 + c100 * 0.5) / (c300 + c100 + cMiss);
                case OsuGameMode.Catch: return (double)(c300 + c100 + c50) / (c300 + c100 + c50 + c200 + cMiss);
                case OsuGameMode.Mania: return ((c300g + c300) + c200 * (2 / 3.0) + c100 * (1 / 3.0) + (c50 * 1 / 6.0)) / (c300g + c300 + c200 + c100 + c50 + cMiss); 
                default: return -1; 
            }
        }
        /// <summary>
        /// 使用时间判断两个成绩是否为同一个成绩
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(OsuScoreInfo a,OsuScoreInfo b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.PlayTime == b.PlayTime;
        }
        /// <summary>
        /// 使用时间判断两个成绩是否为同一个成绩
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(OsuScoreInfo a, OsuScoreInfo b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.PlayTime != b.PlayTime;
        }
       /// <summary>
       /// 使用特定的数据来构造一个ScoreDBData对象
       /// </summary>
       /// <param name="mode">游戏模式</param>
       /// <param name="ver">游戏版本</param>
       /// <param name="bmd5">谱面的MD5</param>
       /// <param name="name">玩家名</param>
       /// <param name="rmd5">回放的MD5</param>
       /// <param name="c300">300的数量</param>
       /// <param name="c100">100的数量</param>
       /// <param name="c50">50的数量</param>
       /// <param name="c300g">激或彩300的数量</param>
       /// <param name="c200">喝或200的数量</param>
       /// <param name="cmiss">Miss的数量</param>
       /// <param name="score">分数</param>
       /// <param name="maxcombo">最大连击</param>
       /// <param name="per">是否为Perfect</param>
       /// <param name="mods">使用了的Mod的整数形式</param>
       /// <param name="empty">一个必须为空的字符串</param>
       /// <param name="playtime">游玩的时间，以Tick为单位</param>
       /// <param name="verify">一个值必须为-1的整数</param>
       /// <param name="scoreid">ScoreID</param>
       /// <param name="acc">准确度</param>
        public OsuScoreInfo(OsuGameMode mode, int ver, string bmd5, string name, string rmd5, short c300, short c100, short c50, short c300g, short c200, short cmiss, int score, short maxcombo, bool per, int mods, string empty, long playtime, int verify, long scoreid,double acc)
        {
            this.mode = mode;
            //System.Windows.Forms.MessageBox.Show(Mode.ToString());
            this.ver = ver;
            bemd5 = bmd5;
            remd5 = rmd5;
            pn = name;
            C300g = c300g;
            C300 = c300;
            C200 = c200;
            C100 = c100;
            C50 = c50;
            CMiss = cmiss;
            sc = score;
            maxc = maxcombo;
            this.per = per;
            this.mods = Beatmaps.HitObject.HitObjectTools.GetGenericTypesByInt<OsuGameMod>(mods);
            pt = playtime;
            pdt = new DateTime(pt);
            if (verify != -1) throw new osuToolsException.FailToParseException("验证失败");
            sid = scoreid;
            Debug.Assert((c300 + c100 + c50 + cMiss) != 0);
            this.acc = AccCalc(mode);
            //System.Windows.Forms.MessageBox.Show(Score.ToString());
        }
        short C300g, C300, C200, C100, C50, CMiss, maxc;
        bool per; long pt, sid; int ver, sc; OsuGameMode mode; string bemd5, remd5, pn;
        DateTime pdt;double acc;
        /// <summary>
        /// 游戏版本
        /// </summary>
        public int GameVersion { get => ver; }
        /// <summary>
        /// 游戏模式
        /// </summary>
        public OsuGameMode Mode { get => mode; }
        /// <summary>
        /// 谱面的MD5
        /// </summary>
        public string BeatmapMD5 { get => bemd5; }
        /// <summary>
        /// 玩家名
        /// </summary>
        public string PlayerName { get => pn; }
        /// <summary>
        /// 回放的MD5
        /// </summary>
        public string ReplayMD5 { get => remd5; }
        /// <summary>
        /// 激或彩300的数量
        /// </summary>
        public short c300g { get => C300g; }
        /// <summary>
        /// 300的数量
        /// </summary>
        public short c300 { get => C300; }
        /// <summary>
        /// 喝或200的数量
        /// </summary>
        public short c200 { get => C200; }
        /// <summary>
        /// 100的数量
        /// </summary>
        public short c100 { get => C100; }
        /// <summary>
        /// 50的数量
        /// </summary>
        public short c50 { get => C50; }
        /// <summary>
        /// Miss的数量
        /// </summary>
        public short cMiss { get => CMiss; }
        /// <summary>
        /// 分数
        /// </summary>
        public override int Score { get => sc; }
        /// <summary>
        /// 最大连击
        /// </summary>
        public short MaxCombo { get => maxc; }
        /// <summary>
        /// 是否达成Perfect判定
        /// </summary>
        public bool Perfect { get => per; }
        internal List<OsuGameMod> mods = new List<OsuGameMod>();
        /// <summary>
        /// 本次游戏使用的Mod
        /// </summary>
        public IReadOnlyList<OsuGameMod> Mods { get => mods.AsReadOnly(); }
        /// <summary>
        /// 游玩时间
        /// </summary>
        public DateTime PlayTime { get => pdt; }
        /// <summary>
        /// 分数ID
        /// </summary>
        public long ScoreID { get => sid; }
        /// <summary>
        /// 准确度
        /// </summary>
        public double Accuracy { get=>acc; }
    }
    /// <summary>
    /// 从scores.db中读取成绩。
    /// </summary>
    public class OsuScoreDB : IOsuDB
    {
        List<OsuScoreInfo> Score = new List<OsuScoreInfo>();
        int beatmapnum = 0;
        /// <summary>
        /// 存储的分数
        /// </summary>
        public IReadOnlyList<OsuScoreInfo> Scores => Score.AsReadOnly();
        /// <summary>
        /// scores,db中的头部数据
        /// </summary>
        public ScoreManifest Manifest { get; internal set; } = new ScoreManifest(-1);
        BinaryReader reader;
        /// <summary>
        /// 从score.db中获取数据
        /// </summary>
        public OsuScoreDB()
        {
            OsuInfo info = new OsuInfo();
            string dbfile = info.OsuDirectory + "scores.db";
            var stream = File.OpenRead(dbfile);
            reader = new BinaryReader(stream);
            Manifest.Version = reader.ReadInt32();
            beatmapnum = reader.ReadInt32();
            Read();

        }
        bool IsString()
        {
            reader.ReadByte(); return true;

            //else if (reader.ReadByte() == 0x0b) return true;
            // else throw new osuToolsException.FailToParse("无法读取字符串。");
        }
        System.Windows.Forms.DialogResult msgbox(object o)
        {
            return System.Windows.Forms.MessageBox.Show(o.ToString());
        }
        string GetString() { if (IsString()) return reader.ReadString(); else return string.Empty; }
        short GetShort()
        {
            var v = reader.ReadInt16();
            // msgbox(v);
            return v;
        }
        int GetInt32()
        {
            var v = reader.ReadInt32();
            // msgbox(v);
            return v;
        }
        long GetInt64()
        {
            var v = reader.ReadInt64();
            // msgbox(v);
            return v;
        }
        byte GetByte()
        {
            var v = reader.ReadByte();
            //msgbox(v);
            return v;
        }
        bool GetBool()
        {
            var v = reader.ReadBoolean();
            //msgbox(v);
            return v;
        }
        double GetDouble()
        {
            var v = reader.ReadDouble();
            //msgbox(v);
            return v;
        }
        string GetEmptyString()
        {
            byte b = reader.ReadByte();
            if (b == 0x0b)
            {
                return reader.ReadString();
            }
            //System.Windows.Forms.MessageBox.Show(reader.ReadByte().ToString());
            return "";
        }
       
        /// <summary>
        /// 从scores.db中读取
        /// </summary>
        public void Read()
        {
            int x = 0;
            for (int i = 0; i < beatmapnum; i++)
            {
                string curmd5 = "";
                curmd5 = GetString();
                int scorenum = reader.ReadInt32();
                for (int j = 0; j < scorenum; j++)
                {
                    
                    var mode = (OsuGameMode)GetByte();
                    var ver = GetInt32();
                    var beatmapmd5 = GetString();
                    var playername= GetString();
                    var replaymd5 = GetString();
                    var c300 = GetShort();  
                    var c100 = GetShort(); 
                    var c50 = GetShort();  
                    var c300g = GetShort(); 
                    var c200 = GetShort();  
                    var cmiss = GetShort();
                    var score = GetInt32();
                    var maxcombo = GetShort();
                    var per = GetBool();
                    var mods = GetInt32();
                    var emp = GetEmptyString();
                    var timestamp = GetInt64();
                    var veri = GetInt32();
                    var onlineid = GetInt64();
                    if ((c300 + c100 + c50 + cmiss) != 0)
                    {

                        var newscore = new OsuScoreInfo(mode, ver, beatmapmd5, playername, replaymd5, c300, c100, c50, c300g, c200, cmiss, score, maxcombo, per, mods, emp, timestamp, veri, onlineid,
                                                       0);
                        if (Score.Count > 0)
                        {
                            if (newscore.PlayTime != Score.Last().PlayTime)
                                Score.Add(newscore);
                        }
                        else Score.Add(newscore);
                    }
                    else
                    {
                        x++;
                    }
                }
            }
        }
    }
}