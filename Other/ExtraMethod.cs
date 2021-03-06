﻿namespace osuTools.ExtraMethods
{
    using System;
    using osuTools.Skins.Colors;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using System.Text;
    using osuTools.Game.Mods;
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ExtraMethod
    {
        /// <summary>
        /// 将Mod数组转换成ModList
        /// </summary>
        /// <param name="modarr"></param>
        /// <returns></returns>
        public static ModList ToModList(this Mod[] modarr)
        {
            return ModList.FromModArray(modarr);
        }
        public static DateTime? ToNullableDateTime(this string s)
        {
            DateTime d;
            return DateTime.TryParse(s, out d) ? (DateTime?)d : null;
        }
        public static DateTime ToDateTime(this string s) => DateTime.Parse(s);
        public static bool ToBool(this int i) => Convert.ToBoolean(i);
        public static bool ToBool(this string i) => 
            Convert.ToBoolean(i == "1"||string.Equals(i,"True",StringComparison.OrdinalIgnoreCase) ? "True" : "False");
        public static bool? ToNullableBool(this string i) => string.IsNullOrEmpty(i) ? null : (bool?)Convert.ToBoolean(i == "1" ? "True" : "False");
        public static int ToInt32(this string i) => int.Parse(i);
        public static int? ToNullableInt32(this string i) => string.IsNullOrEmpty(i) ? null : (int?)int.Parse(i);
        public static uint ToUInt32(this string i) => uint.Parse(i);
        public static uint? ToNullableUInt32(this string i) => string.IsNullOrEmpty(i) ? null : (uint?)uint.Parse(i);
        public static double ToDouble(this string i) => double.Parse(i);
        public static double? ToNullableDouble(this string i) => string.IsNullOrEmpty(i) ? null : (double?)double.Parse(i);
        public static RGBColor ToRGBColor(this string i) => RGBColor.Parse(i);
        public static RGBAColor ToRGBAColor(this string i) => RGBAColor.Parse(i);
        public static bool IsDigit(this char c) => c >= '0' && c <= '9';
        internal static Keys CheckIndexAndGetValue(this Dictionary<string, Keys> var, string index)
        {
            try
            {
                string tmp =
                        index == "LeftShift" ? "LShiftKey" :
                        index == "RightShift" ? "RShiftKey" :
                        index == "LeftControl" ? "LControlKey" :
                        index == "RightControl" ? "RControlKey" :
                        index == "LeftAlt" ? "LMenu" :
                        index == "RightAlt" ? "RMenu" : index;
                return var[tmp];
            }
            catch (KeyNotFoundException)
            {
                System.Diagnostics.Debug.WriteLine($"找不到键位:{index}");
                return (Keys)(-1);
            }
        }
        internal static OsuGameMod CheckIndexAndGetValue(this Dictionary<string, OsuGameMod> var, string index)
        {
            try
            {
                if (index == "Auto")
                {
                    index = "AutoPlay";
                }
                if (index == "Autopilot")
                {
                    index = "AutoPilot";
                }
                if (index.StartsWith("key"))
                {
                    return var[index.Replace("key", "")];
                }
                return var[index];

            }
            catch (KeyNotFoundException)
            {
                System.Diagnostics.Debug.WriteLine($"不支持的Mod:{index}");
                return OsuGameMod.Unknown;
            }
        }
        internal static Keys CheckIndexAndGetValue(this Dictionary<OsuGameMod, Keys> var, OsuGameMod index)
        {
            try
            {
                var tmp = index;
                if (index.ToString().Contains("Key"))
                {
                    tmp = OsuGameMod.Relax;
                }
                if (index == OsuGameMod.NightCore) tmp = OsuGameMod.DoubleTime;
                if (index == OsuGameMod.Perfect) tmp = OsuGameMod.SuddenDeath;
                if (index == OsuGameMod.FadeIn) tmp = OsuGameMod.Hidden;
                if (index == OsuGameMod.Random) tmp = OsuGameMod.AutoPilot;
                return var[tmp];

            }
            catch (KeyNotFoundException)
            {
                System.Diagnostics.Debug.WriteLine($"不支持的Mod:{index}");
                return (Keys)(-1);
            }
        }
        public static byte[] ToBytes(this string str, Encoding encoding = null)
        {
            
            encoding = encoding ?? Encoding.UTF8;
            return encoding.GetBytes(str);
        }
        public static string GetString(this byte[] b, Encoding encoding = null)
        {

            encoding = encoding ?? Encoding.UTF8;
            return encoding.GetString(b);
        }

    }
}