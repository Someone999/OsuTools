namespace osuTools.Beatmaps
{
    partial class Beatmap
    {
        /// <summary>
        /// 使用OsuBeatmap初始化Beatmap对象
        /// </summary>
        /// <param name="beatmap"></param>
        /// <param name="getStars"></param>
        public Beatmap(OsuDB.OsuBeatmap beatmap, bool getStars = true)
        {
            OsuInfo info = new OsuInfo();
            t = beatmap.Title;
            ut = beatmap.TitleUnicode;
            a = beatmap.Artist;
            ua = beatmap.ArtistUnicode;
            c = beatmap.Creator;
            dif = beatmap.Difficulty;
            ver = dif;
            fn = beatmap.FileName;
            fullfn = info.BeatmapDirectory + "\\" + beatmap.FolderName + "\\" + beatmap.FileName;
            dlnk = $"http://osu.ppy.sh/b/{beatmap.BeatmapID}";
            sou = beatmap.Source;
            tag = beatmap.Tags;
            mak = "";
            md = new MD5String(beatmap.MD5);
            fuau = info.BeatmapDirectory + "\\" + beatmap.FolderName + "\\" + beatmap.AudioFileName;
            fuvi = "";
            od = beatmap.OD;
            hp = beatmap.HPDrain;
            ar = beatmap.AR;
            cs = beatmap.CS;
            setid = beatmap.BeatmapSetID;
            au = beatmap.AudioFileName;
            mode = beatmap.Mode;
            if (getStars)
                double.TryParse(beatmap.Stars.ToString(), out stars);
            else stars = 0;
            if (fullfn == "" || !System.IO.File.Exists(fullfn)) return;
            var alllines = System.IO.File.ReadAllLines(FullPath);
            foreach (string line in alllines)
            {
                var temparr = line.Split(':');
                if (temparr[0].StartsWith("0,0,\""))
                {
                    if (!string.IsNullOrEmpty(bgf))
                        bgf = temparr[0].Split(',')[2].Replace("\"", "").Trim();
                    continue;
                }
                if (temparr[0].StartsWith("Video,"))
                {
                    vi = temparr[0].Split(',')[2].Replace("\"", "").Trim();
                    if (!string.IsNullOrEmpty(vi))
                    {
                        hasvideo = true;
                    }
                    else
                    {
                        hasvideo = false;
                    }
                    continue;
                }
                fuvi = FullPath.Replace(FileName, vi);
                if (line.Contains("TimingPoints"))
                {
                    break;
                }
            }
            SetBeatmapID(beatmap.BeatmapID);
            getAddtionalInfo(alllines);

        }
    }
}