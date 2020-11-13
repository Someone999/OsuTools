using osuTools.Beatmaps;
using osuTools.Game.Modes;

namespace osuTools.Game.Mods
{
    public class ScoreV2Mod : Mod, ILegacyMod
    {
        public override bool IsRankedMod { get => true;  }
        public override string Name { get => "ScoreV2";}
        public override string ShortName { get => "V2";  }
        public override double ScoreMultiplier { get => 1;  }
        public override ModType Type { get => ModType.Fun; }
        public override string Description { get => "新版的计分方式";  }
        public OsuGameMod LegacyMod { get => OsuGameMod.ScoreV2; }
    }
}