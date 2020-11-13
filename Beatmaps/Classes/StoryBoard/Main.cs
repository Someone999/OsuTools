namespace osuTools.StoryBoard
{
    using System.Drawing;
    using System.Collections.Generic;
    /// <summary>
    /// 表示StoryBoard可用的资源
    /// </summary>
    public interface IStoryBoardResource
    {
        /// <summary>
        /// StoryBoard资源的种类
        /// </summary>
        StoryBoardResourceType ResourceType { get; }
        /// <summary>
        /// StoryBoard资源的路径
        /// </summary>
        string Path { get; }
        /// <summary>
        /// StoryBoard资源使用的时间点与开始时的偏移
        /// </summary>
        int Offset { get; }
        /// <summary>
        /// 将字符串解析为IStoryBoardResource
        /// </summary>
        /// <param name="data"></param>
        void Parse(string data);
        /// <summary>
        /// 数据的特征字符串
        /// </summary>
        string DataIdentifier { get; }
        /// <summary>
        /// 预计的数据的项数
        /// </summary>
        int ExcpectLength { get; }
    }
    /// <summary>
    /// StoryBoard的图像资源
    /// </summary>
    public interface IStoryBoardImage:IStoryBoardResource
    {
        /// <summary>
        /// 图片的位置
        /// </summary>
        Beatmaps.HitObject.OsuPixel Position { get; }
    }
    /// <summary>
    /// StoryBoard的动画资源
    /// </summary>
    public interface  IStoryBoardAnimation:IStoryBoardResource
    {
        /// <summary>
        /// 动画的位置
        /// </summary>
        Beatmaps.HitObject.OsuPixel Position { get; }
        /// <summary>
        /// 动画的帧数
        /// </summary>
        double FrameCount { get; }
        /// <summary>
        /// 动画帧与帧之间的间隔
        /// </summary>
        double FrameDelay { get; }
        /// <summary>
        /// 动画的循环类型
        /// </summary>
        StoryBoardAnimationLoopType LoopType { get; }
    }
   
    
}