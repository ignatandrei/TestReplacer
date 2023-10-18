
namespace GenerateContractTest;
[FeatureDescription(@"find documents and loads")]
public partial class DocumentsTest
{
    [Scenario]
    public async Task FindNumberDocuments()
    {
        await Runner.RunScenarioAsync(
               _ => Given_Documents_Location("docs"),
               _ => Then_Must_Find_Documents(1) 
               );

    }
    [Scenario]
    [InlineData("AndreiIgnat",1)]
    [InlineData("asda@2312", 0)]
    public async Task FindDocuments(string name , int nr)
    {
        await Runner.RunScenarioAsync(
               _ => Given_Documents_Location("docs"),
               _ => Then_Must_Find_Document(name,nr)
               );

    }

}