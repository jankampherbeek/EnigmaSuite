// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Charts;
using Enigma.Domain.Points;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Interfaces;
using System;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.PresentationFactories;


public sealed class CelPointForDataGridFactory : ICelPointForDataGridFactory
{
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly GlyphsForChartPoints _glyphsForChartPoints;

    public CelPointForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
        _glyphsForChartPoints = new GlyphsForChartPoints();
    }

    public List<PresentableCelPointPositions> CreateCelPointPosForDataGrid(List<FullChartPointPos> celPointPositions)
    {
        List<PresentableCelPointPositions> positions = new();
        foreach (var celPos in celPointPositions)
        {
            positions.Add(CreateSinglePos(celPos));
        }
        return positions;
    }

    private PresentableCelPointPositions CreateSinglePos(FullChartPointPos celPointFullPos)
    {
        char pointGlyph = _glyphsForChartPoints.FindGlyph(celPointFullPos.ChartPoint);
        double tempPos = celPointFullPos.PointPos.Longitude.Position;
        double tempSpeed = celPointFullPos.PointPos.Longitude.Speed;
        string tempSpeedText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(tempSpeed);

        string tempPosText = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(tempPos).longTxt;
        char tempGlyph = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(tempPos).glyph;

        var eclipticalLong = new Tuple<string, char, string>(tempPosText, tempGlyph, tempSpeedText);
        var eclipticalLat = new Tuple<string, string>(
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(celPointFullPos.PointPos.Latitude.Position),
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(celPointFullPos.PointPos.Latitude.Speed));
        var equatorialRa = new Tuple<string, string>(
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(celPointFullPos.PointPos.RightAscension.Position),
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(celPointFullPos.PointPos.RightAscension.Speed));
        var equatorialDecl = new Tuple<string, string>(
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(celPointFullPos.PointPos.Declination.Position),
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(celPointFullPos.PointPos.Declination.Speed));
        var distance = new Tuple<string, string>(
            String.Format("{0:F8}", celPointFullPos.Distance.Position),
            String.Format("{0:F8}", celPointFullPos.Distance.Speed));
        string azimuth = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(celPointFullPos.PointPos.AzimuthAltitude.Azimuth);
        string altitude = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(celPointFullPos.PointPos.AzimuthAltitude.Altitude);

        return new PresentableCelPointPositions(
            pointGlyph, eclipticalLong, eclipticalLat, equatorialRa, equatorialDecl, distance, azimuth, altitude);
    }


}