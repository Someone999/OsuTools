using System.Collections.Generic;
using System;
namespace osuTools.Beatmaps.HitObject
{
    /// <summary>
    /// 表示一个转盘
    /// </summary>
    public class Spinner : IHitObject, INoteGrouped
    {
        public bool IsNewGroup { get; set; }
        /// <summary>
        /// Note的类型
        /// </summary>
        public HitObjectTypes HitObjectType { get; } = HitObjectTypes.Spinner;
        /// <summary>
        /// Note相对于开始的位置
        /// </summary>
        public int Offset { get; set; } = -1;
        /// <summary>
        /// Note的音效类型
        /// </summary>
        public HitSounds HitSound { get; set; } = HitSounds.Normal;
        /// <summary>
        /// Note的音效
        /// </summary>
        public Sounds.HitSample HitSample { get; set; }=new Sounds.HitSample();
        /// <summary>
        /// Note的位置
        /// </summary>
        public OsuPixel Position { get; } = new OsuPixel(256, 192);
        /// <summary>
        /// Note会出现的模式
        /// </summary>
        public OsuGameMode SpecifiedMode { get; } = OsuGameMode.Osu;
        /// <summary>
        /// Spinner的结束时间
        /// </summary>
        public int EndTime { get; set; }
        string type;
        /// <summary>
        /// 使用字符串构造一个Spinner对象
        /// </summary>
        /// <param name="data"></param>
        public void Parse(string data)//(x,y)_,time,type,hitSound,endTime,hitSample
        {
            var info = data.Split(',');
            Offset = int.Parse(info[2]);
            this.type = info[3];
            int type = int.Parse(info[3]);
            var types = HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(type);
            if (!types.Contains(HitObjectTypes.Spinner))
            {
                throw new ArgumentException("该行的数据不适用。");
            }
            else
            {
                if (types.Contains(HitObjectTypes.NewCombo))
                    IsNewGroup = true;
                HitSound = HitObjectTools.GetGenericTypesByInt<HitSounds>(int.Parse(info[4]))[0];
                EndTime = int.Parse(info[5]);
                if (info.Length>6)
                HitSample = new Sounds.HitSample(info[6]);
            }
        }
        /// <summary>
        /// 获取以osu文件中的描述为格式的字符串
        /// </summary>
        /// <returns></returns>
        public string ToOsuFormat()
        {

            return $"256,192,{Offset},{type},{1<<(int)HitSound},{EndTime},{HitSample.GetData()}";
        }
        public override string ToString()
        {
            return $"Type:{HitObjectType} Offset:{Offset}";
        }
    }

}