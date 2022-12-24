// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Interfaces;

namespace Enigma.Frontend.Helpers.Support;


/// <summary>Handler for texts from resource bundle.</summary>
/// <remarks>Implemented as singleton, based on code by Jon Skeet: https://csharpindepth.com/articles/singleton .</remarks>
public sealed class Rosetta
{
    private static readonly Rosetta _instance = new();
    private static readonly bool _initialized = false;
    private static ITextFileReaderFE _fileReader;
    private static List<KeyValuePair<string, string>> _texts;

    // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. N/A becasue Rosetta is a singleton.
    static Rosetta()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor.
    {
    }

    private Rosetta()
    {
    }

    public static Rosetta Instance
    {
        get
        {
            if (!_initialized)
            {
                _fileReader = new TextFileReader();
                _texts = new List<KeyValuePair<string, string>>();
                ReadTextsFromFile();
            }
            return _instance;
        }
    }

    public string TextForId(string id)
    {
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
