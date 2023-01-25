// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Handlers.Interfaces;

/// <summary>Interfaces for handlers that manage the Swiss Ephemeris.</summary>
public interface ISeHandler
{
    /// <summary>Initialize the Se.</summary>
    /// <param name="pathToSeFiles">Full path to the CommonSE data files.</param>
    public void SetupSe(string pathToSeFiles);

    public void CloseSe();
}

