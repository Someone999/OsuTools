namespace osuTools.Beatmaps.HitObject
{
    using System.Collections.Generic;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// HitObject的工具类
    /// </summary>
    public static class HitObjectTools
    {
        /// <summary>
        /// 将整数分解，并解析成相应类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bit"></param>
        /// <returns></returns>
        static public List<T> GetGenericTypesByInt<T>(int bit) where T:Enum
        {
            List<T> lst = new List<T>();
            int cur = bit;
            if(typeof(T)==typeof(HitSounds))
            {
                if(cur==0)
                {
                    lst.Add((T)(object)HitSounds.Normal);
                }
            }
            if (typeof(T) == typeof(SampleSets))
            {
                if (cur == 0)
                {
                    lst.Add((T)(object)SampleSets.Default);
                }
            }
            if(typeof(T)==typeof(OsuGameMod))
            {
                if (cur == 0)
                    lst.Add((T)(object)OsuGameMod.None);
            }
           
            while (cur > 0)
            {
                double log2 = Math.Log(cur, 2);
                int log2int = (int)Math.Truncate(log2);
                int value = (int)Math.Pow(2, log2int);
                if (typeof(T) != typeof(OsuGameMod))
                {
                    T rslt = (T)Enum.Parse(typeof(T), log2int.ToString());
                    lst.Add(rslt);
                }
                else
                {                  
                    lst.Add((T)Enum.Parse(typeof(T),value.ToString()));
                }
                cur -= value;
            }
            return lst;
        }
        /// <summary>
        /// 将普通打击物件转化成特定模式的打击物件
        /// </summary>
        /// <param name="gameMode"></param>
        /// <param name="hitObjectType"></param>
        /// <returns></returns>
        public static IHitObject GetHitObjectClass(OsuGameMode gameMode,HitObjectTypes hitObjectType)
        {

            switch(gameMode)
            {
                case OsuGameMode.Osu:
                    switch(hitObjectType)
                    {
                        case HitObjectTypes.HitCircle: return new HitCircle();
                        case HitObjectTypes.Slider: return new Slider();
                        case HitObjectTypes.Spinner: return new Spinner();
                        default: return null;
                    }
                case OsuGameMode.Catch:
                    switch(hitObjectType)
                    {
                        case HitObjectTypes.HitCircle: return new Fruit();
                        case HitObjectTypes.Slider:return new JuiceStream();
                        case HitObjectTypes.Spinner:return new BananaShower();
                        default: return null;
                    }
                case OsuGameMode.Taiko:
                    switch(hitObjectType)
                    {
                        case HitObjectTypes.LargeTaikoBlueHit: return new LargeTaikoBlueHit();
                        case HitObjectTypes.LargeTaikoRedHit:  return new LargeTaikoRedHit();
                        case HitObjectTypes.TaikoBlueHit:return new TaikoBlueHit();
                        case HitObjectTypes.TaikoRedHit:  return new TaikoRedHit();
                        case HitObjectTypes.Slider: return new DrumRoll();
                        case HitObjectTypes.Spinner: return new DrumRoll();
                        default: return null;
                    }
                case OsuGameMode.Mania:
                    switch(hitObjectType)
                    {
                        case HitObjectTypes.HitCircle: return new ManiaHit();
                        case HitObjectTypes.ManiaHold:  return new ManiaHold();
                        default: return null;
                    }
                default: return null;
            }
        }
    }
}