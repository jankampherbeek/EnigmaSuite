// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Communication;

namespace Enigma.Api.Interfaces;

/// <summary>API for communication over the internet.</summary>
public interface ICommunicationApi
{
    /// <summary>Retrieve info about the latest release from the RadixPro site.</summary>
    /// <returns>Info about the latest release,</returns>
    public ReleaseInfo LatestAvaialableRelease();
}