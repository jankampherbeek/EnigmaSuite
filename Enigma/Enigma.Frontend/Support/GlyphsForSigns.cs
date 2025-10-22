// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Frontend.Ui.Support;

/// <summary>Returns default glyph for a given zodiac sign</summary>
/// <remarks>A temporary solution, ultimately the glyphs should be retrieved from the current configuration.</remarks>
public sealed class GlyphsForSigns
{
    private const char EMPTY_GLYPH = ' ';

    public static char FindGlyph(int sign)
    {
        return sign switch
        {
            1 => '1',
            2 => '2',
            3 => '3',
            4 => '4',
            5 => '5',
            6 => '6',
            7 => '7',
            8 => '8',
            9 => '9',
            10 => '0',
            11 => '-',
            12 => '=',
            _ => EMPTY_GLYPH
        };
    }
}