// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Ui.Interfaces;
using Serilog;
using System.Collections.Generic;
using System.IO;

namespace Enigma.Frontend.Ui.Support;

public class TextFileReader : ITextFileReaderFE
{
    private IEnumerable<string> _lines;

    public TextFileReader()
    {
        _lines = new List<string>();
    }

    public string ReadAllText(string fileName)
    {
        string _allText = File.ReadAllText(fileName);
        return _allText;
    }

    public IEnumerable<string> ReadSeparatedLines(string fileName)
    {
        try
        {
            _lines = File.ReadLines(fileName);
            return _lines;
        }
        catch (FileNotFoundException e)
        {
            Log.Error("Could not read file {fileName}, a FileNotFoundException was thrown : {e}", fileName, e);
        }
        catch (IOException e)
        {
            Log.Error("An IOException was thrown while reading file {fileName}. The exception msg was : {e}", fileName, e);
        }
        return _lines;

    }
}