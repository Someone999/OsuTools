using osuTools.Game.Mods;

namespace osuTools.Game.Modes
{
    public class UnknownMode : GameMode 
    {
        public override Mod[] AvaliableMods { get => new Mod[0]; }
        public override string ModeName { get => "Unknown"; }
        public override string Description { get => "未知的模式。"; }
    }
}