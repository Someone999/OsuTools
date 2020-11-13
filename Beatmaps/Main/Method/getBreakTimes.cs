namespace osuTools.Beatmaps
{
    using System.Collections.Generic;
    partial class Beatmap
    {
        void getBreakTimes()
        {
            BreakTimeCollection breaktimes =new BreakTimeCollection();
            DataBlock block = DataBlock.None;
            string[] map = System.IO.File.ReadAllLines(FullPath);
            foreach (string str in map)
            {
                if (str.Contains("Break Periods") && str.StartsWith("//"))
                {
                    block = DataBlock.BreakTime;
                }
                if (block == DataBlock.BreakTime)
                {

                    string[] breakstr = str.Split(',');
                    if (breakstr.Length == 3)
                    {
                        int i = 0;
                        if (int.TryParse(breakstr[0], out i))
                        {
                            if (i == 2)
                                breaktimes.BreakTimes.Add(new BreakTime(long.Parse(breakstr[1]), long.Parse(breakstr[2])));
                        }
                    }
                }
                if (str.Contains("HitObjects"))
                {
                    block = DataBlock.HitObjects;
                    break;
                }
            }
            breakTimes = breaktimes;
        }


    }
}