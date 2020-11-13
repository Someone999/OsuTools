namespace osuTools.KeyLayouts
{


    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using osuTools.ExtraMethods;
    /// <summary>
    /// CTB模式的按键
    /// </summary>
    public class CatchKeyLayout
    {
        internal List<string> InternalName = new List<string>(new string[] { "keyFruitsLeft", "keyFruitsRight", "keyFruitsDash" });
        string[] lines;
        Dictionary<string, Keys> layout;
        /// <summary>
        /// CTB模式的键位
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
            layout.Add("Left", Keys.Left);
            layout.Add("Right", Keys.Right);
            layout.Add("Dash", Keys.LShiftKey);
        }
        /// <summary>
        /// 将字符串解析成CTB键位
        /// </summary>
        /// <param name="data"></param>
        public CatchKeyLayout(string[] data)
        {
            lines = data;
            InitKeysDict();
            InitKeyLayout();
            Parse();
        }
        /// <summary>
        /// 从配置文件中读取CTB键位
        /// </summary>
        /// <param name="ConfigFile"></param>
        public CatchKeyLayout(string ConfigFile)
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
                if (data.StartsWith("keyFruitsLeft"))
                {
                    layout["Left"] = keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
                }
                if (data.StartsWith("keyFruitsRight"))
                {
                    layout["Right"] = keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
                }
                if (data.StartsWith("keyFruitsDash"))
                {
                    layout["Dash"] = keyandint.CheckIndexAndGetValue(data.Trim().Split('=')[1].Trim());
                }
            }
        }
    }

}