﻿namespace osuTools.Beatmaps
{
    using Skins.Tools;
    using ExtraMethods;
    using System;
    using osuToolsException;
    partial class Beatmap
    {

        void getAddtionalInfo(string[] dataPares)
        {
            foreach (var d in dataPares)
            {
                try
                {
                    var data = d.Split(':');
                    if (data[0].Contains("AudioLeadIn:"))
                    {
                        double val = 0;
                        double.TryParse(data[1].Trim(), out val);
                        AudioLeadIn = val;
                        continue;
                    }
                    if (data[0].Contains("PreviewTime:"))
                    {
                        double val = 0;
                        double.TryParse(data[1].Trim(), out val);
                        PreviewTime = val;
                        continue;
                    }
                    if (data[0].Contains("Countdown:"))
                    {
                        int val = 0;
                        int.TryParse(data[1].Trim(), out val);
                        HasCountdown = val.ToBool();
                        continue;

                    }
                    if (data[0].Contains("SampleSet:"))
                    {
                        int val = 0;
                        if (int.TryParse(data[1].Trim(), out val))
                            SampleSet = (SampleSets)val;
                        else
                            SampleSet = SkinTools.StringToEnum<SampleSets>(data[1].Trim());
                        continue;
                    }
                    if (data[0].Contains("StackLeniency:"))
                    {
                        double val = 0;
                        double.TryParse(data[1].Trim(), out val);
                        StackLeniency = val;
                        continue;
                    }
                    if (data[0].Contains("LetterboxInBreaks:"))
                    {
                        int val = 0;
                        int.TryParse(data[1].Trim(), out val);
                        LetterboxInBreaks = val.ToBool();
                        continue;
                    }
                    if (data[0].Contains("WidescreenStoryboard"))
                    {
                        int val = 0;
                        int.TryParse(data[1].Trim(), out val);
                        WidescreenStoryboard = val.ToBool();
                        continue;
                    }
                    if (data[0].Contains("Bookmarks"))
                    {

                        string bookmarks = data[1].Trim();
                        if (bookmarks == "0")
                            return;
                        string[] offsets = bookmarks.Split(',');
                        foreach (var offset in offsets)
                        {
                            try
                            {
                                int val = 0;
                                val = int.Parse(offset);
                                Bookmarks.Add(val);
                            }
                            catch (Exception ex)
                            {
                                throw new FailToParseException("无法通过Bookmarks标签获取书签。", ex);
                            }

                        }
                        continue;
                    }
                    if (data[0].Contains("DistanceSpacing"))
                    {
                        double val = 0;
                        double.TryParse(data[1].Trim(), out val);
                        DistanceSpacing = val;
                        continue;
                    }
                    if (data[0].Contains("BeatDivisor"))
                    {
                        double val = 0;
                        double.TryParse(data[1].Trim(), out val);
                        BeatDivisor = val;
                        continue;
                    }
                    if (data[0].Contains("GridSize"))
                    {
                        double val = 0;
                        double.TryParse(data[1].Trim(), out val);
                        GridSize = val;
                        continue;
                    }
                    if (data[0].Contains("TimelineZoom"))
                    {
                        double val = 0;
                        double.TryParse(data[1].Trim(), out val);
                        TimelineZoom = val;
                        continue;
                    }
                    if (data[0].Contains("SliderMultiplier"))
                    {
                        double val = 0;
                        double.TryParse(data[1].Trim(), out val);
                        SliderMultiplier = val;
                        continue;
                    }
                    if (data[0].Contains("SliderTickRate"))
                    {
                        double val = 0;
                        double.TryParse(data[1].Trim(), out val);
                        SliderTickRate = val;
                        continue;
                    }
                }
                catch(Exception ex)
                {
                    throw new FailToParseException("从谱面文件获取信息失败。", ex);
                }
            }
        }
    }
}