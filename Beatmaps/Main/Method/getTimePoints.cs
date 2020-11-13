namespace osuTools.Beatmaps
{
    using System.Collections.Generic;
    using System.Diagnostics;

    partial class Beatmap
    {
        TimePointCollection tps = null;
        public TimePointCollection TimePoints {
            get 
            {
                if (tps == null)
                    getTimePoints();
                return tps;
            } 
            private set => tps = value; }
        void getTimePoints()
        {
            DataBlock block = DataBlock.None;
            string[] map = System.IO.File.ReadAllLines(FullPath);
            TimePointCollection timePoints = new TimePointCollection();
            var nstr = "";
            foreach (var str in map)
            {
                if (str.Trim().StartsWith("[") && str.Trim().EndsWith("]"))
                {
                    nstr = str.Trim().TrimStart('[').TrimEnd(']');
                }
                if (nstr == "TimingPoints")
                {
                    string[] comasp = str.Split(',');
                    if (comasp.Length > 7)
                    {
                        timePoints.TimePoints.Add(new TimePoint(str));
                    }
                    continue;
                }
                if (nstr != "TimingPoints")
                {
                    continue;
                }

            }
            tps = timePoints;
        }
    }
}