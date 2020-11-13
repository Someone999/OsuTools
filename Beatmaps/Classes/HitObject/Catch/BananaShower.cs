using System.Collections.Generic;
using System;
using System.Linq;

namespace osuTools.Beatmaps.HitObject
{


    public class BananaShower : IHitObject,INoteGrouped,IHasEndHitObject
    {
        public  bool IsNewGroup { get; set; }
        public HitObjectTypes HitObjectType { get; } = HitObjectTypes.BananaShower;
        public int Offset { get; set; } = -1;
        public OsuPixel Position { get; } = new OsuPixel(256, 192);
        public HitSounds HitSound { get;  set; } = HitSounds.Normal;
        public Sounds.HitSample HitSample { get; set; }=new Sounds.HitSample();
        public OsuGameMode SpecifiedMode { get; } = OsuGameMode.Catch;
        public int EndTime { get; internal set; }
        int type = 0;
        string hitsample;

        public void Parse(string data)
        {
            var info = data.Split(',');
            Offset = int.Parse(info[2]);
            type = int.Parse(info[3]);
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
                if(info.Length > 6)
                HitSample = new Sounds.HitSample(info[6]);
            }
        }
        public string ToOsuFormat()
        {
            return $"256,192,{Offset},{type},{1<<(int)HitSound},{EndTime},{hitsample}";
        }
        public override string ToString()
        {
            return $"Type:{HitObjectType} Offset:{Offset}";
        }
    }
}