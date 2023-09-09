// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Serilog;

namespace Enigma.Core.Persistency.Helpers;



/// <inheritdoc/>
public sealed class TextFileWriter : ITextFileWriter
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
            Log.Error("An exception {E} was thrown when TextFileWriter was writing a text to file {Location}. Text to write: {Text}", e.Message, location, text);
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
            Log.Error("An exception {E} ws thrown when TextFileWriter was writing several textlines to file {Location}. Text to write: {@TextLines}", e.Message, location, textLines);
            success = false;
        }
        return success;
    }
}