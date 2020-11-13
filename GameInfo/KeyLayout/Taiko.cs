namespace osuTools.KeyLayouts
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using osuTools.ExtraMethods;
    /// <summary>
    /// Taiko模式的按键
    /// </summary>
    public class TaikoKeyLayout
    {
        string[] lines;
        Dictionary<string, Keys> layout;
        internal List<string> InternalName = new List<string>(new string[] { "keyTaikoInnerLeft", "keyTaikoInnerRight", "keyTaikoOuterLeft", "keyTaikoOuterRight" });
        /// <summary>
        /// 键位
        /// </summary>
        public Dictionary<string, Keys> KeyLayout { get => layout; }
        Dictionary<string, Keys> keyandint = new Dictionary<string, Keys>();
        void InitKeysDict()
        {
            var values = Enum.GetValues(typeof(Keys));
            var names = Enum.GetNames(typeof(Keys));
            try
            {
                for (int i = 0; i < values.Length; i++)
                {
                    keyandint.Add(names[i], (Keys)values.GetValue(i));
                }
            }
            catch
            {

            }
        }
        void InitKeyLayout()
        {
            layout = new Dictionary<string, Keys>();
            layout.Add("RedLeft", Keys.X);
            layout.Add("RedRight", Keys.C);
            layout.Add("BlueLeft", Keys.Z);
            layout.Add("BlueRight", Keys.V);
        }
        /// <summary>
        /// 使用格式正确的字符数组构造一个KeyLayout
        /// </summary>
        /// <param name="data">要使用的字符数组</param>
        public TaikoKeyLayout(string[] data)
        {
            lines = data;
            InitKeysDict();
            InitKeyLayout();
            Parse();
        }
        /// <summary>
        /// 从配置文件读取
        /// </summary>
        /// <param name="ConfigFile">配置文件的目录</param>
        public TaikoKeyLayout(string ConfigFile)
        {
            lines = File.ReadAllLines(ConfigFile);
            InitKeysDict();
            InitKeyLayout();
            Parse();
        }
        void Parse()
        {
            foreach (var data in lines)
            {
                if (data.StartsWith("keyTaikoInnerLeft"))
                {
                    layout["RedLeft"] = keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
                }
                if (data.StartsWith("keyTaikoInnerRight"))
                {
                    layout["RedRight"] = keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
                }
                if (data.StartsWith("keyTaikoOuterLeft"))
                {
                    layout["BlueLeft"] = keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
                }
                if (data.StartsWith("keyTaikoOuterRight"))
                {
                    layout["BlueRight"] = keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
                }
            }
        }
        /// <summary>
        /// Taiko的键位信息
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string tmp = "";
            for (int i = 0; i < layout.Count; i++)
            {
                if (i + 1 != layout.Count)
                {

                    tmp += layout.Values.ToString() + " ";
                }
                else
                {
                    tmp += layout.Values.ToString();
                }

            }
            return tmp;
        }
    }
}