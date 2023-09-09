// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Research.Interfaces;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Configuration;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Points;
using Enigma.Domain.Research;
using Serilog;

namespace Enigma.Core.Research.Helpers;

/// <inheritdoc/>
public sealed class ResearchMethodUtils : IResearchMethodUtils
{

    /// <inheritdoc/>
    public Dictionary<AspectTypes, AspectConfigSpecs> DefineConfigSelectedAspects(AstroConfig config)
    {
        Dictionary<AspectTypes, AspectConfigSpecs> allAspects = config.Aspects;
        return allAspects.Where(acSpec => acSpec.Value.IsUsed).ToDictionary(acSpec 
            => acSpec.Key, acSpec => acSpec.Value);
    }

    /// <inheritdoc/>
    public Dictionary<ChartPoints, ChartPointConfigSpecs> DefineConfigSelectedChartPoints(AstroConfig config)
    {
        Dictionary<ChartPoints, ChartPointConfigSpecs> allChartPoints = config.ChartPoints;
        return allChartPoints.Where(cpConfigSpec 
            => cpConfigSpec.Value.IsUsed).ToDictionary(cpConfigSpec => cpConfigSpec.Key, 
            cpConfigSpec => cpConfigSpec.Value);
    }

    /// <inheritdoc/>
    public Dictionary<ChartPoints, FullPointPos> DefineSelectedPointPositions(CalculatedResearchChart calcResearchChart, ResearchPointsSelection pointsSelection)
    {
        Dictionary<ChartPoints, FullPointPos> chartPointPositions = calcResearchChart.Positions;
        Dictionary<ChartPoints, FullPointPos> selectedChartPointPositions = new();
        foreach (ChartPoints point in pointsSelection.SelectedPoints)
        {
            if (chartPointPositions.TryGetValue(point, out FullPointPos? position)) selectedChartPointPositions.Add(point, position);
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
        Log.Error("AspectsCounting.FindIndexForPoint(). Could not find index for ChartPoint : {Point}", point);
        throw new EnigmaException("Wrong chart point index for FindIndexForPoint()");
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
        Log.Error("AspectsCounting.FindIndexForAspectType(). Could not find index for AspectType : {AT}", aspectType);
        throw new EnigmaException("Wrong aspect type index for FindIndexForAspoectType");
    }

}