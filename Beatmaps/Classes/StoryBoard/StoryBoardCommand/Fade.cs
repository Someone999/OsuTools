namespace osuTools.StoryBoard.Command
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Forms;
    public class FadeTranslation : ITranslation
    {
        /// <summary>
        /// 变化开始时的透明度。值在0-1之间
        /// </summary>
        public double StartOpacity { get;  set; }
        /// <summary>
        /// 变化结束时的透明度。值在0-1之间
        /// </summary>
        public double TargetOpacity { get;  set; }
        public int StartTime { get;  set; }
        public int EndTime { get;  set; }
        public FadeTranslation(double start,double target,int starttm,int endtm)
        {
            StartOpacity = start;
            TargetOpacity = target;
        }
    }

    /// <summary>
    /// 渐隐
    /// </summary>
    public class Fade:IStoryBoardSubCommand, IDurable, IHasEasing, IShortcutableCommand
    {
        
        /// <summary>
        /// 子命令
        /// </summary>
        public List<IStoryBoardSubCommand> SubCommands { get;  set; } = new List<IStoryBoardSubCommand>();
        /// <summary>
        /// 开始时间
        /// </summary>
        public int StartTime { get;  set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public int EndTime { get;  set; }
        public List<ITranslation> Translations { get;  set; } = new List<ITranslation>();
        /// <summary>
        /// 缓入缓出模式
        /// </summary>
        public StoryBoardEasing Easing { get;  set; }
        /// <summary>
        /// 父命令
        /// </summary>
        public IStoryBoardCommand ParentCommand { get;  set; }
        /// <summary>
        /// StoryBoard事件
        /// </summary>
        public StoryBoardEvent Command { get;  set; } = StoryBoardEvent.Fade;
        /// <summary>
        /// 将字符串解析成Fade
        /// </summary>
        /// <param name="data"></param>
        public void Parse(string data)
        {
            while (data.StartsWith(" "))
              data = data.Remove(0, 1);
            string[] parts = data.Split(',');
            if (parts[0] != "F") throw new ArgumentException("该行的数据不适用。");
            int eas = 0;
            var ed = parts[3];
            if (string.IsNullOrEmpty(ed))parts[3] = parts[2];
            bool suc=int.TryParse(parts[1], out eas);
            if (suc) Easing = (StoryBoardEasing)eas;
            else Easing = StoryBoardTools.GetStoryBoardEasingByString(parts[1]);
            StartTime = int.Parse(parts[2]);
            EndTime = int.Parse(parts[3]);
            int i = 4;
            int j = 1;
            if (i + 1 == parts.Length)
                Translations.Add(new FadeTranslation(double.Parse(parts[4]), double.Parse(parts[4]), StartTime, EndTime));
            while(i + 1 < parts.Length)
            {
                double end = 0;
                int stindex = i;
                double st = double.Parse(parts[i++]);
                end = double.Parse(parts[i + 1 < parts.Length ? i++ : i + 1 == parts.Length ? i: stindex]);               
                var dur = StartTime - EndTime;
                Translations.Add(new FadeTranslation(st, end, StartTime + dur * j, EndTime + dur * j));
                if (i + 1 < parts.Length) i--;
                j++;
            }
        }
    }
}