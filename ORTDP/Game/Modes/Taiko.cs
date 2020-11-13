using osuTools;
using osuTools.Beatmaps;
using osuTools.Beatmaps.HitObject;
using osuTools.Game.Modes;
using osuTools.Game.Mods;
using osuTools.OsuDB;
using osuTools.osuToolsException;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Osu.Game.Modes
{
    public class TaikoMode : GameMode, ILegacyMode
    {
        public OsuGameMode LegacyMode { get => OsuGameMode.Taiko; }
        public override string ModeName { get => "Taiko"; }
        public override Mod[] AvaliableMods { get => Mod.TaikoMods; }
        public override string Description { get => "打鼓";}
        public override double AccuracyCalc(ScoreInfo scoreInfo)
        {
            double c300 = scoreInfo.c300;
            double c100 = scoreInfo.c100;
            double cMiss = scoreInfo.cMiss;
            double rawValue = (c300 + c100 * 0.5d) / (c300 + c100 + cMiss);
            return double.IsNaN(rawValue) ? 0 : double.IsInfinity(rawValue) ? 0 : rawValue;
        }
        public override double AccuracyCalc(ORTDP scoreInfo)
        {
            double c300 = scoreInfo.c300;
            double c100 = scoreInfo.c100;
            double cMiss = scoreInfo.cMiss;
            double rawValue = (c300 + c100 * 0.5d ) / (c300 + c100 + cMiss);
            return double.IsNaN(rawValue) ? 0 : double.IsInfinity(rawValue) ? 0 : rawValue;
        }
        public override bool IsPerfect(ORTDP info) => info.cMiss <= 0;
        public override int GetBeatmapHitObjectCount(Beatmap b)
        {
            if (b is null) return 0;
            var hitObjects = b.HitObjects;
            int normalHit = hitObjects.Where((h) => h.HitObjectType != HitObjectTypes.DrumRoll).Count();
            return normalHit;
        }
        public override double GetC300gRate(ORTDP info)
        {
            if (info is null) return 0;
            return GetC300Rate(info);
        }
        public override double GetC300Rate(ORTDP info)
        {
            if (info is null) return 0;
            return (double)(info.c300) / (info.c300 + info.c100 + info.cMiss);
        }
        
        public override string GetRanking(ORTDP info)
        {
            if (info is null) return "???";
            bool NoMiss = info.cMiss == 0;
            double All = info.c300 + info.c100 + info.c50 + info.cMiss;
            double c100Rate = info.c100 / All;
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
            if (info.Accuracy * 100 > 93.17 && c100Rate < 0.1 && info.c300Rate > 0.9 && NoMiss)
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
        public override IHitObject CreateHitObject(string data)
        {
            
            IHitObject hitobject = null;
            var d = data.Split(',');
            var types = HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(int.Parse(d[3]));
            var hitSounds= HitObjectTools.GetGenericTypesByInt<HitSounds>(int.Parse(d[4]));
            if (types.Contains(HitObjectTypes.Slider) || types.Contains(HitObjectTypes.Spinner))
                hitobject = new DrumRoll();
            if(types.Contains(HitObjectTypes.HitCircle))
            {
                if (hitSounds.Contains(HitSounds.Finish))
                    hitobject = new LargeTaikoRedHit();
                if (hitSounds.Contains(HitSounds.Normal))
                    if (hitSounds.Contains(HitSounds.Finish))
                        hitobject = new LargeTaikoRedHit();
                    else
                        hitobject = new TaikoRedHit();
                if (hitSounds.Contains(HitSounds.Whistle) || hitSounds.Contains(HitSounds.Clap))
                    if (hitSounds.Contains(HitSounds.Finish))
                        hitobject = new LargeTaikoBlueHit();
                    else
                        hitobject = new TaikoBlueHit();
            }

            if (hitobject == null)
            {
                StringBuilder builder = new StringBuilder("HitObject类型:");
                for (int i = 0; i < types.Count; i++)
                {
                    builder.Append(types[i]);
                    if (i != types.Count - 1)
                        builder.Append(", ");
                }
                builder.Append("  HitSounds:");
                for (int i = 0; i < hitSounds.Count; i++)
                {
                    builder.Append(hitSounds[i]);
                    if (i != hitSounds.Count - 1)
                        builder.Append(", ");
                }
                throw new IncorrectHitObjectException(this, types[0], builder.ToString());
            }
            hitobject.Parse(data);
            return hitobject;
        }
        public override int GetPassedHitObjectCount(ORTDP info)
        {
            return info.c300 + info.c100 + info.cMiss;
        }
    }
}