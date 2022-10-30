// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Interfaces;
using System.Collections.Generic;

namespace Enigma.Frontend.Support;

public class Rosetta : IRosetta
{
    readonly private ITextFileReaderFE _fileReader;
    readonly private List<KeyValuePair<string, string>> _texts;

    public Rosetta(ITextFileReaderFE fileReader)
    {
        _fileReader = fileReader;
        _texts = new List<KeyValuePair<string, string>>();
        ReadTextsFromFile();
    }

    public string TextForId(string id)
    {
        foreach (KeyValuePair<string, string> _text in _texts)
        {
            if (_text.Key == id) return _text.Value;
        }
        return "-NOT FOUND-";
    }

    private void ReadTextsFromFile()
    {
        string[] _partsOfText;
        string _filename = @"res\texts.txt";
        IEnumerable<string> _allLines = _fileReader.ReadSeparatedLines(_filename);

        foreach (string _line in _allLines)
        {
            _partsOfText = _line.Split('=');
            // skip empty lines and remarks
            if (_partsOfText.Length == 2)
            {
                _texts.Add(new KeyValuePair<string, string>(_partsOfText[0].Trim(), _partsOfText[1].Trim()));
            }
        }
    }

}
