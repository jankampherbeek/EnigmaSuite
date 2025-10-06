// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Core.Slices.BlaSchema;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Frontend.Ui.PresentationFactories;

/// <summary>
/// General description of a reinforcement position
/// </summary>
/// <param name="Factor">Glyph for the chart point</param>
/// <param name="Descr">Description of the type of position, typically 'in'</param>
/// <param name="Position">The index of the position, this can be a house or a sign</param>
/// <param name="PositionGlyph">Glyph for the position, only to be used for signs</param>
public record PresenTableReinforcementFactor(char Factor, string Descr, int Position, char PositionGlyph);

/// <summary>
/// Factor pair in analog house and sign
/// </summary>
/// <param name="Factor1">Glyph for first factor</param>
/// <param name="Plus1">First plus sign</param>
/// <param name="Factor2">Glyph for second factor</param>
/// <param name="EqualSign">Equals sign</param>
/// <param name="House">Number for house (1..12)</param>
/// <param name="Plus2">Second plus sign</param>
/// <param name="Sign">Glyph for sign</param>
public record PresentablePairAnalogHouseSign(char Factor1, string Plus1, char Factor2, string EqualSign, string House, string Plus2, char Sign);


/// <summary>
/// Presentable versions of reinforcements for the VBLA Schema
/// </summary>
public class BlaReinforcementsPresFactory
{

    public List<PresenTableReinforcementFactor> CreateReinforcementFactors(Dictionary<ChartPoints, int> factors)
    {
        var presFactors = new List<PresenTableReinforcementFactor>();
        foreach (var factor in factors)
        {
            var factorGlyph = GlyphsForChartPoints.FindGlyph(factor.Key);
            var position = factor.Value;
            var positionGlyph = DefineGlyph(position);
            var descr = " in ";
            presFactors.Add(new PresenTableReinforcementFactor(factorGlyph, descr, position, positionGlyph));
        }
        return presFactors;
    }

    public List<PresentablePairAnalogHouseSign> CreatePairsWithAnalogHouseSigns(List<FactorPairAnalogHouseSign> factorPairs)
    {
        var presFactorPairs = new List<PresentablePairAnalogHouseSign>();
        var plus = " + ";
        var eqSign = " = ";
        foreach (var fp in factorPairs)
        {
            var factor1Glyph = GlyphsForChartPoints.FindGlyph(fp.PointInSign);
            var factor2Glyph = GlyphsForChartPoints.FindGlyph(fp.PointInHouse);
            var signGlyph = DefineGlyph(fp.Sign);
            presFactorPairs.Add(new PresentablePairAnalogHouseSign(factor1Glyph, plus,factor2Glyph, eqSign, fp.House.ToString(), plus, signGlyph));
        }
        return presFactorPairs;
    }
    
    
    private char DefineGlyph(int sign)
    {
        char[] glyphs = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-', '='];
        return glyphs[sign-1];
    }
}
    