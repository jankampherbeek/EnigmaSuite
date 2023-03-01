// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Communication;

namespace Enigma.Core.Handlers.Interfaces;

/// <summary>Performs http requests.</summary>
public interface IHttpRequester
{
    /// <summary>Send a get request.</summary>
    /// <param name="url">The full url to access.</param>
    /// <returns>The result as tretrieved via the url. If no connection could be made the string will be empty.</returns>
    public string GetHttpRequest(string url);
}

/// <summary>Handler for communication with the outside world.</summary>
public interface ICommunicationHandler
{
    /// <summary>Find info on the latest release of Enigma on the Internet.</summary>
    /// <returns>Info on the latest release. If no connection could be made, ReleaseINfo will contain empty strings.</returns>
    public ReleaseInfo FindLatestRelease();
}