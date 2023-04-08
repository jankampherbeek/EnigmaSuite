// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Serilog;
using System.Net;

namespace Enigma.Core.Handlers.Communication.Helpers;

/// <inheritdoc/>
public class HttpRequester : IHttpRequester
{

    /// <inheritdoc/>
    public string GetHttpRequest(string url)
    {
        return ReadJsonFromSite(url);
    }

    private static string ReadJsonFromSite(string url)
    {
        try
        {
            var request = WebRequest.Create(url);               // TODO 0.2 find alternative for WebRequest.
            request.Method = "GET";
            using var webResponse = request.GetResponse();
            using var webStream = webResponse.GetResponseStream();
            using var reader = new StreamReader(webStream);
            var data = reader.ReadToEnd();
            return data;
        }
        catch (Exception e)     // System.Net.WebException  and   HttpRequestException
        {
            Log.Error("HttpRequester.ReadJsonFromSite using url : " + url + " . Exception occurred, probably no internet connection. Exception message : " + e.Message);
        }
        return "";
    }



}