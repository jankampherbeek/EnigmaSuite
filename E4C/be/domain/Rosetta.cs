// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using E4C.be.persistency;

namespace E4C.be.domain
{

    /// <summary>
    /// Manage texts that are stored in an external dictionary file.
    /// </summary>
    public interface IRosetta
    {
        /// <summary>
        /// Retrieve text for a specific id.
        /// </summary>
        /// <param name="Id">The id to search.</param>
        /// <returns>The text for the Id. Returns the string '-NOT FOUND-' if the text could not be found.</returns>
        public string TextForId(string Id);
    }

    public class Rosetta: IRosetta
    {
        private ITextFromFileReader fileReader;
        readonly private List<KeyValuePair<string, string>> texts;

        public Rosetta(ITextFromFileReader fileReader)
        {
            this.fileReader = fileReader;
            texts = new List<KeyValuePair<string, string>>();
            ReadTextsFromFile();
        }

        public string TextForId(string Id)
        {
            foreach (KeyValuePair<string, string> text in texts)
            {
                if (text.Key == Id) return text.Value;
            }
            return "-NOT FOUND-";
        }

        private void ReadTextsFromFile()
        {
            string[] PartsOfText;
            string filename = @"..\..\..\res\texts.txt";
            IEnumerable<string> allLines = fileReader.readSeparatedLines(filename);

            foreach (string line in allLines)
            {
                PartsOfText = line.Split('=');
                // skip empty lines and remarks
                if (PartsOfText.Length == 2)   
                {
                    texts.Add(new KeyValuePair<string, string>(PartsOfText[0].Trim(), PartsOfText[1].Trim()));
                }
            }
        }

    }

}