﻿// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain;
using Enigma.Domain.Analysis;
using Enigma.Domain.Positional;

namespace Enigma.Core.Analysis.Aspects;


/// <summary>
/// Checks for aspects.
/// </summary>
public interface IAspectChecker
{
    /// <summary>
    /// Find aspects between solar systempoints.
    /// </summary>
    /// <param name="calculatedChart">Chart with positions.</param>
    /// <returns>List with effective aspects.</returns>
    List<EffectiveAspect> FindAspectsForSolSysPoints(CalculatedChart calculatedChart);
    /// <summary>
    /// Find aspects between a mundane point and a solar system point.
    /// </summary>
    /// <param name="calculatedChart">Chart with positions.</param>
    /// <returns>List with effective aspects.</returns>
    List<EffectiveAspect> FindAspectsForMundanePoints(CalculatedChart calculatedChart);
}


/// <inheritdoc/>
public class AspectChecker : IAspectChecker
{
    private IAspectOrbConstructor _orbConstructor;
    private IAspectSpecifications _aspectSpecifications;

    public AspectChecker(IAspectOrbConstructor orbConstructor, IAspectSpecifications aspectSpecifications)
    {
        _orbConstructor = orbConstructor;
        _aspectSpecifications = aspectSpecifications;
    }


    /// <inheritdoc/>
    public List<EffectiveAspect> FindAspectsForSolSysPoints(CalculatedChart calculatedChart)
    {
        return AspectsForSolSysPoints(calculatedChart);
    }

    /// <inheritdoc/>
    public List<EffectiveAspect> FindAspectsForMundanePoints(CalculatedChart calculatedChart)
    {
        return AspectsForMundanePoints(calculatedChart);
    }

    private List<AspectDetails> DefineSupportedAspects()
    {
        // TODO replace with configurable set of aspect(details).
        List<AspectDetails> aspectDetails= new();
        aspectDetails.Add(_aspectSpecifications.DetailsForAspect(AspectTypes.Conjunction));
        aspectDetails.Add(_aspectSpecifications.DetailsForAspect(AspectTypes.Opposition));
        aspectDetails.Add(_aspectSpecifications.DetailsForAspect(AspectTypes.Triangle));
        aspectDetails.Add(_aspectSpecifications.DetailsForAspect(AspectTypes.Square));
        aspectDetails.Add(_aspectSpecifications.DetailsForAspect(AspectTypes.Sextile));
        aspectDetails.Add(_aspectSpecifications.DetailsForAspect(AspectTypes.Inconjunct));
        aspectDetails.Add(_aspectSpecifications.DetailsForAspect(AspectTypes.SemiSquare));
        aspectDetails.Add(_aspectSpecifications.DetailsForAspect(AspectTypes.SesquiQuadrate));
        aspectDetails.Add(_aspectSpecifications.DetailsForAspect(AspectTypes.Quintile));
        aspectDetails.Add(_aspectSpecifications.DetailsForAspect(AspectTypes.BiQuintile));
        aspectDetails.Add(_aspectSpecifications.DetailsForAspect(AspectTypes.Septile));
        aspectDetails.Add(_aspectSpecifications.DetailsForAspect(AspectTypes.SemiSextile));
        return aspectDetails;
    }

    private List<EffectiveAspect> AspectsForSolSysPoints(CalculatedChart calculatedChart)
    {
        var effectiveAspects = new List<EffectiveAspect>();
        List <AspectDetails> supportedAspects = DefineSupportedAspects();

        List<FullSolSysPointPos> solSysPointPositions = calculatedChart.SolSysPointPositions;
        int count = solSysPointPositions.Count;
        for (int i = 0; i < count; i++)
        {
            var solSysPointPos1 = solSysPointPositions[i];
            for (int j = i + 1; j < solSysPointPositions.Count; j++)
            {
                var solSysPointPos2 = solSysPointPositions[j];
                double distance = NormalizeDistance(solSysPointPos1.Longitude.Position - solSysPointPos2.Longitude.Position);
                for (int k = 0; k < supportedAspects.Count; k++)
                {   AspectDetails aspectToCheck = supportedAspects[k];
                    double angle = aspectToCheck.Angle;
                    double maxOrb = _orbConstructor.DefineOrb(solSysPointPos1.SolarSystemPoint, solSysPointPos2.SolarSystemPoint, aspectToCheck);
                    double actualOrb = Math.Abs(angle - distance);
                    if (actualOrb < maxOrb)
                    {
                        effectiveAspects.Add(new EffectiveAspect(solSysPointPos1.SolarSystemPoint, solSysPointPos2.SolarSystemPoint, aspectToCheck, maxOrb, actualOrb));
                    }
                }
            }
        }
        return effectiveAspects;
    } 

    private List<EffectiveAspect> AspectsForMundanePoints(CalculatedChart calculatedChart)
    {
        var effectiveAspects = new List<EffectiveAspect>();
        List<AspectDetails> supportedAspects = DefineSupportedAspects();
        List<FullSolSysPointPos> solSysPointPositions = calculatedChart.SolSysPointPositions;
        var mundanePointPositions = new List<String>() { "MC", "ASC" };
        int countMundanePoints = mundanePointPositions.Count;
        int countSolSysPoints = solSysPointPositions.Count;
        for (int i = 0; i < countMundanePoints; i++)
        {
            string mPointTxt = mundanePointPositions[i];
            double mLong = mPointTxt == "MC" ? calculatedChart.FullHousePositions.Mc.Longitude : calculatedChart.FullHousePositions.Ascendant.Longitude;           
            for (int j = 0; j < countSolSysPoints; j++)
            {
                var ssPoint = solSysPointPositions[j];
                double distance = NormalizeDistance(mLong - ssPoint.Longitude.Position);
                for (int k = 1; k < supportedAspects.Count; k++)
                {
                    var aspectToCheck = supportedAspects[k];
                    double angle = aspectToCheck.Angle;
                    double maxOrb = _orbConstructor.DefineOrb(mPointTxt, ssPoint.SolarSystemPoint, aspectToCheck);
                    double actualOrb = Math.Abs(angle - distance);
                    if (actualOrb < maxOrb)
                    {
                        effectiveAspects.Add(new EffectiveAspect(mPointTxt, ssPoint.SolarSystemPoint, aspectToCheck, maxOrb, actualOrb));
                    }
                }
            }
        }
        return effectiveAspects;
    }

    private double NormalizeDistance(double InitialValue)
    {
        double distance = InitialValue;
        if (distance < 0) distance += 360.0;
        if (distance > 180.0) distance = 360.0 - distance;
        return distance;
    }

}