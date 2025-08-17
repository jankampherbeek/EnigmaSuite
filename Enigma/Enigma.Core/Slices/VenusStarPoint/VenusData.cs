// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Slices.VenusStarPoint;

/// <summary>
/// Astronomical data for the calculation of conjunctions between Venus and the Sun, from Jean Meeus, Astronomical
/// Algorithms, ed.2, p. 250
/// </summary>
public static class VenusData
{
    public const double VenusInferiorConjunctionFactorA = 2451_996.706;
    public const double VenusInferiorConjunctionFactorB = 583.921361;
    public const double VenusInferiorConjunctionFactorM0 = 82.7311;
    public const double VenusInferiorConjunctionFactorM1 = 215.513058;
    
    public const double VenusSuperiorConjunctionFactorA = 2451_704.746;
    public const double VenusSuperiorConjunctionFactorB = 583.921361;
    public const double VenusSuperiorConjunctionFactorM0 = 154.9745;
    public const double VenusSuperiorConjunctionFactorM1 = 215.513058;
}

