using osuTools;
using osuTools.Beatmaps;
using osuTools.Beatmaps.HitObject;
using osuTools.Game.Modes;
using osuTools.Game.Mods;
using osuTools.OsuDB;
using osuTools.osuToolsException;
using System.Linq;
using System.Windows.Forms;
using System;

namespace Osu.Game.Modes
{
    public class CatchMode : GameMode, ILegacyMode
    {
        public OsuGameMode LegacyMode { get => OsuGameMode.Catch; }
        public override string ModeName { get => "Catch"; }
        public override Mod[] AvaliableMods { get => Mod.CatchMods; }
        public override string Description { get => "接水果"; }
        public override double AccuracyCalc(ScoreInfo scoreInfo)
        {
            double c300 = scoreInfo.c300;
            double c200 = scoreInfo.c200;
            double c50 = scoreInfo.c50;
            double c100 = scoreInfo.c100;
            double cMiss = scoreInfo.cMiss;
            double rawValue = (c300 + c100 + c50) / (c300 + c100 + c200 + c50 + cMiss);
            return double.IsNaN(rawValue) ? 0 : double.IsInfinity(rawValue) ? 0 : rawValue;
        }
        public override double AccuracyCalc(ORTDP scoreInfo)
        {
            if (scoreInfo is null) return 0;
            double c300 = scoreInfo.c300;
            double c200 = scoreInfo.c200;
            double c50 = scoreInfo.c50;
            double c100 = scoreInfo.c100;
            double cMiss = scoreInfo.cMiss;

            double rawValue = (c300 + c100 + c50) / (c300 + c100 + c200 + c50 + cMiss);
            return double.IsNaN(rawValue) ? 0 : double.IsInfinity(rawValue) ? 0 : rawValue;
        }
        public override int GetBeatmapHitObjectCount(Beatmap b)
        {
            if (b == null) return 0;
            var hitObjects = b.HitObjects;
            var fruits = hitObjects.Where((h) => h.HitObjectType == HitObjectTypes.Fruit);
            var juice = hitObjects.Where((h) => h.HitObjectType == HitObjectTypes.JuiceStream);
            var bananaShower = hitObjects.Where((h) => h.HitObjectType == HitObjectTypes.BananaShower);
            return hitObjects.Count + juice.Count() - bananaShower.Count();
        }
        public override bool IsPerfect(ORTDP info) => info.Accuracy >= 100;
        public override int GetPassedHitObjectCount(ORTDP i)
        {
            if (i is null) return 0;
            return i.c300;
        }
        public override double GetC300Rate(ORTDP info)
        {
            if (info is null) return 0;
            return AccuracyCalc(info);
        }
        public override double GetC300gRate(ORTDP info)
        {
            if (info is null) return 0;
            return AccuracyCalc(info);
        }
        public override IHitObject CreateHitObject(string data)
        {
            IHitObject hitobject = null;
            var d = data.Split(',');
            var types = HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(int.Parse(d[3]));
            if(types.Contains(HitObjectTypes.HitCircle))
                hitobject = new Fruit();
            if (types.Contains(HitObjectTypes.Slider))
                hitobject = new JuiceStream();                
            if (types.Contains(HitObjectTypes.Spinner))
                hitobject = new BananaShower();
            if (hitobject == null) throw new IncorrectHitObjectException(this, types[0]);
            hitobject.Parse(data);
            return hitobject;
        }
        public override string GetRanking(ORTDP info)
        {
            if (info is null) return "???";
            bool isHDOrFL = false;
            if (!string.IsNullOrEmpty(info.ModShortNames))
                isHDOrFL = info.ModShortNames.Contains("HD") || info.ModShortNames.Contains("FL");
            string Ranking = "";
            if (info.Accuracy * 100 == 100)
            {
                Ranking = "SS";
                if (isHDOrFL)
                {
                    Ranking += "+";
                }
                return Ranking;
            }

            if (info.Accuracy * 100 > 98.01)
            {

                Ranking = "S";
                if (isHDOrFL)
                {
                    Ranking += "+";
                }
                return Ranking;
            }
            if (info.Accuracy * 100 > 94.01)
            {
                Ranking = "A"; return Ranking;
            }
            if (info.Accuracy * 100 > 90)
            {
                Ranking = "B"; return Ranking;
            }
            if (info.Accuracy * 100 > 85.01)
            {
                Ranking = "C"; return Ranking;
            }
            if (info.Accuracy * 100 < 85)
            {
                Ranking = "D"; return Ranking;
            }
            return "???";
        }       
    }

}