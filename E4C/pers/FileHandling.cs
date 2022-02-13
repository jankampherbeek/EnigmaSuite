// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.IO;

namespace E4C.be.persistency
{
    public interface ITextFromFileReader
    {
        public IEnumerable<string> ReadSeparatedLines(string fileName);
        public string ReadAllText(string fileName);
    }

    public class TextFromFileReader : ITextFromFileReader
    {
        private IEnumerable<string> _lines;

        public TextFromFileReader()
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
            catch (FileNotFoundException fnfe)
            {
                // todo log exception
            }
            catch (IOException ioe)
            {
                // todo log exception
            }
            return _lines;

        }
    }
}