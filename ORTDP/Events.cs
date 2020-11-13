using Sync.Tools;
using System;
namespace osuTools
{
    using OsuRTDataProvider.Mods;
    using osuTools.Beatmaps;
    using osuTools.ExtraMethods;
    using osuTools.Game.Modes;
    using osuTools.Game.Mods;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Emit;
    using System.Windows.Forms;
    using osuTools.osuToolsAttributes;
    using System.Diagnostics;
    using System.Text;
    using osuTools.Online.ApiV1;

    partial class ORTDP
    {
        bool InDebugMode()
        {
            return DebugMode;
        }
        System.Timers.Timer timer1 = new System.Timers.Timer();
        int cc = 0;
        int allhit;
        double persec = 0;
        void HitChanged()
        {
            System.Threading.Tasks.Task.Run(new Action(() =>
            {
                while (true)
                {

                    if (GameMode.CurrentMode == OsuGameMode.Mania)
                    {
                        allhit = c300g + c300 + c200 + c100 + c50 + cMiss;
                        System.Threading.Thread.Sleep(1000);
                        persec = ((c300g + c300 + c200 + c100 + c50 + cMiss) - allhit);
                    }
                    if (GameMode.CurrentMode == OsuGameMode.Osu)
                    {
                        allhit = c300 + c100 + c50 + cMiss;
                        System.Threading.Thread.Sleep(1000);
                        persec = ((c300 + c100 + c50 + cMiss) - allhit);
                    }
                    if (GameMode.CurrentMode == OsuGameMode.Catch)
                    {
                        allhit = c300 + c50 + cMiss;
                        System.Threading.Thread.Sleep(1000);//间隔0.5秒
                        persec = ((c300 + c50 + cMiss) - allhit);
                    }
                }
            }));

        }
        void InitLisenter()
        {
            //InfoReaderPlugin.InfoReader reader = new InfoReaderPlugin.InfoReader();
            p = new OsuRTDataProvider.OsuRTDataProviderPlugin();
            p.OnEnable();
            lm = p.ListenerManager;
            lm.OnCountGekiChanged += Lm_OnCountGekiChanged;
            lm.OnCount300Changed += Lm_OnCount300Changed;
            lm.OnCountKatuChanged += Lm_OnCountKatuChanged;
            lm.OnCount100Changed += Lm_OnCount100Changed;
            lm.OnCount50Changed += Lm_OnCount50Changed;
            lm.OnCountMissChanged += Lm_OnCountMissChanged;
            lm.OnComboChanged += Lm_OnComboChanged;
            lm.OnScoreChanged += Lm_OnScoreChanged;
            lm.OnPlayingTimeChanged += Lm_OnPlayingTimeChanged;
            lm.OnHealthPointChanged += Lm_OnHealthPointChanged;
            lm.OnAccuracyChanged += Lm_OnAccuracyChanged;
            lm.OnBeatmapChanged += Lm_OnBeatmapChanged;
            lm.OnModsChanged += Lm_OnModsChanged;
            lm.OnPlayModeChanged += Lm_OnPlayModeChanged;
            lm.OnStatusChanged += Lm_OnStatusChanged;
            lm.OnPlayerChanged += Lm_OnPlayerChanged;
            gm = new GMMode(OsuRTDataProvider.Listen.OsuPlayMode.Unknown, OsuRTDataProvider.Listen.OsuPlayMode.Unknown);
            gs = new GMStatus(OsuRTDataProvider.Listen.OsuListenerManager.OsuStatus.Unkonwn, OsuRTDataProvider.Listen.OsuListenerManager.OsuStatus.Unkonwn);
            b = null;
        }

        private void Lm_OnPlayerChanged(string player)
        {
            pn = player.Trim();
        }

        void InitLisenter(OsuRTDataProvider.OsuRTDataProviderPlugin pl)
        {
            HitChanged();
            p = pl;
            lm = p.ListenerManager;
            lm.OnCountGekiChanged += Lm_OnCountGekiChanged;
            lm.OnCount300Changed += Lm_OnCount300Changed;
            lm.OnCountKatuChanged += Lm_OnCountKatuChanged;
            lm.OnCount100Changed += Lm_OnCount100Changed;
            lm.OnCount50Changed += Lm_OnCount50Changed;
            lm.OnCountMissChanged += Lm_OnCountMissChanged;
            lm.OnComboChanged += Lm_OnComboChanged;
            lm.OnScoreChanged += Lm_OnScoreChanged;
            lm.OnPlayingTimeChanged += Lm_OnPlayingTimeChanged;
            lm.OnHealthPointChanged += Lm_OnHealthPointChanged;
            lm.OnAccuracyChanged += Lm_OnAccuracyChanged;
            lm.OnBeatmapChanged += Lm_OnBeatmapChanged;
            lm.OnModsChanged += Lm_OnModsChanged;
            lm.OnPlayModeChanged += Lm_OnPlayModeChanged;
            lm.OnStatusChanged += Lm_OnStatusChanged;
            lm.OnHitEventsChanged += Lm_OnHitEventsChanged;
            gm = new GMMode(OsuRTDataProvider.Listen.OsuPlayMode.Unknown, OsuRTDataProvider.Listen.OsuPlayMode.Unknown);
            gs = new GMStatus(OsuRTDataProvider.Listen.OsuListenerManager.OsuStatus.Unkonwn, OsuRTDataProvider.Listen.OsuListenerManager.OsuStatus.Unkonwn);
            b = null;
        }

        private void Lm_OnHitEventsChanged(OsuRTDataProvider.Listen.PlayType playType, System.Collections.Generic.List<OsuRTDataProvider.Listen.HitEvent> hitEvents)
        {

        }
        private void Lm_OnPlayModeChanged(OsuRTDataProvider.Listen.OsuPlayMode last, OsuRTDataProvider.Listen.OsuPlayMode mode)
        {
            gm = new GMMode(last, mode);
            canfail = !mod.Contains(new NoFailMod()) && !mod.Contains(new AutoPilotMod()) && !mod.Contains(new RelaxMod());
        }
        int tmpHitObjectCount = 0;
        private void Lm_OnModsChanged(ModsInfo mods)
        {
            //MessageBox.Show(((int)mods.Mod).ToString());
            mod = ModList.FromInteger((int)mods.Mod).Mods.Where((m) => m.CheckAndSetForMode(CurrentMode)).ToArray().ToModList();
            if (CurrentMode == OsuGameMode.Mania && mod.Contains(new ScoreV2Mod()))
                tmpHitObjectCount = hitObjects.Count + hitObjects.Where((h) => h.HitObjectType == HitObjectTypes.ManiaHold).Count();
            else tmpHitObjectCount = hitObjects.Count;
            if (DebugMode)
            {
                if (tmpHitObjectCount != hitObjects.Count)
                {
                    IO.CurrentIO.Write($"[osuTools] HitObject Count after applied Mods: {tmpHitObjectCount}.");
                }
            }

        }
        OsuDB.OsuBeatmap bp;
        bool player = false;
        private void Lm_OnStatusChanged(OsuRTDataProvider.Listen.OsuListenerManager.OsuStatus last_status, OsuRTDataProvider.Listen.OsuListenerManager.OsuStatus status)
        {

            gs = new GMStatus(last_status, status);
            if (status == OsuRTDataProvider.Listen.OsuListenerManager.OsuStatus.SelectSong || status == OsuRTDataProvider.Listen.OsuListenerManager.OsuStatus.Rank) rt = 0;
            
            while (status != OsuRTDataProvider.Listen.OsuListenerManager.OsuStatus.Rank && status!=OsuRTDataProvider.Listen.OsuListenerManager.OsuStatus.Playing)
            {
                if (C300 != 0 || C300g != 0 || C200 != 0 || C100 != 0 || C50 != 0 || Cmiss != 0 || score != 0 || acc != 0)
                {
                    setzero();
                }
                else
                {
                    break;
                }
                if (!(b is null))
                {
                    if (!string.IsNullOrEmpty(np))
                        np = $"{b.Artist} - {b.Title} [{b.Difficulty}]";
                    stars = b.Stars;
                }
            }
        }
        void setzero()
        {
            C300 = 0;
            C300g = 0;
            C200 = 0;
            C100 = 0;
            C50 = 0;
            Cmiss = 0;
            score = 0;
            acc = 0;
            i = 0;
            cBtime = 0;
            cTmpoint = 0;
            Ranking = "???";
        }
        void ReadFromORTDP(OsuRTDataProvider.BeatmapInfo.Beatmap beatmap)
        {
            np = "Loading...";
            b_status = OsuDB.OsuBeatmapStatus.Loading;
            if (beatmap == null || beatmap == OsuRTDataProvider.BeatmapInfo.Beatmap.Empty)
            {
                IO.CurrentIO.WriteColor("Beatmap is null or empty.", ConsoleColor.Red, true, false);
            }
            b = new Beatmaps.Beatmap(beatmap);
            if (b == null)
            {
                IO.CurrentIO.Write("Fail to read beatmap.", true, false);
            }
            else
            {
                np = b.ToString();
                System.Threading.Thread.Sleep(100);
                stars = b.Stars;
                hitObjects = b.HitObjects;
                b_status = OsuDB.OsuBeatmapStatus.Unknown;
                dur = hitObjects.LastOrDefault() == null ? TimeSpan.FromMilliseconds(0) : TimeSpan.FromMilliseconds(hitObjects.LastOrDefault().Offset);
                if (DebugMode)
                {
                    IO.CurrentIO.Write("Beatmap read from ORTDP.",true,false);
                }
            }
        }
        Beatmaps.MD5String md5str = new Beatmaps.MD5String(), md5str1 = new Beatmaps.MD5String();
        void ReadFromOsudb(OsuRTDataProvider.BeatmapInfo.Beatmap map)
        {
            np = "Loading...";
            b_status = OsuDB.OsuBeatmapStatus.Loading;
            if (md5str is null || md5str1 is null) return;

            md5str = new Beatmaps.MD5String("");
            if (md5str is null || md5str1 is null) return;
            if (md5str != md5str1)
            {
                bd = new OsuDB.OsuBeatmapDB();
                md5str1 = md5str;
            }


            try
            {
                if (map != null)
                {
                    string md5 = Beatmaps.MD5String.GetString(new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(System.IO.File.ReadAllBytes(map.FilenameFull)));
                    var beatmapa = bd.Beatmaps.FindByMD5(md5);
                    if (!(beatmapa is null))
                    {
                        b = new Beatmaps.Beatmap(beatmapa);
                        bp = beatmapa;
                        np = b.ToString();
                        hitObjects = b.HitObjects;
                        b_status = beatmapa.BeatmapStatus;
                        dur = beatmapa.TotalTime;
                        if (CurrentMode is ILegacyMode le)
                            stars = beatmapa.ModStarPair[le.LegacyMode][0];
                        else stars = 0;
                        if (DebugMode)
                        {
                            IO.CurrentIO.Write("Beatmap read from OsuDB.",true,false);
                        }
                    }
                    else
                    {
                        IO.CurrentIO.WriteColor("Fail to get beatmap info.", ConsoleColor.Red,true,false);
                    }
                }
            }
            catch (osuToolsException.BeatmapNotFoundException)
            {
                Beatmaps.MD5String m5 = bd.MD5;
                bd = new OsuDB.OsuBeatmapDB();
                Beatmaps.MD5String m51 = bd.MD5;
                if (m5 == m51)
                    ReadFromORTDP(map);
                IO.CurrentIO.WriteColor("Can not find this beatmap by MD5 in osu!DataBase.Beatmap has read from osu file,Info may be correct after re-read OsuDataBase.", ConsoleColor.Red);
            }
            catch (Exception)
            {

            }
        }
        Dictionary<int, Beatmaps.HitObject.IHitObject> timeHitObjectPair = new Dictionary<int, Beatmaps.HitObject.IHitObject>();
        private void Lm_OnBeatmapChanged(OsuRTDataProvider.BeatmapInfo.Beatmap map)
        {
            try
            {
               
                b = new Beatmap();
                if (DebugMode)
                    IO.CurrentIO.Write("[osuTools] Beatmap Changed");
                if (BeatmapReadMethod == BeatmapReadMethods.OsuRTDataProvider)
                {
                    ReadFromORTDP(map);                    
                }
                if (BeatmapReadMethod == BeatmapReadMethods.OsuDB)
                {
                    ReadFromOsudb(map);                  
                }
                i = 0;
                System.Threading.Tasks.Task.Run(() =>
                {
                    timeHitObjectPair.Clear();
                    foreach(var h in b.HitObjects)
                    {
                        if (!timeHitObjectPair.ContainsKey(h.Offset))
                            timeHitObjectPair.Add(h.Offset, h);
                    }
                });
                timepoints.TimePoints = b.TimePoints.TimePoints.Where((tp) => tp.Uninherited).ToList();
                breaktimes = b.BreakTimes;
                np = b.ToString();
                if(DebugMode)
                {
                    if (!(b is null))
                    {
                        var hit = b.HitObjects;
                        var tm = b.TimePoints;
                        var uinh = b.TimePoints.TimePoints.Where((t) => t.Uninherited).ToArray();
                        var inh = b.TimePoints.TimePoints.Where((t) => !t.Uninherited).ToArray();
                        var btm = b.BreakTimes;
                        StringBuilder builder = new StringBuilder();                       
                        builder.AppendLine($"Beatmap: {b}\nHitObject Count: {hit.Count}");
                        builder.AppendLine($"BreakTime: {btm.Count}");
                        builder.AppendLine($"TimePoints: {tm.Count} Inherited TimePoint Count: {inh.Count()} UnInherited TimePoint Count: {uinh.Count()}");
                        builder.AppendLine($"[Beatmap BreakTime Detector] BreakTime:{breaktimes.Count}");
                        builder.AppendLine($"[Beatmap Uninherited TimePoint Detector] Uninherited TimePoint:{timepoints.Count}");
                        IO.CurrentIO.Write(builder.ToString(), true, false);
                    }
                    else IO.CurrentIO.Write("[osuTools] Beatmap is null", true, false);
                }
            }
            catch(KeyNotFoundException) {}
            catch (Exception ex)
            {
                IO.CurrentIO.WriteColor($"[osuTools] Exception:{ex.ToString()}", ConsoleColor.Red);
            }
        }
        private void Lm_OnCountGekiChanged(int hit)
        {
            C300g = hit;
        }
        private void Lm_OnCount300Changed(int hit)
        {
            C300 = hit;
        }
        private void Lm_OnCountKatuChanged(int hit)
        {
            if (!string.IsNullOrEmpty(ModShortNames))
                if (ModShortNames.Contains("PF"))
                    rt++;
            C200 = hit;
        }
        private void Lm_OnCount100Changed(int hit)
        {
            if (!string.IsNullOrEmpty(ModShortNames))
                if (ModShortNames.Contains("PF"))
                    rt++;
            C100 = hit;
        }
        private void Lm_OnCount50Changed(int hit)
        {
            if (!string.IsNullOrEmpty(ModShortNames))
                if (ModShortNames.Contains("PF"))
                    rt++;
            C50 = hit;
        }
        private void Lm_OnCountMissChanged(int hit)
        {
            if (!string.IsNullOrEmpty(ModShortNames))
                if (ModShortNames.Contains("PF"))
                    rt++;
            if (!string.IsNullOrEmpty(ModShortNames))
                if (ModShortNames.Contains("SD"))
                    OnFail(this);
            Cmiss = hit;

        }
        private void Lm_OnComboChanged(int combo)
        {
            this.combo = combo;
            if (GameMode.CurrentMode == OsuGameMode.Catch || GameMode.CurrentMode == OsuGameMode.Osu)
            {
                if (combo == 3)
                {
                    dmp = (int)Score - 900;
                }
            }
        }
        int i = 0;
        /// <summary>
        /// 当前BreakTime剩余的时间
        /// </summary>
        [AvailableVariable("RemainingBreakTime", "当前BreakTime剩余的时间")]
        public TimeSpan RemainingBreakTime { get; private set; }
        /// <summary>
        /// BreakTime的数量
        /// </summary>
        [AvailableVariable("RemainingBreakTime", "BreakTime的数量")]
        public int BreakTimeCount { get => b is null ? 0 : b.BreakTimes.Count; }
        /// <summary>
        /// 距离下一个BreakTime的时间
        /// </summary>
        [AvailableVariable("TimeToNextBreakTime", "距离下一个BreakTime的时间")]
        public TimeSpan TimeToNextBreakTime { get; private set; }
        /// <summary>
        /// 当前BreakTime剩余的时间，以秒为单位
        /// </summary>
        [AvailableVariable("RemainingBreakTimeStr", "当前BreakTime剩余的时间，以秒为单位")]
        public string RemainingBreakTimeStr { get => RemainingBreakTime.TotalSeconds.ToString("f2"); }
        /// <summary>
        /// 距离下一个BreakTime的时间，以秒为单位
        /// </summary>
        [AvailableVariable("TimeToNextBreakTimeStr", "距离下一个BreakTime的时间，以秒为单位")]
        public string TimeToNextBreakTimeStr { get => TimeToNextBreakTime.TotalSeconds.ToString("f2"); }
        /// <summary>
        /// 谱面当前的非继承时间线(编辑器中红色时间线)后的BPM
        /// </summary>
        [AvailableVariable("CurrentBPM", "谱面当前的非继承时间线(编辑器中红色时间线)后的BPM")]
        public double CurrentBPM { get; private set; }
        /// <summary>
        /// CurrentBPM保留两位小数
        /// </summary>
        [AvailableVariable("CurrentBPMStr", "CurrentBPM保留两位小数")]
        public string CurrentBPMStr { get => CurrentBPM.ToString("f2"); }
        int cBtime = 0;
        int cTmpoint = 0;
        TimePointCollection timepoints = new TimePointCollection();
        BreakTimeCollection breaktimes = new BreakTimeCollection();
        /// <summary>
        /// 当前出现的HitObject，有多个的时候仅显示第一个
        /// </summary>
        [AvailableVariable("CurrentHitObject","当前出现的HitObject，有多个的时候仅显示第一个。")]
        private void Lm_OnPlayingTimeChanged(int ms)
        {
            
            if (bp != null)
            {
                tmper = ms / SongDuration.TotalMilliseconds;
                if (CurrentStatus == OsuGameStatus.Playing)
                {
                    if (timepoints.Count > 0)
                    {
                        if (ms < timepoints[0].Offset)
                        {
                            cTmpoint = 0;
                            CurrentBPM = timepoints[cTmpoint].BPM;
                        }                            
                        if (cTmpoint < timepoints.Count)
                        {
                            CurrentBPM = timepoints[cTmpoint].BPM;
                            foreach(var m in mod)
                            {
                                if (m is IChangeTimeRateMod changeTimeRateMod)
                                    CurrentBPM *= changeTimeRateMod.TimeRate;
                            }
                            
                            if (ms > timepoints[cTmpoint + 1 < timepoints.Count - 1 ? cTmpoint + 1 : cTmpoint].Offset)
                            {
                               cTmpoint = cTmpoint == timepoints.Count - 1 ? cTmpoint : cTmpoint + 1;
                            }
                        }
                    }
   

                    if (breaktimes.Count > 0)
                    {
                        //try
                        {
                            BreakTime curBreaktime = breaktimes[cBtime > breaktimes.Count - 1 ? breaktimes.Count : cBtime];
                            if (breaktimes.Count > 0)
                            {
                                if (ms < breaktimes[0].Start)
                                {
                                    cBtime = 0;
                                    curBreaktime = breaktimes[0];
                                    TimeToNextBreakTime = TimeSpan.FromMilliseconds(curBreaktime.Start - ms);
                                }
                                if (ms > breaktimes.BreakTimes.Last().End)
                                {
                                    curBreaktime = BreakTime.ZeroBreakTime;
                                    RemainingBreakTime = TimeSpan.Zero;
                                    TimeToNextBreakTime = TimeSpan.Zero;
                                }
                                else
                                {
                                    if (ms > breaktimes[cBtime > breaktimes.Count - 1 ? breaktimes.Count - 1 : cBtime].End)
                                    {
                                        if (cBtime < breaktimes.Count)
                                        {
                                            TimeToNextBreakTime = TimeSpan.FromMilliseconds(curBreaktime.Start - ms);

                                            cBtime = cBtime == breaktimes.Count - 1 ? cBtime : cBtime + 1;
                                        }
                                        else
                                        {
                                            RemainingBreakTime = TimeSpan.Zero;
                                            TimeToNextBreakTime = TimeSpan.Zero;
                                        }
                                    }
                                    if (curBreaktime.InBreakTime(ms))
                                    {
                                        RemainingBreakTime = TimeSpan.FromMilliseconds(curBreaktime.End - ms);
                                        TimeToNextBreakTime = TimeSpan.Zero;
                                    }
                                    else
                                    {
                                        RemainingBreakTime = TimeSpan.Zero;
                                        TimeToNextBreakTime = TimeSpan.FromMilliseconds(curBreaktime.Start - ms);
                                    }
                                }
                            }
                           
                        }
                        /*catch(IndexOutOfRangeException)
                        {
                            IO.CurrentIO.Write("BreakTime List Index out of range.");
                        }*/
                    }
                    else
                    {
                        RemainingBreakTime = TimeToNextBreakTime = TimeSpan.Zero;
                    }
                }
                else
                {
                    RemainingBreakTime = TimeToNextBreakTime = TimeSpan.Zero;
                    if (timepoints.Count > 0)
                    {
                        CurrentBPM = timepoints[0].BPM;
                    }
                }
            }
            if (tmper > 1)
            {
                tmper = 1;
            }
            if (CurrentStatus != OsuGameStatus.Playing)
            {
                tmper = 0;
            }
            if (CurrentStatus == OsuGameStatus.Rank && LastStatus == OsuGameStatus.Playing) tmper = 1;
            time = ms;
            cur = TimeSpan.FromMilliseconds(ms);
        }
        private void Lm_OnScoreChanged(int obj)
        {
            try
            {
                double current = obj - score;
                double retryflag = 0;
                score = obj;
                bool ScoreZeroed = obj == 0;
                if (CurrentStatus == OsuGameStatus.SelectSong)
                    retryflag++;
                if (ScoreZeroed && retryflag >= 0 && PlayerMaxCombo != 0)
                {
                    if (CurrentStatus != OsuGameStatus.Rank)
                    {
                        if (InDebugMode())
                            IO.CurrentIO.WriteColor($"[osuTools] Retry at {time}ms", ConsoleColor.Yellow);
                        rt++;
                        OnRetry(this, rt);
                    }
                }
            }
            catch
            {

            }
        }
        bool noFailTriggered = false;
        private void Lm_OnHealthPointChanged(double hp)
        {
            this.hp = hp;
            bool NoFail = !CanFail;
            bool IsPlaying = gs.CurrentStatus == OsuGameStatus.Playing && gs.LastStatus != OsuGameStatus.Playing;
            bool ModsAreValid = string.IsNullOrEmpty(ModShortNames) ? false : !ModShortNames.Contains("Unknown");
            if (hp <= 0 && Score > 0 && CanFail && IsPlaying && ModsAreValid)
            {
                System.Threading.Tasks.Task.Run(() => {
                    if (!noFailTriggered)
                    {
                        OnFail(this);
                        noFailTriggered = true;
                        System.Threading.Thread.Sleep(2000);
                        noFailTriggered = false;
                    }

                });
            }
            if (hp <= 0 && Score > 0 && NoFail && IsPlaying)
            {
                OnNoFail(this);
            }
        }
        private void Lm_OnAccuracyChanged(double acc)
        {
            this.acc = acc;
        }
    }
}