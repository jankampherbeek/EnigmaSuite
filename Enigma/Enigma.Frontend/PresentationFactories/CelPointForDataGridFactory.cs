// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.CalcVars;
using Enigma.Domain.Positional;
using Enigma.Frontend.InputSupport.Conversions;
using Enigma.Frontend.UiDomain;
using System;
using System.Collections.Generic;

namespace Enigma.Frontend.PresentationFactories;


public interface ICelPointForDataGridFactory
{
    List<PresentableSolSysPointPositions> CreateCelPointPosForDataGrid(List<FullSolSysPointPos> fullSolSysPointPositions);
}


public class CelPointForDataGridFactory : ICelPointForDataGridFactory
{
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly ISolarSystemPointSpecifications _solarSystemPointSpecifications;

    public CelPointForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions, ISolarSystemPointSpecifications solarSystemPointSpecifications)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
        _solarSystemPointSpecifications = solarSystemPointSpecifications;
    }

    public List<PresentableSolSysPointPositions> CreateCelPointPosForDataGrid(List<FullSolSysPointPos> fullSolSysPointPositions)
    {
        List<PresentableSolSysPointPositions> positions = new();
        foreach (var celPos in fullSolSysPointPositions)
        {
            positions.Add(CreateSinglePos(celPos));
        }
        return positions;
    }


    private PresentableSolSysPointPositions CreateSinglePos(FullSolSysPointPos celPointFullPos)
    {
        string pointGlyph = _solarSystemPointSpecifications.DetailsForPoint(celPointFullPos.SolarSystemPoint).DefaultGlyph;
        double tempPos = celPointFullPos.Longitude.Position;
        double tempSpeed = celPointFullPos.Longitude.Speed;
        string tempSpeedText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(tempSpeed);

        string tempPosText = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(tempPos).longTxt;
        string tempGlyphText = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(tempPos).glyph;

        var eclipticalLong = new Tuple<string, string, string>(tempPosText, tempGlyphText, tempSpeedText);
        var eclipticalLat = new Tuple<string, string>(
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(celPointFullPos.Latitude.Position),
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(celPointFullPos.Latitude.Speed));
        var equatorialRa = new Tuple<string, string>(
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(celPointFullPos.RightAscension.Position),
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(celPointFullPos.RightAscension.Speed));
        var equatorialDecl = new Tuple<string, string>(
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(celPointFullPos.Declination.Position),
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(celPointFullPos.Declination.Speed));
        var distance = new Tuple<string, string>(
            String.Format("{0:F8}", celPointFullPos.Distance.Position),
            String.Format("{0:F8}", celPointFullPos.Distance.Speed));
        string azimuth = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(celPointFullPos.AzimuthAltitude.Azimuth);
        string altitude = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(celPointFullPos.AzimuthAltitude.Altitude);

        return new PresentableSolSysPointPositions(
            pointGlyph, eclipticalLong, eclipticalLat, equatorialRa, equatorialDecl, distance, azimuth, altitude);
    }


}