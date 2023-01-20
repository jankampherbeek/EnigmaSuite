// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Handlers.Research.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Configuration;
using Enigma.Domain.Points;
using Enigma.Research.Domain;

namespace Enigma.Core.Handlers.Research.Helpers;

public class ResearchMethodUtils: IResearchMethodUtils {

    private readonly IAspectPointSelector _aspectPointSelector;

    public ResearchMethodUtils(IAspectPointSelector aspectPointSelector)
    {
        _aspectPointSelector = aspectPointSelector;
    }

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


    public List<FullChartPointPos> DefineSelectedPointPositions(AstroConfig config, CalculatedResearchChart calcResearchChart, List<ChartPoints> selectedChartPoints, ResearchPointsSelection pointsSelection)
    {
        List<FullChartPointPos> chartPointPositions = calcResearchChart.CelPointPositions;
        FullHousesPositions fullHousesPositions = calcResearchChart.FullHousePositions;
        List<FullChartPointPos> configChartPointPositions = _aspectPointSelector.SelectPoints(chartPointPositions, fullHousesPositions, config.ChartPoints);
        List<FullChartPointPos> selectedChartPointPositions = new();
        foreach (FullChartPointPos configPoint in configChartPointPositions)
        {
            foreach (ChartPoints selectPoint in selectedChartPoints)
            {
                if (configPoint.ChartPoint == selectPoint)
                {
                    selectedChartPointPositions.Add(configPoint);
                }
            }
        }
        if (pointsSelection.SelectedMundanePoints.Contains(ChartPoints.Mc))
        {
            selectedChartPointPositions.Add(calcResearchChart.FullHousePositions.Mc);
        };
        if (pointsSelection.SelectedMundanePoints.Contains(ChartPoints.Ascendant))
        {
            selectedChartPointPositions.Add(calcResearchChart.FullHousePositions.Ascendant);
        };
        if (pointsSelection.SelectedMundanePoints.Contains(ChartPoints.Vertex))
        {
            selectedChartPointPositions.Add(calcResearchChart.FullHousePositions.Vertex);
        };
        if (pointsSelection.SelectedMundanePoints.Contains(ChartPoints.EastPoint))
        {
            selectedChartPointPositions.Add(calcResearchChart.FullHousePositions.EastPoint);
        };
        return selectedChartPointPositions;
    }


}