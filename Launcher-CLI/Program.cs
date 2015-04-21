using System;
using System.IO;
using LauncherCore;

namespace LauncherCLI
{
	class MainClass
	{
		public static void  PrintHelp()
		{
			Console.WriteLine("   help | ?          Print this help");
			Console.WriteLine("   list-gamespace    Print all gamespaces");
            Console.WriteLine("   launch-gamespace  Launch the gamespace");
			Console.WriteLine("   list-profiles     Print all saved accounts");
			Console.WriteLine("   exit | quit       Quit the Program");
		}

		public static void Main(string[] args)
		{
			string home_folder_path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string launcher_home = Path.Combine(home_folder_path, ".rmcl");
			Console.WriteLine("Launcher Home: " + launcher_home);

            LauncherDirectory filesystem = new LauncherDirectory(launcher_home);
            GamespacesManager spaces = new GamespacesManager(filesystem);
            UserProfilesManager users = new UserProfilesManager(filesystem);

			Console.WriteLine("=== Recursive Minecraft Launcher ===");
			while (true)
			{
				Console.Write("command> ");
				string cmd = Console.ReadLine().Trim();
                if (cmd == "help" || cmd == "?")
                {
                    PrintHelp();
                }
                else if (cmd == "list-gamespace")
                {
                    Console.WriteLine("* Space Name: Space ID");
                    if (spaces.GameSpaces.Count == 0)
                        Console.WriteLine("* No GameSpace Yet");
                    else
                        foreach (JsonGameSpace space in spaces.GameSpaces)
                            Console.WriteLine("* " + space.GameSpaceName + ": " + space.Id);
                }
                else if (cmd.StartsWith("launch-gamespace"))
                {
                    string gamespace_id = cmd.Split(' ')[1];
                    if (!spaces.GameSpaces.ContainsKey(gamespace_id))
                        Console.WriteLine("* The gamespace does not exists.");
                    else
                    {
                        JsonGameSpace space = spaces.GameSpaces[gamespace_id];
                    }
                }
				else if (cmd == "list-profiles")
				{

				}
				else if (cmd == "exit" || cmd == "quit")
				{
					break;
				}
				else
				{
					Console.WriteLine("Unknown command. Try \"help\"");
				}
			}
		}
	}
}
