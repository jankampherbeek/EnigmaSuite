// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;

namespace Enigma.Api.Communication;

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