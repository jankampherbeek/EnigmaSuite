// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Research.Interfaces;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Configuration;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Points;
using Enigma.Research.Domain;
using Serilog;

namespace Enigma.Core.Handlers.Research.Helpers;

/// <inheritdoc/>
public sealed class ResearchMethodUtils : IResearchMethodUtils
{

    /// <inheritdoc/>
    public Dictionary<AspectTypes, AspectConfigSpecs> DefineConfigSelectedAspects(AstroConfig config)
    {
        Dictionary<AspectTypes, AspectConfigSpecs> allAspects = config.Aspects;
        Dictionary<AspectTypes, AspectConfigSpecs> selectedAspects = new();
        foreach (KeyValuePair<AspectTypes, AspectConfigSpecs> acSpec in allAspects)
        {
            if (acSpec.Value.IsUsed) selectedAspects.Add(acSpec.Key, acSpec.Value);
        }
        return selectedAspects;
    }

    /// <inheritdoc/>
    public Dictionary<ChartPoints, ChartPointConfigSpecs> DefineConfigSelectedChartPoints(AstroConfig config)
    {
        Dictionary<ChartPoints, ChartPointConfigSpecs> allChartPoints = config.ChartPoints;
        Dictionary<ChartPoints, ChartPointConfigSpecs> selectedPoints = new();
        foreach (KeyValuePair<ChartPoints, ChartPointConfigSpecs> cpConfigSpec in allChartPoints)
        {
            if (cpConfigSpec.Value.IsUsed) selectedPoints.Add(cpConfigSpec.Key, cpConfigSpec.Value);
        }
        return selectedPoints;
    }

    /// <inheritdoc/>
    public Dictionary<ChartPoints, FullPointPos> DefineSelectedPointPositions(CalculatedResearchChart calcResearchChart, ResearchPointsSelection pointsSelection)
    {
        Dictionary<ChartPoints, FullPointPos> chartPointPositions = calcResearchChart.Positions;
        Dictionary<ChartPoints, FullPointPos> selectedChartPointPositions = new();
        foreach (ChartPoints point in pointsSelection.SelectedPoints)
        {
            selectedChartPointPositions.Add(point, chartPointPositions[point]);
        }
        return selectedChartPointPositions;
    }




    /// <inheritdoc/>
    public int FindIndexForPoint(ChartPoints point, List<PositionedPoint> allPoints)
    {
        for (int i = 0; i < allPoints.Count; i++)
        {
            if (allPoints[i].Point == point) return i;
        }
        string errorText = "AspectsCounting.FindIndexForPoint(). Could not find index for ChartPoint : " + point;
        Log.Error(errorText);
        throw new EnigmaException(errorText);
    }

    /// <inheritdoc/>
    public int FindIndexForAspectType(AspectTypes aspectType, Dictionary<AspectTypes, AspectConfigSpecs> allAspects)
    {
        int counter = 0;
        foreach (var item in allAspects)
        {
            if (item.Key == aspectType) return counter;
            counter++;
        }
        string errorText = "AspectsCounting.FindIndexForAspectType(). Could not find index for AspectType : " + aspectType;
        Log.Error(errorText);
        throw new EnigmaException(errorText);
    }

}