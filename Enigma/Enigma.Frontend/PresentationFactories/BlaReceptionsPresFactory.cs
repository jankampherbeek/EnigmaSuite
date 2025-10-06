// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Core.Slices.BlaSchema;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Frontend.Ui.PresentationFactories;


/// <summary>
/// Factor pair in analog house and sign
/// </summary>
/// <param name="Factor1">Glyph for first factor</param>
/// <param name="Plus1">First plus sign</param>
/// <param name="Factor2">Glyph for second factor</param>
/// <param name="EqualSign">Equals sign</param>
/// <param name="Position1">Number for first house (1..12) or glyph for first sign</param>
/// <param name="Plus2">Second plus sign</param>
/// <param name="Position2">Number or second houdes (1..12) or glyph for second sign</param>
public record PresentableReception(char Factor1, string Plus1, char Factor2, string EqualSign, string Position1, string Plus2, string Position2);

public class BlaReceptionsPresFactory
{
    private const string PLUS = " + ";
    private const string EQUAL = " = ";
    
    public List<PresentableReception> CreateReceptionsInSigns(List<Reception> receptions)
    {
        var presReceptions = new List<PresentableReception>();
        foreach (var rec in receptions)
        {
            var f1Glyph = GlyphsForChartPoints.FindGlyph(rec.Point1);
            var f2Glyph = GlyphsForChartPoints.FindGlyph(rec.Point2);
            var pos1 = rec.SignOrHouse1.ToString();
            var pos2 = rec.SignOrHouse2.ToString();
            presReceptions.Add(new PresentableReception(f1Glyph, PLUS, f2Glyph, EQUAL, pos1, PLUS, pos2));
        }
        return presReceptions;
    }
    
    public List<PresentableReception> CreateReceptionsInHouses(List<Reception> receptions)
    {
        var presReceptions = new List<PresentableReception>();
        foreach (var rec in receptions)
        {
            var f1Glyph = GlyphsForChartPoints.FindGlyph(rec.Point1);
            var f2Glyph = GlyphsForChartPoints.FindGlyph(rec.Point2);
            var pos1 = rec.SignOrHouse1.ToString();
            var pos2 = rec.SignOrHouse2.ToString();
            presReceptions.Add(new PresentableReception(f1Glyph, PLUS, f2Glyph, EQUAL, pos1, PLUS, pos2));
        }
        return presReceptions;
    }
    
    private string DefineGlyph(int sign)
    {
        string[] glyphs = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "="];
        return glyphs[sign-1];
    }
    
}