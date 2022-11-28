// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Work.Analysis.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;
using Enigma.Domain.Interfaces;

namespace Enigma.Core.Work.Analysis.Aspects;


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
    public List<EffectiveAspect> FindAspectsCelPoints(CalculatedChart calculatedChart)
    {
        return AspectsForCelPoints(calculatedChart);
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

    private List<EffectiveAspect> AspectsForCelPoints(CalculatedChart calculatedChart)
    {
        var effectiveAspects = new List<EffectiveAspect>();
        List<AspectDetails> supportedAspects = DefineSupportedAspects();

        List<FullCelPointPos> celPointPositions = calculatedChart.CelPointPositions;
        int count = celPointPositions.Count;
        for (int i = 0; i < count; i++)
        {
            var celPointPos1 = celPointPositions[i];
            for (int j = i + 1; j < celPointPositions.Count; j++)
            {
                var celPointPos2 = celPointPositions[j];
                double distance = NormalizeDistance(celPointPos1.Longitude.Position - celPointPos2.Longitude.Position);
                for (int k = 0; k < supportedAspects.Count; k++)
                {
                    AspectDetails aspectToCheck = supportedAspects[k];
                    double angle = aspectToCheck.Angle;
                    double maxOrb = _orbConstructor.DefineOrb(celPointPos1.CelPoint, celPointPos2.CelPoint, aspectToCheck);
                    double actualOrb = Math.Abs(angle - distance);
                    if (actualOrb < maxOrb)
                    {
                        effectiveAspects.Add(new EffectiveAspect(celPointPos1.CelPoint, celPointPos2.CelPoint, aspectToCheck, maxOrb, actualOrb));
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
        List<FullCelPointPos> celPointPositions = calculatedChart.CelPointPositions;
        var mundanePointPositions = new List<String>() { "MC", "ASC" };
        int countMundanePoints = mundanePointPositions.Count;
        int countCelPoints = celPointPositions.Count;
        for (int i = 0; i < countMundanePoints; i++)
        {
            string mPointTxt = mundanePointPositions[i];
            double mLong = mPointTxt == "MC" ? calculatedChart.FullHousePositions.Mc.Longitude : calculatedChart.FullHousePositions.Ascendant.Longitude;
            for (int j = 0; j < countCelPoints; j++)
            {
                var ssPoint = celPointPositions[j];
                double distance = NormalizeDistance(mLong - ssPoint.Longitude.Position);
                for (int k = 0; k < supportedAspects.Count; k++)
                {
                    var aspectToCheck = supportedAspects[k];
                    double angle = aspectToCheck.Angle;
                    double maxOrb = _orbConstructor.DefineOrb(mPointTxt, ssPoint.CelPoint, aspectToCheck);
                    double actualOrb = Math.Abs(angle - distance);
                    if (actualOrb < maxOrb)
                    {
                        effectiveAspects.Add(new EffectiveAspect(mPointTxt, ssPoint.CelPoint, aspectToCheck, maxOrb, actualOrb));
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
