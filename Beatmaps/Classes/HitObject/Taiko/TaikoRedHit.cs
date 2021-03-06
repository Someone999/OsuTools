﻿namespace osuTools.Beatmaps.HitObject
{
    /// <summary>
    /// 表示一个红色的太鼓note
    /// </summary>
    public class TaikoRedHit : IHitObject
    {
        /// <summary>
        /// Note的类型
        /// </summary>
        public HitObjectTypes HitObjectType { get; } = HitObjectTypes.TaikoRedHit;
        /// <summary>
        /// 该Note相对于歌曲开始的时间点
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// 自定义的音效
        /// </summary>
        public Sounds.HitSample HitSample { get; set; }
        /// <summary>
        /// 音效
        /// </summary>
        public HitSounds HitSound { get; set; } = HitSounds.Normal;
        /// <summary>
        /// 该Note存在的模式
        /// </summary>
        public OsuGameMode SpecifiedMode { get; } = OsuGameMode.Taiko;
        /// <summary>
        /// Note的位置。在Taiko模式中，该属性不生效。
        /// </summary>
        public OsuPixel Position { get; private set; }
        /// <summary>
        /// 使用正确格式的字符串创建一个TaikoRedHit对象
        /// </summary>
        /// <param name="data">要使用的字符串</param>
        public void Parse(string data)
        {
            string[] info = data.Split(',');
            Position = new OsuPixel(int.Parse(info[0]), int.Parse(info[1]));
            Offset = int.Parse(info[2]);
            type = int.Parse(info[3]);
            if (HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(type).Contains(HitObjectTypes.HitCircle))
            {
                if (info.Length > 5)
                    HitSample = new Sounds.HitSample(info[5]);
            }
        }
        int type;
        /// <summary>
        /// 获取osu!格式的数据
        /// </summary>
        /// <returns></returns>
        public string ToOsuFormat()
        {
            return $"{Position.x},{Position.y},{Offset},{type},{0},{HitSample.GetData()}";
        }
        public override string ToString()
        {
            return $"Type:{HitObjectType} Offset:{Offset}";
        }
    }
}