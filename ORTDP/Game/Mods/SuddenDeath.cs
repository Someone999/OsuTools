namespace osuTools.Game.Mods
{
    public class SuddenDeathMod : Mod, ILegacyMod, IHasConflictMods
    {
        public override bool IsRankedMod { get => true; protected set => IsRankedMod = value; }
        public override string Name { get => "SuddenDeath"; protected set => Name = value; }
        public override string ShortName { get => "SD"; protected set => ShortName = value; }
        public override double ScoreMultiplier { get => 0.3; protected set => ScoreMultiplier = value; }
        public override ModType Type { get => ModType.DifficultyIncrease; protected set => Type = value; }
        public OsuGameMod LegacyMod { get => OsuGameMod.SuddenDeath; }
        /// <summary>
        /// 与这个Mod相冲突的Mod
        /// </summary>
        public Mod[] ConflictMods { get => new Mod[] { new PerfectMod(), new NoFailMod() }; }

    }
}