// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Dtos;

namespace Enigma.Domain.Requests;

/// <summary>Request to calculate aspects.</summary>
/// <param name="CalcChart">Calculated chart.</param>
/// <param name="Config">Current configuration.</param>
public record AspectRequest(CalculatedChart CalcChart, AstroConfig Config);

