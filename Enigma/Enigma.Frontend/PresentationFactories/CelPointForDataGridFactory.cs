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

    public List<PresentableCommonPositions> CreateCelPointPosForDataGrid(Dictionary<ChartPoints, FullPointPos> commonPositions)
    {
        List<PresentableCommonPositions> positions = new();
        foreach (var celPos in commonPositions)
        {
            positions.Add(CreateSinglePos(celPos));
        }
        return positions;
    }

    private PresentableCommonPositions CreateSinglePos(KeyValuePair<ChartPoints,  FullPointPos> commonPos)
    {
        char pointGlyph = _glyphsForChartPoints.FindGlyph(commonPos.Key);
        double longPos = commonPos.Value.Ecliptical.MainPosSpeed.Position;
        double longSpeed = commonPos.Value.Ecliptical.MainPosSpeed.Speed;
        string longSpeedText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(longSpeed);

        string longPosText = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(longPos).longTxt;
        char longGlyph = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(longPos).glyph;

        var eclipticalLong = new Tuple<string, char, string>(longPosText, longGlyph, longSpeedText);
        var eclipticalLat = new Tuple<string, string>(
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(commonPos.Value.Ecliptical.DeviationPosSpeed.Position),
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(commonPos.Value.Ecliptical.DeviationPosSpeed.Speed));
        var equatorialRa = new Tuple<string, string>(
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(commonPos.Value.Equatorial.MainPosSpeed.Position),
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(commonPos.Value.Equatorial.MainPosSpeed.Speed));
        var equatorialDecl = new Tuple<string, string>(
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(commonPos.Value.Equatorial.DeviationPosSpeed.Position),
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(commonPos.Value.Equatorial.DeviationPosSpeed.Speed));
        var distance = new Tuple<string, string>(
            String.Format("{0:F8}", commonPos.Value.Equatorial.DistancePosSpeed.Position),
            String.Format("{0:F8}", commonPos.Value.Equatorial.DistancePosSpeed.Speed));
        string azimuth = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(commonPos.Value.Horizontal.MainPosSpeed.Position);
        string altitude = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(commonPos.Value.Horizontal.DeviationPosSpeed.Position);

        return new PresentableCommonPositions(
            pointGlyph, eclipticalLong, eclipticalLat, equatorialRa, equatorialDecl, distance, azimuth, altitude);
    }


}