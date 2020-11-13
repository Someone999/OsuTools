namespace osuTools
{
    namespace Online.ApiV1
    {
        using Newtonsoft.Json.Linq;
        using OsuRTDataProvider.BeatmapInfo;
        using System;
        using System.Text;
        using System.Collections.Generic;
        using Newtonsoft.Json;
        using osuTools.OtherTools;
        using System.IO;

        /// <summary>
        /// 在线获取的谱面的集合
        /// </summary>
        public class OnlineBeatmapCollection : OnlineInfo<OnlineBeatmap>
        {

            List<OnlineBeatmap> beatmaps = new List<OnlineBeatmap>();
            int pos = -1;
            int i = 0;
            /// <summary>
            /// 指示本次查询是否失败
            /// </summary>
            public bool Failed { get; internal set; } = false;
            /// <summary>
            /// 使用整数索引从列表获取OnlineBeatmap
            /// </summary>
            /// <param name="x"></param>
            /// <returns></returns>
            public OnlineBeatmap this[int x]
            {
                get => beatmaps[x];
                set => beatmaps[x] = value;
            }
            /// <summary>
            /// 构造一个空的OnlineBeatmapCollection对象
            /// </summary>
            public OnlineBeatmapCollection()
            {
               

            }
            /// <summary>
            /// 存储的<see cref="OnlineBeatmap"/>
            /// </summary>
            public List<OnlineBeatmap> Beatmaps { get => beatmaps; }
            /// <summary>
            /// 存储的谱面的数量
            /// </summary>
            public int Count { get => beatmaps.Count; }
            /// <summary>
            /// 通过关键词搜索谱面
            /// </summary>
            /// <param name="keyword"></param>
            /// <returns></returns>
            public OnlineBeatmapCollection Find(string keyword)
            {
                OnlineBeatmapCollection bc = new OnlineBeatmapCollection();
                foreach (var beat in beatmaps)
                {
                    if (beat.ToBeatmap().ToString().Trim().ToUpper().Contains(keyword.ToUpper().Trim()) ||
                        beat.ToBeatmap().Source.Trim().ToUpper().Contains(keyword.ToUpper().Trim()) ||
                        beat.ToBeatmap().Tags.Trim().ToUpper().Contains(keyword.ToUpper().Trim()))
                    {
                        bc.beatmaps.Add(beat);
                    }
                }
                return bc;
            }
            /// <summary>
            /// 判断列表中是否包含指定谱面
            /// </summary>
            /// <param name="b"></param>
            /// <returns></returns>
            public bool Contains(OnlineBeatmap b) => beatmaps.Contains(b);
            /// <summary>
            /// 返回循环访问List的枚举数
            /// </summary>
            /// <returns></returns>
            public IEnumerator<OnlineBeatmap> GetEnumerator()
            {
                return beatmaps.GetEnumerator();

            }
        }
        /// <summary>
        /// 在线获取的谱面
        /// </summary>
        [Serializable]
        public partial class OnlineBeatmap : IEquatable<OnlineBeatmap>, IFormattable,IJsonSerilizable
        {
            JObject jobj = new JObject();
            /// <summary>
            /// 使用BeatmapID判断两个OnlineBeatmap是否相同
            /// </summary>
            /// <param name="olb"></param>
            /// <param name="lob"></param>
            /// <returns></returns>
            public static bool operator ==(OnlineBeatmap olb, Beatmaps.Beatmap lob)
            {
                if (lob.BeatmapID == 0 || lob.BeatmapID == -1) throw new NotSupportedException();
                if (olb.BeatmapID == lob.BeatmapID && (lob.BeatmapID != 0 && lob.BeatmapID != -1)) return true;
                else return false;
            }
            /// <summary>
            /// 使用BeatmapID判断两个OnlineBeatmap是否相同
            /// </summary>
            /// <param name="olb"></param>
            /// <param name="lob"></param>
            /// <returns></returns>
            public static bool operator !=(OnlineBeatmap olb, Beatmaps.Beatmap lob)
            {
                if (lob.BeatmapID == 0 || lob.BeatmapID == -1) throw new NotSupportedException();
                if (olb.BeatmapID == lob.BeatmapID && (lob.BeatmapID != 0 && lob.BeatmapID != -1)) return false;
                else return true;
            }
            /// <summary>
            /// 使用BeatmapID判断OnlineBeatmap和Beatmap是否相同
            /// </summary>
            /// <param name="olb"></param>
            /// <param name="lob"></param>
            /// <returns></returns>
            public static bool operator ==(OnlineBeatmap olb, Beatmap lob)
            {
                if (lob.BeatmapID == 0 || lob.BeatmapID == -1) throw new NotSupportedException();
                if (olb.BeatmapID == lob.BeatmapID && (lob.BeatmapID != 0 && lob.BeatmapID != -1)) return true;
                else return false;
            }
            /// <summary>
            /// 使用BeatmapID判断OnlineBeatmap和Beatmap是否相同
            /// </summary>
            /// <param name="olb"></param>
            /// <param name="lob"></param>
            /// <returns></returns>
            public static bool operator !=(OnlineBeatmap olb, Beatmap lob)
            {
                if (lob.BeatmapID == 0 || lob.BeatmapID == -1) throw new NotSupportedException();
                if (olb.BeatmapID == lob.BeatmapID && (lob.BeatmapID != 0 && lob.BeatmapID != -1)) return false;
                else return true;
            }
            /// <summary>
            /// 该谱面对象是否为空
            /// </summary>
            /// <returns></returns>
            public bool IsEmpty()
            {
                if (file_md5 == "0")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            /// <summary>
            /// 构造一个空的OnlineBeatmap对象
            /// </summary>
            public OnlineBeatmap()
            {

            }
            /// <summary>
            /// 使用正确的JObject填充一个OnlineBeatmap对象
            /// </summary>
            /// <param name="jobj"></param>
            public OnlineBeatmap(JObject jobj)
            {
                Parse(jobj);
            }
            void Parse(JObject jobj)
            {
                this.jobj = jobj;
                int.TryParse(jobj["beatmapset_id"].ToString(), out beatmapset_id);
                int.TryParse(jobj["beatmap_id"].ToString(), out beatmap_id);
                int.TryParse(jobj["approved"].ToString(), out approved);
                int.TryParse(jobj["total_length"].ToString(), out total_length);
                int.TryParse(jobj["hit_length"].ToString(), out hit_length);
                int.TryParse(jobj["mode"].ToString(), out mode);
                int.TryParse(jobj["count_normal"].ToString(), out count_normal);
                int.TryParse(jobj["count_slider"].ToString(), out count_slider);
                int.TryParse(jobj["count_spinner"].ToString(), out count_spinner);
                int.TryParse(jobj["creator_id"].ToString(), out creator_id);
                int.TryParse(jobj["genre_id"].ToString(), out genre_id);
                int.TryParse(jobj["language_id"].ToString(), out language_id);
                int.TryParse(jobj["playcount"].ToString(), out playcount);
                int.TryParse(jobj["passcount"].ToString(), out passcount);
                int.TryParse(jobj["favourite_count"].ToString(), out favourite_count);
                int.TryParse(jobj["max_combo"].ToString(), out max_combo);
                double.TryParse(jobj["diff_overall"].ToString(), out diff_overall);
                double.TryParse(jobj["diff_approach"].ToString(), out diff_approach);
                double.TryParse(jobj["diff_drain"].ToString(), out diff_drain);
                double.TryParse(jobj["diff_size"].ToString(), out diff_size);
                double.TryParse(jobj["bpm"].ToString(), out bpm);
                double.TryParse(jobj["rating"].ToString(), out rating);
                double.TryParse(jobj["diff_speed"].ToString(), out diff_speed);
                double.TryParse(jobj["diff_aim"].ToString(), out diff_aim);
                double.TryParse(jobj["difficultyrating"].ToString(), out difficultyrating);
                version = jobj["version"].ToString();
                file_md5 = jobj["file_md5"].ToString();
                submit_date = jobj["submit_date"].ToString();
                last_update = jobj["last_update"].ToString();
                approved_date = jobj["approved_date"].ToString();
                title = jobj["title"].ToString();
                artist = jobj["artist"].ToString();
                creator = jobj["creator"].ToString();
                tags = jobj["tags"].ToString();
                source = jobj["source"].ToString();
                status = (BeatmapStatus)(approved);
            }
            int beatmapset_id = -2;

            int beatmap_id = -2;
            BeatmapStatus status = BeatmapStatus.None;
            int approved = -1;
            int total_length = -1;
            int hit_length = -1;
            string version = "";
            string file_md5 = "0";
            double diff_size = -1;
            double diff_overall = -1;

            double diff_approach = -1;

            double diff_drain = -1;

            int mode = -1;

            int count_normal = -1;

            int count_slider = -1;

            int count_spinner = -1;

            string submit_date = "0-0-0 0:0:0";

            string approved_date = "0-0-0 0:0:0";

            string last_update = "0-0-0 0:0:0";

            string artist = "";

            string title = "";

            string creator = "";

            int creator_id = 0;

            double bpm = 0.0;

            string source = "";

            string tags = "";

            int genre_id = 0;

            int language_id = 0;

            int favourite_count = 0;

            double rating = 0.0;

            int download_unavailable = 0;

            int audio_unavailable = 0;

            int playcount = 0;

            int passcount = 0;

            int max_combo = 0;

            double diff_aim = 0.0;

            double diff_speed = 0.0;

            double difficultyrating = 0.0;
            /// <summary>
            /// 使用一定的格式构造一个字符串
            /// </summary>
            /// <param name="format"></param>
            /// <param name="formatProvider"></param>
            /// <returns></returns>
            public string ToString(string format, IFormatProvider formatProvider)
            {
                StringBuilder b = new StringBuilder(format);
                b.Replace("Artist", Artist);
                b.Replace("artist", Artist);
                b.Replace("bpm", BPM.ToString());
                b.Replace("stars", Stars.ToString());
                b.Replace("mode", Mode.ToString());
                b.Replace("cs", CS.ToString());
                b.Replace("ar", AR.ToString());
                b.Replace("hp", HP.ToString());
                b.Replace("od", OD.ToString());
                b.Replace("circles", HitCircle.ToString());
                b.Replace("sliders", Slider.ToString());
                b.Replace("spiners", Spinner.ToString());
                b.Replace("creator", Creator);
                b.Replace("Title", Title);
                b.Replace("title", Title);
                b.Replace("Tags", Tags);
                b.Replace("tags", Tags);
                b.Replace("setid", BeatmapSetID.ToString());
                b.Replace("maxcombo", MaxCombo.ToString());
                b.Replace("approved", Approved.ToString());
                TimeSpan total = TimeSpan.FromSeconds(TotalTime);
                TimeSpan hit = TimeSpan.FromSeconds(DrainTime);
                b.Replace("length", $"{total.TotalMinutes}:{total.Seconds}");
                b.Replace("hitlength", $"{hit.TotalMinutes}:{hit.Seconds}");
                b.Replace("version", Version);
                b.Replace("md5", MD5);
                b.Replace("id", BeatmapID.ToString());
                return b.ToString();
            }
            /// <summary>
            /// 将对象序列化
            /// </summary>
            /// <param name="file"></param>
            public void Serialize(string file)
            {
                if (string.IsNullOrEmpty(file))
                    throw new ArgumentNullException("文件名不能为空。");
                string dir = Path.GetDirectoryName(file);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                File.WriteAllText(file,jobj.ToString());
            }
            /// <summary>
            /// 从文件反序列化
            /// </summary>
            /// <param name="file"></param>
            public void Deserialize(string file)
            {
                Parse((JObject)JsonConvert.DeserializeObject(File.ReadAllText(file)));
            }
            public string ToString(string format)
            {
                return ToString(format, null);
            }
            /// <summary>
            /// 返回基础信息
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return $"{Artist} - {Title} [{Version}]\nMode:{Mode}\nStars:{difficultyrating:f2} BPM:{bpm}\nOD:{OD} AR:{AR} CS:{CS} HP:{HP}";
            }
            /// <summary>
            /// 将该OnlineBeatmap转化为<see cref="Beatmaps.Beatmap"/>
            /// </summary>
            /// <returns></returns>
            public Beatmaps.Beatmap ToBeatmap()
            {
                return new Beatmaps.Beatmap(this);
            }
            bool IEquatable<OnlineBeatmap>.Equals(OnlineBeatmap other)
            {
                if (file_md5 == other.file_md5)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}