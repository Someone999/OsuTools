namespace osuTools.KeyLayouts
{

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using osuTools.ExtraMethods;
    /// <summary>
    /// Mod对应的快捷键
    /// </summary>
    public class ModsKeyLayout
    {
        Dictionary<OsuGameMod, Keys> mods = new Dictionary<OsuGameMod, Keys>();
        Dictionary<string, Keys> keyandint = new Dictionary<string, Keys>();
        Dictionary<string, OsuGameMod> modlist = new Dictionary<string, OsuGameMod>();
        internal List<string> InternalName = new List<string>(new string[]{"keyEasy","keyNoFail","keyHalfTime","keyHardRock","keySuddenDeath","keyDoubleTime","keyHidden",
                                                                          "keyFlashlight","keyRelax","keyAutopilot","keySpunOut","keyAuto"});
        internal List<string> Name = new List<string>(new string[]{"Easy","NoFail","HalfTime","HardRock","SuddenDeath","DoubleTime","Hidden",
                                                                          "Flashlight","Relax","AutoPilot","SpunOut","AutoPlay"});
        void InitModList()
        {
            var values = Enum.GetValues(typeof(OsuGameMod));
            foreach (string modstr in Name)
            {
                foreach (var mod in values)
                {
                    if (mod.ToString() == modstr)
                    {
                        modlist.Add(modstr, (OsuGameMod)mod);
                    }
                }
            }
        }
        /// <summary>
        /// 获取指定Mod对应的键位
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public Keys GetKey(OsuGameMod mod)
        {
            if (mod.ToString().Contains("Key"))
            {
                return mods.CheckIndexAndGetValue(OsuGameMod.Relax);
            }
            return mods.CheckIndexAndGetValue(mod);
        }
        string ModConvert(string mod)
        {

            return modlist.CheckIndexAndGetValue(mod.Trim().Replace("key", "")).ToString();

        }
        string[] lines;
        /// <summary>
        /// 使用字符串数组构建Mods和Keys的键值对
        /// </summary>
        /// <param name="data"></param>
        public ModsKeyLayout(string[] data)
        {
            InitKeys();
            lines = data;
            InitModList();
            GetKeys();
        }
        /// <summary>
        /// 从配置文件读取并构建Mods和Keys的键值对
        /// </summary>
        /// <param name="configfile"></param>
        public ModsKeyLayout(string configfile)
        {
            InitKeys();
            lines = File.ReadAllLines(configfile);
            InitModList();
            GetKeys();
        }
        void InitKeys()
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
        void GetKeys()
        {
            foreach (var data in lines)
            {
                foreach (var name in InternalName)
                {

                    var tmp = data.Split('=');
                    if (tmp[0].Trim().Replace("key", "") == ModConvert(name))
                    {
                        var tmpmod = ModConvert(tmp[0].Trim());
                        bool IsValid = tmpmod != OsuGameMod.Unknown.ToString();
                        if (IsValid)
                            mods.Add(modlist.CheckIndexAndGetValue(tmpmod.Trim()), keyandint.CheckIndexAndGetValue(tmp[1].Trim()));
                    }
                }
            }
        }
    }

}