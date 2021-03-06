﻿using osuTools.Beatmaps;
using osuTools.Game.Modes;

namespace osuTools.Game.Mods
{
    public class FlashlightMod : Mod, ILegacyMod, IHasConflictMods
    {
        double scoreMultiplier = 1.12;
        public override bool IsRankedMod { get => true;}
        public override string Name { get => "Flashlight"; }
        public override string ShortName { get => "FL"; }
        public override double ScoreMultiplier { get => scoreMultiplier; protected set => scoreMultiplier = value; }
        public override ModType Type { get => ModType.DifficultyIncrease;}
        public OsuGameMod LegacyMod { get => OsuGameMod.Flashlight; }
        Mod[] confictMods=new Mod[0];
        public Mod[] ConflictMods { get => confictMods; set => confictMods = value; }
        public override string Description { get => "极限视野"; protected set { } }
        public override bool CheckAndSetForMode(GameMode mode)
        {
           if(mode == OsuGameMode.Mania)
            {
                scoreMultiplier = 1d;
                confictMods = new Mod[] { new HiddenMod(), new FadeInMod() };                
            }
           if(mode==OsuGameMode.Catch)
            {
                scoreMultiplier = 1.06d;
            }
            return base.CheckAndSetForMode(mode) && true;
        }
        public override Beatmap Apply(Beatmap beatmap)
        {
            CheckAndSetForMode(GameMode.FromLegacyMode(beatmap.Mode));
            return beatmap;
        }

    }
}