
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Xml.Linq;

namespace GenerateContractObjects;

public class Document
{
    
    public Document(string location)
    {
        
        Location = location;
        var comparer = StringComparer.OrdinalIgnoreCase;
        replacements = new (comparer);
    }
    public string Location { get; }
    private Dictionary<string,WhatToReplace> replacements { get; set; }
    public int CountReplacements()
    {
        return replacements.Count;
    }
    public WhatToReplace? Replacement(string name)
    {
        if (replacements.ContainsKey(name))
            return replacements[name];
        else
            return null;
    }
    public string[] Replacements()
    {
        return replacements.Keys.ToArray();
    }
    public Task Initialize()
    {
        List<WhatToReplace> result = new();
        string xml = "";

        using (WordprocessingDocument myDocument = WordprocessingDocument.Open(Location, true))
        {
            xml = myDocument.MainDocumentPart!.Document!.Body!.InnerText;
        }
        var arr = xml.Split("@!");
        for (var i = 1; i < arr.Length; i++)
        {
            var item = arr[i];
            var index = item.IndexOf("!@");
            if (index < 1)
                throw new ApplicationException("cannot parse doc");
            
            result.Add(new WhatToReplace(item.Substring(0, index)));
        }


        foreach(var item in result)
        {

            if (!replacements.ContainsKey(item.Originals.First()))
                replacements.Add(item.Originals.First(), item);
            else
                replacements[item.Originals.First()].Originals.Add(item.Originals.First());
        }
        //var s = doc.Document.body;
        //await Task.Delay(1000);
        return Task.CompletedTask;

    }

    public void AddTextToReplace(string replacement, string text)
    {
        replacements[replacement].ReplaceText = text;
    }

    private string? FindBookmarkText(BookmarkStart bookmark)
    {
        
        var run = bookmark.Descendants<Text>().Select(it => it.Text).ToArray();
        return string.Join("", run);
        
    }
    public async Task<byte[]> Replace()
    {
        var noReplacement = replacements.Where(it => it.Value.ReplaceText == null).ToArray();
        if (noReplacement.Length > 0)
            throw new ArgumentException("please add replacements for" + noReplacement[0].Key);

        //File.Copy(Location, locationNewFile);
        var content = await File.ReadAllBytesAsync(Location);
        using var ms = new MemoryStream(content);
        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(ms, true))
        {
            var body = wordDoc!.MainDocumentPart!.Document!.Body;
            var cc = body!.Descendants<SdtElement>();
            foreach(var item in cc)
            {
                var s = item.Descendants<SdtContentRun>();
                var text = item.InnerText;
                foreach (var rep in replacements)
                {
                    foreach (var orig in rep.Value.Originals)
                        text = text.Replace("@!" + orig + "!@", rep.Value.ReplaceText);
                }

                item.PreviousSibling()!.Append(new Run(new Text(text)));
                                
            }
            while(body!.Descendants<SdtElement>().Count() > 0)
            {
                body!.Descendants<SdtElement>().First().Remove();
            }

            wordDoc.Save();

        }
        ms.Position = 0;

        return ms.ToArray();

    }
}
    