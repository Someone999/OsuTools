namespace osuTools.KeyBindings
{
    using osuTools.KeyLayouts;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using osuTools.ExtraMethods;
    /// <summary>
    /// 存储所有的快捷键及按键信息。
    /// </summary>
    public class KeyBinding
    {
        Dictionary<string, Keys> kb;
        OsuKeyLayout olayout;
        ManiaKeyLayout mlayout;
        CatchKeyLayout clayout;
        TaikoKeyLayout tlayout;
        ModsKeyLayout modlayout;
        /// <summary>
        /// Mods的按键绑定
        /// </summary>
        public ModsKeyLayout ModKeyLayouts { get => modlayout; }
        /// <summary>
        /// Mania模式的键位
        /// </summary>
        public ManiaKeyLayout ManiaKeyLayouts { get => mlayout; }
        /// <summary>
        /// CTB模式的键位
        /// </summary>
        public CatchKeyLayout CatchKeyLayouts { get => clayout; }
        /// <summary>
        /// Osu模式的键位
        /// </summary>
        public OsuKeyLayout OsuKeyLayouts { get => olayout; }
        /// <summary>
        /// Taiko模式的键位
        /// </summary>
        public TaikoKeyLayout TaikoKeyLayouts { get => tlayout; }
        Dictionary<string, Keys> keyandint = new Dictionary<string, Keys>();
        string[] lines;
        /// <summary>
        /// 绑定的快捷键
        /// </summary>
        public Dictionary<string, Keys> Bindings { get => kb; }
        void Init()
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
        /// <summary>
        /// 将字符串数组解析成键位信息
        /// </summary>
        /// <param name="data"></param>
        public KeyBinding(string[] data)
        {
            Init();
            lines = data;
            mlayout = new ManiaKeyLayout(lines);
            olayout = new OsuKeyLayout(lines);
            clayout = new CatchKeyLayout(lines);
            tlayout = new TaikoKeyLayout(lines);
            modlayout = new ModsKeyLayout(lines);
            Parse();
        }
        void Parse()
        {
            kb = new Dictionary<string, Keys>();
            foreach (var data in lines)
            {
                var tmp = data.Split('=');
                if (tmp.Length > 1)
                {
                    if (tmp[1].Trim().Split(' ').Length > 1)
                    {
                        continue;
                    }
                    else
                    {


                        bool Invalid = !keyandint.ContainsKey(tmp[1].Trim());
                        bool IsLayOutKey = OsuKeyLayouts.InternalName.Contains(tmp[0].Trim()) || CatchKeyLayouts.InternalName.Contains(tmp[0].Trim()) ||
                                         TaikoKeyLayouts.InternalName.Contains(tmp[0].Trim()) || modlayout.InternalName.Contains(tmp[0].Trim());
                        if (Invalid || IsLayOutKey)
                        {
                            continue;
                        }
                        else
                        {

                            kb.Add(tmp[0], keyandint.CheckIndexAndGetValue(tmp[1].Trim()));
                        }
                    }
                }
                else
                {
                    continue;
                }
            }
        }
    }
}