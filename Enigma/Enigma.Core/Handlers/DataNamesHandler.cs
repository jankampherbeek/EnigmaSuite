﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Persistency;
using Enigma.Domain.Dtos;

namespace Enigma.Core.Handlers;

/// <summary>Handler for data names.</summary>
public interface IDataNamesHandler
{
    /// <summary>Retrieve data names from data folders.</summary>
    /// <returns>Data names.</returns>
    public List<string> GetExistingDataNames();
}

/// <inheritdoc/>
public sealed class DataNamesHandler : IDataNamesHandler
{
    private readonly IFoldersInfo _foldersInfo;

    public DataNamesHandler(IFoldersInfo foldersInfo)
    {
        _foldersInfo = foldersInfo;
    }

    /// <inheritdoc/>
    public List<string> GetExistingDataNames()
    {
        string path = ApplicationSettings.LocationDataFiles;
        return _foldersInfo.GetExistingFolderNames(path, false);
    }

}