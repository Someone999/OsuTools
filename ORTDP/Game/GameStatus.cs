namespace osuTools
{
    /// <summary>
    /// 记录游戏的状态
    /// </summary>
    [System.Serializable]
    public partial class GMStatus
    {
        OsuGameStatus l, m;
        /// <summary>
        /// 上一次的状态
        /// </summary>
        public OsuGameStatus LastStatus { get => l; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public OsuGameStatus CurrentStatus { get => m; }
        /// <summary>
        /// 使用两个<see cref="OsuRTDataProvider.Listen.OsuListenerManager.OsuStatus"/>构造一个GMStatus
        /// </summary>
        /// <param name="Last"></param>
        /// <param name="Now"></param>
        public GMStatus(OsuRTDataProvider.Listen.OsuListenerManager.OsuStatus Last, OsuRTDataProvider.Listen.OsuListenerManager.OsuStatus Now)
        {
            l = (OsuGameStatus)Last;
            m= (OsuGameStatus)Now;
        }
    }
   

}