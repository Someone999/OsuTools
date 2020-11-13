using osuTools.Beatmaps;
using osuTools.Beatmaps.HitObject;

namespace osuTools.Game.Mods
{
    public class AutoPlayMod : Mod, ILegacyMod, IHasConflictMods
    {
        public override bool IsRankedMod { get; protected set; }= false;
        public override string Name { get; protected set; } = "AutoPlay";
        public override string ShortName { get; protected set; } = "Auto";
        public override double ScoreMultiplier { get; protected set; } = 1.0d;
        public override ModType Type { get => ModType.Automation; }
        public OsuGameMod LegacyMod { get => OsuGameMod.AutoPlay; }
        public override string Description { get; protected set; } = "全自动游玩";
        public Mod[] ConflictMods { get => new Mod[] {new RelaxMod(), new AutoPilotMod(), new SpunOutMod(),  new CinemaMod(), new SuddenDeathMod(), new PerfectMod() }; }
       
    }
}