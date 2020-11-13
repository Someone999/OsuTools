using osuTools.Beatmaps;

namespace osuTools.Game.Mods
{
    public class HalfTimeMod : Mod, ILegacyMod, IChangeTimeRateMod
    {
        public override bool IsRankedMod { get => true; }
        public override string Name { get => "HalfTime"; }
        public override string ShortName { get => "HT";  }
        public override double ScoreMultiplier { get => 0.3; }
        public override ModType Type { get => ModType.DifficultyReduction; }
        public OsuGameMod LegacyMod { get => OsuGameMod.HalfTime; }
        public override string Description { get => "0.75倍速";}
        public Mod[] ConflictMods { get => new Mod[] { new DoubleTimeMod(), new NightCoreMod() }; }
        public double TimeRate { get => 0.75d; }
        public override Beatmap Apply(Beatmap beatmap)
        {
            beatmap.HitObjects.ForEach((h) => h.Offset=(int)(h.Offset / 0.75));
            return beatmap;

        }
    }
}