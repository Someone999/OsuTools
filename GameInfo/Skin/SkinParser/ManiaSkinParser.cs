﻿using osuTools.Skins.SkinObjects.Mania;
using System.IO;

namespace osuTools.Skins
{
    public partial class Skin
    {
        void getManiaSkinImages()
        {
            #region ManiaHitBurstImages
            var dir = Path.GetDirectoryName(ConfigFileDirectory);
            var lst = Tools.SkinTools.GetMultipleFileSkinObject(files, "mania-hit300g");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.ManiaHitBurstImages.Hit300g.Add(new ManiaSkinImage(this, Path.GetFileName(file), "HitBurst"));

            lst = Tools.SkinTools.GetMultipleFileSkinObject(files, "mania-hit300");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.ManiaHitBurstImages.Hit300.Add(new ManiaSkinImage(this, Path.GetFileName(file), "HitBurst"));

            lst = Tools.SkinTools.GetMultipleFileSkinObject(files, "mania-hit200");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.ManiaHitBurstImages.Hit200.Add(new ManiaSkinImage(this, Path.GetFileName(file), "HitBurst"));

            lst = Tools.SkinTools.GetMultipleFileSkinObject(files, "mania-hit100");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.ManiaHitBurstImages.Hit100.Add(new ManiaSkinImage(this, Path.GetFileName(file), "HitBurst"));

            lst = Tools.SkinTools.GetMultipleFileSkinObject(files, "mania-hit50");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.ManiaHitBurstImages.Hit50.Add(new ManiaSkinImage(this, Path.GetFileName(file), "HitBurst"));

            lst = Tools.SkinTools.GetMultipleFileSkinObject(files, "mania-hit0");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.ManiaHitBurstImages.Hit0.Add(new ManiaSkinImage(this, Path.GetFileName(file), "HitBurst"));
            lst = Tools.SkinTools.GetMultipleFileSkinObject(files, "comboburst-mania-");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.ManiaComboBurstImages.ComboBurstImages.Add(new ManiaSkinImage(this, Path.GetFileName(file), "ComboBurst"));
            #endregion
        }
    }
}