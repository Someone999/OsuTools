namespace osuTools.Beatmaps
{
    using System.Collections.Generic;
    using HitObject;
    using System;
    using System.Security.Cryptography;
    using System.Text;
    partial class Beatmap
    {
        string  t = "",
                ut = "",
                a = "",
                ua = "",
                c = "",
                dif = "",
                ver = "",
                fn = "",
                fullfn = "",
                dlnk = "",
                bgf = "",
                au = "",
                sou = "",
                tag = "",
                mak = "",
                fuau = "",
                vi = "",
                fuvi = "",
                fubgf = "";
        OsuGameMode mode;
        HitObjectCollection hitObjects = null;
        BreakTimeCollection breakTimes = null;
        MD5String md = new MD5String();
        bool hasvideo = false;
        double
               od = 0,
               cs = 0,
               hp = 0,
               ar = 0,
               stars = 0;
        int m = 0;
        int id = -2048,
            setid = -2048;
        bool ModeHasSet = false;
        [NonSerialized]
        StringBuilder b = new StringBuilder();
        bool FileDataAvalable = false, FileStreamAvalable = false;
        System.IO.FileInfo info;
        [NonSerialized]
        MD5CryptoServiceProvider md5calc = new MD5CryptoServiceProvider();
        double bpm;
    }

}