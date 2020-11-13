namespace osuTools.Beatmaps.HitObject
{
    using Sounds;
    using System.Collections.Generic;
    using System.Text;
    /// <summary>
    /// 表示CTB中一个果汁流
    /// </summary>
    public class JuiceStream : IHitObject,INoteGrouped
    {
        public bool IsNewGroup { get; set; }
        /// <summary>
        /// 打击物件的类型
        /// </summary>      
        public HitObjectTypes HitObjectType { get; } = HitObjectTypes.JuiceStream;
        /// <summary>
        /// 打击物件的位置
        /// </summary>
        public OsuPixel Position { get; set; }
        /// <summary>
        /// 打击物件相对于开始的偏移
        /// </summary>
        public int Offset { get; set; } = -1;
        /// <summary>
        /// 音效类型
        /// </summary>
        public HitSounds HitSound { get; set; } = HitSounds.Normal;
        /// <summary>
        /// 音效
        /// </summary>
        public Sounds.HitSample HitSample { get; set; } = new Sounds.HitSample();
        /// <summary>
        /// 接住头部时播放的音效
        /// </summary>
        public SliderHitSound StartingHitSound { get; set; } = new SliderHitSound();
        /// <summary>
        /// 中途播放的音效
        /// </summary>
        public SliderHitSound DuringHitSound { get; set; } = new SliderHitSound();
        /// <summary>
        /// 接住尾部时播放的音效
        /// </summary>
        public SliderHitSound EndingHitSound { get; set; } = new SliderHitSound();
        List<OsuPixel> curvePoints { get; set; } = new List<OsuPixel>();
        /// <summary>
        /// 绘制曲线所用到的点
        /// </summary>
        public IReadOnlyList<OsuPixel> CurvePoints { get => curvePoints.AsReadOnly(); }
        /// <summary>
        /// 曲线的类型
        /// </summary>
        public CurveTypes CurveType { get; set; }
        /// <summary>
        /// 此属性对JuiceStream无效
        /// </summary>
        public int RepeatTime { get; set; }
        /// <summary>
        /// 此属性对JuiceStream无效
        /// </summary>
        public double Length { get; set; }
        /// <summary>
        /// 会出现该打击物件的模式
        /// </summary>
        public OsuGameMode SpecifiedMode { get; } = OsuGameMode.Catch;
        string curvetype;
        /// <summary>
        /// 将字符串解析为JuiceStream对象
        /// </summary>
        /// <param name="data"></param>
        public void Parse(string data)
        {
            var info = data.Split(',');
            Position = new OsuPixel(int.Parse(info[0]), int.Parse(info[1]));
            Offset = int.Parse(info[2]);
            int type = int.Parse(info[3]);
            var types = HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(type);
            if (!types.Contains(HitObjectTypes.Slider))
            {
                throw new System.ArgumentException("该行的数据不适用。");
            }
            else
            {
                if (types.Contains(HitObjectTypes.NewCombo))
                    IsNewGroup = true;
                HitSound = HitObjectTools.GetGenericTypesByInt<HitSounds>(int.Parse(info[4]))[0];
                var sliderinfo = info[5];
                var typeAndPoint = sliderinfo.Split('|');
                curvetype = typeAndPoint[0];
                CurveType = Slider.GetCurveTypeByString(curvetype);
                for (int i = 1; i < typeAndPoint.Length; i++)
                {
                    var point = typeAndPoint[i].Split(':');
                    if (point.Length == 2)
                    {
                        int x = int.Parse(point[0]);
                        int y = int.Parse(point[1]);
                        curvePoints.Add(new OsuPixel(x, y));
                    }
                }
                RepeatTime = int.Parse(info[6]);
                Length = double.Parse(info[7]);
                if (info.Length > 8)
                {
                    List<SampleSets> sampleSets = new List<SampleSets>();
                    List<SampleSets> additionSampleSets = new List<SampleSets>();
                    List<HitSounds> hitSounds = new List<HitSounds>();
                    var hitSoundstrs = info[8].Split('|');
                    foreach (var str in hitSoundstrs)
                    {
                        hitSounds.Add(HitObjectTools.GetGenericTypesByInt<HitSounds>(int.Parse(str))[0]);
                    }
                    if (hitSoundstrs.Length > 0)
                        StartingHitSound = new SliderHitSound(hitSounds[0]);
                    if (hitSoundstrs.Length > 1)
                        DuringHitSound = new SliderHitSound(hitSounds[1]);
                    if (hitSoundstrs.Length > 2)
                        EndingHitSound = new SliderHitSound(hitSounds[2]);
                    if (info.Length > 9)
                    {
                        var sampleSetstrs = info[9].Split('|');
                        for (int i = 0; i < sampleSetstrs.Length; i++)
                        {
                            var samples = sampleSetstrs[i].Split(':');
                            var sampleSet = int.Parse(samples[0]);
                            var addionSampleSet = int.Parse(samples[1]);
                            sampleSets.Add((SampleSets)sampleSet);
                            additionSampleSets.Add((SampleSets)addionSampleSet);
                        }
                        if (sampleSets.Count > 1)
                            StartingHitSound = new SliderHitSound(hitSounds[0], new EdgeSound(sampleSets[0], additionSampleSets[0]));
                        if (sampleSets.Count > 2)
                            DuringHitSound = new SliderHitSound(hitSounds[1], new EdgeSound(sampleSets[1], additionSampleSets[1]));
                        if (sampleSets.Count > 3)
                            EndingHitSound = new SliderHitSound(hitSounds[2], new EdgeSound(sampleSets[2], additionSampleSets[2]));
                    }
                    if (info.Length > 10)
                        HitSample = new HitSample(info[10]);
                }
            }          
        }
        /// <summary>
        /// 返回一个以osu文件中格式为标准的字符串
        /// </summary>
        /// <returns></returns>
        public string ToOsuFormat()
        {
            StringBuilder b=new StringBuilder($"{Position.x},192,{Offset},{2},{curvetype}");
            for(int i=0;i<curvePoints.Count;i++)
            {
                if (curvePoints.Count == 1)
                {
                    b.Append("|" + curvePoints[i].GetData() + ",");
                    break;
                }
                if (i == curvePoints.Count - 1)
                {
                    b.Append("|" + curvePoints[i].GetData() + ",");
                }
                else
                    b.Append($"|{curvePoints[i].GetData()}");
                
            }
            b.Append($"{RepeatTime},{Length},{1<<(int)StartingHitSound.HitSound}|{1<<(int)DuringHitSound.HitSound}|{1<<(int)EndingHitSound.HitSound},");
            b.Append($"{StartingHitSound.Sound.GetData()}|{DuringHitSound.Sound.GetData()}|{EndingHitSound.Sound.GetData()},");
            b.Append($"{HitSample}");
            return b.ToString() ;
        }
        public override string ToString()
        {
            return $"Type:{HitObjectType} Offset:{Offset}";
        }
    }
}