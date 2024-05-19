// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Persistency;

namespace Enigma.Core.Handlers;

/// <summary>Handler for resource bundles.</summary>
public interface IResourceBundleHandler
{
    /// <summary>Read all texts from a resourcebundle.</summary>
    /// <param name="path">The path including the filename.</param>
    /// <returns>All texts.</returns>
    public Dictionary<string, string> GetAllTextsFromResourceBundle(string path);
}

/// <inheritdoc/>
public class TestResourceBundleHandler:IResourceBundleHandler
{
    private readonly ITextFileReader _fileReader;

    public TestResourceBundleHandler(ITextFileReader fileReader)
    {
        _fileReader = fileReader;
    }
    
    /// <inheritdoc/>
    public Dictionary<string, string> GetAllTextsFromResourceBundle(string path)
    {
        List<string> allLines = _fileReader.ReadAllLines(path);
        return ProcessLines(allLines);
    }

    private Dictionary<string, string> ProcessLines(List<string> lines)
    {
        const char splitter = '=';
        return (from line in lines where line.Contains('=') select line.Split(splitter)).
            ToDictionary(keyAndValue => keyAndValue[0].Trim(), keyAndValue => keyAndValue[1].Trim());
    }
}