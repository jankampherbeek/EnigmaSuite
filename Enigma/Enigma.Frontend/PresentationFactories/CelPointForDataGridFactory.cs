// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Support.Conversions;

namespace Enigma.Frontend.Ui.PresentationFactories;

public interface ICelPointForDataGridFactory
{
    public List<PresentableCommonPositions> CreateCelPointPosForDataGrid(Dictionary<ChartPoints, FullPointPos> commonPositions);
}

public sealed class CelPointForDataGridFactory : ICelPointForDataGridFactory
{
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly GlyphsForChartPoints _glyphsForChartPoints;

    public CelPointForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
        _glyphsForChartPoints = new GlyphsForChartPoints();
    }

    public List<PresentableCommonPositions> CreateCelPointPosForDataGrid(Dictionary<ChartPoints, FullPointPos> positions)
    {
        return (from celPos in positions 
            where celPos.Key.GetDetails().PointCat == PointCats.Common || celPos.Key.GetDetails().PointCat == PointCats.Zodiac 
                                                                       || celPos.Key.GetDetails().PointCat == PointCats.Lots 
            select CreateSinglePos(celPos)).ToList();
    }

    private PresentableCommonPositions CreateSinglePos(KeyValuePair<ChartPoints, FullPointPos> commonPos)
    {
        const string noData = "--";
        char pointGlyph = GlyphsForChartPoints.FindGlyph(commonPos.Key);
        double longPos = commonPos.Value.Ecliptical.MainPosSpeed.Position;
        double longSpeed = commonPos.Value.Ecliptical.MainPosSpeed.Speed;
        string longSpeedText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(longSpeed);

        string longPosText = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(longPos).longTxt;
        char longGlyph = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(longPos).glyph;

        var eclipticalLong = new Tuple<string, char, string>(longPosText, longGlyph, longSpeedText);
        var eclipticalLat = new Tuple<string, string>(
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(commonPos.Value.Ecliptical.DeviationPosSpeed
                .Position),
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(commonPos.Value.Ecliptical.DeviationPosSpeed
                .Speed));
        var equatorialRa = new Tuple<string, string>(
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(commonPos.Value.Equatorial.MainPosSpeed.Position),
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(commonPos.Value.Equatorial.MainPosSpeed.Speed));
        var equatorialDecl = new Tuple<string, string>(
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(commonPos.Value.Equatorial.DeviationPosSpeed
                .Position),
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(commonPos.Value.Equatorial.DeviationPosSpeed
                .Speed));
        var distance = new Tuple<string, string>(
            $"{commonPos.Value.Equatorial.DistancePosSpeed.Position:F8}",
            $"{commonPos.Value.Equatorial.DistancePosSpeed.Speed:F8}");
        string azimuth =
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(commonPos.Value.Horizontal.MainPosSpeed.Position);
        string altitude =
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(commonPos.Value.Horizontal.DeviationPosSpeed
                .Position);

        if (commonPos.Key == ChartPoints.VulcanusCarteret || commonPos.Key == ChartPoints.PersephoneCarteret ||
            commonPos.Key == ChartPoints.ApogeeDuval)
        {
            eclipticalLong = new Tuple<string, char, string>(longPosText, longGlyph, noData);
            eclipticalLat = new Tuple<string, string>(noData, noData);
            equatorialRa = new Tuple<string, string>(noData, noData);
            equatorialDecl = new Tuple<string, string>(noData, noData);
            distance = new Tuple<string, string>(noData, noData);
            azimuth = noData;
            altitude = noData;
        }
        return new PresentableCommonPositions(
            pointGlyph, eclipticalLong, eclipticalLat, equatorialRa, equatorialDecl, distance, azimuth, altitude);
    }


}