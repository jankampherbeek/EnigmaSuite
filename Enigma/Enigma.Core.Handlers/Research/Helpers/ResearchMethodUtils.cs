// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Handlers.Research.Interfaces;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Configuration;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Points;
using Enigma.Research.Domain;
using Serilog;

namespace Enigma.Core.Handlers.Research.Helpers;

/// <inheritdoc/>
public class ResearchMethodUtils: IResearchMethodUtils {

    /// <inheritdoc/>
    public List<AspectConfigSpecs> DefineConfigSelectedAspects(AstroConfig config)
    {
        List<AspectConfigSpecs> allAspects = config.Aspects;
        List<AspectConfigSpecs> selectedAspects = new();
        foreach (AspectConfigSpecs acSpec in allAspects)
        {
            if (acSpec.IsUsed) selectedAspects.Add(acSpec);
        }
        return selectedAspects;
    }

    /// <inheritdoc/>
    public List<ChartPoints> DefineConfigSelectedChartPoints(AstroConfig config)
    {
        List<ChartPointConfigSpecs> allChartPoints = config.ChartPoints;
        List<ChartPoints> selectedPoints = new();
        foreach (ChartPointConfigSpecs cpConfigSpec in allChartPoints)
        {
            if (cpConfigSpec.IsUsed) selectedPoints.Add(cpConfigSpec.Point);
        }
        return selectedPoints;
    }

    /// <inheritdoc/>
    public Dictionary<ChartPoints, FullPointPos> DefineSelectedPointPositions(CalculatedResearchChart calcResearchChart, ResearchPointsSelection pointsSelection)
    {
        Dictionary<ChartPoints, FullPointPos> chartPointPositions = calcResearchChart.Positions.CommonPoints;
        Dictionary<ChartPoints, FullPointPos> angles = calcResearchChart.Positions.Angles;
        Dictionary<ChartPoints, FullPointPos> selectedChartPointPositions = new();
        foreach (ChartPoints point in pointsSelection.SelectedPoints)
        {
            selectedChartPointPositions.Add(point, chartPointPositions[point]);
        }
        foreach (ChartPoints point in pointsSelection.SelectedMundanePoints)
        {
            selectedChartPointPositions.Add(point, angles[point]);
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
    public int FindIndexForAspectType(AspectTypes aspectType, List<AspectConfigSpecs> allAspects)
    {
        for (int i = 0; i < allAspects.Count; i++)
        {
            if (allAspects[i].AspectType == aspectType) return i;
        }
        string errorText = "AspectsCounting.FindIndexForAspectType(). Could not find index for AspectType : " + aspectType;
        Log.Error(errorText);
        throw new EnigmaException(errorText);
    }

}