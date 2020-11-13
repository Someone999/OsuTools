namespace osuTools.StoryBoard.Command
{
    using System.Collections.Generic;
    /// <summary>
    /// 表示一个StoryBoard的命令
    /// </summary>
    public interface IStoryBoardCommand
    {
        /// <summary>
        /// 将字符串解析为命令
        /// </summary>
        /// <param name="data"></param>
        void Parse(string data);
        /// <summary>
        /// 子命令列表
        /// </summary>
        List<IStoryBoardSubCommand> SubCommands { get; }
    }
    /// <summary>
    /// 表示一个变换命令
    /// </summary>
    public interface ITranslation:IDurable
    {

    }
    /// <summary>
    /// 表示一个可缩写的命令
    /// </summary>
    public interface IShortcutableCommand
    {
        /// <summary>
        /// 可缩写命令中包含的变换
        /// </summary>
        List<ITranslation> Translations { get; }
    }
    /// <summary>
    /// 有开始时间的命令
    /// </summary>
    public interface IHasStartTime
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        int StartTime { get; }
    }
    /// <summary>
    /// 有结束时间的命令
    /// </summary>
    public interface IHasEndTime
    {
        /// <summary>
        /// 结束时间
        /// </summary>
        int EndTime { get; }
    }
    /// <summary>
    /// 会持续的命令
    /// </summary>
    public interface IDurable:IHasStartTime,IHasEndTime
    {

    }
    /// <summary>
    /// 有渐变的命令
    /// </summary>
    public interface IHasEasing
    {
        /// <summary>
        /// 渐变方式
        /// </summary>
        StoryBoardEasing Easing { get; }
    }
    /// <summary>
    /// 表示一个StroyBoard子命令
    /// </summary>
    public interface IStoryBoardSubCommand:IStoryBoardCommand
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        StoryBoardEvent Command { get; }
        
        /// <summary>
        /// 父命令
        /// </summary>
        IStoryBoardCommand ParentCommand { get; }
    }
    /// <summary>
    /// 表示一个StoryBoard的主命令
    /// </summary>
    public interface IStoryBoardMainCommand:IStoryBoardCommand
    {
        /// <summary>
        /// StoryBoard资源类型
        /// </summary>
        StoryBoardResourceType ResourceType { get; }
        /// <summary>
        /// StoryBoard资源
        /// </summary>
        IStoryBoardResource Resource { get; }
    }
    /// <summary>
    /// 表示一个触发器命令
    /// </summary>
    public interface ITriggerCommand:IStoryBoardSubCommand,IDurable
    {
        /// <summary>
        /// 触发器类型
        /// </summary>
        string TriggerType { get; }
       
    }
    /// <summary>
    /// 表示一个循环命令
    /// </summary>
    public interface ILoopCommand:IStoryBoardSubCommand,IHasStartTime
    {
       
        /// <summary>
        /// 循环次数
        /// </summary>
        int LoopCount { get; }
    }

}