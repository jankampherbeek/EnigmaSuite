// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;
using Enigma.Domain.Enums;
using Enigma.Frontend.Interfaces;
using System.Collections.Generic;

namespace Enigma.Frontend.PresentationFactories;



public class SortedGraphicSolSysPointsFactory : ISortedGraphicSolSysPointsFactory
{
    private readonly ICelPointForDataGridFactory _celPointFactory;

    public SortedGraphicSolSysPointsFactory(ICelPointForDataGridFactory celPointFactory)
    {
        _celPointFactory = celPointFactory;
    }

    public List<GraphicSolSysPointPositions> CreateSortedList(List<FullSolSysPointPos> solSysPointPositions, double longitudeAsc, double minDistance)
    {
        List<GraphicSolSysPointPositions> graphPositions = CreateGraphicPositions(solSysPointPositions, longitudeAsc);
        graphPositions.Sort(delegate (GraphicSolSysPointPositions pos1, GraphicSolSysPointPositions pos2) {
            return pos1.MundanePos.CompareTo(pos2.MundanePos);
        });
        double actDistance = 0.0;
        GraphicSolSysPointPositions lastPos = null;
        foreach (var pos in graphPositions)
        {
            if (lastPos != null)
            {
                actDistance = pos.MundanePos - lastPos.PlotPos;
                if (actDistance < minDistance)
                {
                    pos.PlotPos += (minDistance - actDistance);
                    if (pos.PlotPos >= 360.0) pos.PlotPos -= 360.0;
                }
            }
           
            lastPos = pos;
        }
        return graphPositions;
    }


    private List<GraphicSolSysPointPositions> CreateGraphicPositions(List<FullSolSysPointPos> fullPositions, double longitudeAsc)
    {
        List<GraphicSolSysPointPositions> graphPositions = new();
        List<PresentableSolSysPointPositions> presentablePositions = _celPointFactory.CreateCelPointPosForDataGrid(fullPositions);
        int count = 0;
        foreach (FullSolSysPointPos solSysPointPos in fullPositions)
        {
            double longitude = solSysPointPos.Longitude.Position;
            SolarSystemPoints solSysPoint = solSysPointPos.SolarSystemPoint;
            double mundanePos = longitude - longitudeAsc + 90.0;
            if (mundanePos < 0.0) mundanePos += 360.0;
            if (mundanePos >= 360.0) mundanePos -= 360.0;
            string longitudeText = presentablePositions[count].LongText;

            GraphicSolSysPointPositions graphicPos = new(solSysPoint, longitude, mundanePos, longitudeText);
            graphPositions.Add(graphicPos);
            count++;
        }
        return graphPositions;
    }

}