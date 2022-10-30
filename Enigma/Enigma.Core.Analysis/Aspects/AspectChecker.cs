// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis.Interfaces;
using Enigma.Domain;
using Enigma.Domain.Analysis;
using Enigma.Domain.Positional;

namespace Enigma.Core.Analysis.Aspects;





/// <inheritdoc/>
public class AspectChecker : IAspectChecker
{
    private readonly IAspectOrbConstructor _orbConstructor;
    private readonly IAspectSpecifications _aspectSpecifications;

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
        List<AspectDetails> aspectDetails = new()
        {
            _aspectSpecifications.DetailsForAspect(AspectTypes.Conjunction),
            _aspectSpecifications.DetailsForAspect(AspectTypes.Opposition),
            _aspectSpecifications.DetailsForAspect(AspectTypes.Triangle),
            _aspectSpecifications.DetailsForAspect(AspectTypes.Square),
            _aspectSpecifications.DetailsForAspect(AspectTypes.Sextile),
            _aspectSpecifications.DetailsForAspect(AspectTypes.Inconjunct),
            _aspectSpecifications.DetailsForAspect(AspectTypes.SemiSquare),
            _aspectSpecifications.DetailsForAspect(AspectTypes.SesquiQuadrate),
            _aspectSpecifications.DetailsForAspect(AspectTypes.Quintile),
            _aspectSpecifications.DetailsForAspect(AspectTypes.BiQuintile),
            _aspectSpecifications.DetailsForAspect(AspectTypes.Septile),
            _aspectSpecifications.DetailsForAspect(AspectTypes.SemiSextile)
        };
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

    private static double NormalizeDistance(double InitialValue)
    {
        double distance = InitialValue;
        if (distance < 0) distance += 360.0;
        if (distance > 180.0) distance = 360.0 - distance;
        return distance;
    }

}
