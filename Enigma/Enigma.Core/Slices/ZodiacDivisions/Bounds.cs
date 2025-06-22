// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Slices.ZodiacDivisions;

internal record TermPosition(int Degrees, int Planet);

/// <summary>
/// Define the planetary index for bounds (terms)
/// </summary>
public static class Bounds
{
    // Sun 0, Moon 1, Merc 2, Venus 3, Mars 4, Jupiter 5, Saturn 6
    
    
    private static readonly TermPosition[] Terms = new TermPosition[60];


    /// <summary>
    /// Define sign index for dodecatemoria
    /// </summary>
    /// <remarks>Prompt: Check the longitude, it should be equal or larger than 0 and smaller than 360.
    /// Return -1 if the longitude is out of bounds. Otherwise, calculate the index. To do so divide the longitude in
    /// 12 parts (signs of the zodiac). Each part gets a number, 0 for Aries up to 11 for Pisces.
    /// The index of the planet should be retrieved from the table TermPosition. Use the value of the planet and
    /// check what the smallest value for Degrees is in the record Termposition in this table, which is larger than
    /// the value for longitude. If the parameter 'egyptian' is true, the table should be filled wth the function
    /// DefineEgyptioanBounds, otherwise with the function DefinePtolemyBound.  
    /// </remarks>
    /// <param name="longitude">The ecliptic longitude which should be minimal 0.0 and smaller than 360.0</param>
    /// <param name="egyptian">True for Egyptian bounds, false for Ptolemy bounds</param>
    /// <returns>The planet index for the bound: Merc 2, Venus 3, Mars 4, Jupiter 5, Saturn 6 (Sun and Moon are not used).</returns>
    public static int IndexBound(double longitude, bool egyptian)
    {
        // Check if longitude is within valid bounds
        if (longitude < 0.0 || longitude >= 360.0)
        {
            return -1;
        }
        
        // Fill the Terms array based on the egyptian parameter
        if (egyptian)
        {
            DefineEgyptianBounds();
        }
        else
        {
            DefinePtolemyBounds();
        }
        
        // Find the smallest degree value that is larger than the longitude
        foreach (var t in Terms)
        {
            if (t.Degrees > longitude)
            {
                return t.Planet;
            }
        }
        
        // If no degree is found larger than longitude, return the last planet
        // This handles the case where longitude is exactly 360 or very close to it
        return Terms[^1].Planet;
    }
    

    private static void DefineEgyptianBounds()
    {
        // Aries
        Terms[0] = new TermPosition(6, 5);   
        Terms[1] = new TermPosition(12, 3); 
        Terms[2] = new TermPosition(20, 2); 
        Terms[3] = new TermPosition(25, 4); 
        Terms[4] = new TermPosition(30, 6); 
        // Taurus
        Terms[5] = new TermPosition(38, 3); 
        Terms[6] = new TermPosition(44, 2); 
        Terms[7] = new TermPosition(52, 5); 
        Terms[8] = new TermPosition(57, 6); 
        Terms[9] = new TermPosition(60, 4); 
        // Gemini
        Terms[10] = new TermPosition(66, 2); 
        Terms[11] = new TermPosition(72, 5); 
        Terms[12] = new TermPosition(77, 3); 
        Terms[13] = new TermPosition(84, 4); 
        Terms[14] = new TermPosition(90, 6); 
        // Cancer                                              
        Terms[15] = new TermPosition(97, 4);
        Terms[16] = new TermPosition(103, 3); 
        Terms[17] = new TermPosition(109, 2); 
        Terms[18] = new TermPosition(116, 5); 
        Terms[19] = new TermPosition(120, 6); 
        // Leo
        Terms[20] = new TermPosition(126, 5); 
        Terms[21] = new TermPosition(131, 3); 
        Terms[22] = new TermPosition(138, 6); 
        Terms[23] = new TermPosition(144, 2); 
        Terms[24] = new TermPosition(150, 4); 
        // Virgo
        Terms[25] = new TermPosition(157, 2); 
        Terms[26] = new TermPosition(167, 3); 
        Terms[27] = new TermPosition(171, 5); 
        Terms[28] = new TermPosition(178, 4); 
        Terms[29] = new TermPosition(180, 6); 
        // Libra                                                    
        Terms[30] = new TermPosition(186, 6); 
        Terms[31] = new TermPosition(194, 2); 
        Terms[32] = new TermPosition(201, 5); 
        Terms[33] = new TermPosition(208, 3); 
        Terms[34] = new TermPosition(210, 4);
        // Scorpio
        Terms[35] = new TermPosition(217, 4); 
        Terms[36] = new TermPosition(221, 3); 
        Terms[37] = new TermPosition(229, 2); 
        Terms[38] = new TermPosition(234, 5); 
        Terms[39] = new TermPosition(240, 6); 
        // Sagittarius
        Terms[40] = new TermPosition(252, 5); 
        Terms[41] = new TermPosition(257, 3); 
        Terms[42] = new TermPosition(261, 2); 
        Terms[43] = new TermPosition(266, 6); 
        Terms[44] = new TermPosition(270, 4);
        // Capricorn                                    
        Terms[45] = new TermPosition(277, 2); 
        Terms[46] = new TermPosition(284, 5); 
        Terms[47] = new TermPosition(292, 3); 
        Terms[48] = new TermPosition(296, 6); 
        Terms[49] = new TermPosition(300, 4);
        // Aquarius
        Terms[50] = new TermPosition(307, 2); 
        Terms[51] = new TermPosition(313, 3); 
        Terms[52] = new TermPosition(320, 5); 
        Terms[53] = new TermPosition(325, 4); 
        Terms[54] = new TermPosition(330, 6); 
        // Pisces
        Terms[55] = new TermPosition(342, 3); 
        Terms[56] = new TermPosition(346, 5); 
        Terms[57] = new TermPosition(349, 2); 
        Terms[58] = new TermPosition(358, 4); 
        Terms[59] = new TermPosition(360, 6); 
    }
    
  private static void DefinePtolemyBounds()
    {
                                                              
        // Aries
        Terms[0] = new TermPosition(6, 5); 
        Terms[1] = new TermPosition(14, 3); 
        Terms[2] = new TermPosition(21, 2); 
        Terms[3] = new TermPosition(26, 4); 
        Terms[4] = new TermPosition(30, 6); 
        // Taurus
        Terms[5] = new TermPosition(38, 3); 
        Terms[6] = new TermPosition(45, 2); 
        Terms[7] = new TermPosition(52, 5); 
        Terms[8] = new TermPosition(54, 6); 
        Terms[9] = new TermPosition(60, 4); 
        // Gemini
        Terms[10] = new TermPosition(67, 2); 
        Terms[11] = new TermPosition(73, 5); 
        Terms[12] = new TermPosition(80, 3); 
        Terms[13] = new TermPosition(86, 4); 
        Terms[14] = new TermPosition(90, 6); 
        // Cancer
        Terms[15] = new TermPosition(96, 4); 
        Terms[16] = new TermPosition(103, 5); 
        Terms[17] = new TermPosition(110, 2); 
        Terms[18] = new TermPosition(117, 3); 
        Terms[19] = new TermPosition(120, 6); 
        // Leo
        Terms[20] = new TermPosition(126, 5); 
        Terms[21] = new TermPosition(133, 2); 
        Terms[22] = new TermPosition(139, 6); 
        Terms[23] = new TermPosition(145, 3); 
        Terms[24] = new TermPosition(150, 4); 
        // Virgo                                                  
        Terms[25] = new TermPosition(157, 2); 
        Terms[26] = new TermPosition(163, 3); 
        Terms[27] = new TermPosition(168, 5); 
        Terms[28] = new TermPosition(174, 6); 
        Terms[29] = new TermPosition(180, 4); 
        // Libra
        Terms[30] = new TermPosition(186, 6); 
        Terms[31] = new TermPosition(191, 3); 
        Terms[32] = new TermPosition(196, 2); 
        Terms[33] = new TermPosition(204, 5); 
        Terms[34] = new TermPosition(210, 4);
        // Scorpio
        Terms[35] = new TermPosition(216, 4); 
        Terms[36] = new TermPosition(223, 3); 
        Terms[37] = new TermPosition(231, 5); 
        Terms[38] = new TermPosition(237, 2); 
        Terms[39] = new TermPosition(240, 6); 
        // Sagittarius                                                                
        Terms[40] = new TermPosition(248, 5); 
        Terms[41] = new TermPosition(254, 3); 
        Terms[42] = new TermPosition(259, 2); 
        Terms[43] = new TermPosition(265, 6); 
        Terms[44] = new TermPosition(270, 4);
        // Capricorn
        Terms[45] = new TermPosition(276, 3); 
        Terms[46] = new TermPosition(282, 2); 
        Terms[47] = new TermPosition(289, 5); 
        Terms[48] = new TermPosition(295, 6); 
        Terms[49] = new TermPosition(300, 4);
        // Aquarius
        Terms[50] = new TermPosition(306, 6);        
        Terms[51] = new TermPosition(312, 2); 
        Terms[52] = new TermPosition(320, 3); 
        Terms[53] = new TermPosition(325, 5); 
        Terms[54] = new TermPosition(330, 4); 
        // Pisces
        Terms[55] = new TermPosition(338, 3); 
        Terms[56] = new TermPosition(344, 5); 
        Terms[57] = new TermPosition(350, 2); 
        Terms[58] = new TermPosition(355, 4); 
        Terms[59] = new TermPosition(360, 6); 
    }
  
    
}