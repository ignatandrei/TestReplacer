namespace GenerateContractTest;

partial class TestContractPrestariServiciiAndrei : FeatureFixture
{
    DocumentList docs = new();
    Document? d;
    private Document MyDoc()
    {
        ArgumentNullException.ThrowIfNull(d);
        return d;
    }
    async Task Given_Documents_Location(string location)
    {
        await docs.InitializeFromHdd(location, false);
    }
    
    Task Then_Must_Find_Document()
    {

        var docsFind = docs.FindDocument("ContractPrestariServiciiAndrei");
        docsFind.Should().NotBeNull(); 
        docsFind!.Length.Should().Be(1);
        d =docsFind!.First();
        return Task.CompletedTask;
    }
    async Task Then_Must_HaveReplacements()
    {
        var doc = MyDoc();
        await doc.Initialize();
        doc.Replacements().Should().NotBeNull();
        doc.Replacements().Count().Should().Be(7);
    }
}
