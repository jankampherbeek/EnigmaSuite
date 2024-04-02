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

/// <summary>Handle the creation of presentable longitude equivalants for use in datagrid.</summary>
public interface ILongitudeEquivalentsForDataGridFactory
{
    /// <summary>Create longitude equivalents.</summary>
    /// <param name="equivalents">Calculated equivalents.</param>
    /// <param name="radixPositions">Full positions from radix.
    /// All chartpoints in equivalents should have a counterpart in radixPositionss.</param>
    /// <param name="obliquity">Obliquity.</param>
    /// <returns>The presentable longitude equivalents.</returns>
    List<PresentableLongitudeEquivalent> CreateLongitudeEquivalentsForDataGrid
        (List<Tuple<PositionedPoint, bool>> equivalents, Dictionary<ChartPoints, FullPointPos> radixPositions, 
            double obliquity);
}

/// <inheritdoc/>
public class LongitudeEquivalentsForDataGridFactory:ILongitudeEquivalentsForDataGridFactory
{
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
   
    public LongitudeEquivalentsForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
    }
    
    /// <inheritdoc/>
    public List<PresentableLongitudeEquivalent> CreateLongitudeEquivalentsForDataGrid
        (List<Tuple<PositionedPoint, bool>> equivalents, Dictionary<ChartPoints, FullPointPos> radixPositions, 
            double obliquity)
    {
        List<PresentableLongitudeEquivalent> presentableEquivalents = new();

        foreach (var equivalent in equivalents)
        {
            string pointText = equivalent.Item1.Point.GetDetails().Text;
            char pointGlyph = GlyphsForChartPoints.FindGlyph(equivalent.Item1.Point);
            double radixLongitude = 0.0;
            double radixDeclination = 0.0;
            foreach (KeyValuePair<ChartPoints, FullPointPos> radixPos in radixPositions.Where(radixPos 
                         => radixPos.Key == equivalent.Item1.Point))
            {
                radixLongitude = radixPos.Value.Ecliptical.MainPosSpeed.Position;
                radixDeclination = radixPos.Value.Equatorial.DeviationPosSpeed.Position;
            }

            (string longitude, char longitudeGlyph) = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(radixLongitude);
            string declination = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(radixDeclination);
            string coDeclination = "";
            double coDeclValue = 0.0;
            if (equivalent.Item2)       // OOB
            {
                double oobDiff = 0.0;
                if (radixDeclination > 0)
                {
                    oobDiff = radixDeclination - obliquity;
                    coDeclValue = obliquity - oobDiff;
                }
                else
                {
                    oobDiff = Math.Abs(radixDeclination) - obliquity;
                    coDeclValue = oobDiff - obliquity;
                }
                coDeclination = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(coDeclValue);
            }
            (string longitudeEquivalent, char leGlyph) =
                _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(equivalent.Item1.Position);
            presentableEquivalents.Add(new PresentableLongitudeEquivalent(pointText, pointGlyph, longitude, 
                longitudeGlyph, declination, coDeclination, longitudeEquivalent, leGlyph));
        }
        return presentableEquivalents;
    }
    
}