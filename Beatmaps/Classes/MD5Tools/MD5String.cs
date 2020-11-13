using System;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Cryptography;
namespace osuTools.Beatmaps
{
    /// <summary>
    /// 将MD5以字符串的形式表示。
    /// </summary>
    [Serializable]
    public class MD5String
    {
        [System.NonSerialized]
        MD5 md5;
        string md5str = "";
       
        /// <summary>
        /// 存储在类中的System.Security.Cryptography.MD5对象
        /// </summary>
        public MD5 CurrentMD5 { get => md5; }
        /// <summary>
        /// 使用MD5字符串构建新的MD5String对象。CurrentMD5将会被设置为null。
        /// </summary>
        /// <param name="md5"></param>
        public MD5String(string md5)
        {
            this.md5 = null;
            md5str = md5;
        }
        /// <summary>
        /// 使用System.Security.Cryptography.MD5来构建一个新的MD5String。
        /// </summary>
        /// <param name="md5"></param>
        public MD5String(MD5 md5)
        {
            this.md5 = md5;
        }
        /// <summary>
        /// 将System.Security.Cryptography.MD5中的Hash转化为字符串。
        /// </summary>
        /// <param name="md5">要转化的MD5对象</param>
        /// <returns>转换后的MD5字符串</returns>
        public static string GetString(MD5 md5)
        {
            if (md5.Hash.Length == 0) throw new System.NullReferenceException();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in md5.Hash)
            {
                stringBuilder.Append(b.ToString("x2"));
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 将一个byte数组转换成MD5字符串。
        /// </summary>
        /// <param name="md5">要转换的字符数组</param>
        /// <returns>转换后的MD5字符串</returns>
        public static string GetString(byte[] md5)
        {

            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in md5)
            {
                stringBuilder.Append(b.ToString("x2"));
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 构造一个不包含MD5的MD5String
        /// </summary>
        public MD5String()
        {
            md5str = "";
            md5 = new MD5CryptoServiceProvider();
        }
        /// <summary>
        /// 返回其中存储的转化为字符串的MD5
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            ConvertToString();
            return md5str;
        }
        void ConvertToString()
        {
            if (string.IsNullOrEmpty(md5str))
            {

                if (md5 is null || md5.Hash is null)
                {
                    md5str = "";
                    return;
                }

                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte b in md5.Hash)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }
                md5str = stringBuilder.ToString();
            }
        }
        /// <summary>
        /// 使用两个MD5String判断两个MD5是否相等
        /// </summary>
        /// <param name="obja"></param>
        /// <param name="objb"></param>
        /// <returns></returns>
        public static bool operator ==(MD5String obja, MD5String objb)
        {
            if (obja is null && objb is null) return true;
            if (!(obja is null) && objb is null) return false;
            if (obja is null && !(objb is null)) return false;
            if (obja is null && objb is null || !(obja is null) && !(objb is null)) return true;
            if (string.Compare(obja.ToString(), objb.ToString(), true) != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 使用两个MD5String判断两个MD5是否相等
        /// </summary>
        /// <param name="obja"></param>
        /// <param name="objb"></param>
        /// <returns></returns>
        public static bool operator !=(MD5String obja, MD5String objb)
        {
            if (obja is null && objb is null) return false;
            if (!(obja is null) && objb is null) return true;
            if (obja is null && !(objb is null)) return true;
            if (obja is null && objb is null || !(obja is null) && !(objb is null)) return false;
            if (string.Compare(obja.ToString(), objb.ToString(), true) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 使用MD5字符串判断两个MD5是否相等
        /// </summary>
        /// <param name="obja"></param>
        /// <param name="objb"></param>
        /// <returns></returns>
        public static bool operator ==(MD5String obja, string objb)
        {
            if (obja is null && string.IsNullOrEmpty(objb)) return true;
            if (!(obja is null) && string.IsNullOrEmpty(objb)) return false;
            if (obja is null && !string.IsNullOrEmpty(objb)) return false;
            if (obja is null && string.IsNullOrEmpty(objb) || !(obja is null) && !string.IsNullOrEmpty(objb)) return true;
            if (string.Compare(obja.ToString(), objb.ToString(), true) != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 使用MD5字符串判断两个MD5是否相等
        /// </summary>
        /// <param name="obja"></param>
        /// <param name="objb"></param>
        /// <returns></returns>
        public static bool operator !=(MD5String obja, string objb)
        {
            if (obja is null && objb is null) return false;
            if (!(obja is null) && objb is null) return true;
            if (obja is null && !(objb is null)) return true;
            if (obja is null && objb is null || !(obja is null) && !(objb is null)) return false;
            if (obja is null && string.IsNullOrEmpty(objb) || !(obja is null) && !string.IsNullOrEmpty(objb)) return false;
            if (string.Compare(obja.ToString(), objb.ToString(), true) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}