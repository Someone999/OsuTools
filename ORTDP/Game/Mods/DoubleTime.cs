using osuTools.Beatmaps;
using osuTools.Beatmaps.HitObject;
using osuTools.Game.Modes;

namespace osuTools.Game.Mods
{
    public class DoubleTimeMod : Mod, ILegacyMod, IHasConflictMods, IChangeTimeRateMod
    {
        public override string Name { get; protected set; } = "DoubleTime";
        public override string ShortName { get; protected set; } = "DT";
        public override double ScoreMultiplier { get; protected set; } = 1.12d;
        public override ModType Type { get => ModType.DifficultyIncrease; }
        public OsuGameMod LegacyMod { get => OsuGameMod.DoubleTime; }
        public Mod[] ConflictMods { get => new Mod[] { new NightCoreMod(), new HalfTimeMod() }; }
        public override string Description { get; protected set; }= "1.5倍速";
        public double TimeRate { get => 1.5d; }
        public override bool CheckAndSetForMode(GameMode mode)
        {
            if (mode == OsuGameMode.Catch) ScoreMultiplier = 1.06d;
            if (mode == OsuGameMode.Mania) ScoreMultiplier = 1d;
            return true && base.CheckAndSetForMode(mode);
        }
        public override Beatmap Apply(Beatmap beatmap)
        {
            if (beatmap.Mode == OsuGameMode.Mania)
                ScoreMultiplier = 1d;
            HitObjectCollection hitObjects = beatmap.HitObjects;
            hitObjects.ForEach((hitObject) => hitObject.Offset = (int)(hitObject.Offset / 1.25d));
            beatmap.HitObjects = hitObjects;
            return beatmap;
        }
    }
}