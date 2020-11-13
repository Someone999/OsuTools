using osuTools.Beatmaps;
using osuTools.Game.Modes;

namespace osuTools.Game.Mods
{
    public class HardRockMod : Mod, ILegacyMod, IHasConflictMods
    {
        bool isRanked = true;
        double scoreMultiplier = 1.06;
        public override bool IsRankedMod { get => isRanked;  }
        public override string Name { get => "HardRock"; }
        public override string ShortName { get => "HR"; }
        public override double ScoreMultiplier { get => scoreMultiplier;}
        public override ModType Type { get => ModType.DifficultyIncrease;  }
        public OsuGameMod LegacyMod { get => OsuGameMod.HardRock; }
        public Mod[] ConflictMods { get => new Mod[] { new EasyMod() }; }
        public override string Description { get => "所有难度参数都提高一点";  }
        public override bool CheckAndSetForMode(GameMode mode)
        {
            if (mode == OsuGameMode.Mania) scoreMultiplier = 1d;
            if (mode == OsuGameMode.Catch) scoreMultiplier = 1.12d;
            return base.CheckAndSetForMode(mode) && true;
        }
        public override Beatmap Apply(Beatmap beatmap)
        {
            if (beatmap.Mode == OsuGameMode.Mania)
            {
                IsRankedMod = false;
                scoreMultiplier = 1;
            }
            beatmap.AR *= 1.4;
            beatmap.OD *= 1.4;
            beatmap.HP *= 1.4;
            if (beatmap.Mode == OsuGameMode.Osu || beatmap.Mode == OsuGameMode.Catch)
                beatmap.CS *= 1.3;
            return beatmap;
        }

    }
}