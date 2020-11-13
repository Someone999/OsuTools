namespace osuTools
{
    namespace Replay
    {
        /// <summary>
        /// 录像文件包含的数据
        /// </summary>
        partial class Replay
        {
            /// <summary>
            /// 300g或激的数量
            /// </summary>
            public int c300g { get => C300g; }
            /// <summary>
            /// 300的数量
            /// </summary>
            public int c300 { get => C300; }
            /// <summary>
            /// 200或喝的数量
            /// </summary>
            public int c200 { get => C200; }
            /// <summary>
            /// 100的数量
            /// </summary>
            public int c100 { get => C100; }
            /// <summary>
            /// 50的数量
            /// </summary>
            public int c50 { get => C50; }
            /// <summary>
            /// 最大连击
            /// </summary>
            public int MaxCombo { get => maxco; }
            /// <summary>
            /// Miss的数量
            /// </summary>
            public int cMiss { get => Cmiss; }
            /// <summary>
            /// 分数
            /// </summary>
            public int Score { get => sco; }
            /// <summary>
            /// 保存录像的osu!的版本
            /// </summary>
            public int OsuVersion { get => ver; }
            /// <summary>
            /// 准度的字符串，格式为百分比，精确到两位小数
            /// </summary>
            public string AccuracyStr { get => accstr; }
            /// <summary>
            /// 准确度
            /// </summary>
            public double Accuracy { get => acc; }
            /// <summary>
            /// 游戏模式
            /// </summary>
            public OsuGameMode Mode { get => osumode; }
            /// <summary>
            /// 玩家名
            /// </summary>
            public string Player { get => playern; }
            /// <summary>
            /// 录像的MD5
            /// </summary>
            public string ReplayMD5 { get => md5; }
            /// <summary>
            /// 谱面的MD5
            /// </summary>
            public string BeatmapMD5 { get => beatmapmd5; }
            /// <summary>
            /// 是否达成Perfect判定
            /// </summary>
            public bool Perfect { get => perb; }

        }
    }

}