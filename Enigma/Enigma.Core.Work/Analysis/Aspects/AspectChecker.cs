// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Work.Analysis.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;
using static Enigma.Core.Work.Analysis.Interfaces.IAspectChecker;

namespace Enigma.Core.Work.Analysis.Aspects;


/// <inheritdoc/>
public class AspectChecker : IAspectChecker
{
    private readonly IAspectOrbConstructor _orbConstructor;

    public AspectChecker(IAspectOrbConstructor orbConstructor)
    {
        _orbConstructor = orbConstructor;
    }


    /// <inheritdoc/>
    public List<EffectiveAspect> FindAspectsCelPoints(CalculatedChart calculatedChart)
    {
        List<AspectDetails> aspectDetails = DefineSupportedAspects();
        return AspectsForCelPoints(aspectDetails, calculatedChart.CelPointPositions);
    }

    /// <inheritdoc/>
    public List<EffectiveAspect> FindAspectsCelPoints(List<AspectDetails> aspectDetails, List<FullCelPointPos> fullCelPointPositions)
    { 
        return AspectsForCelPoints(aspectDetails, fullCelPointPositions);
    }


    /// <inheritdoc/>
    public List<EffectiveAspect> FindAspectsForMundanePoints(CalculatedChart calculatedChart)
    {
        List<AspectDetails> aspectDetails = DefineSupportedAspects();
        return AspectsForMundanePoints(aspectDetails, calculatedChart.CelPointPositions, calculatedChart.FullHousePositions);
    }

    /// <inheritdoc/>
    public List<EffectiveAspect> FindAspectsForMundanePoints(List<AspectDetails> aspectDetails, CalculatedChart calculatedChart)
    {
        return AspectsForMundanePoints(aspectDetails, calculatedChart.CelPointPositions, calculatedChart.FullHousePositions);
    }

    private List<AspectDetails> DefineSupportedAspects()
    {
        // TODO replace with configurable set of aspect(details).
        List<AspectDetails> aspectDetails = new()
        {
            AspectTypes.Conjunction.GetDetails(),
            AspectTypes.Opposition.GetDetails(),
            AspectTypes.Triangle.GetDetails(),
            AspectTypes.Square.GetDetails(),
            AspectTypes.Sextile.GetDetails(),
            AspectTypes.Inconjunct.GetDetails(),
            AspectTypes.SemiSquare.GetDetails(),
            AspectTypes.SesquiQuadrate.GetDetails(),
            AspectTypes.Quintile.GetDetails(),
            AspectTypes.BiQuintile.GetDetails(),
            AspectTypes.Septile.GetDetails(),
            AspectTypes.SemiSextile.GetDetails()
        };
        return aspectDetails;
    }

 /*   private List<EffectiveAspect> AspectsForCelPoints(List<FullCelPointPos> celPointPositions)
    {
        var effectiveAspects = new List<EffectiveAspect>();
        List<AspectDetails> supportedAspects = DefineSupportedAspects();

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
 */

    private List<EffectiveAspect> AspectsForCelPoints(List<AspectDetails> supportedAspects, List<FullCelPointPos> celPointPositions)
    {
        var effectiveAspects = new List<EffectiveAspect>();

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


    private List<EffectiveAspect> AspectsForMundanePoints(List<AspectDetails> supportedAspects, List<FullCelPointPos> celPointPositions, FullHousesPositions fullHousePositions )
    {
        var effectiveAspects = new List<EffectiveAspect>();


        var mundanePointPositions = new List<String>() { "MC", "ASC" };
        int countMundanePoints = mundanePointPositions.Count;
        int countCelPoints = celPointPositions.Count;
        for (int i = 0; i < countMundanePoints; i++)
        {
            string mPointTxt = mundanePointPositions[i];
            double mLong = mPointTxt == "MC" ? fullHousePositions.Mc.Longitude : fullHousePositions.Ascendant.Longitude;
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
