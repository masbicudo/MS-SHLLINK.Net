// [x] https://github.com/libyal/libfwsi/wiki/Shell-Folder-identifiers
// [x] https://docs.rainmeter.net/tips/launching-windows-special-folders/
// [x] https://winaero.com/windows-11-shell-commands-the-complete-list/
// [x] https://gist.github.com/davehull/b6c119e3afd63053bb92?permalink_comment_id=4384945
// [x] https://gist.github.com/davehull/50c09b5160dfceb5bb13#comment-1439249
// [x] https://learn.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2010/bb882665(v=vs.100)?redirectedfrom=MSDN
// [x] https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/known-folder-guids-for-file-dialog-custom-places?view=netframeworkdesktop-4.8
// [x] https://learn.microsoft.com/en-us/windows/win32/shell/knownfolderid
// [ ] https://learn.microsoft.com/en-us/windows/win32/shell/csidl

using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Newtonsoft.Json;
using ShellFoldersCodeGenerator;
using ShellFoldersCodeGenerator.Model.Source;
using System.Text.RegularExpressions;


var allData = new AllItemsData();

AllItemsSourceData allSrcItems = null;

if (File.Exists("allSrcItems.json"))
{
    var json1 = await File.ReadAllTextAsync("allSrcItems.json");
    allSrcItems = JsonConvert.DeserializeObject<AllItemsSourceData>(json1);
}
else
{
    allSrcItems = new AllItemsSourceData();

    {
        var url1 = "https://github.com/libyal/libfwsi/wiki/Shell-Folder-identifiers";
        var contents = await Helper.DownloadAsync(url1, "libfwsi-Shell-Folder-identifiers.html");
        var parser = new HtmlParser();
        var document = await parser.ParseDocumentAsync(contents);
        var tables = document.QuerySelectorAll("table").ToArray();
        foreach (var tr in tables[0].QuerySelectorAll("tr"))
        {
            var tds = tr.QuerySelectorAll("td").ToArray();
            if (tds.Length > 0)
            {
                var guid = tds[0].TextContent;
                var winVers = tds[1].TextContent;
                var clsid = tds[2].TextContent;
                var description = tds[3].TextContent;
                allSrcItems.libfwsi_Items.Add(new(guid, winVers, clsid, description, "libfwsi"));
            }
        }
    }

    {
        var url = "https://docs.rainmeter.net/tips/launching-windows-special-folders/";
        var contents = await Helper.DownloadAsync(url, "rainmeter-launching-windows-special-folders.html");
        var parser = new HtmlParser();
        var document = await parser.ParseDocumentAsync(contents);
        var lines = document.QuerySelectorAll("span.line").ToArray();
        foreach (var line in lines)
        {
            var match = Regex.Split(line.TextContent, @"\s+::");
            if (match.Length == 2)
            {
                var title = match[0];
                var guid = match[1];
                if (title.Trim() == "OR")
                    title = allSrcItems.RainMeterTitleGuid.Last().title;
                allSrcItems.RainMeterTitleGuid.Add(new(title, guid, "RainMeter"));
            }
            var match2 = Regex.Split(line.TextContent, @"(?<=shell:[\w\d_]+(?:\s[\w\d_]+))\s+");
            if (match2.Length == 2)
            {
                var shellName = match2[0].Trim();
                var title = match2[1];
                allSrcItems.RainMeterShellIdTitle.Add(new(shellName, title, "RainMeter"));
            }
        }
    }

    {
        var url = "https://winaero.com/windows-11-shell-commands-the-complete-list/";
        var contents = await Helper.DownloadAsync(url, "winaero-windows-11-shell-commands.html");
        var parser = new HtmlParser();
        var document = await parser.ParseDocumentAsync(contents);
        var tables = document.QuerySelectorAll("table").ToArray();
        foreach (var tr in tables[0].QuerySelectorAll("tr"))
        {
            var tds = tr.QuerySelectorAll("td").ToArray();
            if (tds.Length > 0)
            {
                var shellName = tds[0].TextContent;
                var title = tds[1].TextContent;
                allSrcItems.WinAeroShellIdTitle.Add(new(shellName, title, "WinAero"));
            }
        }
        foreach (var tr in tables[1].QuerySelectorAll("tr"))
        {
            var tds = tr.QuerySelectorAll("td").ToArray();
            if (tds.Length > 0)
            {
                var guid = tds[0].TextContent;
                var title = tds[1].TextContent;
                allSrcItems.WinAeroGuidTitle.Add(new(title, guid, "WinAero"));
            }
        }
    }

    {
        var url = "https://gist.github.com/davehull/b6c119e3afd63053bb92";
        var contents = await Helper.DownloadAsync(url, "davehull-gist-b6c119e3afd63053bb92.html");
        var parser = new HtmlParser();
        var document = await parser.ParseDocumentAsync(contents);
        var lines = document.QuerySelectorAll("td.js-file-line").ToArray();
        foreach (var line in lines)
        {
            var match = Regex.Match(line.TextContent, @"""([0-9a-fA-F\-]{36})"" = ""(.*)""");

            if (match.Success)
            {
                var guid = match.Groups[1].Value;
                var title = match.Groups[2].Value;
                allSrcItems.DaveHullGuidTitle.Add(new(title, guid, "DaveHull"));
            }
        }
    }

    {
        var url = "https://gist.github.com/davehull/50c09b5160dfceb5bb13";
        var contents = await Helper.DownloadAsync(url, "davehull-gist-50c09b5160dfceb5bb13.html");
        var parser = new HtmlParser();
        var document = await parser.ParseDocumentAsync(contents);
        var comment = document.QuerySelectorAll("div.highlight-source-cs").ToArray();
        string guid = null;
        string title = null;
        foreach (var line in comment[0].TextContent.Split("\n"))
        {
            var matchGuid = Regex.Match(line, @"case ""(.*)""");
            if (matchGuid.Success)
                guid = matchGuid.Groups[1].Value;

            var matchTitle = Regex.Match(line, @"return ""(.*)"";");
            if (matchTitle.Success)
            {
                title = matchTitle.Groups[1].Value;
                allSrcItems.EricZimmermanGuidTitle.Add(new(title, guid, "EricZimmerman"));
            }
        }
    }

    {
        var url = "https://learn.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2010/bb882665(v=vs.100)";
        var contents = await Helper.DownloadAsync(url, "microsoft-known-folder-guids-vista.html");
        var parser = new HtmlParser();
        var document = await parser.ParseDocumentAsync(contents);
        var li = document.QuerySelectorAll("ul > li").ToArray();
        foreach (var line in li)
        {
            var match = Regex.Match(line.TextContent, @"(.*)\s+([0-9a-fA-F\-]{36})");
            if (match.Success)
            {
                var title = match.Groups[1].Value;
                var guid = match.Groups[2].Value;
                allSrcItems.Microsoft2013.Add(new(title, guid, "Vista", "Microsoft-2013"));
            }
        }
    }

    {
        var url = "https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/known-folder-guids-for-file-dialog-custom-places";
        var contents = await Helper.DownloadAsync(url, "microsoft-known-folder-guids-net48.html");
        var parser = new HtmlParser();
        var document = await parser.ParseDocumentAsync(contents);
        var li = document.QuerySelectorAll("div.content > p").ToArray();
        foreach (var line in li)
        {
            var match = Regex.Match(line.TextContent, @"(.*)\s+([0-9a-fA-F\-]{36})");
            if (match.Success)
            {
                var title = match.Groups[1].Value;
                var guid = match.Groups[2].Value;
                allSrcItems.Microsoft2022.Add(new(title, guid, "Microsoft-2022"));
            }
        }
    }

    {
        var url = "https://learn.microsoft.com/en-us/windows/win32/shell/knownfolderid";
        var contents = await Helper.DownloadAsync(url, "microsoft-knownfolderid.html");
        var parser = new HtmlParser();
        var document = await parser.ParseDocumentAsync(contents);
        var table = document.QuerySelectorAll("table").First();
        var trs = table.QuerySelectorAll("> tbody > tr").ToArray();
        foreach (var tr in trs)
        {
            var tds = tr.QuerySelectorAll("> td");
            var folderid = tds[0].TextContent.Trim();
            var subtrs = tds[1].QuerySelectorAll("table > tbody > tr");
            string guid = null;
            string displayName = null;
            string folderType = null;
            string defaultPath = null;
            string[] csidls = null;
            string legacyDisplayName = null;
            string legacyDefaultPath = null;

            foreach (var subtr in subtrs)
            {
                var subtds = subtr.QuerySelectorAll("td");
                var key = subtds[0].TextContent.Trim();
                var value = subtds[1].TextContent.Trim();
                if (key == "GUID") guid = value;
                if (key == "Display Name") displayName = value;
                if (key == "Folder Type") folderType = value;
                if (key == "Default Path") defaultPath = value;
                if (key == "CSIDL") csidls = value.Split(",").Select(s=>s.Trim()).ToArray();
                if (key == "CSIDL Equivalent") csidls = value.Split(",").Select(s=>s.Trim()).ToArray();
                if (key == "CSIDL Equivalents") csidls = value.Split(",").Select(s=>s.Trim()).ToArray();
                if (key == "Legacy Display Name") legacyDisplayName = value;
                if (key == "Legacy Default Path") legacyDefaultPath = value;
            }

            if (guid != null)
            {
                var winVers = new HashSet<string>();
                foreach (var csidl in csidls)
                {
                    var matchWinVer = Regex.Matches(csidl, @"Windows\s+([\d\w\.]+)");
                    foreach (Match match in matchWinVer)
                    {
                        winVers.Add(match.Groups[1].Value);
                    }
                    if (csidl == "None")
                    {
                        csidls = Array.Empty<string>();
                    }
                }
                allSrcItems.MicrosoftKnownFolderId.Add(new(
                        folderid,
                        guid,
                        displayName,
                        folderType,
                        defaultPath,
                        csidls,
                        legacyDisplayName,
                        legacyDefaultPath,
                        winVers
                    ));
            }
        }
    }

    //var json = JsonConvert.SerializeObject(allSrcItems, Formatting.Indented);
    //await File.WriteAllTextAsync("allSrcItems.json", json);
}

foreach (var item in allSrcItems.libfwsi_Items)
{
    Helper.AddItem(
        data: allData,
        guid: new(item.guid),
        shellNames: new(),
        clsid: item.clsid == "" ? null : item.clsid,
        csidls: new(),
        folderid: null,
        description: item.description,
        os: new(item.winvers.Split(",").Select(x => x.Trim())),
        source: item.source);
}

{
    var dictTitleToShellName = new Dictionary<string, HashSet<string>>();
    foreach (var item in allSrcItems.RainMeterShellIdTitle)
    {
        if (!dictTitleToShellName.TryGetValue(item.title, out var shellNames))
        {
            shellNames = new HashSet<string>();
            dictTitleToShellName.Add(item.title, shellNames);
        }
        shellNames.Add(item.shellName.Replace("shell:", ""));
    }
    foreach (var item in allSrcItems.RainMeterTitleGuid)
    {
        if (!dictTitleToShellName.TryGetValue(item.title, out var shellNames))
            shellNames = new HashSet<string>();
        Helper.AddItem(
            data: allData,
            guid: new(item.guid),
            shellNames: shellNames,
            clsid: null,
            csidls: new(),
            folderid: null,
            description: item.title,
            os: new(),
            source: item.source);
    }
}

{
    var dictTitleToShellNames = new Dictionary<string, HashSet<string>>();
    foreach (var item in allSrcItems.WinAeroShellIdTitle)
    {
        if (!dictTitleToShellNames.TryGetValue(item.title, out var shellName))
        {
            shellName = new HashSet<string>();
            dictTitleToShellNames.Add(item.title, shellName);
        }
        shellName.Add(item.shellName.Replace("shell:", ""));
    }
    foreach (var item in allSrcItems.WinAeroGuidTitle)
    {
        if (!dictTitleToShellNames.TryGetValue(item.title, out var shellNames))
            shellNames = new HashSet<string>();
        Helper.AddItem(
            data: allData,
            guid: new(item.guid.Replace("shell:::", "")),
            shellNames: shellNames,
            clsid: null,
            csidls: new(),
            folderid: null,
            description: item.title,
            os: new(new[] { "11" }),
            source: item.source);
    }
}

{
    foreach (var item in allSrcItems.DaveHullGuidTitle)
    {
        Helper.AddItem(
            data: allData,
            guid: new(item.guid),
            shellNames: new(),
            clsid: null,
            csidls: new(),
            folderid: null,
            description: item.title,
            os: new(),
            source: item.source);
    }
}

{
    foreach (var item in allSrcItems.EricZimmermanGuidTitle)
    {
        Helper.AddItem(
            data: allData,
            guid: new(item.guid),
            shellNames: new(),
            clsid: null,
            csidls: new(),
            folderid: null,
            description: item.title,
            os: new(),
            source: item.source);
    }
}

{
    foreach (var item in allSrcItems.Microsoft2013)
    {
        Helper.AddItem(
            data: allData,
            guid: new(item.guid),
            shellNames: new(),
            clsid: null,
            csidls: new(),
            folderid: null,
            description: item.title,
            os: new(item.winvers.Split(",").Select(x => x.Trim())),
            source: item.source);
    }
}

{
    foreach (var item in allSrcItems.Microsoft2022)
    {
        Helper.AddItem(
            data: allData,
            guid: new(item.guid),
            shellNames: new(),
            clsid: null,
            csidls: new(),
            folderid: null,
            description: item.title,
            os: new(),
            source: item.source);
    }
}

{
    foreach (var item in allSrcItems.MicrosoftKnownFolderId)
    {
        Helper.AddItem(
            data: allData,
            guid: new(item.guid),
            shellNames: new(),
            clsid: null,
            csidls: new(item.csidls),
            folderid: item.folderid,
            description: item.displayName,
            os: new(item.os),
            source: "Microsoft-KnownFolderIds");
    }
}


var allControlPanel = Helper.GetGuidsByDescription(allData, "control panel").ToArray();
foreach (var guid in allControlPanel)
    Console.WriteLine(guid);

var code = CodeGenerator.GenerateClassOfKnownFolders("ShellLink.KnownFolders", allData);
var pathForClassOfKnownFoldersCs = Helper.FindFilePath("ShellLink\\KnownFolders\\KnownFolders.generated.cs");
await File.WriteAllTextAsync(pathForClassOfKnownFoldersCs, code);

var json = JsonConvert.SerializeObject(allData, Formatting.Indented);
var pathForClassOfKnownFoldersJson = Helper.FindFilePath("ShellLink\\KnownFolders\\AllKFData.json");
await File.WriteAllTextAsync(pathForClassOfKnownFoldersJson, json);
