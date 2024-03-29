﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Text.Json;
using Enigma.Core.Communication;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;

namespace Enigma.Core.Handlers;


/// <summary>Handler for communication with the outside world.</summary>
public interface ICommunicationHandler
{
    /// <summary>Find info on the latest release of Enigma on the Internet.</summary>
    /// <returns>Info on the latest release. If no connection could be made, ReleaseINfo will contain empty strings.</returns>
    public ReleaseInfo FindLatestRelease();
}

/// <inheritdoc/>
public class CommunicationHandler : ICommunicationHandler
{
    private readonly IHttpRequester _httpRequester;

    public CommunicationHandler(IHttpRequester httpRequester)
    {
        _httpRequester = httpRequester;
    }

    /// <inheritdoc/>
    public ReleaseInfo FindLatestRelease()
    {
        string json = _httpRequester.GetHttpRequest(EnigmaConstants.RELEASE_CHECK_URL);
        return json == "" ? new ReleaseInfo("", "", "", "", "") : JsonSerializer.Deserialize<ReleaseInfo>(json)!;
    }


}

