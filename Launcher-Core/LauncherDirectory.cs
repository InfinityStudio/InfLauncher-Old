using System;
using System.IO;

namespace LauncherCore
{
    public class LauncherDirectory
    {
        private readonly string launcher_directory;
        private JsonLauncher json_data = null;

        private string getMainJsonPath() {
            return Path.Combine(launcher_directory, "rmcl_profile.json");
        }

        public LauncherDirectory(string dir)
        {
            launcher_directory = dir;
            if (!Directory.Exists(launcher_directory))
            {
                Directory.CreateDirectory(launcher_directory);
                Console.WriteLine("Directory created: " + launcher_directory);
            }
            //lockLauncherDir();  TODO
        }

        internal JsonLauncher GetJsonData() {
            if (json_data == null)
            {
                if (!File.Exists(getMainJsonPath()))
                {
                    Console.WriteLine("Creating new main config.");
                    json_data = new JsonLauncher();
                    json_data.DumpToFile(getMainJsonPath());
                }
                else
                {
                    json_data = JsonLauncher.LoadFromFile(getMainJsonPath());
                    Console.WriteLine("Loading json ... " + json_data.ToString());
                }
            }
            return json_data;
        }

        public string GetSpaceJsonPath(string space_id) {
            return Path.Combine(launcher_directory, "spaces", space_id + ".json");
        }

        public void SaveJsonData() {
            if (json_data != null)
            {
                json_data.DumpToFile(getMainJsonPath());
            }
            else
            {
                throw new Exception("Attempt to save config before loading.");
            }
        }
    }
}

