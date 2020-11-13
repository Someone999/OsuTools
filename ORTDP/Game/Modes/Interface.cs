using osuTools.Beatmaps;
using osuTools.Beatmaps.HitObject;
using osuTools.Game.Mods;
using osuTools.OsuDB;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace osuTools.Game.Modes
{    
    /// <summary>
    /// 代表这个游戏模式存在于<see cref="OsuGameMode"/>
    /// </summary>
    public interface ILegacyMode
    {
        /// <summary>
        /// 对应的<see cref="OsuGameMode"/>
        /// </summary>
        OsuGameMode LegacyMode { get; }
    }
    /// <summary>
    /// 分数的组成
    /// </summary>
    public class ScoreInfo
    {
        /// <summary>
        /// 300g的数量
        /// </summary>
        public int c300g { get; set; }
        /// <summary>
        /// 300的数量
        /// </summary>
        public int c300 { get; set; }
        /// <summary>
        /// 200的数量
        /// </summary>
        public int c200 { get; set; }
        /// <summary>
        /// 100的数量
        /// </summary>
        public int c100 { get; set; }
        /// <summary>
        /// 50的数量
        /// </summary>
        public int c50 { get; set; }
        /// <summary>
        /// Miss的数量
        /// </summary>
        public int cMiss { get; set; }
    }
    /// <summary>
    /// 表示一个游戏模式
    /// </summary>
    public abstract class GameMode : IEqualityComparer<GameMode>
    {
        /// <summary>
        /// 创建一个对应模式的<see cref="IHitObject"/>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual IHitObject CreateHitObject(string data) => null;
        /// <summary>
        /// 创建一个包含列数的对应模式的<see cref="IHitObject"/>
        /// </summary>
        /// <param name="data"></param>
        /// <param name="maniaColumns"></param>
        /// <returns></returns>
        public virtual IHitObject CreateHitObject(string data, int maniaColumns) => null;
        /// <summary>
        /// 模式的名字
        /// </summary>
        public virtual string ModeName { get; protected set; } = "";
        /// <summary>
        /// 模式的描述
        /// </summary>
        public virtual string Description { get; protected set; } = "";
        /// <summary>
        /// 可用的Mod
        /// </summary>
        public virtual Mod[] AvaliableMods { get; protected internal set; }
        /// <summary>
        /// 这个模式的准度计算方法
        /// </summary>
        /// <param name="scoreInfo"></param>
        /// <returns></returns>
        public virtual double AccuracyCalc(ORTDP scoreInfo) => 0;
        /// <summary>
        /// 这个模式的准度计算方法
        /// </summary>
        /// <param name="scoreInfo"></param>
        /// <returns></returns>
        public virtual double AccuracyCalc(ScoreInfo scoreInfo) => 0;
        /// <summary>
        /// 比较两个模式是否为同一个模式
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool Equals(GameMode a, GameMode b)
        {
            if (a is ILegacyMode && b is ILegacyMode)
                return ((ILegacyMode)a).LegacyMode == ((ILegacyMode)b).LegacyMode;
            return a.ModeName == b.ModeName;
        }
        /// <summary>
        /// 获取模式的Hash。如果模式为<see cref="ILegacyMode"/>则返回对应的枚举值，否则返回模式名称的Hash。
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public int GetHashCode(GameMode a)
        {
            if (a is ILegacyMode)
                return (int)(a as ILegacyMode).LegacyMode;
            else
                return a.ModeName.GetHashCode();
        }
        public static bool operator ==(GameMode a, GameMode b) => a.Equals(a, b);
        public static bool operator !=(GameMode a, GameMode b) => !a.Equals(a, b);
        public override bool Equals(object obj)
        {
            if (obj is GameMode) return Equals(this, obj);
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return GetHashCode(this);
        }
        /// <summary>
        /// 将<see cref="ILegacyMode"/>转换成GameMode
        /// </summary>
        /// <param name="legacyMode"></param>
        /// <returns></returns>
        public static GameMode FromLegacyMode(OsuGameMode legacyMode)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            var types = asm.GetTypes();
            foreach (var type in types)
            {
                if (type.GetInterfaces().Any((i) => i == typeof(ILegacyMode)))
                {
                    ILegacyMode mode = (ILegacyMode)type.GetConstructor(new Type[0]).Invoke(new object[0]);
                    if (mode.LegacyMode == legacyMode)
                        return (GameMode)mode;
                }
            }
            return null;
        }
        public static bool operator ==(GameMode mode, OsuGameMode enumMode)
        {
            if (mode is ILegacyMode gamemode)
            {
                return gamemode.LegacyMode == enumMode;
            }
            return false;
        }
        public static bool operator !=(GameMode mode, OsuGameMode enumMode)
        {
            if (mode is ILegacyMode gamemode)
            {
                return gamemode.LegacyMode != enumMode;
            }
            return true;
        }
        /// <summary>
        /// 获取谱面的HitObject数量
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public virtual int GetBeatmapHitObjectCount(Beatmap b) => 0;
        /// <summary>
        /// 获取已经经过了的HitObject的数量
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public virtual int GetPassedHitObjectCount(ORTDP info) => 0;
        /// <summary>
        /// 返回Mode的名称
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ModeName;
        }
        /// <summary>
        /// 判断成绩是否达到Perfect判定
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public virtual bool IsPerfect(ORTDP info) => false;
        /// <summary>
        /// 300g出现率的计算方法
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public virtual double GetC300gRate(ORTDP info) => 0;
        /// <summary>
        /// 300出现率方法
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public virtual double GetC300Rate(ORTDP info) => 0;
        /// <summary>
        /// 当前的评级的判定方法
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public virtual string GetRanking(ORTDP info) => "Unknown";
    }
}