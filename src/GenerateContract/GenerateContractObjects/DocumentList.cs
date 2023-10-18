using System.Linq;

namespace GenerateContractObjects;
public class DocumentList
{
    public string? location;
    private string[]? Documents;
    public long Count()
    {
        return Documents?.LongLength??0; 
    }
    public string[]? FindDocuments()
    {
        return Documents;
    }
    public Document[]? FindDocument(string name)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(name);
        ArgumentNullException.ThrowIfNullOrEmpty(location);

        if (Documents == null)
            return null;

        var docs = Documents.Where(it=>it.Contains(name,StringComparison.InvariantCultureIgnoreCase));
        return  docs.Select(it=> new Document(it)).ToArray();
    }

    public Task InitializeFromHdd(string location, bool excludeTest)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(location));
        if(!Directory.Exists(location))
        {
            throw new DirectoryNotFoundException(location);
        }
        this.location = location;
        Documents = Directory.GetFiles(location, "*.doc?");
        if (excludeTest)
        {
            Documents =
                Documents
                .Where(it => !Path.GetFileNameWithoutExtension(it).StartsWith("Test"))
                .ToArray();
        }
        return Task.CompletedTask;
    }
}
    