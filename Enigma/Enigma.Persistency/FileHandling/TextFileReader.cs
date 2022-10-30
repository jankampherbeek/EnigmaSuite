// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Persistency.Interfaces;

namespace Enigma.Persistency.FileHandling;


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