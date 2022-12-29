// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;
using Enigma.Domain.Enums;

namespace Enigma.Core.Work.Analysis.Interfaces;


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
    List<EffectiveAspect> FindAspectsCelPoints(CalculatedChart calculatedChart);

    /// <summary>
    /// Find aspects between celestial points.
    /// </summary>
    /// <param name="aspectDetails">Suppported aspects.</param>
    /// <param name="fullCelPointPositions">Supported celestial points.</param>
    /// <returns>List with effective aspects.</returns>
    public List<EffectiveAspect> FindAspectsCelPoints(List<AspectDetails> aspectDetails, List<FullCelPointPos> fullCelPointPositions);

    /// <summary>
    /// Find aspects between a mundane point and a celestial point.
    /// </summary>
    /// <param name="calculatedChart">Chart with positions.</param>
    /// <returns>List with effective aspects.</returns>
    List<EffectiveAspect> FindAspectsForMundanePoints(CalculatedChart calculatedChart);

    /// <summary>
    /// Find aspects between a mundane point and a celestial point.
    /// </summary>
    /// <param name="aspectDetails">Suppported aspects.</param>
    /// <param name="fullCelPointPositions">Supported celestial points.</param>
    /// <returns>List with effective aspects.</returns>
    public List<EffectiveAspect> FindAspectsForMundanePoints(List<AspectDetails> aspectDetails, CalculatedChart calculatedChart);
}

    /// <summary>
    /// Define actual orb for an aspect.
    /// </summary>
    public interface IAspectOrbConstructor
    {
        /// <summary>Define orb between two celestial points.</summary>
        public double DefineOrb(CelPoints point1, CelPoints point2, AspectDetails aspectDetails);
        /// <summary>Define orb between mundane point and celestial point.
        public double DefineOrb(string mundanePoint, CelPoints celPoint, AspectDetails aspectDetails);
    }



