// Jan Kampherbeek, (c) 2022, 2023.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Charts;
using Enigma.Domain.Configuration;
using Enigma.Domain.Points;

namespace Enigma.Core.Handlers.Interfaces;


/// <summary>
/// Checks for aspects.
/// </summary>
public interface IAspectChecker
{

    /// <summary>Find aspects between general points.</summary>
    /// <param name="aspectDetails"></param>
    /// <param name="positionedPoints"></param>
    /// <returns>List with defined aspects.</returns>
    public List<DefinedAspect> FindAspectsForGeneralPoints(List<AspectDetails> aspectDetails, List<PositionedPoint> positionedPoints);

    /// <summary>
    /// Find aspects between celestial points.
    /// </summary>
    /// <param name="calculatedChart">Chart with positions.</param>
    /// <param name="config">Current configuration.</param>
    /// <returns>List with effective aspects.</returns>
    List<EffectiveAspect> FindAspectsCelPoints(CalculatedChart calculatedChart, AstroConfig config);

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
    /// <param name="config">Current configuration.</param> 
    /// <returns>List with effective aspects.</returns>
    List<EffectiveAspect> FindAspectsForMundanePoints(CalculatedChart calculatedChart, AstroConfig config);

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
        /// <summary>Define orb between two general points.</summary>
        public double DefineOrb(GeneralPoint point1, GeneralPoint point2, AspectDetails aspectDetails);
    }





