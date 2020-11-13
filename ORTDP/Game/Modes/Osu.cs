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
    public class OsuMode : GameMode, ILegacyMode
    {
        public OsuGameMode LegacyMode { get => OsuGameMode.Osu; }
        public override string ModeName { get => "Osu"; }
        public override Mod[] AvaliableMods { get => Mod.OsuMods; }
        public override string Description { get => "戳泡泡";}
        public override double AccuracyCalc(ScoreInfo scoreInfo)
        {
            double c300 = scoreInfo.c300;
            double c100 = scoreInfo.c100;
            double c50 = scoreInfo.c50;
            double cMiss = scoreInfo.cMiss;
            double rawValue = (c300 + c100 * (1 / 3d) + c50 * (1 / 6d)) / (c300 + c100 + c50 + cMiss);
            return double.IsNaN(rawValue) ? 0 : double.IsInfinity(rawValue) ? 0 : rawValue;
        }
        public override IHitObject CreateHitObject(string data)
        {
            IHitObject hitobject = null;
            HitObjectTypes type = HitObjectTypes.Unknown;
            var d = data.Split(',');
            var types = HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(int.Parse(d[3]));
            if (types.Contains(HitObjectTypes.HitCircle))
                hitobject = new HitCircle();
            if (types.Contains(HitObjectTypes.Slider))
                hitobject = new Slider();
            if (types.Contains(HitObjectTypes.Spinner))
                hitobject = new Spinner();
            type = types[0];
            if (hitobject == null) throw new IncorrectHitObjectException(this, type);
            hitobject.Parse(data);
            return hitobject;
        }
        public override int GetBeatmapHitObjectCount(osuTools.Beatmaps.Beatmap b) => b is null ? 0 : b.HitObjects.Count;
        public override int GetPassedHitObjectCount(ORTDP info) => info is null ? 0 : info.c300 + info.c100 + info.c50 + info.cMiss;
        public override bool IsPerfect(ORTDP info) => info is null ? false : info.PlayerMaxCombo == info.MaxCombo;
        public override double AccuracyCalc(ORTDP scoreInfo)
        {
            if (scoreInfo is null) return 0;
            double c300 = scoreInfo.c300;
            double c100 = scoreInfo.c100;
            double c50 = scoreInfo.c50;
            double cMiss = scoreInfo.cMiss;
            double rawValue = (c300 + c100 * (1 / 3d) + c50 * (1 / 6d)) / (c300 + c100 + c50 + cMiss);
            return double.IsNaN(rawValue) ? 0 : double.IsInfinity(rawValue) ? 0 : rawValue;
        }
        
        public override string GetRanking(ORTDP info)
        {
            if (info is null) return "???";

            bool NoMiss = info.cMiss == 0;
            double All = info.c300 + info.c100 + info.c50 + info.cMiss;
            double c100Rate = info.c100 / All;
            double c50Rate = info.c50 / All;
            string Ranking = "";
            bool isHDOrFL = false;
            if (!string.IsNullOrEmpty(info.ModShortNames))
                isHDOrFL = info.ModShortNames.Contains("HD") || info.ModShortNames.Contains("FL");
            if (info.Accuracy * 100 == 100 && info.c300 == All)
            {
                Ranking = "SS";
                if (isHDOrFL)
                {
                    Ranking += "+";
                }
                return Ranking;
            }
            if (info.Accuracy * 100 > 93.17 && c50Rate < 0.01 && c100Rate < 0.1 && info.c300Rate > 0.9 && NoMiss)
            {
                Ranking = "S";
                if (isHDOrFL)
                {
                    Ranking += "+";
                }
                return Ranking;

            }
            if ((info.c300Rate > 0.8 && NoMiss) || ((info.c300Rate > 0.9 && !NoMiss)))
            {
                Ranking = "A";
                return Ranking;
            }
            if ((info.c300Rate > 0.8 && !NoMiss) || (info.c300Rate > 0.7 && NoMiss))
            {
                Ranking = "B";
                return Ranking;
            }
            if (info.c300Rate > 0.6)
            {
                Ranking = "C";
                return Ranking;
            }
            else
            {
                Ranking = "D";
                return Ranking;
            }
        }
        public override double GetC300gRate(ORTDP info)
        {
            double rawValue = info.c300g / (double)(info.c300g + info.c200);
            if (double.IsNaN(rawValue) || double.IsInfinity(rawValue))
                return 0;
            else
                return rawValue;
        }
        public override double GetC300Rate(ORTDP info)
        {
            double rawValue = info.c300 / (double)(info.c300 + info.c100 + info.c50 + info.cMiss);
            if (double.IsNaN(rawValue) || double.IsInfinity(rawValue))
                return 0;
            else
                return rawValue;
        }
        
    }
}