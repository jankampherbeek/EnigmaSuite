// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Enigma.Frontend.Support;

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
        catch (FileNotFoundException)
        {
            // todo log exception
        }
        catch (IOException)
        {
            // todo log exception
        }
        return _lines;

    }
}