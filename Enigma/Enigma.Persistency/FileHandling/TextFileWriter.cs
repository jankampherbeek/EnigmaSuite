// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Persistency.Interfaces;
using Serilog;

namespace Enigma.Persistency.FileHandling;



/// <inheritdoc/>
public class TextFileWriter : ITextFileWriter
{

    /// <inheritdoc/>
    public bool WriteFile(string location, string text)
    {
        bool success = true;
        try
        {
            File.WriteAllText(location, text);
        }
        catch (Exception e)
        {
            Log.Error("An exception {e} occurred when writing a text to file {location}. Text to write: {text}", e.Message, location, text);
            success = false;
        }
        return success;
    }

    public bool WriteFile(string location, List<string> textLines)
    {
        bool success = true;
        try
        {
            File.WriteAllLines(location, textLines);
        }
        catch (Exception e)
        {
            Log.Error("An exception {e} occurred when writing several textlines to file {location}. Text to write: {@textLines}", e.Message, location, textLines);
            success = false;
        }
        return success;
    }
}