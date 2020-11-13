using osuTools.ExtraMethods;

namespace osuTools.Skins.Colors
{
    /// <summary>
    /// RGB颜色
    /// </summary>
    public class RGBColor
    {
        /// <summary>
        /// 红色部分
        /// </summary>
        public int R { get; private set; }
        /// <summary>
        /// 绿色部分
        /// </summary>
        public int G { get; private set; }
        /// <summary>
        /// 蓝色部分
        /// </summary>
        public int B { get; private set; }
        /// <summary>
        /// 构造一个rgb均为0的RGB颜色
        /// </summary>
        public RGBColor()
        {
            R = 0;
            B = 0;
            G = 0;
        }
        /// <summary>
        /// 使用指定的rgb构造一个颜色
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public RGBColor(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }
        static bool isdig(char c) => c >= '0' && c <= '9';
        /// <summary>
        /// 将字符串转换成RGBColor
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static RGBColor Parse(string s)
        {
            char spliter = (char)0;
            foreach (var ch in s)
                if (!isdig(ch) && ch != ' ')
                {
                    spliter = ch;
                    break;
                }
            var vals=s.Split(spliter);
            RGBColor c = new RGBColor(int.Parse(vals[0]), int.Parse(vals[1]), int.Parse(vals[2]));
            return c;
        }
        public static bool operator ==(RGBColor a, RGBColor b) => a.R == b.R && a.B == b.B && a.G == b.G;
        public static bool operator !=(RGBColor a, RGBColor b) => a.R != b.R || a.B != b.B || a.G == b.G;
        /// <summary>
        /// 获取RGBColor的Hash，返回R*B*G
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return R * B * G ;
        }
    }
    /// <summary>
    /// 带有透明度的RGB颜色
    /// </summary>
    public class RGBAColor:RGBColor
    {
        /// <summary>
        /// 初始化一个rbga都为0的RGBAColor
        /// </summary>
        public RGBAColor():base(0,0,0)
        {
            Alpha = 0;
        }
        /// <summary>
        /// 透明度
        /// </summary>
        public int Alpha { get; private set; }
        /// <summary>
        /// 使用指定的rgba初始化一个RGBAColor
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="alpha"></param>
        public RGBAColor(int r, int g, int b, int alpha=255) : base(r, g, b)
        {            
            Alpha = alpha;
        }
        /// <summary>
        /// 将字符串转换成RGBAColor
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public new static RGBAColor Parse(string s)
        {
            char spliter = (char)0;
            RGBAColor c=new RGBAColor(0,0,0);
            foreach (var ch in s)
                if (!ch.IsDigit() && ch != ' ')
                {
                    spliter = ch;
                    break;
                }
            var vals = s.Split(spliter);
            if (vals.Length > 3)
            {
                c = new RGBAColor(int.Parse(vals[0]), int.Parse(vals[1]), int.Parse(vals[2]), int.Parse(vals[3]));
            }
            else
            {
                c = new RGBAColor(int.Parse(vals[0]), int.Parse(vals[1]), int.Parse(vals[2]));
            }
            return c;
        }
    }
}