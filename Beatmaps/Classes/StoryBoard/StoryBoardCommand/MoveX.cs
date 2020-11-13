using osuTools.StoryBoard;
using osuTools.StoryBoard.Command;
using System.Collections.Generic;
using System.Drawing;
public class MoveXTranslation : ITranslation
{
    public double StartPoint { get;  set; }
    public double TargetPoint { get;  set; }
    public int StartTime { get;  set; }
    public int EndTime { get;  set; }
    public MoveXTranslation(double start, double target, int sttm, int edtm)
    {
        StartPoint = start;
        TargetPoint = target;
        StartTime = sttm;
        EndTime = edtm;
    }
}
public class MoveX:IStoryBoardSubCommand, IDurable, IHasEasing,IShortcutableCommand
{
    public StoryBoardEvent Command { get; } = StoryBoardEvent.MoveX;
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
        StartTime = int.Parse(parts[2]);
        if (string.IsNullOrEmpty(parts[3]))parts[3] = parts[2];
        EndTime = int.Parse(parts[3]);
        int i = 4, j = 1;
        if (i + 1 == parts.Length)
            Translations.Add(new MoveXTranslation(double.Parse(parts[4]), double.Parse(parts[4]), StartTime, EndTime));
        while (i + 1 < parts.Length)
        {
            int stindex = i;
            double st=double.Parse(parts[i++]);
            double ed = double.Parse(parts[i + 1 < parts.Length ? i++ : i + 1 == parts.Length ? i : stindex + 1]);
            int dur = EndTime - StartTime;
            Translations.Add(new MoveXTranslation(st, ed, StartTime + j * dur, EndTime + j * dur));
            j++;
            if (i + 1 < parts.Length)
                i--;
        }
    }
}