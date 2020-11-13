using System.Collections.Generic;
using System.Drawing;
using osuTools.StoryBoard;
using osuTools.StoryBoard.Command;
using System.Collections.Generic;
using System.Drawing;
public class Trigger : ITriggerCommand, IDurable
{
    public StoryBoardEvent Command { get; } = StoryBoardEvent.Trigger;
    public List<IStoryBoardSubCommand> SubCommands { get;  set; }
    public IStoryBoardCommand ParentCommand { get;  set; }
    public string TriggerType { get;  set; }
    public int StartTime { get;  set; }
    public int EndTime { get;  set; }
    public void Parse(string line)
    {
        string[] parts = line.Split(',');
        TriggerType = parts[1];
        var ed = parts[3];
        if (string.IsNullOrEmpty(ed)) parts[3] = parts[2];
        StartTime = int.Parse(parts[2]);
        EndTime = int.Parse(parts[3]);             
    }
}