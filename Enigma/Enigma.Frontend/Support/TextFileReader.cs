// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.IO;

namespace Enigma.Frontend.Support;

public interface ITextFileReader
{
    public IEnumerable<string> ReadSeparatedLines(string fileName);
    public string ReadAllText(string fileName);
}

public class TextFileReader : ITextFileReader
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