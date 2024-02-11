// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;

namespace Enigma.Api;

/// <summary>API for communication over the internet.</summary>
public interface ICommunicationApi
{
    /// <summary>Retrieve info about the latest release from the RadixPro site.</summary>
    /// <returns>Info about the latest release,</returns>
    public ReleaseInfo LatestAvaialableRelease();
}

/// <inheritdoc/>
public class CommunicationApi : ICommunicationApi
{
    private readonly ICommunicationHandler _communicationHandler;

    public CommunicationApi(ICommunicationHandler communicationHandler)
    {
        _communicationHandler = communicationHandler;
    }

    /// <inheritdoc/>
    public ReleaseInfo LatestAvaialableRelease()
    {
        return _communicationHandler.FindLatestRelease();
    }
}