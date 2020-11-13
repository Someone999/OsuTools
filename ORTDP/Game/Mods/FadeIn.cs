namespace osuTools.Game.Mods
{
    public class FadeInMod : Mod, ILegacyMod, IHasConflictMods
    {
        public override bool IsRankedMod { get => true;  }
        public override string Name { get => "FadeIn"; }
        public override string ShortName { get => "FI"; }
        public override double ScoreMultiplier { get => 1d; }
        public override ModType Type { get => ModType.DifficultyIncrease;  }
        public OsuGameMod LegacyMod { get => OsuGameMod.FadeIn; }
        public override string Description { get => "上隐"; }
        public Mod[] ConflictMods { get => new Mod[] { new HiddenMod() }; }

    }
}