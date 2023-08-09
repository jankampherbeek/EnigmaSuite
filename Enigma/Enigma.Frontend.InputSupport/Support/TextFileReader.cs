// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Interfaces;
using Serilog;

namespace Enigma.Frontend.Helpers.Support;

public class TextFileReader : ITextFileReaderFe
{
    private IEnumerable<string> _lines = new List<string>();

    public string ReadAllText(string fileName)
    {
        string allText = File.ReadAllText(fileName);
        return allText;
    }

    public IEnumerable<string> ReadSeparatedLines(string fileName)
    {
        IEnumerable<string> readSeparatedLines = _lines.ToList();
        try
        {
            _lines = File.ReadLines(fileName);
            return readSeparatedLines;
        }
        catch (FileNotFoundException e)
        {
            Log.Error("Could not read file {FileName}, a FileNotFoundException was thrown : {Msg}", 
                fileName, e.Message);
        }
        catch (IOException e)
        {
            Log.Error("An IOException was thrown while reading file {FileName}. The exception msg was : {Msg}", 
                fileName, e.Message);
        }
        return readSeparatedLines;

    }
}