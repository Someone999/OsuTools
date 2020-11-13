namespace osuTools.Game.Mods
{
    public class RandomMod : Mod, ILegacyMod
    {
        public override bool IsRankedMod { get => false;}
        public override string Name { get => "Random"; }
        public override string ShortName { get => "RD"; }
        public override ModType Type { get => ModType.DifficultyIncrease; }
        public OsuGameMod LegacyMod { get => OsuGameMod.Random; }
        public override string Description { get => "随机排列Mania Note";  }

    }
}