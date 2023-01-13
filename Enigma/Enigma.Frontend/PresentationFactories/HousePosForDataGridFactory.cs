// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
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

    public List<PresentableHousePositions> CreateHousePosForDataGrid(FullHousesPositions fullHousesPositions)
    {
        List<PresentableHousePositions> positions = new()
        {
            CreateSingleCuspPos("MC", fullHousesPositions.Mc),
            CreateSingleCuspPos("Asc", fullHousesPositions.Ascendant)
        };
        int index = 1;
        foreach (var cusp in fullHousesPositions.Cusps)
        {
            positions.Add(CreateSingleCuspPos(index++.ToString(), cusp));
        }
        positions.Add(CreateSingleCuspPos("Vertex", fullHousesPositions.Vertex));
        positions.Add(CreateSingleCuspPos("East point", fullHousesPositions.EastPoint));
        return positions;
    }


    private PresentableHousePositions CreateSingleCuspPos(string identification, FullChartPointPos cuspFullPos)
    {
        PresentableHousePositions positions = new(
            identification,
            _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(cuspFullPos.PointPos.Longitude.Position).longTxt,
            _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(cuspFullPos.PointPos.Longitude.Position).glyph,
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(cuspFullPos.PointPos.RightAscension.Position),
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(cuspFullPos.PointPos.Declination.Position),
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(cuspFullPos.PointPos.AzimuthAltitude.Azimuth),
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(cuspFullPos.PointPos.AzimuthAltitude.Altitude));
        return positions;
    }


}