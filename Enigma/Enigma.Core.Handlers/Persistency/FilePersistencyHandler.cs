// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;

namespace Enigma.Core.Handlers.Persistency;


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