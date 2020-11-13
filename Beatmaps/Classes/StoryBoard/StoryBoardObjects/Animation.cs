namespace osuTools.StoryBoard
{
    public class Animation : IStoryBoardAnimation
    {
        public Beatmaps.HitObject.OsuPixel Position { get;  set; }
        public StoryBoardResourceType ResourceType { get; } = StoryBoardResourceType.Animation;
        public StoryBoardOrigin Origin { get;  set; }
        public string DataIdentifier { get; } = "Animation";
        public string Path { get;  set; } = "";
        public int Offset { get; set; } = -1;
        public int ExcpectLength { get;  set; } = 9;
        public StoryBoardLayer Layer { get;  set; }
        public double FrameCount { get;  set; }
        public double FrameDelay { get;  set; }
        public StoryBoardAnimationLoopType LoopType { get;  set; }
         
        public void Parse(string dataline)
        {
            if (!dataline.StartsWith("Animation,"))
            {
                throw new osuToolsException.FailToParseException("该行的数据不适用。");
            }
            else
            {
                bool suc = false;
                string[] data = dataline.Split(',');
                string sprite = data[0];
                int layer = 0;
                suc = int.TryParse(data[1], out layer);
                if (!suc)
                    Layer = StoryBoardTools.GetLayerByString(data[1]);
                else
                {
                    Layer = (StoryBoardLayer)layer;
                    suc = false;
                }
                int origin = 0;
                suc = int.TryParse(data[2], out origin);
                if (!suc)
                    Origin = StoryBoardTools.GetOriginByString(data[2]);
                else
                    Origin = (StoryBoardOrigin)(origin);
                Path = data[3].Trim('\"');
                Position = new Beatmaps.HitObject.OsuPixel(double.Parse(data[4]), double.Parse(data[5]));
                FrameCount = double.Parse(data[6]);
                FrameDelay = double.Parse(data[7]);
                int loopType = 0;
                suc = int.TryParse(data[8], out loopType);
                if (!suc)
                    LoopType = StoryBoardTools.GetLoopTypeByString(data[8]);
                else
                    LoopType = (StoryBoardAnimationLoopType)loopType;
            }
        }
        public override string ToString()
        {
            return $"Type:{ResourceType} File:{Path} Layer:{Layer} Offset:{Offset} x:{Position.x} y={Position.y} FrameCount:{FrameCount} LoopType:{LoopType}";
        }
    }
}