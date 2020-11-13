namespace osuTools.Beatmaps.HitObject
{
    using System;
    /// <summary>
    /// osu!在游戏中使用的像素
    /// </summary>
    public class OsuPixel
    {
        double _x = 0;
        double _y = 0;
        /// <summary>
        /// 缩放前的x
        /// </summary>
        public double x { get => _x; }
        /// <summary>
        /// 缩放前的y
        /// </summary>
        public double y { get => _y; }
        /// <summary>
        /// 构造一个新OsuPixel对象
        /// </summary>
        public OsuPixel()
        {

        }
        /// <summary>
        /// 使用特定的x和y构造一个新OsuPixel对象
        /// </summary>
        /// <param name="x">指定的x值</param>
        /// <param name="y">指定的y值</param>

        public OsuPixel(double x, double y)
        {
            
            _x = x;
            _y = y;
        }
        /// <summary>
        /// 将osu!Pixel放大至屏幕大小
        /// </summary>
        /// <returns></returns>
        public System.Drawing.Point Scale()
        {
            int Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            int Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - 40;
             return new System.Drawing.Point((int)(x * (Width / 640) - x + 320),(int)(y * (Height / 480) - y + 240));
        }
        /// <summary>
        /// 返回描述osu!Pixel的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"x:{x} y:{y}";
        }
        /// <summary>
        /// 返回以osu!文件中格式为标准的字符串
        /// </summary>
        /// <returns></returns>
        public string GetData()
        {
            return $"{x}:{y}";
        }

    }
}