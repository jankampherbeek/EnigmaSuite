// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Text;

namespace Enigma.Persistency.FileHandling;

/// <summary>
/// Read text from a file.
/// </summary>
public interface ITextFileReader
{
    /// <summary>Read all text from a file inton a string.</summary>
    /// <param name="location"></param>
    /// <returns>If the file is found and no exception occured: the content of the file. Otherwile: an empty string.</returns>
    public string ReadFile(string location);
}
/// <inheritdoc/>
public class TextFileReader : ITextFileReader
{

    /// <inheritdoc/>
    public String ReadFile(string location)
    {
        string text = "";
        try
        {
            if (File.Exists(location))
            {
                text = File.ReadAllText(location);
            } 
        }
        catch (Exception)
        {
            text = "";
        }
        return text;
    }
}