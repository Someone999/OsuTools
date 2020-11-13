using System.Collections.Generic;
using System;
namespace osuTools.Beatmaps.HitObject
{
    
    /// <summary>
    /// 表示一个圈圈
    /// </summary>
    public class HitCircle:IHitObject,INoteGrouped
    {
        public bool IsNewGroup { get; set; }
        /// <summary>
        /// 打击物件的类型
        /// </summary>
        public HitObjectTypes HitObjectType { get; } = HitObjectTypes.HitCircle;
        /// <summary>
        /// 打击物件相对于开始时的偏移
        /// </summary>
        public int Offset { get; set; } = -1;
        /// <summary>
        /// 打击物件的音效类型
        /// </summary>
        public HitSounds HitSound { get;  set; } = HitSounds.Normal;
        /// <summary>
        /// 打击物件的音效
        /// </summary>
        public Sounds.HitSample HitSample { get;  set; }=new Sounds.HitSample();
        /// <summary>
        /// 打击物件的位置
        /// </summary>
        public OsuPixel Position { get; set; }
        /// <summary>
        /// 会出现该打击物件的模式
        /// </summary>
        public OsuGameMode SpecifiedMode { get; } = OsuGameMode.Osu;
        int type;
        /// <summary>
        /// 将字符串解析成HitCircle
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
                if(info.Length>5)
                HitSample = new Sounds.HitSample(info[5]);
            }
        }
        /// <summary>
        /// 返回一个以osu格式为标准的字符串
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