
using System.Text.Json;
using System.Text.Json.Nodes;


void updateJson(JsonObject obj, Dictionary<string, string> keyValuePairs)
{
    var errorPaths = keyValuePairs.Keys.ToArray();

    foreach (var singlePath in errorPaths)
    {
        var realPath = singlePath.Split('.');
        JsonNode? elem = obj;

        for (int i = 0; i < realPath.Length-1; i++)
        {
            JsonNode? next = elem?[realPath[i]];
            elem = next;
        }
        if(elem != null)
        elem[realPath.Last()] = keyValuePairs[singlePath];
    }
    
}


Dictionary<string, string> keyValuePairs = new();


foreach (var ln in File.ReadAllLines(args[1]).Skip(1))
{

    string[] line = ln.Split(",");
    if (line.Length > 0 && line[1]!= "")
    {
      
        string errToast = line[0].TrimEnd(':');

        string en_us = line[3];
      
        if(!String.IsNullOrEmpty(en_us))
            keyValuePairs.Add(errToast, en_us);
    }
}


string filePath = @"C:\Users\kyoon\src\LBMX.Phoenix.Web.App.Main\src";
string? directoryName;

directoryName = Path.GetDirectoryName(filePath);
directoryName += @"\src\assets\i18n";

string[] subfolder = {@"\marketplace", @"\sc" };
List<string> totalEntries = new();


foreach (var name in subfolder)
{
    string fullPath1 = Path.GetFullPath(directoryName + name);
    List<string> fileEntries = Directory.GetFiles(fullPath1).Where(p => p.EndsWith("en-us.json")).ToList();
    totalEntries.AddRange(fileEntries);
}


foreach (string languageFile in totalEntries)
{
    JsonNode? doc;
    JsonObject root;


    using (FileStream fs = new FileStream(languageFile, FileMode.Open, FileAccess.ReadWrite))
    {
        doc = JsonNode.Parse(fs);
        root = doc!.Root.AsObject();
        fs.Position = 0;
    }

    updateJson(root, keyValuePairs);

    var jsonOptions = new JsonSerializerOptions() { WriteIndented = true };
    var coderJson = doc.ToJsonString(jsonOptions);
    
    System.IO.File.WriteAllText(languageFile, coderJson);
}






