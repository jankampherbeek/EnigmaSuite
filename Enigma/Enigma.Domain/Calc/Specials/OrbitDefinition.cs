// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Calc.Specials;

/// <summary>Data for an orbit-definition. Used for the calculation of celestial (hypothetical) planets that are not supported by the Swiss Ephemeris.</summary>
/// <param name="MeanAnomaly">Array with three elements for the mean anomaly.</param>
/// <param name="Eccentricity">Array with three elements for the Eccentricity.</param>
/// <param name="SemiMajorAxis">Value for the semi major axis.</param>
/// <param name="ArgumentPerihelion">Array with three elements for the argument of the perihelion.</param>
/// <param name="AscendingNode">Array with three elements for the ascending node.</param>
/// <param name="Inclination">Array with three elements for the Inclination.</param> 
public record OrbitDefinition(double[] MeanAnomaly, double[] Eccentricity, double SemiMajorAxis, double[] ArgumentPerihelion, double[] AscendingNode, double[] Inclination);
