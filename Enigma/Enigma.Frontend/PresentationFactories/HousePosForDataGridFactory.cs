// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Charts;
using Enigma.Domain.Points;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Interfaces;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.PresentationFactories;


public class HousePosForDataGridFactory : IHousePosForDataGridFactory
{
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;

    public HousePosForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
    }

    public List<PresentableHousePositions> CreateHousePosForDataGrid(Dictionary<ChartPoints, FullPointPos> positions)
    {
        List<PresentableHousePositions> presPositions = new()
        {
            CreateSingleCuspPos("MC", positions[ChartPoints.Mc]),
            CreateSingleCuspPos("Asc", positions[ChartPoints.Ascendant])
        };
        if (positions.ContainsKey(ChartPoints.Vertex))
        {
            string descr = Rosetta.TextForId(ChartPoints.Vertex.GetDetails().TextId);
            presPositions.Add(CreateSingleCuspPos(descr, positions[ChartPoints.Vertex]));
        }
        if (positions.ContainsKey(ChartPoints.EastPoint))
        {
            string descr = Rosetta.TextForId(ChartPoints.EastPoint.GetDetails().TextId);
            presPositions.Add(CreateSingleCuspPos(descr, positions[ChartPoints.EastPoint]));
        }


        foreach (var item in positions)
        {
            if (item.Key.GetDetails().PointCat == PointCats.Cusp)
            {
                string descr = Rosetta.TextForId(item.Key.GetDetails().TextId);
                presPositions.Add(CreateSingleCuspPos(descr, item.Value));
            }
        }
        return presPositions;
    }


    private PresentableHousePositions CreateSingleCuspPos(string identification, FullPointPos cuspFullPos)
    {
        PresentableHousePositions positions = new(
            identification,
            _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(cuspFullPos.Ecliptical.MainPosSpeed.Position).longTxt,
            _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(cuspFullPos.Ecliptical.MainPosSpeed.Position).glyph,
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(cuspFullPos.Equatorial.MainPosSpeed.Position),
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(cuspFullPos.Equatorial.DeviationPosSpeed.Position),
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(cuspFullPos.Horizontal.MainPosSpeed.Position),
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(cuspFullPos.Horizontal.DeviationPosSpeed.Position));
        return positions;
    }


}