// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;
using Enigma.Frontend.Interfaces;
using Enigma.InputSupport.Interfaces;
using System.Collections.Generic;

namespace Enigma.Frontend.PresentationFactories;


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


    private PresentableHousePositions CreateSingleCuspPos(string identification, CuspFullPos cuspFullPos)
    {
        PresentableHousePositions positions = new(
            identification,
            _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(cuspFullPos.Longitude).longTxt,
            _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(cuspFullPos.Longitude).glyph,
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(cuspFullPos.RaDecl.RightAscension),
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(cuspFullPos.RaDecl.Declination),
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(cuspFullPos.AzimuthAltitude.Azimuth),
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(cuspFullPos.AzimuthAltitude.Altitude));
        return positions;
    }


}