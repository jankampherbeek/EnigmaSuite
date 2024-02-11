// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Core.Persistency;

/// <summary>Write text to a file.</summary>
public interface ITextFileWriter
{
    /// <summary>Write a full string to a textfile. Overwrites any existing file with the same name (location).</summary>
    /// <param name="location">Full pathname of the file.</param>
    /// <param name="text">Content to write to the file.</param>
    /// <returns>True is the write was successful, otherwise false.</returns>
    public bool WriteFile(string location, string text);

    /// <summary>Writes multiple lines to a textfile. Overwrites any existing file with the same name (location).</summary>
    /// <param name="location">Full pathname of the file.</param>
    /// <param name="textLines">Content to write to the file.</param>
    /// <returns>True is the write was successful, otherwise false.</returns>
    public bool WriteFile(string location, List<string> textLines);
}

/// <inheritdoc/>
public sealed class TextFileWriter : ITextFileWriter
{

    /// <inheritdoc/>
    public bool WriteFile(string location, string text)
    {
        bool success = true;
        try
        {
            File.WriteAllText(location, text);
        }
        catch (Exception e)
        {
            Log.Error("An exception {E} was thrown when TextFileWriter was writing a text to file {Location}. Text to write: {Text}", e.Message, location, text);
            success = false;
        }
        return success;
    }

    public bool WriteFile(string location, List<string> textLines)
    {
        bool success = true;
        try
        {
            File.WriteAllLines(location, textLines);
        }
        catch (Exception e)
        {
            Log.Error("An exception {E} ws thrown when TextFileWriter was writing several textlines to file {Location}. Text to write: {@TextLines}", e.Message, location, textLines);
            success = false;
        }
        return success;
    }
}