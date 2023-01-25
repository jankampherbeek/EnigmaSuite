// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Charts;
using Enigma.Domain.Points;
using Enigma.Frontend.Helpers.Interfaces;
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

    public List<PresentableHousePositions> CreateHousePosForDataGrid(FullHousesPositions fhPositions)
    {
        List<PresentableHousePositions> positions = new()
        {
            CreateSingleCuspPos("MC", fhPositions.Angles[ChartPoints.Mc]),
            CreateSingleCuspPos("Asc", fhPositions.Angles[ChartPoints.Ascendant])
        };
        int index = 1;
        foreach (var cusp in fhPositions.Cusps)
        {
            positions.Add(CreateSingleCuspPos(index++.ToString(), cusp.Value));
        }
        positions.Add(CreateSingleCuspPos("Vertex",   fhPositions.Angles[ChartPoints.Vertex]));
        positions.Add(CreateSingleCuspPos("East point", fhPositions.Angles[ChartPoints.EastPoint]));
        return positions;
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