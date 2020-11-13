namespace osuTools
{
    using Osu.Game.Modes;
    using osuTools.Game.Modes;
    using Sync.Tools;

    /// <summary>
    /// 记录游戏模式
    /// </summary>
    [System.Serializable]  
    public partial class GMMode
    {
        GameMode l = null, m = null;
        /// <summary>
        /// 上一次的游戏模式
        /// </summary>
        public GameMode LastMode { get => l; }
        /// <summary>
        /// 当前游戏模式
        /// </summary>
        public GameMode CurrentMode { get => m; }
        /// <summary>
        /// 使用两个<see cref="OsuRTDataProvider.Listen.OsuPlayMode"/>构造一个GMMode
        /// </summary>
        /// <param name="LastMode"></param>
        /// <param name="NowMode"></param>
        public GMMode(OsuRTDataProvider.Listen.OsuPlayMode LastMode, OsuRTDataProvider.Listen.OsuPlayMode NowMode)
        {
            //IO.CurrentIO.Write($"Get Mode {LastMode} -> {NowMode}");
            l = GameMode.FromLegacyMode((OsuGameMode)LastMode);
            m = GameMode.FromLegacyMode((OsuGameMode)(NowMode));
            //IO.CurrentIO.Write("");
        }
    }
    namespace Tools
    {
        /// <summary>
        /// 游戏模式的工具
        /// </summary>
        public static class OsuGameModeTools
        {
            /// <summary>
            /// 将游戏模式从字符串转换为枚举
            /// </summary>
            public static GameMode Parse(string mode)
            {
                if (string.Compare(mode, "mania", true) == 0) return new ManiaMode();
                if (string.Compare(mode, "osu", true) == 0) return new OsuMode();
                if (string.Compare(mode, "catch", true) == 0 || string.Compare(mode, "Ctb", true) == 0) return new CatchMode();
                if (string.Compare(mode, "taiko", true) == 0) return new TaikoMode();
                return null;
            }
            /// <summary>
            /// 将游戏模式从整数转换为枚举
            /// </summary>
            public static GameMode Parse(int mode)
            {
                switch (mode)
                {
                    case 0:return new OsuMode();
                    case 1: return new TaikoMode();
                    case 2:return new CatchMode();
                    case 3:return new ManiaMode();
                    default:return null;
                }
            }
        }
    }
}
