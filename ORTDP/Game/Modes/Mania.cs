using osuTools;
using osuTools.Beatmaps;
using osuTools.Beatmaps.HitObject;
using osuTools.Game.Modes;
using osuTools.Game.Mods;
using osuTools.OsuDB;
using osuTools.osuToolsException;
using System.Windows.Forms;

namespace Osu.Game.Modes
{
    public class ManiaMode : GameMode, ILegacyMode
    {
        public OsuGameMode LegacyMode { get => OsuGameMode.Mania; }
        public override string ModeName { get => "Mania"; }
        public override Mod[] AvaliableMods { get => Mod.ManiaMods; }
        public override string Description { get => "砸键盘";  }
        public override double AccuracyCalc(ScoreInfo scoreInfo)
        {
            double c300g = scoreInfo.c300g;
            double c300 = scoreInfo.c300;
            double c200 = scoreInfo.c200;
            double c50 = scoreInfo.c50;
            double c100 = scoreInfo.c100;
            double cMiss = scoreInfo.cMiss;
            double rawValue = (c300g + c300 + c200 * (2 / 3.0) + c100 * (1 / 3.0) + c50 * (1 / 6.0)) / (c300g + c300 + c100 + c200 + c50 + cMiss);
            return double.IsNaN(rawValue) ? 0 : double.IsInfinity(rawValue) ? 0 : rawValue;
        }
        public override double AccuracyCalc(ORTDP scoreInfo)
        {
            double c300g = scoreInfo.c300g;
            double c300 = scoreInfo.c300;
            double c200 = scoreInfo.c200;
            double c50 = scoreInfo.c50;
            double c100 = scoreInfo.c100;
            double cMiss = scoreInfo.cMiss;
            double rawValue = (c300g + c300 + c200 * (2 / 3.0) + c100 * (1.0 / 3) + c50 * (1 / 6.0)) / (c300g + c300 + c100 + c200 + c50 + cMiss);
            return double.IsNaN(rawValue) ? 0 : double.IsInfinity(rawValue) ? 0 : rawValue;
        }
        public override int GetPassedHitObjectCount(ORTDP info)
        {
             if (info is null) return 0;
             return info.c300g + info.c300 + info.c200 + info.c100 + info.c50 + info.cMiss;
        }
        public override int GetBeatmapHitObjectCount(Beatmap b)
        {
            if (b is null) return 0;
            return b.HitObjects.Count;
        }
        public override double GetC300gRate(ORTDP info)
        {
            if (info is null) return 0;
            double rawValue = info.c300g / (double)(info.c300 + info.c300g);
            if (double.IsNaN(rawValue) || double.IsInfinity(rawValue))
                return 0;
            else
                return rawValue;
        }
        public override double GetC300Rate(ORTDP info)
        {
            if (info is null) return 0;
            double rawValue = 0;
            if (info.c300g > 0 && info.c300 == 0)
                rawValue = GetC300gRate(info);
            else
                rawValue = (info.c300 + info.c300g) / (double)(info.c300g + info.c300 + info.c200 + info.c100 + info.c50 + info.cMiss);
            if (double.IsNaN(rawValue) || double.IsInfinity(rawValue))
                return 0;
            else
                return rawValue;
        }
        public override string GetRanking(ORTDP info)
        {
            if (info is null) return "???";
            bool isHDOrFL = false;
            if(!string.IsNullOrEmpty(info.ModShortNames))
            isHDOrFL = info.ModShortNames.Contains("HD") || info.ModShortNames.Contains("FL");
            return info.Accuracy * 100 == 100 ? isHDOrFL ? "SS+" : "SS" :
                   info.Accuracy * 100 > 95 ? isHDOrFL ? "S+" : "S" :
                   info.Accuracy * 100 > 90 ? "A" :
                   info.Accuracy * 100 > 80 ? "B" :
                   info.Accuracy * 100 > 70 ? "C" :
                   "D";
        }
        public override IHitObject CreateHitObject(string data, int maniaColumn)
        {
            var d = data.Split(',');
            var types = HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(int.Parse(d[3]));
            IHitObject hitobject = null;
            if (types.Contains(HitObjectTypes.HitCircle))
                hitobject = new ManiaHit();
            if (types.Contains(HitObjectTypes.ManiaHold))
                hitobject = new ManiaHold();
            if (hitobject == null)
                throw new IncorrectHitObjectException(this, types[0]);
            (hitobject as IManiaHitObject).BeatmapColumn = maniaColumn;
            hitobject.Parse(data);
            return hitobject;            
        }
        public override bool IsPerfect(ORTDP info)
        {
            if (info is null) return false;
            return info.c100 + info.c50 + info.cMiss == 0;
        } 
    }
}