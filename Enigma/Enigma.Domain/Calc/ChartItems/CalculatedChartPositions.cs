// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Points;

namespace Enigma.Domain.Calc.ChartItems;

/// <summary>All calculated values (position, speed and distance) for points in a chart.</summary>
/// <param name="CommonPoints"/>
/// <param name="Angles"/>
/// <param name="Cusps"/>
/// <param name="ZodiacPoints"/>
/// <param name="ArabicPoints"/>
/// <param name="FixStars"/>
public record CalculatedChartPositions(Dictionary<ChartPoints, FullPointPos> CommonPoints,
                                       Dictionary<ChartPoints, FullPointPos> Angles,
                                       Dictionary<ChartPoints, FullPointPos> Cusps,
                                       Dictionary<ChartPoints, FullPointPos> ZodiacPoints,
                                       Dictionary<ChartPoints, FullPointPos> ArabicPoints,
                                       Dictionary<ChartPoints, FullPointPos> FixStars);

