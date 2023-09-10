// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Text.Json;
using Enigma.Core.Interfaces;
using Enigma.Domain.Communication;
using Enigma.Domain.Constants;

namespace Enigma.Core.Communication;

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

