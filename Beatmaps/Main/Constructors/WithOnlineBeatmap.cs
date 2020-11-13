namespace osuTools.Beatmaps
{
    using osuTools.Online.ApiV1;
    partial class Beatmap
    {
        public Beatmap(OnlineBeatmap olbeatmap)
        {
            t = olbeatmap.Title;
            ut = t;
            a = olbeatmap.Artist;
            ua = a;
            c = olbeatmap.Creator;
            dif = olbeatmap.Version;
            ver = dif;
            fn = "";
            fullfn = "";
            dlnk = "";
            sou = olbeatmap.Source;
            tag = olbeatmap.Tags;
            mak = "";
            md = new MD5String(olbeatmap.MD5);
            fuau = "";
        }
    }
}