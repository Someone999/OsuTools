﻿namespace osuTools.Beatmaps.HitObject
{
    /// <summary>
    /// 表示一个水果
    /// </summary>
    public class Fruit : IHitObject,INoteGrouped
    {
        public bool IsNewGroup { get; set; }
        /// <summary>
        /// 打击物件的类型
        /// </summary>
        public HitObjectTypes HitObjectType { get; } = HitObjectTypes.Fruit;
        /// <summary>
        ///相对于开始的偏移
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public OsuPixel Position { get; set; } = new OsuPixel(256, 192);
        /// <summary>
        /// 音效类型
        /// </summary>
        public HitSounds HitSound { get; set; }
        /// <summary>
        /// 音效
        /// </summary>
        public Sounds.HitSample HitSample { get; set; }=new Sounds.HitSample();
        /// <summary>
        /// 会出现的模式
        /// </summary>
        public OsuGameMode SpecifiedMode { get; } = OsuGameMode.Catch;
        int type;
        /// <summary>
        /// 将字符串解析成Fruit
        /// </summary>
        /// <param name="data"></param>
        public void Parse(string data)
        {
            string[] info = data.Split(',');
            Position = new OsuPixel(int.Parse(info[0]),int.Parse(info[1]));
            Offset = int.Parse(info[2]);
            type = int.Parse(info[3]);
            var types = HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(type);
            if (!types.Contains(HitObjectTypes.HitCircle))
            {
                throw new System.ArgumentException("该行的数据不适用。");
            }
            else
            {
                if (types.Contains(HitObjectTypes.NewCombo))
                    IsNewGroup = true;
                HitSound = HitObjectTools.GetGenericTypesByInt<HitSounds>(int.Parse(info[4]))[0];
                if (info.Length > 5)
                    HitSample = new Sounds.HitSample(info[5]);
            }
        }
        /// <summary>
        /// 返回一个以osu文件的格式为标准的字符串
        /// </summary>
        /// <returns></returns>
        public string ToOsuFormat()
        {
            return $"{Position.x},{Position.y},{Offset},{type},{1<<(int)HitSound},{HitSample.GetData()}";
        }
        public override string ToString()
        {
            return $"Type:{HitObjectType} Offset:{Offset}";
        }
    }
}