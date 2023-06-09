﻿
using System.Text;

namespace GenerateContractTest;
[FeatureDescription(@"find document and replace")]

public partial class DocumentTest
{
    [Scenario]
    [InlineData("Comodat", "NUME_Contractant",1)]
    [InlineData("Comodat", "DataNastere_Contractant",1)]
    [InlineData("Comodat", "alabala", 0)]
    public async Task ExistsReplacements(string name, string term, int occurences)
    {
        await Runner.RunScenarioAsync(
               _ => Given_Documents_Location("docs"),
               _ => Then_Must_Find_Document(name),
               _ => Then_The_Document_Should_Have_Replacements(),
               _ => Then_The_Document_Should_Have_The_Replacement(term,occurences)
               );

    }
    [Scenario]
    [InlineData("Comodat")]
    public async Task ExistsReplacementsInsensitive(string name)
    {
        await Runner.RunScenarioAsync(
               _ => Given_Documents_Location("docs"),
               _ => Then_Must_Find_Document(name),
               _=> Then_The_Document_Should_Have_Replacements(),
               _=> Then_The_Document_Should_Have_The_Replacement_Case_Insensitive()
               );

    }


    [Scenario]
    [InlineData("Comodat")]

    public async Task Replace(string name)
    {
        await Runner.RunScenarioAsync(
               _ => Given_Documents_Location("docs"),
               _ => Then_Must_Find_Document(name),
               _ => Then_The_Document_Should_Have_Replacements(),
               _ => Then_The_Document_Should_Replace()
               );

    }

    
}
