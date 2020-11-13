using osuTools.StoryBoard;
using osuTools.StoryBoard.Command;
using System;
using System.Collections.Generic;
using System.Drawing;
public class Degrees
{
    /// <summary>
    /// 角度
    /// </summary>
    public double Degree { get; set; } = 0;
    /// <summary>
    /// 弧度
    /// </summary>
    public double Radians { get; set; } = 0;
    public Degrees(double val,bool isDegree)
    {
        if (isDegree)
        {
            Degree = val;
            Radians = (Math.PI / 180) * val;
        }
        else
        {
            Radians = val;
            Degree = (180 / Math.PI) * val;
        }
    }
}
public class RotateTranslation : ITranslation
{
    public Degrees StartDegree { get;  set; }
    public Degrees TargetDegree { get;  set; }
    public int StartTime { get;  set; }
    public int EndTime { get;  set; }
    public RotateTranslation(Degrees start,Degrees tar,int sttm,int edtm)
    {
        StartDegree = start;
        TargetDegree = tar;
        StartTime = sttm;
        EndTime = edtm;
    }

}
public class Rotate : IStoryBoardSubCommand, IDurable, IHasEasing,IShortcutableCommand
{
    public StoryBoardEvent Command { get; } = StoryBoardEvent.Rotate;
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
        if (string.IsNullOrEmpty(parts[3])) parts[3] = parts[2];
        EndTime = int.Parse(parts[3]);
        int i = 4;
        int j = 0;

        if (i + 1 == parts.Length)
            Translations.Add(new RotateTranslation(new Degrees(double.Parse(parts[4]), false), new Degrees(double.Parse(parts[4]), false), StartTime, EndTime));
        while(i + 1 < parts.Length)
        {
            int stindex = i;
            double st = double.Parse(parts[i++]);
            double ed = double.Parse(parts[i + 1 < parts.Length ? i++ : i + 1 == parts.Length ? i : stindex]);
            int du = EndTime - StartTime;
            Translations.Add(new RotateTranslation(new Degrees(st, false), new Degrees(ed, false), StartTime + j * du, EndTime + j * du));
            j++;
            if (i + 1 < parts.Length)
                i--;
        }

    }
}