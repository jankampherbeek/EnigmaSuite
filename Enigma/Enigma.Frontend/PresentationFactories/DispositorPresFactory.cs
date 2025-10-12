// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Core.Slices.BlaSchema;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Frontend.Ui.PresentationFactories;

/// <summary>
/// Presentable dispositor counts for BLA schema  
/// </summary>
/// <param name="Rulers">Contains both both rulers</param>
/// <param name="SignSplitted">Counts for each ruler (string with two numbers)</param>
/// <param name="SignMain">Counts in signs (sum of both values in SignSplitted</param>
/// <param name="SignIndirect">Indirect counts for signs</param>
/// <param name="SignSum">Sum of SignMain and SignIndirect</param>
/// <param name="HouseMain">Counts in houses</param>
/// <param name="HouseIndirect">Indirect counts in houses</param>
/// <param name="HouseSum">Sum of HouseMain and HouseIndirect</param>
/// <param name="DecanateDirect">Counts in direct decanates</param>
/// <param name="DecanateIndirect">Indirect counts in decanates</param>
/// <param name="DecanateSum">Sum of DecanateDirect and DecanateIndirect</param>
/// <param name="Total">Total of SignSum, HouseSum and DecanateSum</param>
public record PresentableDispositorCounts(string Rulers, string SignSplitted, int SignMain, int SignIndirect, int SignSum, 
    int HouseMain, int HouseIndirect, int HouseSum, int DecanateDirect, int DecanateIndirect, int DecanateSum, int Total);

public class DispositorPresFactory
{
    public List<PresentableDispositorCounts> CreatePresDispositorCounts(List<BlaDispositorLine> dispositorLines)
    {
        const string separator = "/";
        const string space = " ";
        var mainRulerGlyph = ' ';
        var subRulerGlyph = ' ';
        var presDispositorCounts = new List<PresentableDispositorCounts>();
        foreach (var dLine in dispositorLines)
        {
            foreach (var rulerPair in BlaDomain.RulerPairs())
            {
                if (rulerPair.MainRuler == dLine.MainRuler)
                {
                    mainRulerGlyph = GlyphsForChartPoints.FindGlyph(rulerPair.MainRuler);
                    subRulerGlyph = GlyphsForChartPoints.FindGlyph(rulerPair.SubRuler);
                }
            }

            var rulerGlyphs = mainRulerGlyph + space + subRulerGlyph;
            var signSplitted = dLine.MainRulerSignCount + separator + dLine.SubRulerSignCount;
            
            presDispositorCounts.Add(new PresentableDispositorCounts(rulerGlyphs, signSplitted,
                dLine.SumRulerSignCount, 
                dLine.IndirectRulerSignCount, 
                dLine.TotalRulerSignCount,
                dLine.DirectRulerHouseCount,
                dLine.IndirectRulerHouseCount,
                dLine.SumRulerHouseCount, 
                dLine.DirectRulerDecanataCount, 
                dLine.IndirectRulerDecanataCount, 
                dLine.SumRulerDecanataCount,
                dLine.Total));
        }
        return presDispositorCounts;   
    }

    
    
    
}