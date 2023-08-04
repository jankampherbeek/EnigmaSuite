// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Serilog;

namespace Enigma.Core.Handlers.Persistency.Helpers;


/// <inheritdoc/>
public sealed class TextFileReader : ITextFileReader
{

    /// <inheritdoc/>
    public string ReadFile(string location)
    {
        string text = "";
        try
        {
            if (File.Exists(location))
            {
                text = File.ReadAllText(location);
            }
        }
        catch (Exception e)
        {
            Log.Error("An exception was thrown in TextFileReader.ReadFile() while reading all text from the file {Location}, the msg of the exception: {Msg}", location, e.Message);
            text = "";
        }
        return text;
    }

    public List<string> ReadAllLines(string location)
    {
        List<string> csvLines = new();
        try
        {
            if (File.Exists(location))
            {
                string[] allLines = File.ReadAllLines(location);
                csvLines.AddRange(allLines);
            }
        }
        catch (Exception e)
        {
            Log.Error("An exception was thrown in TextFileReader.ReadAllLines() while reading separate lines from the file {Location}, the msg of the exception: {Msg}", location, e.Message);
        }
        return csvLines;
    }
}