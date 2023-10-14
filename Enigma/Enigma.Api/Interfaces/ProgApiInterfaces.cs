// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Dtos;
using Enigma.Domain.Points;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Api.Interfaces;

/// <summary>API for the calculation of transits for a given event.</summary>
public interface ICalcTransitsEventApi
{
    /// <summary>Calculate transits.</summary>
    /// <param name="request">Request with date, time and settings.</param>
    /// <returns>Calculated positions.</returns>
    public ProgRealPointsResponse CalcTransits(TransitsEventRequest request);

}

/// <summary>API for the calculation of secundary directions for a given event.</summary>
public interface ICalcSecDirEventApi
{
    /// <summary>Calculate secundary directions.</summary>
    /// <param name="request">Request with date, time and settings.</param>
    /// <returns>Calculated positions.</returns>
    public ProgRealPointsResponse CalcSecDir(SecDirEventRequest request);
}