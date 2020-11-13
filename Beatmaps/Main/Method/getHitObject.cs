namespace osuTools.Beatmaps
{
    using Osu.Game.Modes;
    using osuTools.Beatmaps.HitObject;
    using osuTools.Game.Modes;
    using osuTools.osuToolsException;
    using Sync.Tools;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    public partial class Beatmap
    {
        void getHitObjects()
        {
            //Stopwatch t = new Stopwatch();
           // t.Start();
            DataBlock block = DataBlock.None;
            HitObject.HitObjectCollection objects = new HitObject.HitObjectCollection();
            string[] map = System.IO.File.ReadAllLines(FullPath);
            foreach (var str in map)
            {
               
                if (str.Contains("[HitObjects]"))
                {
                    block = DataBlock.HitObjects;
                    continue;
                }
                if (block == DataBlock.HitObjects)
                {
                    string[] comasp = str.Split(',');
                    if (comasp.Length > 4)
                    {
                        
                        if (Mode == OsuGameMode.Mania)
                        {
                            objects.Add(GameMode.FromLegacyMode(Mode).CreateHitObject(str, (int)CS));
                        }
                        else
                        {
                            objects.Add(GameMode.FromLegacyMode(Mode).CreateHitObject(str));
                        }
                    }
                }
            }
            hitObjects = objects;
           // t.Stop();
            
            //IO.CurrentIO.Write($"Read HitObjects:{objects.Count} Time:{t.Elapsed.TotalSeconds}s");
        }

        /// <summary>
        /// 获取指定类型的打击物件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>包含所有符合要求的打击物件的列表</returns>
        public List<T> GetHitObjects<T>()
        {
            return new List<T>((IEnumerable<T>)hitObjects.Where((hit) => hit is T));
        }
    }
}