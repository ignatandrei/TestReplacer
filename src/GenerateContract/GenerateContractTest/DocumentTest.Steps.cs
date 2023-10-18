
using DocumentFormat.OpenXml.Spreadsheet;
using FluentAssertions;
using System.ComponentModel;
using System.Text.Unicode;

namespace GenerateContractTest;

public partial class DocumentTest : FeatureFixture
{
    DocumentList docs = new();
    Document? doc;
    async Task Given_Documents_Location(string location)
    {
        await docs.InitializeFromHdd(location,false);
    }

    Task Then_Must_Find_Document(string name)
    {

        var docsFind = docs.FindDocument(name);
        docsFind.Should().NotBeNull();
        docsFind!.Length.Should().Be(1);
        doc = docsFind.First();
        return Task.CompletedTask;
    }
    async Task Then_The_Document_Should_Have_Replacements()
    {
        await doc!.Initialize();
        doc.CountReplacements().Should().BeGreaterThan(0);
    }
    Task Then_The_Document_Should_Have_The_Replacement( string term, int occurences)
    {
        if(occurences <= 0)
        {
            doc!.Replacement(term).Should().BeNull();
            return Task.CompletedTask;
        }

        doc!.Replacement(term).Should().NotBeNull();
        doc!.Replacement(term)!.Originals.Should().NotBeNullOrEmpty();
        doc!.Replacement(term)!.Originals.Count.Should().Be(occurences);
        return Task.CompletedTask;
    }

    
    Task Then_The_Document_Should_Have_The_Replacement_Case_Insensitive()
    {        
        doc!.Replacement("COmodant").Should().NotBeNull();
        doc!.Replacement("COMODant").Should().NotBeNull();
        doc!.Replacement("comoDANT").Should().Be(doc!.Replacement("Comodant"));
        return Task.CompletedTask;
    }

    async Task Then_The_Document_Should_Replace()
    {
        doc!.AddTextToReplace("Comodant", "Andrei Ignat");
        doc!.AddTextToReplace("Data_Contract", "16/apr/1970");
        var newFile = ("Andrei" + DateTime.Now.ToString("yyyyMMddHHmmss") + "Andrei.docx");
        var cnt= await doc!.Replace();
        await File.WriteAllBytesAsync(newFile, cnt);
        //var str = System.Text.Encoding.UTF8.GetString(cnt);
        //str.Should().NotContain("@!");
        //str.Should().NotContain("!@");
        //TODO: read the word as text
        return ;

    }
}
