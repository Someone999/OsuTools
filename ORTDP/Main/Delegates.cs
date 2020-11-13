namespace osuTools
{
    /// <summary>
    /// 使用OsuRTDataProvider进行数据收集的类
    /// </summary>
    partial class ORTDP
    {
        /// <summary>
        /// 失败时触发的的事件
        /// </summary>
        /// <param name="currentStatus"></param>
        public delegate void OnFailedHandler(ORTDP currentStatus);
        /// <summary>
        /// 失败时触发的事件
        /// </summary>
        public event OnFailedHandler OnFail;
        /// <summary>
        /// 开启NoFail时血量≤0时触发的事件
        /// </summary>
        /// <param name="currentStatus"></param>
        public delegate void OnNoFailHandler(ORTDP currentStatus);
        /// <summary>
        /// 开启NoFail时血量≤0时触发的事件
        /// </summary>
        public event OnNoFailHandler OnNoFail;
        /// <summary>
        /// 当分数从非0值变成0时触发的事件
        /// </summary>
        /// <param name="currentStatus"></param>
        /// <param name="timesofRetry"></param>
        public delegate void OnRetryHandler(ORTDP currentStatus, int timesofRetry);
        /// <summary>
        /// 当分数从非0值变成0时触发的时间
        /// </summary>
        public event OnRetryHandler OnRetry;
       
    }
}