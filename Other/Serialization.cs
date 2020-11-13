namespace osuTools
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    interface IJsonSerilizable
    {
        void Serialize(string file);
        void Deserialize(string file);
    }
}