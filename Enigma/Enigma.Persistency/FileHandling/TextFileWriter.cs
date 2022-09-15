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
}

/// <inheritdoc/>
public class TextFileWriter: ITextFileWriter
{

    /// <inheritdoc/>
    public bool WriteFile(string location, string text)
    {
        bool succes = true;
        try
        {
            File.WriteAllText(location, text);
            

   /*         FileStream fileStream = new(location, FileMode.CreateNew, FileAccess.Write);
            StreamWriter streamWriter = new(fileStream);
            streamWriter.WriteLine(text);
            streamWriter.Flush();
            streamWriter.Close();
            fileStream.Close(); */
        }
        catch (Exception e)
        {
            succes = false;
        }
        return succes;
    }
}