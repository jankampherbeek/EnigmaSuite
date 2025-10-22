// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Core.Slices.PreNatal;

public record PreNatalParent(double PreNatalJd);

/// <summary>
/// Eclipse to be used in PreNatal calculations</summary>
/// <param name="PreNatalJd">Julian day number the eclipse occurred</param>
/// <param name="Longitude">Longitude of the eclipse</param>
/// <param name="LunarSolar">For lunar eclipse 'L', for solar eclipse 'S'</param>
public record Eclipse(double PreNatalJd, double Longitude, char LunarSolar): PreNatalParent(PreNatalJd);

/// <summary>
/// Change from direct to retrograde movement, or the other way around, to be used in PreNatal calculations
/// </summary>
/// <param name="PreNatalJd">Julian day number the change in movement occurred</param>
/// <param name="Factor">The celestial point that changes direction</param>
/// <param name="Longitude">Longitude of the change</param>
/// <param name="Direction">For direct 'D', for retrograde 'R'</param>
public record RetroDirect(double PreNatalJd, ChartPoints Factor, double Longitude, char Direction): PreNatalParent(PreNatalJd);


/// <summary>
/// Ingress into sign, to be used in PreNatal calculations
/// </summary>
/// <param name="PreNatalJd">Julian Day number the ingress occurred</param>
/// <param name="Factor">The celestial point that ingresses</param>
/// <param name="Sign">The sign that is ingressed (1..12)</param>
public record Ingress(double PreNatalJd, ChartPoints Factor, int Sign): PreNatalParent(PreNatalJd);


/// <summary>
/// A mutual aspect, to be used in PreNatal calculations
/// </summary>
/// <param name="PreNatalJd">Julian Day number the aspect is exact</param>
/// <param name="Factor1">The first celestial point that forms the aspect</param>
/// <param name="Factor2">The second celestial point that forms the aspect</param>
/// <param name="Position1">Longitude of the first celestial point</param>
/// <param name="Position2">Longitude of the second celestial point</param>
/// <param name="Aspect">The aspect that is formed</param>
public record MutualAspect(double PreNatalJd, ChartPoints Factor1, ChartPoints Factor2, double Position1, double Position2, 
    AspectTypes Aspect): PreNatalParent(PreNatalJd);