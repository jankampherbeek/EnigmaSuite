// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.be.persistency;
using System.Collections.Generic;

namespace E4C.Views.ViewHelpers
{

    /// <summary>
    /// Manage texts that are stored in an external dictionary file.
    /// </summary>
    public interface IRosetta
    {
        /// <summary>
        /// Retrieve text for a specific id.
        /// </summary>
        /// <param name="id">The id to search.</param>
        /// <returns>The text for the Id. Returns the string '-NOT FOUND-' if the text could not be found.</returns>
        public string TextForId(string id);
    }

    public class Rosetta : IRosetta
    {
        readonly private ITextFromFileReader _fileReader;
        readonly private List<KeyValuePair<string, string>> _texts;

        public Rosetta(ITextFromFileReader fileReader)
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
            string _filename = @"..\..\..\res\texts.txt";
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

}