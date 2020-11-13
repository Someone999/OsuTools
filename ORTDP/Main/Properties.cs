namespace osuTools
{
    using osuTools.OtherTools;
    using System.Linq;
    using System;
    using osuTools.Beatmaps.HitObject;
    using osuTools.Game.Mods;
    using osuTools.osuToolsAttributes;

    public partial class ORTDP
    {

        /// <summary>
        /// 300g或激的数量
        /// </summary>
        [AvailableVariable("c300g", "300g的数量")]
        public int c300g { get => C300g; }

        /// <summary>
        /// 300的数量
        /// </summary>
        [AvailableVariable("c300", "300的数量")]
        public int c300 { get => C300; }

        /// <summary>
        /// 200或喝的数量
        /// </summary>
        [AvailableVariable("c200", "200的数量")]
        public int c200 { get => C200; }
        /// <summary>
        /// 100的数量
        /// </summary>
        [AvailableVariable("c100", "100的数量")]
        public int c100 { get => C100; }
        /// <summary>
        /// 50的数量
        /// </summary>
        [AvailableVariable("c50", "50的数量")]
        public int c50 { get => C50; }
        /// <summary>
        /// Miss的数量
        /// </summary>
        [AvailableVariable("cMiss", "Miss的数量")]
        public int cMiss { get => Cmiss; }
        /// <summary>
        /// 从游戏开始到当前已经出现过了的HitObject的数量
        /// </summary>
        [AvailableVariable("cPastHitObject","从游戏开始到当前出现过了的HitObject的数量")]
        public int cPastHitObject 
        {
            get
            {
                return CurrentMode.GetPassedHitObjectCount(this);
            }
        }
        /// <summary>
        /// 已经出现过了的HitObject在总HitObject中的占比
        /// </summary>
        [AvailableVariable("HitObjectPercent", "出现过了的HitObject在总HitObject中的占比")]
        public double HitObjectPercent
        {
            get => (double)cPastHitObject / tmpHitObjectCount;
        }
        /// <summary>
        /// 已经过了的HitObject在总HitObject中的占比的格式化后的字符串，百分数，精确到两位小数
        /// </summary>
        [AvailableVariable("HitObjectPercentStr", "出现过了的HitObject在总HitObject中的百分比，保留两位小数")]
        public string HitObjectPercentStr
        {
            get => HitObjectPercent.ToString("p2");
        }
        /// <summary>
        /// 当前的ModList
        /// </summary>
        [AvailableVariable("Mods","这个只在编写代码的时候有用")]
        public ModList Mods { get => mod; }
        /// <summary>
        /// 当前的连击数
        /// </summary>
        [AvailableVariable("Combo","当前的连击数")]
        public int Combo { get => combo; }
        /// <summary>
        /// 当前的分数
        /// </summary>
        [AvailableVariable("Score", "当前的分数")]
        public int Score { get => score; }
        /// <summary>
        /// 游戏进行的时间，以毫秒为单位
        /// </summary>
        [AvailableVariable("PlayTime", "游戏开始后的时间，以毫秒为单位")]
        public int PlayTime { get => time; }
        /// <summary>
        /// 当前剩余的血量，满值为200
        /// </summary>
        [AvailableVariable("HP","当前剩余的血量，满值为200")]
        public double HP { get => hp; }
        /// <summary>
        /// 谱面的状态，详见<seealso cref="OsuDB.OsuBeatmapStatus"/>
        /// </summary>
        [AvailableVariable("BeatmapStatus","谱面当前的状态(Ranked,Loved等)")]
        public OsuDB.OsuBeatmapStatus BeatmapStatus { get => b_status; internal set => b_status = value; }
        /// <summary>
        /// 准度pp的格式化后的字符串，保留两位小数
        /// </summary>
        [AvailableVariable("AccuracyPPStr", "当前的准度pp，保留两位小数")]
        public string AccuracyPPStr { get => ppinfo.AccuracyPPStr; }
        /// <summary>
        /// 定位pp的格式化后的字符串，保留两位小数
        /// </summary>
        [AvailableVariable("AimPPStr", "当前的的定位pp，保留两位小数")]
        public string AimPPStr { get => ppinfo.AimPPStr; }
        /// <summary>
        /// 速度pp的格式化后的字符串，保留两位小数
        /// </summary>
        [AvailableVariable("SpeedPPStr", "当前的速度pp，保留两位小数")]
        public string SpeedPPStr { get => ppinfo.SpeedPPStr; }
        /// <summary>
        /// 当前PP
        /// </summary>
        [AvailableVariable("CurrentPPStr", "当前pp，保留两位小数")]
        public string CurrentPPStr { get => ppinfo.CurrentPPStr; }
        /// <summary>
        /// 难度分
        /// </summary>
        [AvailableVariable("DiffcultyMultiply", "根据谱面计算的难度倍率")]
        public int DiffcultyMultiply { get => dmp; }
        /// <summary>
        /// 最大PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("MaxPPStr", "最大pp，保留两位小数")]
        public string MaxPPStr { get => ppinfo.MaxPPStr; }
        /// <summary>
        /// 最大定位PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("MaxAimPPStr", "最大定位pp，保留两位小数")]
        public string MaxAimPPStr { get => ppinfo.MaxAimPPStr; }
        /// <summary>
        /// 最大速度PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("MaxSpeedPPStr", "最大速度pp，保留两位小数")]
        public string MaxSpeedPPStr { get => ppinfo.MaxSpeedPPStr; }
        /// <summary>
        /// 最大精度PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("MaxAccuracyPPStr", "最大准度pp，保留两位小数")]
        public string MaxAccuracyPPStr { get => ppinfo.MaxAccuracyPPStr; }
        /// <summary>
        /// 在当前状态下全连后能获得的PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("FcPPStr", "当前状态下全连后能够获得的pp，保留两位小数")]
        public string FcPPStr { get => ppinfo.FcPPStr; }
        /// <summary>
        /// 在当前状态下全连后能获得的定位PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("FcAimPPStr", "当前状态下全连后能够获得的定位pp，保留两位小数")]
        public string FcAimPPStr { get => ppinfo.FcAimPPStr; }
        /// <summary>
        /// 全连后速度PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("FcSpeedPPStr", "当前状态下全连后能够获得的速度pp，保留两位小数")]
        public string FcSpeedPPStr { get => ppinfo.FCSpeedPPStr; }
        /// <summary>
        /// 全连后准度PP的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("FcAccuracyPPStr", "当前状态下全连后能够获得的准度pp，保留两位小数")]
        public string FcAccuracyPPStr { get => ppinfo.FCAccuracyPPStr; }
        /// <summary>
        /// 血量的百分比形式，保留两位小数
        /// </summary>
        [AvailableVariable("HPStr", "血量的百分比形式，保留两位小数")]
        public string HPStr
        {
            get
            {

                double hh = hp, ff = 0;
                hh = RealTimePPDisplayer.SmoothMath.SmoothDamp(hh, hp, ref ff, 0.2, 0.033);
                return (hh / 200).ToString("p");
            }
        }
        /// <summary>
        /// 准度
        /// </summary>
        [AvailableVariable("Accuracy", "准度")]
        public double Accuracy { get => CurrentMode.AccuracyCalc(this); }
        /// <summary>
        /// 准度的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("AccuracyStr", "准度的百分比形式，保留两位小数")]
        public string AccuracyStr { get => Accuracy.ToString("p2"); }
        /// <summary>
        /// 当前选中的谱面
        /// </summary>
        [AvailableVariable("Beatmap","当前选中的谱面的基本信息")]
        public Beatmaps.Beatmap Beatmap { get => b; }
        /// <summary>
        /// 每秒HitObject的个数
        /// </summary>
        [AvailableVariable("ObjectPerSecond","每秒HitObject的个数")]
        public double ObjectPerSecond { get => persec; }
        /// <summary>
        /// 当前开启的Mod的分别的简写
        /// </summary>
        [AvailableVariable("ModShortNames", "当前开启的Mod")]
        public string ModShortNames { get => mod.GetShortModsString(); }
        /// <summary>
        /// 当前开启的Mod的分别的名字
        /// </summary>
        [AvailableVariable("ModNames", "当前开启的Mod的全名")]
        public string ModNames { get => mod.GetModsString(); }
        /// <summary>
        /// 当前开启Mod对分数倍率的影响
        /// </summary>
        [AvailableVariable("ModScoreMultiplier", "当前开启的Mod对分数的影响")]
        public double ModScoreMultiplier 
        {
            get
            {
                if(CurrentMode==OsuGameMode.Mania)//如果是Mania模式
                {
                    ModList lst = new ModList();
                    //将原Mod列表中的降低难度筛选出来
                    var collection = mod.Mods.Where((mod) => mod.Type == ModType.DifficultyReduction);
                    //并添加到新的Mod列表里
                    lst = ModList.FromModArray(collection.ToArray());
                    //计算降低难度的Mod的分数倍率
                    return lst.ScoreMultiplier;
                    //在Mania下，只计算降低难度的Mod的分数倍率
                }
                //其余模式都会计算所有Mod的分数倍率
                return mod.ScoreMultiplier;
            }
        }
        /// <summary>
        /// 血量降到0以下时会触发Fail
        /// </summary>
        [AvailableVariable("CanFail", "在当前情况下玩家是否会失败")]
        public bool CanFail { get => canfail; }
        /// <summary>
        /// 当前游玩的谱面
        /// </summary>
        [AvailableVariable("NowPlaying","当前游玩的谱面的基础信息")]
        public string NowPlaying { get => np; }
        /// <summary>
        /// 游戏模式
        /// </summary>
        public GMMode GameMode { get => gm; }
        /// <summary>
        /// 重试的次数，请先不要使用
        /// </summary>
        public int RetryCount { get => rt; }
        /// <summary>
        /// 游玩次数，不准确
        /// </summary>
        public int PlayCount { get => ppc; }
        /// <summary>
        /// 300gRate的字符串形式。
        /// </summary>
        [AvailableVariable("c300gRate", "300gRate的百分比，保留两位小数")]
        public string c300gRateStr
        {
            get
            {
                if (c300g + c300 == 0) return "0%";
                return c300gRate.ToString("p");
            }
        }
        /// <summary>
        /// 300在300,200,100,50,Miss中占的百分比的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("c300Rate", "300Rate的百分比，保留两位小数")]
        public string c300RateStr
        {
            get
            {
                if (!double.IsNaN(c300Rate)&&!double.IsInfinity(c300Rate))
                    return c300Rate.ToString("p");
                return "0%";
            }
        }
       /// <summary>
       /// 300在有效Note数量中所占的比例
       /// </summary>
        [AvailableVariable("c300Rate","300在300和其余有效判定中所占的比例")]
        public double c300Rate
        {
            get
            {
                return CurrentMode.GetC300Rate(this);
            }
        }
        /// <summary>
        /// 玩家名
        /// </summary>
        public string PlayerName { get => pn; }
        /// <summary>
        /// Mania中表示c300g在所有300中所占的比例，std及ctb中表示激在激、喝之和中所占的比例
        /// </summary>
        [AvailableVariable("c300gRate", "Mania中表示c300g在所有300中所占的比例，std及ctb中表示激在激、喝之和中所占的比例，Taiko中表示双打在单打和双打中的比例")]
        public double c300gRate
        {
            get
            {
                return CurrentMode.GetC300gRate(this);
            }
        }        
        /// <summary>
        /// 谱面的标签
        /// </summary>
        [AvailableVariable("Tags","谱面的标签")]
        public string Tags { get => Beatmap == null ? "" : b.Tags; }
        /// <summary>
        /// 谱面的来源
        /// </summary>
        [AvailableVariable("Source", "谱面的来源")]
        public string Source { get => Beatmap == null ? "" : b.Source; }
        //public int MaxCombo { get => mco; }
        /// <summary>
        /// 当前的结算等级
        /// </summary>
        [AvailableVariable("CurrentRank", "当前的评级")]
        public string CurrentRank
        {
            get
            {
               
                // System.Windows.Forms.MessageBox.Show(OsuListenerManager.OsuStatus.CurrentStatus.Contains("Playing").ToString());
                if (GameStatus.CurrentStatus == OsuGameStatus.Playing)
                {
                    return CurrentMode.GetRanking(this);
                }
                return "???";
            }
        }
        /// <summary>
        /// 改变触发前的模式
        /// </summary>
        [AvailableVariable("LastMode", "上次玩的模式")]
        public Game.Modes.GameMode LastMode { get => gm.LastMode; }
        /// <summary>
        /// 改变触发后的模式
        /// </summary>
        [AvailableVariable("CurrentMode", "现在正在玩的模式")]
        public Game.Modes.GameMode CurrentMode { get => gm.CurrentMode; }
        /// <summary>
        /// 游戏状态
        /// </summary>
        public GMStatus GameStatus { get => gs; }
        /// <summary>
        /// 改变触发前的游戏状态
        /// </summary>
        [AvailableVariable("LastStatus", "之前的游戏状态")]
        public OsuGameStatus LastStatus { get => gs.LastStatus; }
        /// <summary>
        /// 当前的游戏状态
        /// </summary>
        [AvailableVariable("CurrentStatus", "现在的游戏状态")]
        public OsuGameStatus CurrentStatus { get => gs.CurrentStatus; }
        //GMMod NMod { get => mo; }
        /// <summary>
        /// 系统时间
        /// </summary>
        [AvailableVariable("SysTime","当前的系统时间")]
        public DateTime SysTime { get => DateTime.Now; }
        /// <summary>
        /// 谱面最大连击
        /// </summary>
        public int FullCombo { get => ppinfo.FullCombo; }
        /// <summary>
        /// 谱面打击物件的数量
        /// </summary>
        public int ObjectCount { get => ppinfo.ObjectCount; }
        /// <summary>
        /// 最大连击
        /// </summary>
        [AvailableVariable("MaxCombo","谱面的最大连击数")]
        public int MaxCombo { get => ppinfo.MaxCombo; }
        /// <summary>
        /// 玩家的最大连击
        /// </summary>
        [AvailableVariable("PlayerMaxCombo", "玩家达到的最大连击数")]
        public int PlayerMaxCombo { get => ppinfo.PlayerMaxCombo; }
        /// <summary>
        /// 当前的连击
        /// </summary>
        [AvailableVariable("CurrentCombo", "当前的连击数")]
        public int CurrentCombo { get => ppinfo.CurrentCombo; }
        /// <summary>
        /// 准度pp
        /// </summary>
        [AvailableVariable("AccuracyPP", "准度pp")]
        public double AccuracyPP { get => ppinfo.AccuracyPP; }
        /// <summary>
        /// 定位pp
        /// </summary>
        [AvailableVariable("AimPP", "定位pp")]
        public double AimPP { get => ppinfo.AimPP; }
        /// <summary>
        /// 速度pp
        /// </summary>
        [AvailableVariable("SpeedPP", "速度pp")]
        public double SpeedPP { get => ppinfo.SpeedPP; }
        /// <summary>
        /// 全连后准度pp
        /// </summary>
        [AvailableVariable("FcAccuracyPP", "当前状态下全连后能获得的精度pp")]
        public double FcAccuracyPP { get => ppinfo.FCAccuracyPP; }
        /// <summary>
        /// 全连后定位pp
        /// </summary>
        [AvailableVariable("FcAimPP", "当前状态下全连后能获得的定位pp")]
        public double FcAimPP { get => ppinfo.FCAimPP; }
        /// <summary>
        /// 全连后速度pp
        /// </summary>
        [AvailableVariable("FcSpeedPP", "当前状态下全连后能获得的速度pp")]
        public double FcSpeedPP { get => ppinfo.FCSpeedPP; }
        /// <summary>
        /// 最大准度pp
        /// </summary>
        [AvailableVariable("MaxAccuracyPP", "最大准度pp")]
        public double MaxAccuracyPP { get => ppinfo.MaxAccuracyPP; }
        /// <summary>
        /// 最大定位pp
        /// </summary>
        [AvailableVariable("MaxAimPP", "最大定位pp")]
        public double MaxAimPP { get => ppinfo.MaxAimPP; }
        /// <summary>
        /// 最大速度pp
        /// </summary>
        [AvailableVariable("MaxSpeedPP", "最大速度pp")]
        public double MaxSpeedPP { get => ppinfo.MaxSpeedPP; }
        /// <summary>
        /// 全连后pp
        /// </summary>
        [AvailableVariable("FcPP", "当前状态下全连后能获得的pp")]
        public double FcPP { get => ppinfo.FcPP; }
        /// <summary>
        /// 总pp
        /// </summary>
        [AvailableVariable("MaxPP", "总pp")]
        public double MaxPP { get => ppinfo.MaxPP; }
        /// <summary>
        /// 当前pp
        /// </summary>
        [AvailableVariable("CurrentPP", "当前pp")]
        public double CurrentPP { get => ppinfo.CurrentPP; }
        /// <summary>
        /// 标题
        /// </summary>
        [AvailableVariable("Title", "曲目的英文名")]
        public string Title { get => Beatmap == null ? "" : Beatmap.Title; }
        /// <summary>
        /// UTF8编码的标题
        /// </summary>
        [AvailableVariable("TitleUnicode", "曲目的原名")]
        public string TitleUnicode { get => Beatmap == null ? "" : Beatmap.TitleUnicode; }
        /// <summary>
        /// 艺术家
        /// </summary>
        [AvailableVariable("Artist", "曲目作者的英文名")]
        public string Artist { get => Beatmap == null ? "" : b.Artist;  }
        /// <summary>
        /// UTF8编码的艺术家
        /// </summary>
        [AvailableVariable("ArtistUnicode", "曲目作者的原名")]
        public string ArtistUnicode { get => Beatmap == null ? "" : b.ArtistUnicode; }
        /// <summary>
        /// 谱面的作者
        /// </summary>
        [AvailableVariable("Creator", "谱面的作者")]
        public string Creator { get => Beatmap == null ? "" : b.Creator; }
        /// <summary>
        /// 谱面难度
        /// </summary>
        [AvailableVariable("Difficulty", "谱面的难度")]
        public string Difficulty { get => Beatmap == null ? "" : b.Difficulty; }
        /// <summary>
        /// 谱面难度
        /// </summary>
        [AvailableVariable("Version", "谱面的难度")]
        public string Version
        {
            get => Beatmap == null ? "" : Beatmap.Version;
        }
        /// <summary>
        /// 谱面文件的文件名
        /// </summary>
        [AvailableVariable("FileName", "谱面文件的文件名")]
        public string FileName
        {
            get => Beatmap == null ? "" : Beatmap.FileName;
        }
        /// <summary>
        /// 谱面文件的全路径
        /// </summary>
        [AvailableVariable("FullPath", "谱面文件的全路径")]
        public string FullPath
        {
            get => Beatmap == null ? "" : Beatmap.FullPath;
        }
        /// <summary>
        /// 谱面的下载链接
        /// </summary>
        [AvailableVariable("DownloadLink", "谱面的下载页的链接")]
        public string DownloadLink
        {
            get => Beatmap == null ? "" : Beatmap.DownloadLink;
        }
        /// <summary>
        /// 谱面的背景图片的文件名
        /// </summary>
        [AvailableVariable("BackgroundFileName", "谱面的背景图片的文件名")]
        public string BackgroundFileName
        {
            get => Beatmap == null ? "" : Beatmap.BackgroundFileName;
        }
        /// <summary>
        /// 谱面ID
        /// </summary>
        [AvailableVariable("BeatmapID", "谱面ID")]
        public int BeatmapID
        {
            get => Beatmap == null ? -1 : Beatmap.BeatmapID;
        }
        /// <summary>
        /// 谱面集ID
        /// </summary>
        [AvailableVariable("BeatmapSetID","谱面集的ID")]
        public int BeatmapSetID { get => Beatmap == null ? -1 : Beatmap.BeatmapSetID; }
        /// <summary>
        /// 此谱面打击物件的数量
        /// </summary>
        [AvailableVariable("cHitObject", "谱面打击物件的数量")]
        public int cHitObject 
        { 
            get
            {
                return GameMode.CurrentMode.GetBeatmapHitObjectCount(b);
            }
        }

        /// <summary>
        /// 综合难度
        /// </summary>
        [AvailableVariable("OD", "谱面的综合难度")]
        public double OD { get => b == null ? -1 : b.OD; }
        /// <summary>
        /// 掉血速度和回血难度
        /// </summary>
        [AvailableVariable("HPRate", "游戏中掉血的速度和回血的难度")]
        public double HPRate { get => b == null ? -1 : b.HP; }
        /// <summary>
        /// 缩圈速度
        /// </summary>
        [AvailableVariable("AR", "缩圈速度")]
        public double AR { get => b == null ? -1 : b.AR; }
        /// <summary>
        /// 圈圈大小或Mania的键位数
        /// </summary>
        [AvailableVariable("CS", "圈圈大小或Mania谱面的键数")]
        public double CS { get => b == null ? -1 : b.CS; }
        /// <summary>
        /// 音频文件名称
        /// </summary>
        [AvailableVariable("AudioFileName", "谱面音频文件名称")]
        public string AudioFileName { get => Beatmap == null ? "" : b.AudioFileName; }
        /// <summary>
        /// 歌曲的长度
        /// </summary>
        [AvailableVariable("SongDuration", "歌曲的时长")]
        public TimeSpan SongDuration
        {
            get
            {
                if (bp == null) return new TimeSpan();
                if (time <= dur.TotalMilliseconds)
                {
                    return dur;
                }
                /*else if (time > dur.TotalMilliseconds + 500)
                {
                    return TimeSpan.FromMilliseconds(time);
                }*/
                return new TimeSpan();
            }
        }
        /// <summary>
        /// 当前的时间
        /// </summary>
        [AvailableVariable("CurrentTime","曲目已播放的时间")]
        public TimeSpan CurrentTime { get => cur; }
        /// <summary>
        /// 当前时间在总时长中的占比
        /// </summary>
        [AvailableVariable("TimePercent", "曲目已播放的时间在总时长中的占比")]
        public double TimePercent { get => tmper; }
        /// <summary>
        /// 时间的百分比，保留两位小数
        /// </summary>
        [AvailableVariable("TimePercent", "曲目已播放的时间在总时长中的百分比，保留两位小数")]
        public string TimePercentStr { get => tmper.ToString("p"); }
        /// <summary>
        /// 格式化后的歌曲时间描述字符串
        /// </summary>
        public string FormatedTimeStr { get => ppinfo.FormatedTimeStr; }
        /// <summary>
        /// 格式化后的pp描述字符串
        /// </summary>
        public string FormatedPPStr { get => ppinfo.FormatedPPStr; }
        /// <summary>
        /// 格式化后的判定描述字符串
        /// </summary>
        public string FormatedHitStr { get => ppinfo.FormatedHitStr; }
        /// <summary>
        /// 从RealTimePPDisplayer得到的原始字符串
        /// </summary>
        public string RawStr { get => ppinfo.RawStr; }
        /// <summary>
        /// 当前pp在总pp中占的百分比
        /// </summary>
        [AvailableVariable("PPpercent", "当前pp在总pp中的百分比")]
        public string PPpercent { get => (ppinfo.CurrentPP / ppinfo.MaxPP).ToString("p"); }
        /// <summary>
        /// 谱面的难度星级
        /// </summary>
        [AvailableVariable("Stars", "谱面的星星数")]
        public double Stars { get => b == null ? -1 : b.Stars; }
        /// <summary>
        /// 谱面的难度星级的字符串形式，保留两位小数
        /// </summary>
        [AvailableVariable("Stars", "谱面的星星数，保留两位小数")]
        public string StarsStr { get => Stars.ToString("f2"); }
        public double TempPP { get; private set; }
        /// <summary>
        /// 是否达到Perfect判定
        /// </summary>
        [AvailableVariable("Perfect", "是否达到Perfect判定")]
        public bool Perfect
        {
            get
            {
                if (Accuracy == 100)
                {
                    return true;
                }
                CurrentMode.IsPerfect(this);
                return false;
            }
        }
        string perfectstr = "&& Perfect";
        /// <summary>
        /// 如当前达到Perfect判定，将会显示的内容
        /// </summary>
        public string PerfectRank { get { if (Perfect) { return perfectstr; } else { return ""; } }set { perfectstr = value; } }
        /// <summary>
        /// 当前时间对应的TimeSpan的字符串形式
        /// </summary>
        [AvailableVariable("CurrentTimeStr", "当前时间的分:秒格式")]
        public string CurrentTimeStr { get => $"{cur.Hours * 60 + cur.Minutes:d2}:{cur.Seconds:d2}"; }
        /// <summary>
        /// 歌曲长度对应的TimeSpan的字符串形式
        /// </summary>
        [AvailableVariable("SongDurationStr", "歌曲总时长的分:秒格式")]
        public string SongDurationStr { get => $"{dur.Hours * 60 + dur.Minutes:d2}:{dur.Seconds:d2}"; }
    }
}