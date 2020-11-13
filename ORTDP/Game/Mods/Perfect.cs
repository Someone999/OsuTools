namespace osuTools.Game.Mods
{
    public class PerfectMod : Mod, ILegacyMod, IHasConflictMods
    {
        public override bool IsRankedMod { get => true; }
        public override string Name { get => "Perfect"; }
        public override string ShortName { get => "PF"; }
        public override ModType Type { get => ModType.DifficultyIncrease;  }
        public OsuGameMod LegacyMod { get => OsuGameMod.Perfect; }
        public override string Description { get => "感受痛苦吧";  }
        public Mod[] ConflictMods { get => new Mod[] { new SuddenDeathMod(),new NoFailMod() }; }

    }
}