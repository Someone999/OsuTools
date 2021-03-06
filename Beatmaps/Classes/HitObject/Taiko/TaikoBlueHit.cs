﻿namespace osuTools.Beatmaps.HitObject
{
    /// <summary>
    /// 表示一个蓝色的Taiko Note
    /// </summary>
    public class TaikoBlueHit : IHitObject
    {
        /// <summary>
        /// Note的类型
        /// </summary>
        public HitObjectTypes HitObjectType { get; } = HitObjectTypes.TaikoBlueHit;
        /// <summary>
        /// Note相对于开始的位置
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// Note的音效
        /// </summary>
        public Sounds.HitSample HitSample { get; set; }
        /// <summary>
        /// Note音效的类型
        /// </summary>
        public HitSounds HitSound { get; set; } = HitSounds.Normal;
        /// <summary>
        /// Note会出现的模式
        /// </summary>
        public OsuGameMode SpecifiedMode { get; } = OsuGameMode.Taiko;
        /// <summary>
        /// Note所在的位置
        /// </summary>
        public OsuPixel Position { get;  set; }
        /// <summary>
        /// 使用字符串构造一个TaikoBlueHit
        /// </summary>
        /// <param name="data"></param>
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
        /// 获取osu文件中描述格式的字符串
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