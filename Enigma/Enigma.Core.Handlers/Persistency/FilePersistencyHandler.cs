// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Work.Persistency.Interfaces;

namespace Enigma.Core.Handlers.Persistency;


/// <inheritdoc/>
public class FilePersistencyHandler: IFilePersistencyHandler
{
    private ITextFileReader _textFileReader;
    private ITextFileWriter _textFileWriter;

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