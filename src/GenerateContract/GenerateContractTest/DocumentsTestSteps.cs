
namespace GenerateContractTest;

public partial class DocumentsTest: FeatureFixture
{
    DocumentList docs = new();
    async Task Given_Documents_Location(string location)
    {
        await docs.InitializeFromHdd(location);
    }
    Task Then_Must_Find_Documents(long nr)
    {
        
        docs.Count().Should().Be(nr);
        return Task.CompletedTask;
    }
    Task Then_Must_Find_Document(string name, int number)
    {

        var docsFind = docs.FindDocument(name);
        docsFind.Should().NotBeNull();
        docsFind!.Length.Should().Be(number);
        return Task.CompletedTask;
    }

}