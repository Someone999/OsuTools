using osuTools.Beatmaps;
using osuTools.Beatmaps.HitObject;

namespace osuTools.Game.Mods
{
    public class RelaxMod : Mod, ILegacyMod, IHasConflictMods
    {
        public override bool IsRankedMod { get => true; }
        public override string Name { get => "Relax";  }
        public override string ShortName { get => "Relax"; }
        public override double ScoreMultiplier { get => 0.0; }
        public OsuGameMod LegacyMod { get => OsuGameMod.Relax; }
        public override ModType Type { get => ModType.Automation; }
        public override string Description { get => "自动按键，只需要定位"; }
        public Mod[] ConflictMods { get => new Mod[] { new AutoPilotMod(), new SpunOutMod(), new AutoPlayMod(), new CinemaMod(), new SuddenDeathMod(), new PerfectMod(), new NoFailMod() }; }
    }
}