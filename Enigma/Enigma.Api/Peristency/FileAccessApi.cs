// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Interfaces;

namespace Enigma.Api.Persistency;

/// <inheritdoc/>
public class FileAccessApi : IFileAccessApi
{
    private IFilePersistencyHandler _filePersistencyHandler;


    public FileAccessApi(IFilePersistencyHandler filePersistencyHandler)
    {
        _filePersistencyHandler = filePersistencyHandler;
    }

    /// <inheritdoc/>
    public bool WriteFile(string path, string text)
    {
        return _filePersistencyHandler.WriteFile(path, text);
    }

    /// <inheritdoc/>
    public string ReadFile(string path)
    {
        return _filePersistencyHandler.ReadFile(path);
    }


}