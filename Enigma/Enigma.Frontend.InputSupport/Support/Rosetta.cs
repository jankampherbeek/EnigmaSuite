// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Interfaces;

namespace Enigma.Frontend.Helpers.Support;


/// <summary>Handler for texts from resource bundle.</summary>
/// <remarks>Implemented as static class.</remarks>
public static class Rosetta
{
    private static readonly bool _initialized = false;
    private static ITextFileReaderFE _fileReader;
    private static List<KeyValuePair<string, string>> _texts;

    private static void CheckInit()
    {
        if (!_initialized)
        {
            _fileReader = new TextFileReader();
            _texts = new List<KeyValuePair<string, string>>();
            ReadTextsFromFile();
        }
    } 


    public static string TextForId(string id)
    {
        CheckInit();
        foreach (KeyValuePair<string, string> _text in _texts)
        {
            if (_text.Key == id) return _text.Value;
        }
        return "-NOT FOUND-";
    }

    private static void ReadTextsFromFile()
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
