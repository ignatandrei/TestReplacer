namespace GenerateContractTest;
[FeatureDescription(@"Contract andrei")]

public partial class TestContractPrestariServiciiAndrei
{
    [Scenario]
    public async Task ReplaceData()
    {
        await Runner.RunScenarioAsync(
               _ => Given_Documents_Location("docs"),
               _ => Then_Must_Find_Document(),
               _ => Then_Must_HaveReplacements()
               );

    }
}
