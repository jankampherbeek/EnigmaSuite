// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Positional;
using Enigma.Frontend.InputSupport.Conversions;
using Enigma.Frontend.UiDomain;
using System.Collections.Generic;

namespace Enigma.Frontend.InputSupport.PresentationFactories;


public interface IHousePosForDataGridFactory
{
    List<PresentableHousePositions> CreateHousePosForDataGrid(FullHousesPositions fullHousesPositions);
}


public class HousePosForDataGridFactory: IHousePosForDataGridFactory
{
    private IDoubleToDmsConversions _doubleToDmsConversions;

    public HousePosForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
    }

    public List<PresentableHousePositions> CreateHousePosForDataGrid(FullHousesPositions fullHousesPositions)
    {
        List<PresentableHousePositions> positions = new();
        positions.Add(CreateSingleCuspPos("MC", fullHousesPositions.Mc));
        positions.Add(CreateSingleCuspPos("Asc", fullHousesPositions.Ascendant));
        int index = 0;
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
        PresentableHousePositions positions = new PresentableHousePositions(
            identification, 
            _doubleToDmsConversions.ConvertDoubleToLongWithGlyph(cuspFullPos.Longitude).longTxt,
            _doubleToDmsConversions.ConvertDoubleToLongWithGlyph(cuspFullPos.Longitude).glyph,
            _doubleToDmsConversions.ConvertDoubleToPositionsText(cuspFullPos.RaDecl.RightAscension),
            _doubleToDmsConversions.ConvertDoubleToPositionsText(cuspFullPos.RaDecl.Declination),
            _doubleToDmsConversions.ConvertDoubleToPositionsText(cuspFullPos.AzimuthAltitude.Azimuth),
            _doubleToDmsConversions.ConvertDoubleToPositionsText(cuspFullPos.AzimuthAltitude.Altitude));
        return positions;
    }
    

}