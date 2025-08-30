// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;

// Creates overview of signrulers
public static class SignRulers
{
    /// <summary>
    /// Creates list with sign rulers and subrulers
    /// </summary>
    /// <returns>Dic itonay with index for sign (1..12) and a RulerPair with the main and the subruler, in that sequence</returns>
    public static Dictionary<int, RulerPair> CreateSignRulers()
    {
        var rulers = new Dictionary<int, RulerPair>
        {
            { 1, new RulerPair(ChartPoints.Mars, ChartPoints.Pluto) },
            { 2, new RulerPair(ChartPoints.Venus, ChartPoints.PersephoneCarteret) },
            { 3, new RulerPair(ChartPoints.Mercury, ChartPoints.VulcanusCarteret) },
            { 4, new RulerPair(ChartPoints.Moon, ChartPoints.Priapus) },
            { 5, new RulerPair(ChartPoints.Sun, ChartPoints.ApogeeMean) },
            { 6, new RulerPair(ChartPoints.Mercury, ChartPoints.VulcanusCarteret) },
            { 7, new RulerPair(ChartPoints.Venus, ChartPoints.PersephoneCarteret) },
            { 8, new RulerPair(ChartPoints.Mars, ChartPoints.Pluto) },
            { 9, new RulerPair(ChartPoints.Jupiter, ChartPoints.Neptune) },
            { 10, new RulerPair(ChartPoints.Sun, ChartPoints.ApogeeMean) },
            { 11, new RulerPair(ChartPoints.Moon, ChartPoints.Priapus) },
            { 12, new RulerPair(ChartPoints.Jupiter, ChartPoints.Neptune) }
        };
        return rulers;
    }
    
}