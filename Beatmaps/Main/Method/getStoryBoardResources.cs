namespace osuTools.Beatmaps
{
    using System.Collections.Generic;
    using StoryBoard;
    partial class Beatmap
    {
        /// <summary>
        /// 获取指定类型的StoryBoard的命令
        /// </summary>
        /// <typeparam name="T">要获取的命令的资源类型</typeparam>
        /// <returns>包含指定资源信息的列表</returns>
        public List<T> GetStoryBoardResources<T>() where T : IStoryBoardResource, new()
        {
            string[] dirs = System.IO.Directory.GetFiles($"{FullPath.Replace(FileName, "")}\\", "*.osb", System.IO.SearchOption.AllDirectories);
            string[] map = new string[1];
            if (dirs.Length > 0)
            {
                map = System.IO.File.ReadAllLines(dirs[0]);
            }
            else
            {
                map = System.IO.File.ReadAllLines(FullPath);
            }
            List<T> resources = new List<T>();
            DataBlock block = DataBlock.None;

            foreach (var str in map)
            {
                T obj = new T();
                string[] comasp = str.Split(',');
                if (comasp.Length == obj.ExcpectLength && comasp[0] == obj.DataIdentifier)
                {
                    obj.Parse(str);
                    resources.Add(obj);
                }
                else
                {
                    continue;
                }
            }
            return resources;

        }
        /// <summary>
        /// 获取谱面所有的StoryBoard命令
        /// </summary>
        /// <returns>包含</returns>
        public List<IStoryBoardResource> GetStoryBoardResources()
        {
            string[] dirs = System.IO.Directory.GetFiles($"{FullPath.Replace(FileName, "")}\\", "*.osb", System.IO.SearchOption.AllDirectories);
            string[] map = new string[1];
            if (dirs.Length > 0)
            {
                map = System.IO.File.ReadAllLines(dirs[0]);
            }
            else
            {
                map = System.IO.File.ReadAllLines(FullPath);
            }
            List<IStoryBoardResource> resources = new List<IStoryBoardResource>();
            IStoryBoardResource resource = null;
            foreach (var line in map)
            {
                string[] parts = line.Split(',');
                if (parts[0] == "Sprite")
                {
                    resource = new Sprite();
                    resource.Parse(line);
                    resources.Add(resource);
                }
                if (parts[0] == "Animation")
                {
                    resource = new Animation();
                    resource.Parse(line);
                    resources.Add(resource);
                }
                if (parts[0] == "Sample")
                {
                    resource = new Audio();
                    resource.Parse(line);
                    resources.Add(resource);
                }
            }
            return resources;
        }
    }
}