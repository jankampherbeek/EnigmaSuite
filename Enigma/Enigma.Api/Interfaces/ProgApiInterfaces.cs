// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Api.Interfaces;

/// <summary>API for the calculation of transits for a given event.</summary>
public interface IProgTransitsEventApi
{
    /// <summary>Calculate transits.</summary>
    /// <param name="request">Request with date, time and settings.</param>
    /// <returns>Calculated positions.</returns>
    public ProgRealPointsResponse CalcTransits(TransitsEventRequest request);

}

/// <summary>API for the calculation of secundary directions for a given event.</summary>
public interface IProgSecDirEventApi
{
    /// <summary>Calculate secundary directions.</summary>
    /// <param name="request">Request with date, time and settings.</param>
    /// <returns>Calculated positions.</returns>
    public ProgRealPointsResponse CalcSecDir(SecDirEventRequest request);
}

/// <summary>API for the calculation of symbolic directions for a given event.</summary>
public interface IProgSymDirEventApi
{
    /// <summary>Calculate symbolic directions.</summary>
    /// <param name="request">Request with date, time and settings.</param>
    /// <returns>Calculated positions.</returns>
    public ProgRealPointsResponse CalcSymDir(SymDirEventRequest request);
}

/// <summary>API for the calculation of primary directions.</summary>
public interface IProgPrimDirApi
{
    /// <summary>Calculate primary directions.</summary>
    /// <param name="request">Request with date, time and settings.</param>
    /// <returns>Calculated results.</returns>
    public ProgPrimDirResponse CalcPrimDir(ProgPrimDirRequest request);
}