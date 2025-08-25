// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.Solar;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;

/// <summary>
/// Combination of ruler and subruler
/// </summary>
/// <param name="Ruler">ChartPoint for the ruler</param>
/// <param name="SubRuler">ChartPoint for the subruler</param>
public record RulerPair(ChartPoints Ruler, ChartPoints SubRuler);


/// <summary>
/// Domain for Black Lights Astrology
/// </summary>
public static class BlaDomain
{

    public static Dictionary<int, RulerPair> SignRulers()
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
    
    private static Dictionary<int, RulerPair> MundaneHouseRulers()
    {
        var rulers = new Dictionary<int, RulerPair>
        {
            { 1, new RulerPair(ChartPoints.Mars, ChartPoints.Pluto) },
            { 2, new RulerPair(ChartPoints.Venus, ChartPoints.PersephoneCarteret) },
            { 3, new RulerPair(ChartPoints.Mercury, ChartPoints.VulcanusCarteret) },
            { 4, new RulerPair(ChartPoints.Moon, ChartPoints.Priapus) },
            { 5, new RulerPair(ChartPoints.Sun, ChartPoints.ApogeeMean) },
            { 6, new RulerPair(ChartPoints.VulcanusCarteret, ChartPoints.Mercury) },
            { 7, new RulerPair(ChartPoints.PersephoneCarteret, ChartPoints.Venus) },
            { 8, new RulerPair(ChartPoints.Pluto, ChartPoints.Mars) },
            { 9, new RulerPair(ChartPoints.Jupiter, ChartPoints.Neptune) },
            { 10, new RulerPair(ChartPoints.ApogeeMean, ChartPoints.Sun) },
            { 11, new RulerPair(ChartPoints.Priapus, ChartPoints.Moon) },
            { 12, new RulerPair(ChartPoints.Neptune, ChartPoints.Jupiter) }
        };
        return rulers;
    }
    
}
