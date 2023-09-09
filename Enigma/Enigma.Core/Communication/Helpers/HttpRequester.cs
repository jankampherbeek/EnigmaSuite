// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Net;
using Enigma.Core.Interfaces;
using Serilog;

namespace Enigma.Core.Communication.Helpers;

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
            Log.Error("HttpRequester.ReadJsonFromSite using url : {Url}. Exception occurred, probably no internet connection. Exception message : {Msg} ", url, e.Message);
        }
        return "";
    }



}