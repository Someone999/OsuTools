namespace osuTools.KeyLayouts
{

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using osuTools.ExtraMethods;
    /// <summary>
    /// std模式的按键
    /// </summary>
    public class OsuKeyLayout
    {
        string[] lines;
        internal List<string> InternalName = new List<string>(new string[] { "keyOsuLeft", "keyOsuRight", "keyOsuSmoke" });
        Dictionary<string, Keys> layout;
        /// <summary>
        /// osu模式的按键布局
        /// </summary>
        public Dictionary<string, Keys> KeyLayout { get => layout; }
        Dictionary<string, Keys> keyandint = new Dictionary<string, Keys>();
        /// <summary>
        /// 使用包含按键布局的字符串初始化一个OsuKeyLayout
        /// </summary>
        /// <param name="data"></param>
        public OsuKeyLayout(string[] data)
        {
            lines = data;
            InitKeysDict();
            InitKeyLayout();
            Parse();
        }
        /// <summary>
        /// 使用配置文件初始化osu的按键布局
        /// </summary>
        /// <param name="ConfigFile"></param>
        public OsuKeyLayout(string ConfigFile)
        {
            lines = File.ReadAllLines(ConfigFile);
            InitKeysDict();
            InitKeyLayout();
            Parse();
        }
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
            layout.Add("Left", Keys.Z);
            layout.Add("Right", Keys.X);
            layout.Add("Smoke", Keys.C);
        }
        void Parse()
        {
            foreach (var data in lines)
            {
                if (data.StartsWith("keyOsuLeft"))
                {
                    layout["Left"] = keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
                }
                if (data.StartsWith("keyOsuRight"))
                {
                    layout["Right"] = keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
                }
                if (data.StartsWith("keyOsuSmoke"))
                {
                    layout["Smoke"] = keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
                }
            }
        }
    }

}