// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;
using Enigma.Domain.Interfaces;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Ui.Interfaces;
using System;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.PresentationFactories;


public class CelPointForDataGridFactory : ICelPointForDataGridFactory
{
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly ICelPointSpecifications _celPointSpecifications;

    public CelPointForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions, ICelPointSpecifications celPointSpecifications)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
        _celPointSpecifications = celPointSpecifications;
    }

    public List<PresentableCelPointPositions> CreateCelPointPosForDataGrid(List<FullCelPointPos> fullCelPointPositions)
    {
        List<PresentableCelPointPositions> positions = new();
        foreach (var celPos in fullCelPointPositions)
        {
            positions.Add(CreateSinglePos(celPos));
        }
        return positions;
    }

    private PresentableCelPointPositions CreateSinglePos(FullCelPointPos celPointFullPos)
    {
        string pointGlyph = _celPointSpecifications.DetailsForPoint(celPointFullPos.CelPoint).DefaultGlyph;
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

        return new PresentableCelPointPositions(
            pointGlyph, eclipticalLong, eclipticalLat, equatorialRa, equatorialDecl, distance, azimuth, altitude);
    }


}