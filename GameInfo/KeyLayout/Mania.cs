namespace osuTools.KeyLayouts
{

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using osuTools.ExtraMethods;
    /// <summary>
    /// Mania模式的按键，1-9k，只有设置过按键的会出现在设置里，其余的按照默认值处理。
    /// </summary>
    public class ManiaKeyLayout
    {
        Dictionary<int, List<Keys>> layout;
        List<KeyValuePair<int, List<Keys>>> layouts;
        /// <summary>
        /// 1K的键位信息
        /// </summary>
        public KeyValuePair<int, List<Keys>> Key1 { get => layouts[0]; }
        /// <summary>
        /// 2K的键位信息
        /// </summary>
        public KeyValuePair<int, List<Keys>> Key2 { get => layouts[1]; }
        /// <summary>
        /// 3K的键位信息
        /// </summary>
        public KeyValuePair<int, List<Keys>> Key3 { get => layouts[2]; }
        /// <summary>
        /// 4K的键位信息
        /// </summary>
        public KeyValuePair<int, List<Keys>> Key4 { get => layouts[3]; }
        /// <summary>
        /// 5K的键位信息
        /// </summary>
        public KeyValuePair<int, List<Keys>> Key5 { get => layouts[4]; }
        /// <summary>
        /// 6K的键位信息
        /// </summary>
        public KeyValuePair<int, List<Keys>> Key6 { get => layouts[5]; }
        /// <summary>
        /// 7K的键位信息
        /// </summary>
        public KeyValuePair<int, List<Keys>> Key7 { get => layouts[6]; }
        /// <summary>
        /// 8K的键位信息
        /// </summary>
        public KeyValuePair<int, List<Keys>> Key8 { get => layouts[7]; }
        /// <summary>
        /// 9K的键位信息
        /// </summary>
        public KeyValuePair<int, List<Keys>> Key9 { get => layouts[8]; }

        string[] lines;
        void InitKeysDict()
        {
            var values = Enum.GetValues(typeof(Keys));
            var names = Enum.GetNames(typeof(Keys));
            try
            {
                for (int i = 0; i < values.Length; i++)
                {
                    string tmp =
                        names[i] == "LeftShift" ? "LShiftKey" :
                        names[i] == "RightShift" ? "RShiftKey" :
                        names[i] == "LeftControl" ? "LControlKey" :
                        names[i] == "RightControl" ? "RControlKey" :
                        names[i] == "LeftAlt" ? "LMenu" :
                        names[i] == "RightAlt" ? "RMenu" : names[i];

                    keyandint.Add(names[i], (Keys)values.GetValue(i));
                }
            }
            catch
            {

            }
        }
        void InitPair()
        {
            layouts = GetFromDict(layout);
        }
        void InitLayoutDict()
        {
            layout = new Dictionary<int, List<Keys>>();
            layout.Add(1, new List<Keys>(new Keys[] { Keys.Space }));
            layout.Add(2, new List<Keys>(new Keys[] { Keys.F, Keys.J }));
            layout.Add(3, new List<Keys>(new Keys[] { Keys.F, Keys.Space, Keys.J }));
            layout.Add(4, new List<Keys>(new Keys[] { Keys.D, Keys.F, Keys.J, Keys.K }));
            layout.Add(5, new List<Keys>(new Keys[] { Keys.D, Keys.F, Keys.Space, Keys.J, Keys.K }));
            layout.Add(6, new List<Keys>(new Keys[] { Keys.S, Keys.D, Keys.F, Keys.J, Keys.K, Keys.L }));
            layout.Add(7, new List<Keys>(new Keys[] { Keys.S, Keys.D, Keys.F, Keys.Space, Keys.J, Keys.K, Keys.L }));
            layout.Add(8, new List<Keys>(new Keys[] { Keys.LShiftKey, Keys.S, Keys.D, Keys.F, Keys.J, Keys.K, Keys.L, Keys.OemSemicolon }));
            layout.Add(9, new List<Keys>(new Keys[] { Keys.LShiftKey, Keys.S, Keys.D, Keys.F, Keys.Space, Keys.J, Keys.K, Keys.L, Keys.OemSemicolon }));
        }
        internal List<KeyValuePair<int, List<Keys>>> GetFromDict(Dictionary<int, List<Keys>> dict)
        {
            List<KeyValuePair<int, List<Keys>>> lst = new List<KeyValuePair<int, List<Keys>>>();
            foreach (var kvp in dict)
            {
                lst.Add(kvp);
            }
            return lst;
        }
        /// <summary>
        /// 从配置文件中读取Mania的键位信息
        /// </summary>
        /// <param name="ConfigFile"></param>
        public ManiaKeyLayout(string ConfigFile)
        {
            lines = File.ReadAllLines(ConfigFile);
            InitKeysDict();
            InitLayoutDict();
            InitPair();
            Parse();

        }
        Dictionary<string, Keys> keyandint = new Dictionary<string, Keys>();
        /// <summary>
        /// 从数据行解析Manua的键位信息
        /// </summary>
        /// <param name="data"></param>
        public ManiaKeyLayout(string[] data)
        {
            lines = data;
            InitKeysDict();
            InitLayoutDict();
            InitPair();
            Parse();

        }
        void Parse()
        {
            int i = 0;
            List<Keys> tmp;
            foreach (string data in lines)
            {
                tmp = new List<Keys>();
                string keystr = $"ManiaLayouts";

                if (data.Trim().StartsWith(keystr))
                {
                    string[] keys = data.Trim().Split('=');
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("\\d");
                    int keycount = int.Parse(regex.Match(keys[0]).Value);
                    if (keys.Length > 1)
                    {

                        string[] keylayout = keys[1].Split(' ');
                        foreach (string maniakey in keylayout)
                        {
                            string tp = maniakey;
                            if (string.IsNullOrWhiteSpace(maniakey))
                                continue;

                            tmp.Add(keyandint.CheckIndexAndGetValue(tp.Trim()));

                        }
                        layout[keycount - 1] = tmp;

                    }
                }
                InitPair();
            }
        }

    }
}