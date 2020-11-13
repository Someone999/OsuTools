namespace osuTools.StoryBoard.Command
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using osuTools.Skins.Colors;

    
    public class ColorTranslation:ITranslation
    {
        public RGBColor StartColor { get;  set; }
        public RGBColor TargetColor { get;  set; }
        public int StartTime { get;  set; }
        public int EndTime { get;  set; }
        public ColorTranslation(RGBColor start,RGBColor target,int starttm,int endtm)
        {
            StartColor = start;
            TargetColor = target;
        }
    }
    /// <summary>
    /// 颜色变换
    /// </summary>
    public class Color : IStoryBoardSubCommand, IDurable, IHasEasing, IShortcutableCommand
    {
        /// <summary>
        /// 子命令
        /// </summary>
        public List<IStoryBoardSubCommand> SubCommands { get; set; } = new List<IStoryBoardSubCommand>();
        /// <summary>
        /// 开始时间
        /// </summary>
        public int StartTime { get;  set; }
        public List<ITranslation> Translations { get;  set; } = new List<ITranslation>();
        /// <summary>
        /// 结束时间
        /// </summary>
        public int EndTime { get;  set; }
        /// <summary>
        /// 缓入缓出模式
        /// </summary>
        public StoryBoardEasing Easing { get;  set; }
        /// <summary>
        /// 父命令
        /// </summary>
        public IStoryBoardCommand ParentCommand { get; set; }
        /// <summary>
        /// StoryBoard事件
        /// </summary>
        public StoryBoardEvent Command { get; set; } = StoryBoardEvent.Color;
        /// <summary>
        /// 将字符串解析成Fade
        /// </summary>
        /// <param name="data"></param>
        public void Parse(string data)
        {
            var parts = data.Split(',');
            int eas = 0;
            bool suc = int.TryParse(parts[1], out eas);
            if (suc) Easing = (StoryBoardEasing)eas;
            else Easing = StoryBoardTools.GetStoryBoardEasingByString(parts[1]);
            StartTime = int.Parse(parts[2]);
            var ed = parts[3];
            if (string.IsNullOrEmpty(ed)) parts[3] = parts[2];
               EndTime = int.Parse(parts[3]);
            int i = 4;
            int j = 1;
            if (i + 3 == parts.Length)
                Translations.Add(new ColorTranslation(new RGBColor(int.Parse(parts[4]), int.Parse(parts[5]), int.Parse(parts[6])),
                                 new RGBColor(int.Parse(parts[4]), int.Parse(parts[5]), int.Parse(parts[6])), StartTime, EndTime));
            while(i + 3 < parts.Length)
            {
                int r = int.Parse(parts[i + 1 < parts.Length ? i++ : i]);
                int g = int.Parse(parts[i + 1 < parts.Length ? i++ : i]);
                int b = int.Parse(parts[i + 1 < parts.Length ? i++ : i]);
                int er = int.Parse(parts[i + 1 < parts.Length ? i++ : i == parts.Length ? r : i]);
                int eg = int.Parse(parts[i + 1 < parts.Length ? i++ : i == parts.Length ? r : i]);
                int eb = int.Parse(parts[i + 1 < parts.Length ? i++ : i == parts.Length ? r : i]);
                var dur = EndTime - StartTime;
                Translations.Add(new ColorTranslation(new RGBColor(r, b, g), new RGBColor(er, eb, eg), StartTime + (j * dur), EndTime + (j * dur)));
                j++;
                if (i + 1 < parts.Length)
                    i -= 3;
            }
        }
    }
}