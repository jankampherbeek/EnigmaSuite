// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Work.Persistency.Interfaces;
using Serilog;

namespace Enigma.Core.Work.Persistency;



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
            Log.Error("An exception {e} was thrown when TextFileWriter was writing a text to file {location}. Text to write: {text}", e.Message, location, text);
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
            Log.Error("An exception {e} ws thrown when TextFileWriter was writing several textlines to file {location}. Text to write: {@textLines}", e.Message, location, textLines);
            success = false;
        }
        return success;
    }
}