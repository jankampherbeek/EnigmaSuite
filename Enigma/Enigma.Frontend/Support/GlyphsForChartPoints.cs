// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Frontend.Ui.Support;

/// <summary>Returns default glyph for a given chart point.</summary>
/// <remarks>A temporary solution, ultimately the glyphs should be retrieved from the current configuration.</remarks>
public sealed class GlyphsForChartPoints            // TODO 0.3 Replace this solution with a configuration based solution.
{
    private const char EMPTY_GLYPH = ' ';

    public static char FindGlyph(ChartPoints point)
    {
        return point switch
        {
            ChartPoints.Sun => 'a',
            ChartPoints.Moon => 'b',
            ChartPoints.Mercury => 'c',
            ChartPoints.Venus => 'd',
            ChartPoints.Earth => 'e',
            ChartPoints.Mars => 'f',
            ChartPoints.Jupiter => 'g',
            ChartPoints.Saturn => 'h',
            ChartPoints.Uranus => 'i',
            ChartPoints.Neptune => 'j',
            ChartPoints.Pluto => 'k',
            ChartPoints.MeanNode => '{',
            ChartPoints.SouthNode => '}',    
            ChartPoints.Chiron => 'w',
            ChartPoints.PersephoneRam => '/',
            ChartPoints.HermesRam => '<',
            ChartPoints.DemeterRam => '>',
            ChartPoints.CupidoUra => 'y',
            ChartPoints.HadesUra => 'z',
            ChartPoints.ZeusUra => '!',
            ChartPoints.KronosUra => '@',
            ChartPoints.ApollonUra => '#',
            ChartPoints.AdmetosUra => '$',
            ChartPoints.VulcanusUra => '%',
            ChartPoints.PoseidonUra => '&',
            ChartPoints.Eris => '*',
            ChartPoints.Pholus => ')',
            ChartPoints.Ceres => '_',
            ChartPoints.Pallas => 'û',
            ChartPoints.Juno => 'ü',
            ChartPoints.Vesta => 'À',
            ChartPoints.Isis => 'â',
            ChartPoints.Nessus => '(',
            ChartPoints.Huya => 'ï',
            ChartPoints.Varuna => 'ò',
            ChartPoints.Ixion => 'ó',
            ChartPoints.Quaoar => 'ô',
            ChartPoints.Haumea => 'í',
            ChartPoints.Orcus => 'ù',
            ChartPoints.Makemake => 'î',
            ChartPoints.Sedna => 'ö',
            ChartPoints.Hygieia => 'Á',
            ChartPoints.Astraea => 'Ã',
            ChartPoints.ApogeeMean => ',',
            ChartPoints.ApogeeCorrected => '.',
            ChartPoints.Dragon => 'è',
            ChartPoints.Beast => ';',
            ChartPoints.BlackSun => '[',
            ChartPoints.Diamond => ']',
            ChartPoints.Priapus => '\\',
            ChartPoints.PriapusCorrected => ':',
            ChartPoints.PersephoneCarteret => 'à',
            ChartPoints.VulcanusCarteret => 'Ï',
            ChartPoints.Ascendant => 'A',
            ChartPoints.Mc => 'M',
            ChartPoints.ZeroAries => '1',
            ChartPoints.FortunaSect => 'e',
            ChartPoints.FortunaNoSect => 'e',
            _ => EMPTY_GLYPH
        };
    }
}

