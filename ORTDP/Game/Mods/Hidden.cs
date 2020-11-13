using osuTools.Beatmaps;

namespace osuTools.Game.Mods
{
    public class HiddenMod : Mod, ILegacyMod, IHasConflictMods
    {
        double scoreMultiplier = 1.06;
        public override bool IsRankedMod { get => true;  }
        public override string Name { get => "Hidden"; }
        public override string ShortName { get => "HD";}
        public override double ScoreMultiplier { get => scoreMultiplier; protected set => scoreMultiplier = value; }
        public override ModType Type { get => ModType.DifficultyIncrease; }
        public OsuGameMod LegacyMod { get => OsuGameMod.Hidden; }
        public override string Description { get => "渐隐"; }
        public Mod[] ConflictMods { get => new Mod[] { new FadeInMod() }; }
        public override Beatmap Apply(Beatmap beatmap)
        {
            if (beatmap.Mode == OsuGameMode.Mania)
                scoreMultiplier = 1;
            return beatmap;
        }

    }
}