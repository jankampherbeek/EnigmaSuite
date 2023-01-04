// Jan Kampherbeek, (c) 2022, 2023.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Charts;
using Enigma.Domain.Configuration;

namespace Enigma.Domain.RequestResponse;

/// <summary>Request to calculate aspects.</summary>
/// <param name="CalcChart">Calculated chart.</param>
/// <param name="Config">Current configuration.</param>
public record AspectRequest(CalculatedChart CalcChart, AstroConfig Config);

