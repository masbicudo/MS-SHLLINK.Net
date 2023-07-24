// https://github.com/libyal/libfwsi/wiki/Shell-Folder-identifiers
// https://docs.rainmeter.net/tips/launching-windows-special-folders/
// https://winaero.com/windows-11-shell-commands-the-complete-list/
// https://gist.github.com/davehull/b6c119e3afd63053bb92?permalink_comment_id=4384945

using Newtonsoft.Json.Linq;
using ShellFoldersCodeGenerator.Model.Source;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShellFoldersCodeGenerator
{
    public static class CodeGenerator
    {
        private static string GenerateArray(IEnumerable<int> values)
        {
            return $$"""new[]{{{string.Join(",", values)}}}""";
        }
        private static string CammelCase(this string str)
        {
            var result = Regex.Replace(
                Regex.Replace(str, @"\b\w", m => m.Groups[0].Value.ToUpperInvariant()),
                @"[^\w\d]+", "");
            return result;
        }
        private static Dictionary<string, HashSet<T>> Postprocess<T>(this Dictionary<string, HashSet<T>> dictLists)
        {
            var result = new Dictionary<string, HashSet<T>>();
            foreach (var kv in dictLists)
            {
                var name = kv.Key;
                var values = kv.Value;
                if (StringComparer.InvariantCultureIgnoreCase.Compare(name, "3DObjects") == 0)
                    name = "Folder3DObjects";
                if (StringComparer.InvariantCultureIgnoreCase.Compare(name, "DESKTOP") == 0)
                    name = "Desktop";
                if (StringComparer.InvariantCultureIgnoreCase.Compare(name, "MYVIDEOS") == 0)
                    name = "MyVideos";
                if (StringComparer.InvariantCultureIgnoreCase.Compare(name, "InPCSettings") == 0)
                    name = "";
                if (StringComparer.InvariantCultureIgnoreCase.Compare(name, "Folder") == 0)
                    name = "";
                if (StringComparer.InvariantCultureIgnoreCase.Compare(name, "SolidWorksEnterprisePDM") == 0)
                    name = "SolidWorksEnterprisePdm";
                if (StringComparer.InvariantCultureIgnoreCase.Compare(name, "ThisPC") == 0)
                    name = "ThisPc";
                if (StringComparer.InvariantCultureIgnoreCase.Compare(name, "MicrosoftFTPFolder") == 0)
                    name = "MicrosoftFtpFolder";
                if (StringComparer.InvariantCultureIgnoreCase.Compare(name, "ProximityCPL") == 0)
                    name = "ProximityCpl";
                if (StringComparer.InvariantCultureIgnoreCase.Compare(name, "DXP") == 0)
                    name = "Dxp";
                if (StringComparer.InvariantCultureIgnoreCase.Compare(name, "InternetExplorerRSSFeedsFolder") == 0)
                    name = "InternetExplorerRssFeedsFolder";
                if (StringComparer.InvariantCultureIgnoreCase.Compare(name, "DBFolder") == 0)
                    name = "DbFolder";
                if (StringComparer.InvariantCultureIgnoreCase.Compare(name, "Windows7") == 0)
                    name = "";
                if (StringComparer.InvariantCultureIgnoreCase.Compare(name, "CommonPlacesFSFolder") == 0)
                    name = "CommonPlacesFsFolder";
                if (StringComparer.InvariantCultureIgnoreCase.Compare(name, "ThisPCDesktopFolder") == 0)
                    name = "ThisPcDesktopFolder";
                if (StringComparer.InvariantCultureIgnoreCase.Compare(name, "TabletPCSettings") == 0)
                    name = "TabletPcSettings";
                if (StringComparer.InvariantCultureIgnoreCase.Compare(name, "CDBURN_AREA") == 0)
                    name = "CdBurn_Area";
                if (StringComparer.InvariantCultureIgnoreCase.Compare(name, "CDBurning") == 0)
                    name = "CdBurning";
                if (StringComparer.InvariantCultureIgnoreCase.Compare(name, "SEARCH_CSC") == 0)
                    name = "Search_Csc";
                if (StringComparer.InvariantCultureIgnoreCase.Compare(name, "TheUserSUsername_Username") == 0)
                    name = "";

                name = Regex.Replace(name, @"UserSFullName", "UserFullName");
                name = Regex.Replace(name, @"EMail", "Email");
                name = Regex.Replace(name, @"MAPI", "Mapi");
                name = Regex.Replace(name, @"DLNA", "Dlna");
                name = Regex.Replace(name, @"Homegroup", "HomeGroup", RegexOptions.IgnoreCase);
                name = Regex.Replace(name, @"UNCFAT", "UncFat");
                name = Regex.Replace(name, @"NVIDIA", "Nvidia");
                name = Regex.Replace(name, @"Infared", "Infrared");
                name = Regex.Replace(name, @"COMMON", "Common");
                name = Regex.Replace(name, @"ADMINTOOLS", "AdminTools");
                name = Regex.Replace(name, @"OEM", "Oem");
                name = Regex.Replace(name, @"InPCSettings", "InPcSettings");
                name = Regex.Replace(name, @"LINKS", "Links");
                name = Regex.Replace(name, @"PROGRAMS", "Programs");
                name = Regex.Replace(name, @"STARTMENU", "StartMenu");
                name = Regex.Replace(name, @"ALTSTARTUP", "AltStartUp");
                name = Regex.Replace(name, @"STARTUP", "StartUp");
                name = Regex.Replace(name, @"TEMPLATES", "Templates");
                name = Regex.Replace(name, @"FAVORITES", "Favorites");
                name = Regex.Replace(name, @"APPDATA", "AppData");
                name = Regex.Replace(name, @"Username", "Username", RegexOptions.IgnoreCase);
                name = Regex.Replace(name, @"INTERNET", "Internet");
                name = Regex.Replace(name, @"PROGRAM_FILES", "Program_Files");
                name = Regex.Replace(name, @"DESKTOPDIRECTORY", "DesktopDirectory");
                name = Regex.Replace(name, @"DOCUMENTS", "Documents");
                name = Regex.Replace(name, @"MUSIC", "Music");
                name = Regex.Replace(name, @"PICTURES", "Pictures");
                name = Regex.Replace(name, @"VIDEO", "Video");
                name = Regex.Replace(name, @"SEARCH", "Search");
                name = Regex.Replace(name, @"BITBUCKET", "BitBucket");
                name = Regex.Replace(name, @"COMPUTERSNEARME", "ComputersNearMe");
                name = Regex.Replace(name, @"CONNECTIONS", "Connections");
                name = Regex.Replace(name, @"CONTROLS", "Controls");
                name = Regex.Replace(name, @"COOKIES", "Cookies");
                name = Regex.Replace(name, @"DRIVES", "Drives");
                name = Regex.Replace(name, @"FONTS", "Fonts");
                name = Regex.Replace(name, @"HISTORY", "History");
                name = Regex.Replace(name, @"CACHE", "Cache");
                name = Regex.Replace(name, @"ISCSI", "Iscsi");
                name = Regex.Replace(name, @"LOCALIZED", "Localized");
                name = Regex.Replace(name, @"LOCAL", "Local");
                name = Regex.Replace(name, @"^MY", "My");
                name = Regex.Replace(name, @"NETHOOD", "NetHood");
                name = Regex.Replace(name, @"NETWORK", "Network");
                name = Regex.Replace(name, @"PERSONAL", "Personal");
                name = Regex.Replace(name, @"PRINTERS", "Printers");
                name = Regex.Replace(name, @"PRINTHOOD", "PrintHood");
                name = Regex.Replace(name, @"RECENT", "Recent");
                name = Regex.Replace(name, @"RecordedTV", "RecordedTv");
                name = Regex.Replace(name, @"RESOURCES", "Resources");
                name = Regex.Replace(name, @"SENDTO", "SendTo");
                name = Regex.Replace(name, @"SYSTEM", "System");
                name = Regex.Replace(name, @"UserSUsername", "UserUsername");
                name = Regex.Replace(name, @"WINDOWS", "Windows");
                name = Regex.Replace(name, @"PROFILE", "Profile");
                name = Regex.Replace(name, @"WPDContentTypeFolder", "WpdContentTypeFolder");

                if (name.Contains("IfInstalled", StringComparison.InvariantCultureIgnoreCase))
                {
                    name = name.Replace("_IfInstalled", "");
                    name = name.Replace("IfInstalled", "");
                }

                var matchTodo = Regex.Match(name, "TODO");
                if (matchTodo.Success)
                {
                    for (int it = 0; it < 1000; it++)
                    {
                        name = "Unknown" + it;
                        if (!dictLists.ContainsKey(name))
                            break;
                    }
                }

                name = name.Replace("_", "");

                if (name != "" && name != null)
                {
                    foreach (var val in values)
                        result.DictListAdd(name, val);
                }
            }
            result = new(result.OrderBy(x => x.Key));
            return result;
        }
        public static string GenerateNamesOfKnownFolders(AllItemsData data)
        {
            var codeItems = new List<string>();
            var properties = new Dictionary<string, HashSet<int>>();
            for (int index = 0; index < data.allItems.Count; index++)
            {
                Item? item = data.allItems[index];
                foreach (var name in item.shellNames)
                {
                    var propName = name.CammelCase();
                    properties.DictListAdd(propName, index);
                }
                if (item.clsid != null)
                {
                    var name = item.clsid.Replace("CLSID_", "");
                    properties.DictListAdd(name, index);
                }
                foreach (var csidl in item.csidls)
                {
                    var name = csidl.Replace("CSIDL_", "");
                    properties.DictListAdd(name, index);
                }
                if (item.folderid != null)
                {
                    var name = item.folderid.Replace("FOLDERID_", "");
                    properties.DictListAdd(name, index);
                }
                if (item.descriptions != null)
                {
                    foreach (var desc in item.descriptions)
                    {
                        var match = Regex.Match(desc, @"^(.*?)(?:\((.*?)\))?$");
                        if (match.Success)
                        {
                            var name1 = match.Groups[1].Value.CammelCase();
                            var name2 = match.Groups[2].Value.CammelCase();
                            var name3 = name1 + "_" + name2;
                            if (!string.IsNullOrEmpty(name1))
                                properties.DictListAdd(name1, index);
                            if (!string.IsNullOrEmpty(name2))
                                properties.DictListAdd(name2, index);
                            if (!string.IsNullOrEmpty(name1) && !string.IsNullOrEmpty(name2))
                                properties.DictListAdd(name3, index);
                        }
                    }
                }
            }

            properties = properties.Postprocess();

            foreach (var property in properties)
            {
                codeItems.Add($$"""
                    public static KnownFolderInfo[] {{property.Key}} => GetManyByIds({{GenerateArray(property.Value)}});
                    """);
            }

            var result = string.Join("\n", codeItems);
            return result;
        }
        public static string GenerateClassOfKnownFolders(string ns, AllItemsData data)
        {
            string code = $$"""
                namespace {{ns}}
                {
                    public static partial class KnownFolders
                    {
                        {{GenerateNamesOfKnownFolders(data).Indent("        ")}}
                    }
                }
                """;
            return code;
        }
    }
}