
namespace GenerateContractTest;

public partial class DocumentsTest: FeatureFixture
{
    DocumentList docs = new();
    async Task Given_Documents_Location(string location)
    {
        await docs.InitializeFromHdd(location,false);
    }
    Task Then_Must_Find_Documents(int nr)
    {
        var comodat = docs.FindDocument("AndreiIgnat");
        comodat.Should().NotBeNullOrEmpty();
        comodat!.Count().Should().Be(nr);
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