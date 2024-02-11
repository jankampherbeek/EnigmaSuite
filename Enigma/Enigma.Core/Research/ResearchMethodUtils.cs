// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.Exceptions;
using Enigma.Domain.References;
using Serilog;

namespace Enigma.Core.Research;


/// <summary>Utilities for research methods.</summary>
public interface IResearchMethodUtils
{
    /// <summary>Create list of aspects as defined in configuration.</summary>
    /// <param name="config">The configuration to check.</param>
    /// <returns>The aspects that are found.</returns>
    public Dictionary<AspectTypes, AspectConfigSpecs> DefineConfigSelectedAspects(AstroConfig config);

    /// <summary>Create list of chart points as defined in configuration.</summary>
    /// <param name="config">The configuration to check.</param>
    /// <returns>The chart points that are found.</returns>
    public Dictionary<ChartPoints, ChartPointConfigSpecs> DefineConfigSelectedChartPoints(AstroConfig config);

    /// <summary>Create list of positioned points according to a given selection of chart points.</summary>
    /// <param name="calcResearchChart">Calculated chart.</param>
    /// <param name="pointSelection">Selection of all points, including mundane points.</param>
    /// <returns>Positioned points that match wityh the selection.</returns>
    public Dictionary<ChartPoints, FullPointPos> DefineSelectedPointPositions(CalculatedResearchChart calcResearchChart, ResearchPointSelection pointSelection);

    /// <summary>Find the index for a chart point in a list with psotioned points.</summary>
    /// <param name="point">The chart point for which to find the index.</param>
    /// <param name="allPoints">Positioned points.</param>
    /// <returns>If found: the index. Otherwise: throws EnigmaException.</returns>
    public int FindIndexForPoint(ChartPoints point, List<PositionedPoint> allPoints);

    /// <summary>Find the index for an aspect type in list with aspect config specs.</summary>
    /// <param name="aspectType">The aspect type for which to find the index.</param>
    /// <param name="allAspects">Aspect config specs.</param>
    /// <returns>If found: the index.  Otherwise: throws EnigmaException.</returns>
    public int FindIndexForAspectType(AspectTypes aspectType, Dictionary<AspectTypes, AspectConfigSpecs> allAspects);
}


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
    public Dictionary<ChartPoints, FullPointPos> DefineSelectedPointPositions(CalculatedResearchChart calcResearchChart, ResearchPointSelection pointSelection)
    {
        Dictionary<ChartPoints, FullPointPos> chartPointPositions = calcResearchChart.Positions;
        Dictionary<ChartPoints, FullPointPos> selectedChartPointPositions = new();
        foreach (ChartPoints point in pointSelection.SelectedPoints)
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