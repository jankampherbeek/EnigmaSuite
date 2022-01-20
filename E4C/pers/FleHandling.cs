// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.IO;

namespace E4C.be.persistency
{
    public interface ITextFromFileReader
    {
        public IEnumerable<string> readSeparatedLines(string fileName);
        public string readAllText(string fileName);
    }

    public class TextFromFileReader : ITextFromFileReader
    {
        public string readAllText(string fileName)
        {
            string allText = File.ReadAllText(fileName);
            return allText;
        }

        public IEnumerable<string>? readSeparatedLines(string fileName)
        {
            try
            {
                IEnumerable<string> lines = File.ReadLines(fileName);
                return lines;
            } 
            catch (FileNotFoundException fnfe)
            {
                // todo log exception
            }
            catch (IOException ioe)
            {
                // todo log exception
            }
            return null;
           
        }
    }
}