// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Dtos;

namespace Enigma.Domain.Requests;

/// <summary>Request to calculate parallels in declination.</summary>
/// <param name="CalcChart">Calculated chart.</param>
/// <param name="Config">Current configuration.</param>
public record ParallelRequest(CalculatedChart CalcChart, AstroConfig Config);