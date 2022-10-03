// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Persistency.FileHandling;

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
public class TextFileWriter: ITextFileWriter
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
            success = false;
            // TODO log
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
            success = false;
            // TODO log
        }
        return success;
    }
}