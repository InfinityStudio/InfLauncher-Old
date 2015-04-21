using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace LauncherCore
{
    public class UserPropertyEntry {
        public string name = "";
        public string value = "";
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnumUserType {
        [EnumMember(Value = "mojang")]
        MOJANG,
        [EnumMember(Value = "legacy")]
        LEGACY
    }

    public class UserLoginEntry {
        public string username = "";  // The name or email used for login.
        public string password = "";  // You can write down the password if you like.
                                      // In the form of BASE64
        public string accessToken = "";
        public string displayName = "";
        public string userid = "";    // ?? What's this for ??
        public string uuid = Guid.NewGuid().ToString("D");
        public EnumUserType userType = EnumUserType.MOJANG;
        public List<UserPropertyEntry> userProperties = new List<UserPropertyEntry>();

    }

    public class JsonLauncher
    {
        public List<string> GameSpaceIds = new List<string>();  // List<lastVersionId>
        public Dictionary<string, UserLoginEntry> Users = new Dictionary<string, UserLoginEntry>();  // Map<id, data>
        public string SelectedLoginEntry = "";

        public override string ToString()
        {
            return "[JsonLauncher]" + JsonConvert.SerializeObject(this);
        }

        public void DumpToFile(string target_file) {
            string data = JsonConvert.SerializeObject(this, Formatting.Indented);
            FileStream stream = new FileStream(target_file, FileMode.Create);
            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
            writer.Write(data);
            writer.Close();
            stream.Close();
        }

        public static JsonLauncher LoadFromFile(string file_path) {
            StreamReader reader = new StreamReader(file_path, Encoding.UTF8);
            string data = "", line = "";
            while ((line = reader.ReadLine()) != null)
                data += line + '\n';
            return JsonConvert.DeserializeObject<JsonLauncher>(data);
        }
    }
}

