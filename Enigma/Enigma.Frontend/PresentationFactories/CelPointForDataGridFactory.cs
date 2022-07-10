// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.CalcVars;
using Enigma.Domain.Positional;
using Enigma.Frontend.InputSupport.Conversions;
using Enigma.Frontend.Support;
using Enigma.Frontend.UiDomain;
using System;
using System.Collections.Generic;

namespace Enigma.Frontend.InputSupport.PresentationFactories;


public interface ICelPointForDataGridFactory
{
    List<PresentableCelPointPositions> CreateCelPointPosForDataGrid(List<FullSolSysPointPos> fullSolSysPointPositions);
}


public class CelPointForDataGridFactory : ICelPointForDataGridFactory
{
    private IDoubleToDmsConversions _doubleToDmsConversions;
    private ISolarSystemPointSpecifications _solarSystemPointSpecifications;
    private IRosetta _rosetta;

    public CelPointForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions, ISolarSystemPointSpecifications solarSystemPointSpecifications, IRosetta rosetta)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
        _solarSystemPointSpecifications = solarSystemPointSpecifications;
        _rosetta = rosetta;
    }

    public List<PresentableCelPointPositions> CreateCelPointPosForDataGrid(List<FullSolSysPointPos> fullSolSysPointPositions)
    {
        List<PresentableCelPointPositions> positions = new();
        foreach (var celPos in fullSolSysPointPositions)
        {
            positions.Add(CreateSinglePos(celPos));
        }
        return positions;
    }


    private PresentableCelPointPositions CreateSinglePos(FullSolSysPointPos celPointFullPos)
    {
        string pointGlyph = _solarSystemPointSpecifications.DetailsForPoint(celPointFullPos.SolarSystemPoint).DefaultGlyph;
        var eclipticalLong = new Tuple<string, string, string>(
            _doubleToDmsConversions.ConvertDoubleToLongWithGlyph(celPointFullPos.Longitude.Position).longTxt, 
            _doubleToDmsConversions.ConvertDoubleToLongWithGlyph(celPointFullPos.Longitude.Position).glyph,
            _doubleToDmsConversions.ConvertDoubleToPositionsText(celPointFullPos.Longitude.Speed));
        var eclipticalLat = new Tuple<string, string>(
            _doubleToDmsConversions.ConvertDoubleToPositionsText(celPointFullPos.Latitude.Position),
            _doubleToDmsConversions.ConvertDoubleToPositionsText(celPointFullPos.Latitude.Speed));
        var equatorialRa = new Tuple<string, string>(
            _doubleToDmsConversions.ConvertDoubleToPositionsText(celPointFullPos.RightAscension.Position),
            _doubleToDmsConversions.ConvertDoubleToPositionsText(celPointFullPos.RightAscension.Speed));
        var equatorialDecl = new Tuple<string, string>(
            _doubleToDmsConversions.ConvertDoubleToPositionsText(celPointFullPos.Declination.Position),
            _doubleToDmsConversions.ConvertDoubleToPositionsText(celPointFullPos.Declination.Speed));
        var distance = new Tuple<string, string>(
            String.Format("{0:F8}", celPointFullPos.Distance.Position),
            String.Format("{0:F8}", celPointFullPos.Distance.Speed));
        string azimuth = _doubleToDmsConversions.ConvertDoubleToPositionsText(celPointFullPos.AzimuthAltitude.Azimuth);
        string altitude = _doubleToDmsConversions.ConvertDoubleToPositionsText(celPointFullPos.AzimuthAltitude.Altitude);

        return new PresentableCelPointPositions(
            pointGlyph, eclipticalLong, eclipticalLat, equatorialRa, equatorialDecl, distance, azimuth, altitude);
    }


}