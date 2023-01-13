// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Facades.Se;
using Serilog;

namespace Enigma.Core.Handlers.Calc;

/// <summary>Handles initialization of the Swiss Ephemeris (SE).</summary>
public sealed class SeHandler : ISeHandler
{
    /// <summary>Initializes the SE and defiens the path to the SE files.</summary>
    /// <param name="pathToSeFiles"></param>
    public void SetupSe(string pathToSeFiles)
    {
        Log.Information("Initializing SE using path: " + pathToSeFiles);
        SeInitializer.SetEphePath(pathToSeFiles);
        Log.Information("SE initialized.");
    }

}
