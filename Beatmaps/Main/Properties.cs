using osuTools.Beatmaps.HitObject;
using System.Collections.Generic;
using System.IO;

namespace osuTools
{
    namespace Beatmaps
    {
        public partial class Beatmap
        {
            /// <summary>
            /// 谱面对应音频文件的全路径
            /// </summary>
            public string FullAudioFileName { get => fuau; }
            /// <summary>
            /// 谱面对应图片文件的全路径
            /// </summary>
            public string FullBackgroundFileName { get => fubgf; }
            /// <summary>
            /// 谱面对应的视频文件的全路径
            /// </summary>
            public string FullVideoFileName { get { if (hasvideo == true) return fuvi; else return null; } }
            /// <summary>
            /// 谱面对应的音频文件名
            /// </summary>
            public string AudioFileName { get => au; set => au = value; }
            /// <summary>
            /// 谱面对应的视频文件名
            /// </summary>
            public string VideoFileName { get { if (hasvideo) return vi; else return null; } set => vi = value; }
            /// <summary>
            /// 存储谱面的文件夹的全路径
            /// </summary>
            public string BeatmapFolder { get => fullfn.Replace(FileName, ""); }
            /// <summary>
            /// 谱面的MD5
            /// </summary>
            public MD5String MD5 { get => md; }
            /// <summary>
            /// 谱面的来源
            /// </summary>
            public string Source { get => sou; set => sou = value; }
            /// <summary>
            /// 谱面的标签
            /// </summary>
            public string Tags { get => tag;set => tag = value; }
            /// <summary>
            /// 谱面的作者
            /// </summary>
            public string Maker { get => mak; set => mak = value; }
            /// <summary>
            /// 标题
            /// </summary>
            public string Title { get => t;set => t = value; }
            /// <summary>
            /// 标题的Unicode形式
            /// </summary>
            public string TitleUnicode { get => ut; set => ut = value; }
            /// <summary>
            /// 艺术家
            /// </summary>
            public string Artist { get => a; set => a = value; }
            /// <summary>
            /// 艺术家的Unicode形式
            /// </summary>
            public string ArtistUnicode { get => ua;  set => ua = value; }
            /// <summary>
            /// 谱面的作者
            /// </summary>
            public string Creator { get => c;  set => c = value; }
            /// <summary>
            /// 谱面的难度
            /// </summary>
            public string Difficulty { get => dif; set => dif = value; }
            /// <summary>
            /// 谱面的难度
            /// </summary>
            public string Version { get => ver;  set => ver = value; }
            /// <summary>
            /// 谱面文件的文件名
            /// </summary>
            public string FileName { get => fn; }
            /// <summary>
            /// 谱面文件的全路径
            /// </summary>
            public string FullPath { get => fullfn; }
            /// <summary>
            /// 谱面文件的下载链接
            /// </summary>
            public string DownloadLink { get => dlnk; }
            /// <summary>
            /// 背景文件的文件名
            /// </summary>
            public string BackgroundFileName { get => bgf; }
            /// <summary>
            /// 谱面ID
            /// </summary>
            public int BeatmapID { get => id; set => id = value; }
            /// <summary>
            /// 综合难度
            /// </summary>
            public double OD { get => od; set => od = value; }
            /// <summary>
            /// 掉血速度，回血难度
            /// </summary>
            public double HP { get => hp;  set => hp = value; }
            /// <summary>
            /// 缩圈速度
            /// </summary>
            public double AR { get => ar;  set => ar = value; }
            /// <summary>
            /// 圈圈大小
            /// </summary>
            public double CS { get => cs;  set => cs = value; }
            /// <summary>
            /// 难度星级
            /// </summary>
            public double Stars { get => stars; set => stars = value; }
            /// <summary>
            /// 谱面包含的所有HitObject
            /// </summary>
            public HitObjectCollection HitObjects 
            {
                get
                {
                    if (hitObjects == null)
                        getHitObjects();
                    return hitObjects;
                }
                set
                {
                    hitObjects = value;
                }
            }
            /// <summary>
            /// 谱面中包含的所有BreakTime
            /// </summary>
            public BreakTimeCollection BreakTimes
            {
                get
                {
                    if (breakTimes == null)
                        getBreakTimes();
                    return breakTimes;
                }
                 set
                {
                    breakTimes = value;
                }
            }
        }

    }
}