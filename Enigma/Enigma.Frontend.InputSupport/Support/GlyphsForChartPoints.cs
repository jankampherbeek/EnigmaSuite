// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Points;

namespace Enigma.Frontend.Helpers.Support;

/// <summary>Returns default glyph for a given chart point.</summary>
/// <remarks>A temporary solution, ultimately the glyphs should be retrieved from the current configuration.</remarks>
public sealed class GlyphsForChartPoints            // TODO 0.3 Replace this solution with a configuration based solution.
{
    private readonly char _emptyGlyph = ' ';

    public char FindGlyph(ChartPoints point)
    {
        switch (point)
        {
            case ChartPoints.None:
                return _emptyGlyph;
            case ChartPoints.Sun:
                return 'a';
            case ChartPoints.Moon:
                return 'b';
            case ChartPoints.Mercury:
                return 'c';
            case ChartPoints.Venus:
                return 'd';
            case ChartPoints.Earth:
                return 'e';
            case ChartPoints.Mars:
                return 'f';
            case ChartPoints.Jupiter:
                return 'g';
            case ChartPoints.Saturn:
                return 'h';
            case ChartPoints.Uranus:
                return 'i';
            case ChartPoints.Neptune:
                return 'j';
            case ChartPoints.Pluto:
                return 'k';
            case ChartPoints.MeanNode:
                return '{';
            case ChartPoints.TrueNode:
                return '{';
            case ChartPoints.Chiron:
                return 'w';
            case ChartPoints.PersephoneRam:
                return '/';
            case ChartPoints.HermesRam:
                return '<';
            case ChartPoints.DemeterRam:
                return '>';
            case ChartPoints.CupidoUra:
                return 'y';
            case ChartPoints.HadesUra:
                return 'z';
            case ChartPoints.ZeusUra:
                return '!';
            case ChartPoints.KronosUra:
                return '@';
            case ChartPoints.ApollonUra:
                return '#';
            case ChartPoints.AdmetosUra:
                return '$';
            case ChartPoints.VulcanusUra:
                return '%';
            case ChartPoints.PoseidonUra:
                return '&';
            case ChartPoints.Eris:
                return '*';
            case ChartPoints.Pholus:
                return ')';
            case ChartPoints.Ceres:
                return '_';
            case ChartPoints.Pallas:
                return 'û';
            case ChartPoints.Juno:
                return 'ü';
            case ChartPoints.Vesta:
                return 'À';
            case ChartPoints.Isis:
                return 'â';
            case ChartPoints.Nessus:
                return '(';
            case ChartPoints.Huya:
                return 'ï';
            case ChartPoints.Varuna:
                return 'ò';
            case ChartPoints.Ixion:
                return 'ó';
            case ChartPoints.Quaoar:
                return 'ô';
            case ChartPoints.Haumea:
                return 'í';
            case ChartPoints.Orcus:
                return 'ù';
            case ChartPoints.Makemake:
                return 'î';
            case ChartPoints.Sedna:
                return 'ö';
            case ChartPoints.Hygieia:
                return 'Á';
            case ChartPoints.Astraea:
                return 'Ã';
            case ChartPoints.ApogeeMean:
                return ',';
            case ChartPoints.ApogeeCorrected:
                return '.';
            case ChartPoints.ApogeeInterpolated:
                return '.';
            case ChartPoints.ApogeeDuval:
                return '.';
            case ChartPoints.PersephoneCarteret:
                return 'à';
            case ChartPoints.VulcanusCarteret:
                return 'Ï';
            case ChartPoints.Ascendant:
                return 'A';
            case ChartPoints.Mc:
                return 'M';
            case ChartPoints.ZeroAries:
                return '1';
            case ChartPoints.ZeroCancer:
                return '4';
            case ChartPoints.FortunaSect:
                return 'e';
            case ChartPoints.FortunaNoSect:
                return 'e';
            default:
                return _emptyGlyph;
        }
    }
}

