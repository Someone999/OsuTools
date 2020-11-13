using System.Collections.Generic;
using System.Drawing;
using osuTools.StoryBoard;
using osuTools.StoryBoard.Command;
using System.Collections.Generic;
using System.Drawing;
public class MoveYTranslation : ITranslation
{
    /// <summary>
    /// 起始点
    /// </summary>
    public double StartPoint { get;  set; }
    /// <summary>
    /// 终点
    /// </summary>
    public double TargetPoint { get;  set; }
    public int StartTime { get;  set; }
    public int EndTime { get;  set; }
    /// <summary>
    /// 使用变化开始时的Y坐标，变化结束时的Y坐标，开始时间和结束时间初始化一个MoveYTranslation
    /// </summary>
    /// <param name="start"></param>
    /// <param name="target"></param>
    /// <param name="sttm"></param>
    /// <param name="edtm"></param>
    public MoveYTranslation(double start, double target, int sttm, int edtm)
    {
        StartPoint = start;
        TargetPoint = target;
        StartTime = sttm;
        EndTime = edtm;
    }
}
public class MoveY : IStoryBoardSubCommand, IDurable, IHasEasing, IShortcutableCommand
{
    public StoryBoardEvent Command { get; } = StoryBoardEvent.MoveY;
    public List<IStoryBoardSubCommand> SubCommands { get;  set; }
    public IStoryBoardCommand ParentCommand { get;  set; }
    public int StartTime { get;  set; }
    public int EndTime { get;  set; }
    public List<ITranslation> Translations { get;  set; } = new List<ITranslation>();
    public StoryBoardEasing Easing { get;  set; }
    public void Parse(string line)
    {
        string[] parts = line.Split(',');
        int eas = 0;
        if (int.TryParse(parts[1], out eas))
            Easing = (StoryBoardEasing)eas;
        else
            Easing = StoryBoardTools.GetStoryBoardEasingByString(parts[1]);
        var ed = parts[3];
        if (string.IsNullOrEmpty(ed)) parts[3] = parts[2];
        StartTime = int.Parse(parts[2]);
        EndTime = int.Parse(parts[3]);
        int i = 4, j = 0;
        if(i + 1 == parts.Length)
            Translations.Add(new MoveYTranslation(double.Parse(parts[4]), double.Parse(parts[4]), StartTime, EndTime));
        while (i + 1 < parts.Length)
        {
            int stindex = i;
            double st = double.Parse(parts[i++]);
            double end = double.Parse(parts[i + 1 < parts.Length ? i++ : i + 1 == parts.Length ? i : stindex]);
            int dur = EndTime - StartTime;
            Translations.Add(new MoveYTranslation(st, end, StartTime + j * dur, EndTime + j * dur));
            j++;
            if (i + 1 < parts.Length)
                i--;
        }
    }
}