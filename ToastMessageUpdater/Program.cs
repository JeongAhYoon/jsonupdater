using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

//using Bogus;
//using Bogus.Extensions;



void updateJson(JsonNode obj, Dictionary<string, string> keyValuePairs)
{
    //What do we have?
    //We have the main json node (all values)
    //We have all updated values in keyValuePairs (PIM.PRODUCT_ADD_VALUE, "Error message string")

    var errorPaths = keyValuePairs.Keys.ToArray();


        foreach (var prod in obj.AsArray())
        {

            foreach (var singlePath in errorPaths)
            {

                var elem = prod;
                foreach (var prop in singlePath)
                {
                    elem = elem[prop];

                }

            elem[singlePath.Last()] = keyValuePairs[singlePath];
            }


        }






    





    //int c = 0;
    //foreach (var child in errorPath)
    //{

      

    //    var elem = node;
    //    var realPath = child.Split('.');
    //    string value = null; 
    //    foreach (var i in realPath)
    //    {
    //        elem = elem[i];
          
            
    //        elem[i] = keyValuePairs[child];
           
          
    //    }

    //    node[realPath.Length - 1] = elem.ToJsonString();
    //    //node[i] = keyValuePairs[i];
    //    c++;
    //}
}



Dictionary<string, string> keyValuePairs = new();


foreach (var ln in File.ReadAllLines("PIM-81 Toast Messaging - Complete Error States.csv").Skip(1))
{

    string[] line = ln.Split(",");
    if (line.Length > 0 && line[1]!= "")
    {
        Console.WriteLine(line.Length);

        string errToast = line[0];
      
        string en_us = line[3];

        if(!String.IsNullOrEmpty(en_us))
            keyValuePairs.Add(errToast, en_us);
    }
}

var sortedDict = keyValuePairs.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);

string filePath = @"C:\Users\kyoon\src\LBMX.Phoenix.Web.App.Main\src";
string directoryName;
directoryName = Path.GetDirectoryName(filePath);
directoryName += @"\src\assets\i18n\sc";

string fullPath2 = Path.GetFullPath(directoryName);

string[] fileEntries = Directory.GetFiles(fullPath2);

foreach (string languageFile in fileEntries)
{
    //load up error message csv

    //iterate over values

    //find key you're searching for

    //overwrite with new value

    //save file
}


FileStream fs = new FileStream("en-us.json", FileMode.Open, FileAccess.ReadWrite);

fs.Position = 0;

var doc = JsonNode.Parse(fs);

var root = doc!.Root.AsObject();

//var a = doc.Root switch
//{
//    JsonObject o => "object",
//    JsonArray a2 => "array",
//    JsonValue v => "value",
//    _ => throw new NotImplementedException()
//};

var objects = root!["PIM"];


updateJson(doc, keyValuePairs);


var jsonOptions = new JsonSerializerOptions() { WriteIndented = true };
var coderJson = doc.ToJsonString(jsonOptions);


System.IO.File.WriteAllText("en-u1.json", coderJson);




Console.WriteLine(objects);








//var options = new JsonWriterOptions
//{
//    Indented = true
//};

//using var stream = new MemoryStream();
//using var writer = new Utf8JsonWriter(stream, options);

//writer.WriteStartObject();

//foreach (var obj in objects)
//{
//    var targetCategory = obj.Key;

//    if (targetCategory == "PIM")
//    {
//        var targetObject = root![targetCategory]!.AsObject();

//        foreach (var item in targetObject)
//        {

//            if (item.Key.Equals("ANALYTICS"))
//            {
//                item.Value?.ReplaceWith("test");
//            }

//        }

//    }
//    else 
//    {
//        writer.WritePropertyName(targetCategory);
//        obj.Value?.WriteTo(writer);
//}


//}

//writer.WriteEndObject();
//writer.Flush();

//string json = Encoding.UTF8.GetString(stream.ToArray());
//Console.WriteLine(json);

//var detailed = root!["PIM"]!;
//var detailedObj = detailed.AsObject();

//string[] propertyNamed = detailedObj.Select(p => p.Key).ToArray();




//foreach (var property in propertyNamed)
//{



//   var idx = sortedDict.Count();
//   bool isThere = SearchforKey(0, idx, property, dictList);

//    if (isThere)


//    Console.WriteLine(property);
//}






////Console.WriteLine(detailedObj);


//for (int i = 0; i < detailedObj.Count(); i++)
//{


//    string prptyname = detailedObj.GetPropertyName();

//        Console.WriteLine(prptyname);

//}




////var firstPrpty = doc!.GetPropertyName().ToImmutableList();

////foreach (var kv in firstPrpty)
//// Console.WriteLine(firstPrpty);


////Console.WriteLine(doc);


//JsonWriterOptions writerOptions = new() { Indented = true, };

//using MemoryStream stream = new();
//using Utf8JsonWriter writer = new(stream, writerOptions);

//writer.WriteStartObject();









