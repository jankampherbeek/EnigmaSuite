﻿// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.Charts;
using Enigma.Domain.Enums;

namespace Enigma.Core.Analysis.Interfaces;
/// <summary>
/// Checks for aspects.
/// </summary>
public interface IAspectChecker
{
    /// <summary>
    /// Find aspects between celestial points.
    /// </summary>
    /// <param name="calculatedChart">Chart with positions.</param>
    /// <returns>List with effective aspects.</returns>
    List<EffectiveAspect> FindAspectsForSolSysPoints(CalculatedChart calculatedChart);
    /// <summary>
    /// Find aspects between a mundane point and a celestial point.
    /// </summary>
    /// <param name="calculatedChart">Chart with positions.</param>
    /// <returns>List with effective aspects.</returns>
    List<EffectiveAspect> FindAspectsForMundanePoints(CalculatedChart calculatedChart);
}


/// <summary>
/// Define actual orb for an aspect.
/// </summary>
public interface IAspectOrbConstructor
{
    /// <summary>Define orb between two celestial points.</summary>
    public double DefineOrb(SolarSystemPoints point1, SolarSystemPoints point2, AspectDetails aspectDetails);
    /// <summary>Define orb between mundane point and celestial point.
    public double DefineOrb(string mundanePoint, SolarSystemPoints solSysPoint, AspectDetails aspectDetails);
}