using osuTools.Skins.Interfaces;

namespace osuTools.Game.Mods
{
    public class NoFailMod:Mod, ILegacyMod, IHasConflictMods
    {
        public override bool IsRankedMod { get => true; }
        public override string Name { get;protected set; } = "NoFail";
        public override string ShortName { get; protected set; } = "NF";
        public override double ScoreMultiplier { get => 0.5;  }
        public override ModType Type { get => ModType.DifficultyReduction;  }
        public OsuGameMod LegacyMod { get => OsuGameMod.NoFail; }
        public override string Description { get => "无论如何都不会失败"; }
        public Mod[] ConflictMods { get => new Mod[] {new SuddenDeathMod(), new PerfectMod() }; }
    }
    
}