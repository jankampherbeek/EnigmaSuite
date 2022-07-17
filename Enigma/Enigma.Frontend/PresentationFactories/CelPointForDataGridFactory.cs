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

namespace Enigma.Frontend.PresentationFactories;


public interface ICelPointForDataGridFactory
{
    List<PresentableSolSysPointPositions> CreateCelPointPosForDataGrid(List<FullSolSysPointPos> fullSolSysPointPositions);
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
        string tempSpeedText = _doubleToDmsConversions.ConvertDoubleToPositionsText(tempSpeed);


        string tempPosText = _doubleToDmsConversions.ConvertDoubleToLongWithGlyph(tempPos).longTxt;
        string tempGlyphText = _doubleToDmsConversions.ConvertDoubleToLongWithGlyph(tempPos).glyph;

        var eclipticalLong = new Tuple<string, string, string>(tempPosText, tempGlyphText, tempSpeedText);

        Console.WriteLine("-----------------------");
        Console.WriteLine(celPointFullPos.SolarSystemPoint.ToString());
        Console.WriteLine("tempSpeed: " + tempSpeed);
        Console.WriteLine("tempSpeedText: " + tempSpeedText);

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

        return new PresentableSolSysPointPositions(
            pointGlyph, eclipticalLong, eclipticalLat, equatorialRa, equatorialDecl, distance, azimuth, altitude);
    }


}