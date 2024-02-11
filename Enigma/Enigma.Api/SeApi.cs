// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;
using Serilog;

namespace Enigma.Api;

/// <summary>API for managing the Swiss Ephemeris.</summary>
public interface ISeApi
{
    /// <summary>Initialize the CommonSE.</summary>
    /// <param name="pathToSeFiles">Full path to datafiles for the CommonSE.</param>
    public void SetupSe(string? pathToSeFiles);

    public void CloseSe();
}


/// <inheritdoc/>
public sealed class SeApi : ISeApi
{
    private readonly ISeHandler _seHandler;

    public SeApi(ISeHandler seHandler) => _seHandler = seHandler;


    /// <inheritdoc/>
    public void SetupSe(string? pathToSeFiles)
    {
        Log.Information("SeApi SetupSe: Setting up CommonSE with path {Path}", pathToSeFiles);
        _seHandler.SetupSe(pathToSeFiles);
    }

    public void CloseSe()
    {
        _seHandler.CloseSe();
    }
}
