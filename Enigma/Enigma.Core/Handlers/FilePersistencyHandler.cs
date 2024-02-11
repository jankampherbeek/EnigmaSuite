// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Persistency;

namespace Enigma.Core.Handlers;


/// <summary>Handler for writing and reading files.</summary>
public interface IFilePersistencyHandler
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
public sealed class FilePersistencyHandler : IFilePersistencyHandler
{
    private readonly ITextFileReader _textFileReader;
    private readonly ITextFileWriter _textFileWriter;

    public FilePersistencyHandler(ITextFileReader textFileReader, ITextFileWriter textFileWriter)
    {
        _textFileReader = textFileReader;
        _textFileWriter = textFileWriter;
    }

    /// <inheritdoc/>
    public string ReadFile(string fullPath)
    {
        return _textFileReader.ReadFile(fullPath);
    }

    /// <inheritdoc/>
    public bool WriteFile(string fullPath, string text)
    {
        return _textFileWriter.WriteFile(fullPath, text);

    }
}