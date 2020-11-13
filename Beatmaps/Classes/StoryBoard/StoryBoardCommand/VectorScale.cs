using osuTools.StoryBoard;
using osuTools.StoryBoard.Command;

using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

public class VectorScaleMultiplier
{
    public double x { get; set; } = 1;
    public double y { get; set; } = 1;
    public VectorScaleMultiplier(double x,double y)
    {
        this.x = x;
        this.y = y;
    }
}
public class VectorScaleTranslation : ITranslation
{
    public VectorScaleMultiplier StartScaleMultiplier { get;  set; }
    public VectorScaleMultiplier TargetScaleMultiplier { get;  set; }
    public int StartTime { get; set; }
    public int EndTime { get;  set; }
    public VectorScaleTranslation(VectorScaleMultiplier start,VectorScaleMultiplier tar,int sttm,int edtm)
    {
        StartScaleMultiplier = start;
        TargetScaleMultiplier = tar;
        StartTime = sttm;
        EndTime = edtm;
    }
}
public class VectorScale : IStoryBoardSubCommand, IDurable, IHasEasing,IShortcutableCommand
{
    public StoryBoardEvent Command { get; } = StoryBoardEvent.VectorScale;
    public List<IStoryBoardSubCommand> SubCommands { get;  set; }
    public IStoryBoardCommand ParentCommand { get;  set; }
    public int StartTime { get;  set; }
    public int EndTime { get;  set; }
    public List<ITranslation> Translations { get;  set; } = new List<ITranslation>();
    public StoryBoardEasing Easing { get;  set; }
    public void Parse(string line)
    {
        string[] datas = line.Split(',');
        int eas = 0;
        if (int.TryParse(datas[1], out eas))
            Easing = (StoryBoardEasing)eas;
        else
            Easing = StoryBoardTools.GetStoryBoardEasingByString(datas[1]);
        if (string.IsNullOrEmpty(datas[3])) datas[3] = datas[2];
        StartTime = int.Parse(datas[2]);
        EndTime = int.Parse(datas[3]);
        int i = 4, j = 0;
        if (i + 2 == datas.Length)
            Translations.Add(new VectorScaleTranslation(new VectorScaleMultiplier(double.Parse(datas[4]), double.Parse(datas[5])),
                                                        new VectorScaleMultiplier(double.Parse(datas[4]), double.Parse(datas[5])), 
                                                        StartTime, EndTime));
        while(i + 2 < datas.Length)
        {
            int stindex = i;
            int initindex = i;
            double xst = double.Parse(datas[i++]);
            double xed = double.Parse(datas[i + 1 < datas.Length ? i++ : i + 1 == datas.Length ? i : stindex]);
            double yst = double.Parse(datas[i + 1 < datas.Length ? i++ : i + 1 == datas.Length ? i : initindex]);
            double yed = double.Parse(datas[i + 1 < datas.Length ? i++ : i + 1 == datas.Length ? i : initindex]);
            int dur = EndTime - StartTime;
            Translations.Add(new VectorScaleTranslation(new VectorScaleMultiplier(xst, xed), new VectorScaleMultiplier(yst, yed), StartTime + j * dur, EndTime + j * dur));
            j++;
            if (i + 1 < datas.Length)
                i -= 2;

        }
    }
}