﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osuTools.Beatmaps.HitObject;
namespace osuTools.StoryBoard
{
    /// <summary>
    /// StoryBoard中，可移动的图片
    /// </summary>
    public class Sprite:IStoryBoardImage
    {
        /// <summary>
        /// 相对于Origin的位置
        /// </summary>
        public OsuPixel Position { get;  set; }
        /// <summary>
        /// 资源类型
        /// </summary>
        public StoryBoardResourceType ResourceType { get; } = StoryBoardResourceType.Sprite;
        /// <summary>
        /// StoryBoard的初步位置
        /// </summary>
        public StoryBoardOrigin Origin { get;  set; }
        /// <summary>
        /// 数据的特征字符
        /// </summary>
        public string DataIdentifier { get; } = "Sprite";
        /// <summary>
        /// 资源的相对路径
        /// </summary>
        public string Path { get;  set; } = "";
        /// <summary>
        /// 相对于开始时间的播放时间
        /// </summary>
        public int Offset { get; set; } = -1;
        /// <summary>
        /// 数据的预计项数
        /// </summary>
        public int ExcpectLength { get;  set; }  = 6;
        /// <summary>
        /// 图层
        /// </summary>
        public StoryBoardLayer Layer { get;  set; }
        /// <summary>
        /// 使用字符串构造一个Sprite对象
        /// </summary>
        /// <param name="dataline"></param>
        public void Parse(string dataline)
        {
            if (!dataline.StartsWith("Sprite,"))
            {
                throw new osuToolsException.FailToParseException("该行的数据不适用。");
            }
            else
            {
                bool suc = false;
                string[] data = dataline.Split(',');
                string sprite = data[0];
                int layer = 0;
                suc=int.TryParse(data[1],out layer);
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
                Position=new OsuPixel(double.Parse(data[4]), double.Parse(data[5]));
            }
        }
        /// <summary>
        /// 描述一个Sprite对象的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Type:{ResourceType} File:{Path} Layer:{Layer} Offset:{Offset} x:{Position.x} y={Position.y}";
        }
    }
}
