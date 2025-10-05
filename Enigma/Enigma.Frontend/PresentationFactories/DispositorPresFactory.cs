// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using Enigma.Core.Slices.BlaSchema;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Frontend.Ui.PresentationFactories;

public record PresentableDispositorCounts(String Rulers, string SignSplitted, int SignMain, int SignSub, int SignSum, int HouseMain, int HouseSub, int HouseSum, int Total);

public class DispositorPresFactory
{
    public List<PresentableDispositorCounts> CreatePresDispositorCounts(List<BlaDispositorLine> dispositorLines)
    {
        const string separator = "/";
        const string space = " ";
        var presDispositorCounts = new List<PresentableDispositorCounts>();
        foreach (var dLine in dispositorLines)
        {
            var mainRulerGlyph = GlyphsForChartPoints.FindGlyph(dLine.MainRuler);
            var subRulerGlyph = GlyphsForChartPoints.FindGlyph(dLine.SubRuler);
            var rulerGlyphs = mainRulerGlyph + space + subRulerGlyph;
            var signSplitted = dLine.MainRulerSignCount + separator + dLine.SubRulerSignCount;


            presDispositorCounts.Add(new PresentableDispositorCounts(rulerGlyphs, signSplitted,
                dLine.MainRulerSignCount,
                dLine.SubRulerSignCount, dLine.SumRulerSignCount, dLine.DirectRulerHouseCount,
                dLine.IndirectRulerHouseCount,
                dLine.SumRulerHouseCount, dLine.Total));
        }
        return presDispositorCounts;   
    }
    
    
    // private static string CreateRulerGlyph(ChartPoints ruler)
    // {
    //     switch (ruler)
    //     {
    //         case ChartPoints.Sun: return "a";
    //         case ChartPoints.Moon: return "b";
    //         case ChartPoints.Mercury: return "c";
    //         case ChartPoints.Venus: return "d";
    //         case ChartPoints.Mars: return "f";
    //         case ChartPoints.Jupiter: return "g";
    //         case ChartPoints.Saturn: return "h";
    //         case ChartPoints.Uranus: return "i";
    //         case ChartPoints.Neptune: return "j";
    //         case ChartPoints.Pluto: return "k";
    //         case ChartPoints.ApogeeMean: return ",";    
    //         case ChartPoints.Priapus: return "\\";
    //         case ChartPoints.PersephoneCarteret: return "à";
    //         case ChartPoints.VulcanusCarteret: return "Ï";            
    //     }
    //     return "";
    // }
    
}