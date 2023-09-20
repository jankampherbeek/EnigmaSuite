// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Dtos;
using Enigma.Domain.Points;
using Enigma.Domain.References;
using Enigma.Domain.Requests;

namespace Enigma.Api.Interfaces;

/// <summary>API for the calculation of transits for a given event.</summary>
public interface ICalcTransitsEventApi
{
    /// <summary>Calculates transits.</summary>
    /// <param name="request">Request with data and settings.</param>
    /// <returns>Calculated positions.</returns>
    public Dictionary<ChartPoints, FullPointPos> CalculateTransits(TransitsEventRequest request);
}