using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Runtime.CompilerServices;

namespace LauncherCore
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnumReleaseType {
        [EnumMember(Value = "release")]
        RELEASE,
        [EnumMember(Value = "snapshot")]
        SNAPSHOT,
        [EnumMember(Value = "old_beta")]
        OLD_BETA,
        [EnumMember(Value = "old_alpha")]
        OLD_ALPHA
    }



    public class JsonGameSpace
    {
        // Attributes about the GameSpace
        public string Id = "";
        public string GameSpaceName = "";
        public EnumReleaseType ReleaseType = EnumReleaseType.RELEASE;
        public string VersionFile = "";


        public override string ToString()
        {
            return "[JsonGameSpace]" + JsonConvert.SerializeObject(this);
        }

        public void DumpToFile(string target_file) {
            string data = JsonConvert.SerializeObject(this, Formatting.Indented);
            FileStream stream = new FileStream(target_file, FileMode.Create);
            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
            writer.Write(data);
            writer.Close();
            stream.Close();
        }

        public static JsonGameSpace LoadFromFile(string file_path) {
            StreamReader reader = new StreamReader(file_path, Encoding.UTF8);
            string data = "", line = "";
            while ((line = reader.ReadLine()) != null)
                data += line + '\n';
            return JsonConvert.DeserializeObject<JsonGameSpace>(data);
        }
    }
}

