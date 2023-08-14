// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts;
using Enigma.Domain.Points;
using Enigma.Frontend.Ui.Interfaces;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.PresentationFactories;



public class SortedGraphicCelPointsFactory : ISortedGraphicCelPointsFactory
{
    private readonly ICelPointForDataGridFactory _celPointFactory;

    public SortedGraphicCelPointsFactory(ICelPointForDataGridFactory celPointFactory)
    {
        _celPointFactory = celPointFactory;
    }

    public List<GraphicCelPointPositions> CreateSortedList(Dictionary<ChartPoints, FullPointPos> celPointPositions, double longitudeAsc, double minDistance)
    {
        List<GraphicCelPointPositions> graphPositions = CreateGraphicPositions(celPointPositions, longitudeAsc);
        graphPositions.Sort((pos1, pos2) => pos1.MundanePos.CompareTo(pos2.MundanePos));
        GraphicCelPointPositions? lastPos = null;
        foreach (var pos in graphPositions)
        {
            if (lastPos != null)
            {
                double actDistance = pos.MundanePos - lastPos.PlotPos;
                if (actDistance < minDistance)
                {
                    pos.PlotPos += minDistance - actDistance;
                    if (pos.PlotPos >= 360.0) pos.PlotPos -= 360.0;
                }
            }
            lastPos = pos;
        }
        return graphPositions;
    }


    private List<GraphicCelPointPositions> CreateGraphicPositions(Dictionary<ChartPoints, FullPointPos> fullPositions, double longitudeAsc)
    {
        List<GraphicCelPointPositions> graphPositions = new();
        List<PresentableCommonPositions> presentablePositions = _celPointFactory.CreateCelPointPosForDataGrid(fullPositions);
        int count = 0;
        foreach ((ChartPoints celPoint, FullPointPos? value) in fullPositions)
        {
            double longitude = value.Ecliptical.MainPosSpeed.Position;
            double mundanePos = longitude - longitudeAsc + 90.0;
            if (mundanePos < 0.0) mundanePos += 360.0;
            if (mundanePos >= 360.0) mundanePos -= 360.0;
            string longitudeText = presentablePositions[count].LongText;

            GraphicCelPointPositions graphicPos = new(celPoint, longitude, mundanePos, longitudeText);
            graphPositions.Add(graphicPos);
            count++;
        }
        return graphPositions;
    }

}