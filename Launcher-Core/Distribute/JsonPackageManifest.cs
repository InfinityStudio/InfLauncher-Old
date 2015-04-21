using System;
using System.Globalization;
using System.Collections.Generic;

namespace LauncherCore
{
    public class JsonPackageManifest
    {
        public string Distributor = "";
        public string Description = "";
        public string GameVersion = "";
        public Dictionary<string, string> FileMap = new Dictionary<string, string>();

        public JsonPackageManifest()
        {
        }
    }
}

