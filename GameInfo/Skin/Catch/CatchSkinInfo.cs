﻿using osuTools.Skins.Colors;

namespace osuTools.Skins.Settings.Catch
{
    /// <summary>
    /// 接水果的皮肤图片
    /// </summary>
    public class CatchSkinSetting
    {
        /// <summary>
        /// 快速冲刺时覆盖显示的颜色
        /// </summary>
        public RGBColor HyperDash { get; internal set; } = new RGBColor(255, 0, 0);
        /// <summary>
        /// 需要快速冲刺才能接到下一个水果时，当前水果覆盖显示的颜色
        /// </summary>
        public RGBColor HyperDashFruit { get; internal set; }
        /// <summary>
        /// 接水果容器皮肤图片的残影的颜色
        /// </summary>
        public RGBColor HyperDashAfterImage { get; internal set; }
        /// <summary>
        /// 创建一个空的CatchSkinSetting对象
        /// </summary>
        public CatchSkinSetting()
        {
            HyperDashAfterImage = HyperDashFruit = HyperDash;
        }
    }
}