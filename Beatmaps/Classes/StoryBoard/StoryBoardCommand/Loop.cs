namespace osuTools.StoryBoard.Command
{
    using System.Collections.Generic;
    class Loop:ILoopCommand,IHasStartTime
    {
        public IStoryBoardCommand ParentCommand { get;  set; }
        public StoryBoardEvent Command { get; } = StoryBoardEvent.Loop;
        public List<IStoryBoardSubCommand> SubCommands { get;  set; } = new List<IStoryBoardSubCommand>();
        public int StartTime { get;  set; }
        public int LoopCount { get;  set; }
        public void Parse(string data)
        {
            var parts = data.Split(',');
            StartTime = int.Parse(parts[1]);
            LoopCount = int.Parse(parts[2]);
        }
    }
}