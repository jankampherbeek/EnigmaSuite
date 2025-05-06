// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


namespace Enigma.Core.Persistency;

/// <summary>Handler for writing and reading files.</summary>
public interface IFilePersistencyHandler    // TODO remove FilePersistencyHandler, this is an unnecessary layer
{
    /// <summary>Reads a textfile.</summary>
    /// <param name="fullPath">Full path to the file to read.</param>
    /// <returns>The textual content of the file.</returns>
    public string ReadFile(string fullPath);

    /// <summary>Writes a textfile.</summary>
    /// <param name="fullPath">Full path to the file to write.</param>
    /// <param name="text">The text to write.</param>
    /// <returns>True if the file was successfully written, otherwise false.</returns>
    public bool WriteFile(string fullPath, string text);
}

/// <inheritdoc/>
public sealed class FilePersistencyHandler(ITextFileReader textFileReader, ITextFileWriter textFileWriter)
    : IFilePersistencyHandler
{
    /// <inheritdoc/>
    public string ReadFile(string fullPath)
    {
        return textFileReader.ReadFile(fullPath);
    }

    /// <inheritdoc/>
    public bool WriteFile(string fullPath, string text)
    {
        return textFileWriter.WriteFile(fullPath, text);

    }
}