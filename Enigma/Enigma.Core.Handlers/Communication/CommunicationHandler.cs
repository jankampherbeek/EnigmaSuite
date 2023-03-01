// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Communication;
using Enigma.Domain.Constants;
using Newtonsoft.Json;

namespace Enigma.Core.Handlers.Communication;

/// <inheritdoc/>
public class CommunicationHandler: ICommunicationHandler
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
        if (json == "") {
            return new ReleaseInfo("", "", "", "", "");
        }
        else
        {
            return JsonConvert.DeserializeObject<ReleaseInfo>(json);
        }
    }


}

