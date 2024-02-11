// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Facades.Se;
using Serilog;

namespace Enigma.Core.Handlers;

/// <summary>Interfaces for handlers that manage the Swiss Ephemeris.</summary>
public interface ISeHandler
{
    /// <summary>Initialize the Se.</summary>
    /// <param name="pathToSeFiles">Full path to the CommonSE data files.</param>
    public void SetupSe(string? pathToSeFiles);

    public void CloseSe();
}

/// <summary>Handles initialization of the Swiss Ephemeris (SE).</summary>
public sealed class SeHandler : ISeHandler
{
    /// <summary>Initializes the SE and defines the path to the SE files.</summary>
    /// <param name="pathToSeFiles"></param>
    public void SetupSe(string? pathToSeFiles)
    {
        Log.Information("Initializing SE using path: {Path}", pathToSeFiles);
        SeInitializer.SetEphePath(pathToSeFiles);
        Log.Information("SE initialized");
    }

    public void CloseSe()
    {
        Log.Information("Closing SE");
        SeInitializer.CloseEphemeris();
        Log.Information("SE closed");
    }

}
