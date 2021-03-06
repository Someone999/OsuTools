﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace osuTools
{
    namespace Online.ApiV1
    {
        /// <summary>
        /// 在线查询的结果
        /// </summary>
        public class QueryResult
        {
            /// <summary>
            /// 从http请求获取到的结果
            /// </summary>
            public JArray Results{ get; private set; }
            /// <summary>
            /// 使用JArray初始化一个QueryResult
            /// </summary>
            /// <param name="jarr"></param>
            public QueryResult(JArray jarr)
            {
                Results = jarr;
            }
            /// <summary>
            /// 创造一个空的QueryResult对象
            /// </summary>
            public QueryResult()
            {
            }
        }
        /// <summary>
        /// 在线查询的通用工具
        /// </summary>
        public static class OnlineQueryTools
        {
            internal static string DefaultOsuApiKey { get; } = "fa2748650422c84d59e0e1d5021340b6c418f62f";
            /// <summary>
            /// 判断值是否在范围内，不包括最大值和最小值
            /// </summary>
            /// <param name="max"></param>
            /// <param name="min"></param>
            /// <param name="value"></param>
            /// <param name="includeEdge"></param>
            /// <returns></returns>
            public static bool InRange(double max,double min,double value,bool includeEdge=true)
            {
                return !includeEdge ? (value > min && value < max) : (value >= min && value <= max);
            }
            /// <summary>
            /// 使用HttpClient向指定的Uri发送请求
            /// </summary>
            /// <param name="target"></param>
            /// <returns>类型为<see cref="QueryResult"/>的查询结果</returns>
            public static QueryResult GetResponse(Uri target)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = target;
                string rslt = client.GetStringAsync(target).Result;
                object obj = Newtonsoft.Json.JsonConvert.DeserializeObject(rslt);
                QueryResult queryResult = null;
                if (obj.GetType() == typeof(JArray))
                {
                    queryResult = new QueryResult((JArray)obj);
                    return queryResult;
                }
                if (obj.GetType() == typeof(JObject))
                {
                    queryResult = new QueryResult();
                    queryResult.Results.Add(obj);
                    return queryResult;
                }
                else
                {
                    queryResult = new QueryResult();
                    return queryResult;
                }
            }
        }
    }
}