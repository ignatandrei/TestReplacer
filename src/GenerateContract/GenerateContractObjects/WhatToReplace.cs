namespace GenerateContractObjects;

public class WhatToReplace
{
    public WhatToReplace(string original)
    {
        Originals = new List<string>();
        Originals.Add(original);
        Name = original.Replace("_", " ");
    }
    public List<string> Originals { get; set; }
    public string Name { get; set; }
    internal string? ReplaceText { get; set; }
}
    