// Jan Kampherbeek, (c) 2022, 2023.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;
using Enigma.Domain.Configuration;
using Enigma.Domain.Points;

namespace Enigma.Core.Handlers.Analysis.Helpers;


/// <inheritdoc/>
public sealed class AspectChecker : IAspectChecker
{
    private readonly IAspectOrbConstructor _orbConstructor;

    public AspectChecker(IAspectOrbConstructor orbConstructor)
    {
        _orbConstructor = orbConstructor;
    }


    /// <inheritdoc/>
    public List<EffectiveAspect> FindAspectsCelPoints(CalculatedChart calculatedChart, AstroConfig config)
    {
        List<AspectDetails> aspectDetails = DefineSupportedAspects(config);
        return AspectsForCelPoints(aspectDetails, calculatedChart.CelPointPositions);
    }

    /// <inheritdoc/>
    public List<EffectiveAspect> FindAspectsCelPoints(List<AspectDetails> aspectDetails, List<FullCelPointPos> fullCelPointPositions)
    { 
        return AspectsForCelPoints(aspectDetails, fullCelPointPositions);
    }


    /// <inheritdoc/>
    public List<EffectiveAspect> FindAspectsForMundanePoints(CalculatedChart calculatedChart, AstroConfig config)
    {
        List<AspectDetails> aspectDetails = DefineSupportedAspects(config);
        return AspectsForMundanePoints(aspectDetails, calculatedChart.CelPointPositions, calculatedChart.FullHousePositions);
    }

    /// <inheritdoc/>
    public List<EffectiveAspect> FindAspectsForMundanePoints(List<AspectDetails> aspectDetails, CalculatedChart calculatedChart)
    {
        return AspectsForMundanePoints(aspectDetails, calculatedChart.CelPointPositions, calculatedChart.FullHousePositions);
    }

    /// <inheritdoc/>
    public List<DefinedAspect> FindAspectsForGeneralPoints(List<AspectDetails> aspectDetails, List<PositionedPoint> positionedPoints)
    {
        return AspectsForGeneralPoints(aspectDetails, positionedPoints);
    }


    private List<AspectDetails> DefineSupportedAspects(AstroConfig conf)
    {
        List<AspectDetails> aspectDetails = new();
        foreach(AspectConfigSpecs aspectSpecs in conf.Aspects) 
        {
            aspectDetails.Add(aspectSpecs.AspectType.GetDetails());
        }
        return aspectDetails;
    }

    private List<DefinedAspect> AspectsForGeneralPoints(List<AspectDetails> supportedAspects, List<PositionedPoint> points)
    {
        List<DefinedAspect> definedAspects = new();
        int count = points.Count;
        for (int i = 0; i < count; i++)
        {
            PositionedPoint pointPos1 = points[i];
            for (int j = i + 1; j < count; j++)
            {
                var pointPos2 = points[j];
                double distance = NormalizeDistance(pointPos1.Position - pointPos2.Position);
                for (int k = 0; k < supportedAspects.Count; k++)
                {
                    AspectDetails aspectToCheck = supportedAspects[k];
                    double angle = aspectToCheck.Angle;
                    double maxOrb = _orbConstructor.DefineOrb(pointPos1.Point, pointPos2.Point, aspectToCheck);
                    double actualOrb = Math.Abs(angle - distance);
                    if (actualOrb < maxOrb)
                    {
                        definedAspects.Add(new DefinedAspect(pointPos1.Point, pointPos2.Point, aspectToCheck, maxOrb, actualOrb));
                    }
                }
            }
        }
        return definedAspects;
    }



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
                double distance = NormalizeDistance(celPointPos1.GeneralPointPos.Longitude.Position - celPointPos2.GeneralPointPos.Longitude.Position);
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
                double distance = NormalizeDistance(mLong - ssPoint.GeneralPointPos.Longitude.Position);
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
