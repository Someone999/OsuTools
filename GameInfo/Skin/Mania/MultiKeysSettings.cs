﻿using osuTools.Skins.SkinObjects.Mania;
using System;
using System.Runtime.CompilerServices;

namespace osuTools.Skins.Settings.Mania
{
    /// <summary>
    /// 多个键位的设置
    /// </summary>
    public class MultipleKeysSettings
    {
        public ManiaSkinSetting Key1 { get; internal set; } = new ManiaSkinSetting();
        public ManiaSkinSetting Key2 { get; internal set; } = new ManiaSkinSetting();
        public ManiaSkinSetting Key3 { get; internal set; } = new ManiaSkinSetting();
        public ManiaSkinSetting Key4 { get; internal set; } = new ManiaSkinSetting();
        public ManiaSkinSetting Key5 { get; internal set; } = new ManiaSkinSetting();
        public ManiaSkinSetting Key6 { get; internal set; } = new ManiaSkinSetting();
        public ManiaSkinSetting Key7 { get; internal set; } = new ManiaSkinSetting();
        public ManiaSkinSetting Key8 { get; internal set; } = new ManiaSkinSetting();
        public ManiaSkinSetting Key9 { get; internal set; } = new ManiaSkinSetting();
        public ManiaSkinSetting Key10 { get; internal set; } = new ManiaSkinSetting();
        public ManiaSkinSetting Key12 { get; internal set; } = new ManiaSkinSetting();
        public ManiaSkinSetting Key14 { get; internal set; } = new ManiaSkinSetting();
        public ManiaSkinSetting Key16 { get; internal set; } = new ManiaSkinSetting();
        public ManiaSkinSetting Key18 { get; internal set; } = new ManiaSkinSetting();
        /// <summary>
        /// 通过键的数目获取相应的键位设置
        /// </summary>
        /// <param name="keyCount">键位的数量，有效值为1-10，12，14，16，18</param>
        /// <returns></returns>
        public ManiaSkinSetting this[int keyCount]
        {
            get
            {
                switch (keyCount)
                {
                    case 1: return Key1;
                    case 2: return Key2;
                    case 3: return Key3;
                    case 4: return Key4;
                    case 5: return Key5;
                    case 6: return Key6;
                    case 7: return Key7;
                    case 8: return Key8;
                    case 9: return Key9;
                    case 10: return Key10;
                    case 12: return Key12;
                    case 14: return Key14;
                    case 16: return Key16;
                    case 18: return Key18;
                    default: throw new ArgumentException();
                }
            }
            set
            {
                switch (keyCount)
                {
                    case 1: Key1 = value; break;
                    case 2: Key2 = value; break;
                    case 3: Key3 = value; break;
                    case 4: Key4 = value; break;
                    case 5: Key5 = value; break;
                    case 6: Key6 = value; break;
                    case 7: Key7 = value; break;
                    case 8: Key8 = value; break;
                    case 9: Key9 = value; break;
                    case 10: Key10 = value; break;
                    case 12: Key12 = value; break;
                    case 14: Key14 = value; break;
                    case 16: Key16 = value; break;
                    case 18: Key18 = value; break;
                    default: throw new ArgumentException();
                }
            }
        }
        /// <summary>
        /// 为特定的键位添加设置
        /// </summary>
        /// <param name="key">键位的数量，有效值为1-10，12，14，16，18</param>
        /// <param name="setting"></param>
        public void SetForKey(int key,ManiaSkinSetting setting)
        {
            switch(key)
            {
                case 1:Key1 = setting;break;
                case 2: Key2 = setting; break;
                case 3: Key3 = setting; break;
                case 4: Key4 = setting; break;
                case 5: Key5 = setting; break;
                case 6: Key6 = setting; break;
                case 7: Key7 = setting; break;
                case 8: Key8 = setting; break;
                case 9: Key9 = setting; break;
                case 10: Key10 = setting; break;
                case 12: Key12 = setting; break;
                case 14: Key14 = setting; break;
                case 16: Key16 = setting; break;
                case 18: Key18 = setting; break;

            }
        }
    }
}