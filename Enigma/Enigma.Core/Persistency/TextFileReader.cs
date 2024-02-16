// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Text;
using Serilog;

namespace Enigma.Core.Persistency;


/// <summary>
/// Read text from a file.
/// </summary>
public interface ITextFileReader
{
    /// <summary>Read all text from a file into a string.</summary>
    /// <param name="location">Full path to the text file </param>
    /// <returns>If the file is found and no exception occured: the content of the file. Otherwise: an empty string.</returns>
    public string ReadFile(string location);

    /// <summary>Read all lines from a text file into a list.</summary>
    /// <param name="location">Full path to the text file </param>
    /// <returns>If the file is found and no exception occured: all lines from the file. Otherwise: an empty list.</returns>
    public List<string> ReadAllLines(string location);
}

/// <inheritdoc/>
public sealed class TextFileReader : ITextFileReader
{

    /// <inheritdoc/>
    public string ReadFile(string location)
    {
        string text = "";
        try
        {
            if (File.Exists(location))
            {
                text = File.ReadAllText(location);
            }
        }
        catch (Exception e)
        {
            Log.Error("An exception was thrown in TextFileReader.ReadFile() while reading all text from the file {Location}, the msg of the exception: {Msg}", location, e.Message);
            text = "";
        }
        return text;
    }

    public List<string> ReadAllLines(string location)
    {
        List<string> csvLines = new();
        if (File.Exists(location))
        {
            using StreamReader reader = new StreamReader(location, Encoding.GetEncoding("iso-8859-1"));
            while (reader.Peek() >= 0)
            {
                csvLines.Add(reader.ReadLine());
            }
        }
        return csvLines;
    }
}