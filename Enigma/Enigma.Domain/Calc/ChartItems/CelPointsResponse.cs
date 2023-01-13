// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Points;

namespace Enigma.Domain.Calc.ChartItems;

/// <summary>Resonse for calculation of celestial points.</summary>
/// <param name="CelPointPositions">Positions of celpoints.</param>
/// <param name="Success">True if no error occurred, otherwise false.</param>
/// <param name="ErrorText">Text for any error, empty string if no error occurred.</param>
public record CelPointsResponse(List<FullChartPointPos> CelPointPositions, bool Success, string ErrorText);



