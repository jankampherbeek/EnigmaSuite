// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
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

/// <summary>Factory for presentable combinations of lontiude and declination.</summary>
public interface IDeclinationLongitudeForDataGridFactory
{
    /// <summary>Builds a presentable combination of longitude and declination to be used in a data grid.</summary>
    /// <param name="calculatedChart">A calculated chart.</param>
    /// <returns>The presentable positions.</returns>
    List<PresentableDeclinationLongitude> CreateDeclinationLongitudeForDataGrid(CalculatedChart calculatedChart);
}


/// <inheritdoc/>
public class DeclinationLongitudeForDataGridFactory: IDeclinationLongitudeForDataGridFactory
{
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;

    public DeclinationLongitudeForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
    }
    /// <inheritdoc/>
    public List<PresentableDeclinationLongitude> CreateDeclinationLongitudeForDataGrid(CalculatedChart calculatedChart)
    {
        List<KeyValuePair<ChartPoints, FullPointPos>> relevantPositions = calculatedChart.Positions.
            Where(candidatePos => candidatePos.Key.GetDetails().PointCat == PointCats.Common || candidatePos.Key.GetDetails().PointCat == PointCats.Angle).ToList();
        return relevantPositions.Select(pointPos => 
            new Tuple<ChartPoints, FullPointPos>(pointPos.Key, pointPos.Value)).
            Select(pointAndPosition => CreatePresPositions(pointAndPosition)).ToList();
    }
        
    private PresentableDeclinationLongitude CreatePresPositions(Tuple<ChartPoints, FullPointPos> pointAndPos)
    {

        char pointglyph = GlyphsForChartPoints.FindGlyph(pointAndPos.Item1);
        var longAndGlyph =
            _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(pointAndPos.Item2.Ecliptical.MainPosSpeed.Position);
        string longitude = longAndGlyph.longTxt;
        char signGlyph = longAndGlyph.glyph;
        string declination =
            _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(pointAndPos.Item2.Equatorial.DeviationPosSpeed
                .Position);

        return new PresentableDeclinationLongitude(pointglyph, longitude, signGlyph, declination);
        
        
    }
}