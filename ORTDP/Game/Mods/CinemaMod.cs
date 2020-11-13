using osuTools.Beatmaps;
using osuTools.Beatmaps.HitObject;

namespace osuTools.Game.Mods
{
    public class CinemaMod : Mod, ILegacyMod, IHasConflictMods
    {
        public override bool IsRankedMod { get; protected set; } = false;
        public override string Name { get; protected set; } = "Cinema";
        public override string ShortName { get; protected set; } = "CM";
        public override double ScoreMultiplier { get; protected set; } = 1.0d;
        public override ModType Type { get => ModType.Automation; }
        public OsuGameMod LegacyMod { get => OsuGameMod.Cinema; }
        public override string Description { get; protected set; } = "看不到Note的全自动游玩";
        public Mod[] ConflictMods { get => new Mod[] { new RelaxMod(), new AutoPilotMod(), new SpunOutMod(), new AutoPlayMod(), new SuddenDeathMod(), new PerfectMod() }; }

    }
}