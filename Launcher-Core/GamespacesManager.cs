using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace LauncherCore
{
	public class GamespacesManager
	{
        private readonly LauncherDirectory launcher_dir;
        private readonly JsonLauncher main_json;
        public readonly Dictionary<string, JsonGameSpace> GameSpaces = new Dictionary<string, JsonGameSpace>();

        public GamespacesManager (LauncherDirectory dir)
		{
            launcher_dir = dir;
            main_json = dir.GetJsonData();
            loadAllGameSpaces();
		}

        private void loadAllGameSpaces(){
            foreach (string id in main_json.GameSpaceIds)
            {
                JsonGameSpace space = JsonGameSpace.LoadFromFile(
                    launcher_dir.GetSpaceJsonPath(id));
                if (space == null)
                    Console.WriteLine("[WARN]Failed Loading GameSpace: " + id);
                else
                    GameSpaces.Add(id, space);
            }
        }

        public void UpdateGameSpace() {}
        public void DeleteGameSpace() {}
        public void CreateGameSpace() {}
        public void CopyGameSpace() {}
    }
}

