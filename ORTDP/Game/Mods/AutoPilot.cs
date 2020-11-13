using osuTools.Beatmaps;
using osuTools.Beatmaps.HitObject;

namespace osuTools.Game.Mods
{
    public class AutoPilotMod : Mod, ILegacyMod, IHasConflictMods
    {

        public override bool IsRankedMod { get; protected set; } = false;
        public override string Name { get; protected set; } = "AutoPilot";
        public override string ShortName { get; protected set; } = "AP";
        public override double ScoreMultiplier { get; protected set; } = 0d;
        public OsuGameMod LegacyMod { get => OsuGameMod.AutoPilot; }
        public override string Description { get; protected set; }= "光标会自动移动，只需要按键";
        public override ModType Type { get => ModType.Automation; }

        public Mod[] ConflictMods { get => new Mod[] {new RelaxMod(),new SpunOutMod(),new AutoPlayMod(),new CinemaMod(),new SuddenDeathMod(),new PerfectMod(),new NoFailMod() }; }
    }
}