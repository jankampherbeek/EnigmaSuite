// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Points;

namespace Enigma.Domain.Calc.ChartItems;

/// <summary>User preferences for the calculation of mundane points.</summary>
/// <param name="HouseSystem">The housesystem to use.</param>
/// <param name="Angles">Additional angles, additional to the cusps, that should be calculated.</param>
/// <param name="ActualAyanamsha">The Ayanamasha, Ayanamshas.None for topocentric.</param>
/// <param name="CoordinateSystem">Thhe coordinate system to use.</param>
public record CalcPrefsMundanePoints(
    HouseSystems HouseSystem,
    List<ChartPoints> Angles,
    Ayanamshas ActualAyanamsha,
    CoordinateSystems CoordinateSystem
);
