﻿using osuTools.Online.ApiV2.Authorization;
using System.Net;
using System.IO;
using System;

namespace osuTools.Online.ApiV2
{
    /// <summary>
    /// 用于向OsuApiV2查询谱面
    /// </summary>
    public class OnlineBeatmapQueryV2
    {
        /// <summary>
        /// <see cref="OsuApiV2Token"/>，可以通过<see cref="OsuApiV2Authorization"/>来获取
        /// </summary>
        public OsuApiV2Token Token { get; set; }
        /// <summary>
        /// 谱面ID
        /// </summary>
        public int BeatmapID { get; set; }
        void getResult()
        {
            if(Token==null) throw new ArgumentNullException("必须指定一个Token。Token可以从Online.ApiV2.Authorization.OsuApiV2Authorization获取。");
            string uri = $"https://osu.ppy.sh/api/v2/beatmaps/{BeatmapID}";
            HttpWebRequest request = WebRequest.CreateHttp(uri);
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {Token.AccessToken}");
            StreamReader r = new StreamReader(request.GetResponse().GetResponseStream());
            string recvjson = r.ReadToEnd();
        }
    }
}