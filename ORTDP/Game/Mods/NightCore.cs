namespace osuTools.Game.Mods
{
    using osuTools.Beatmaps;
    using Beatmaps.HitObject;
    public class NightCoreMod : Mod, ILegacyMod, IHasConflictMods, IChangeTimeRateMod
    {
        public override bool IsRankedMod { get => true;}
        public override string Name { get => "NightCore"; }
        public override string ShortName { get => "NC"; }
        public override double ScoreMultiplier { get => 1.12;  }
        public override ModType Type { get => ModType.DifficultyIncrease; }
        public OsuGameMod LegacyMod { get => OsuGameMod.NightCore; }
        public Mod[] ConflictMods { get => new Mod[] { new DoubleTimeMod(), new HalfTimeMod() }; }
        public double TimeRate { get=> 1.5d; }
        public override string Description { get => "在DoubleTime的基础上加重节奏"; protected set => base.Description = value; }
        public override bool CheckAndSetForMode(Modes.GameMode mode)
        {
            if (mode == OsuGameMode.Catch) ScoreMultiplier = 1.06d;
            if (mode == OsuGameMode.Mania) ScoreMultiplier = 1d;
            return true && base.CheckAndSetForMode(mode);
        }
        public override Beatmap Apply(Beatmap beatmap)
        {
            if (beatmap.Mode == OsuGameMode.Mania)
                ScoreMultiplier = 1;
            HitObjectCollection hitObjects = beatmap.HitObjects;
            hitObjects.ForEach((hitObject) => hitObject.Offset = (int)(hitObject.Offset / 1.25d));
            beatmap.HitObjects = hitObjects;
            return beatmap;
        }

    }
}