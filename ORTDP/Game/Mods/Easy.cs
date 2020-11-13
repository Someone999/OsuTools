using osuTools.Beatmaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osuTools.Game.Mods
{
    public class EasyMod:Mod, ILegacyMod, IHasConflictMods
    {
        public override bool IsRankedMod { get; protected set; } = true;
        public override string Name { get => "Easy"; }
        public override string ShortName { get => "EZ"; }
        public override double ScoreMultiplier { get => 0.5;}
        public override ModType Type { get => ModType.DifficultyReduction;  }
        public OsuGameMod LegacyMod { get=>OsuGameMod.Easy; }
        public override string Description { get => "所有的难度参数都降低一点，并有3次满血复活的机会";  }
        public Mod[] ConflictMods { get => new Mod[] { new HardRockMod() }; }
        public override Beatmap Apply(Beatmap beatmap)
        {
            beatmap.AR /= 2;
            beatmap.HP /= 2;
            beatmap.OD /= 2;
            if (beatmap.Mode == OsuGameMode.Osu || beatmap.Mode == OsuGameMode.Catch)
                beatmap.CS /= 2;
            return beatmap;
        }

    }
}
