namespace osuTools.Beatmaps
{
    using osuTools.ExtraMethods;
    partial class Beatmap
    {
        /// <summary>
        /// 通过osu文件路径来构造一个Beatmap
        /// </summary>
        /// <param name="dir">osu文件路径</param>
        public Beatmap(string dir)
        {
            FileDataAvalable = true;
            FileStreamAvalable = true;

            info = null;

            if (System.IO.File.Exists(dir))
            {
                info = new System.IO.FileInfo(dir);
            }
            else
            {
                throw new System.IO.FileNotFoundException("指定的谱面文件不存在。");
            }
            int i = 0;
            fn = info.Name;
            fullfn = info.FullName;
            string[] map = System.IO.File.ReadAllLines(dir);

            if (map.Length == 0)
            {
                notv = true;
                throw new osuToolsException.InvalidBeatmapFileException($"文件{dir}为空。");
            }
            if (!map[0].Contains("osu file format"))
            {
                notv = true;
                throw new osuToolsException.InvalidBeatmapFileException($"文件{dir}不是谱面文件。");
            }
            DataBlock block = DataBlock.None;

            foreach (string str in map)
            {
                i++;
                string[] temparr = str.Split(':');
                if (temparr[0].Contains("AudioFile"))
                {
                    au = temparr[1].Trim();
                    fuau = info.DirectoryName + "\\" + AudioFileName;
                    continue;
                }
                if (temparr[0].Contains("Title") && temparr[0].Length <= "Titleuni".Length)
                {
                    t = temparr[1].Trim();
                    continue;
                }
                if (temparr[0].Contains("Countdown"))
                {
                    HasCountdown = temparr[1].Trim().ToBool();
                    continue;
                }
                if (temparr[0].Contains("TitleUnicode"))
                {
                    ut = str.Replace("TitleUnicode:", "").Trim();
                    continue;
                }
                if (temparr[0].Contains("Artist") && temparr[0].Length <= "Artistuni".Length)
                {
                    a = str.Replace("Artist:", "").Trim();
                    continue;
                }
                if (temparr[0].Contains("ArtistUnicode"))
                {
                    ua = str.Replace("ArtistUnicode:", "").Trim();
                    continue;
                }

                if (temparr[0].Contains("Creator"))
                {
                    c = str.Replace("Creator:", "").Trim();
                    continue;
                }
                if (temparr[0].Contains("Version"))
                {
                    ver = str.Replace("Version:", "").Trim();
                    dif = ver;
                    continue;
                }
                if (temparr[0].Contains("Maker"))
                {
                    mak = str.Replace("Maker:", "").Trim();
                    continue;

                }
                if (temparr[0].Contains("Source"))
                {
                    sou = str.Replace("Source:", "").Trim();
                    continue;

                }
                if (temparr[0].Contains("Tags"))
                {
                    tag = str.Replace("Tags:", "").Trim();
                    continue;

                }
                if (temparr[0].Contains("BeatmapID"))
                {
                    int.TryParse(temparr[1].Trim(), out id);
                    continue;
                }
                if (temparr[0].Contains("CircleSize"))
                {
                    double.TryParse(temparr[1].Trim(), out cs);
                    continue;
                }
                if (temparr[0].Contains("OverallDifficulty"))
                {
                    double.TryParse(temparr[1].Trim(), out od);
                    continue;
                }
                if (temparr[0].Contains("ApproachRate"))
                {
                    double.TryParse(temparr[1].Trim(), out ar);
                    continue;
                }
                if (temparr[0].Contains("HPDrainRate"))
                {
                    double.TryParse(temparr[1].Trim(), out hp);
                    continue;
                }
                if (temparr[0].Contains("Mode"))
                {
                    if (!ModeHasSet)
                    {
                        int.TryParse(temparr[1].Trim(), out m);
                        mode = (OsuGameMode)(m);
                        ModeHasSet = true;
                    }
                    continue;
                }

                dlnk = $"http://osu.ppy.sh/b/{BeatmapID}";
                if (str.StartsWith("0,0,\""))
                {
                    bgf = str.Split(',')[2].Replace("\"", "").Trim();
                }
                if (str.StartsWith("Video,"))
                {
                    vi = str.Split(',')[2].Replace("\"", "").Trim();
                    hasvideo = true;
                }
            }
            getAddtionalInfo(map);
            md5calc.ComputeHash(System.IO.File.ReadAllBytes(info.FullName));
            md = md5calc.GetMD5String();
        }
    }
}