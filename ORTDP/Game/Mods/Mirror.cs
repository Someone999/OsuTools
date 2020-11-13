namespace osuTools.Game.Mods
{
    public class MirrorMod : Mod, ILegacyMod
    {
        public override bool IsRankedMod { get => true; }
        public override string Name { get => "Mirror";}
        public override string ShortName { get => "MR";}
        public override string Description { get => "左右翻转Mania谱面"; }
        public OsuGameMod LegacyMod { get => OsuGameMod.Mirror; }

    }
}