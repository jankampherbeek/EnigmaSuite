// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;





//
// public class XBlaPositionForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions)
//
// {
//     private readonly BlaPositionsFactory _blaPositionsFactory = new();
//     private readonly HousePositions _housePositions = new();
//
//     public List<PresentableBlaPosition> CreateBlaPositionsForDataGrid(BlaChartDetails blaChartDetails)
//     {
//         List<PresentableBlaPosition> presentableBlaPositions = new();
//
//         foreach (var pos in blaChartDetails.SignsDecansHouses)
//         {
//             if (pos.Point.GetDetails().PointCat == PointCats.Common ||
//                 pos.Point.GetDetails().PointCat == PointCats.Angle)
//             {
//                 var pointGlyph = GlyphsForChartPoints.FindGlyph(pos.Point);
//                 presentableBlaPositions.Add(CreatePresBlaPosition(pointGlyph, pos.Longitude, pos.Decan, pos.House));
//             }
//         }
//
//         return presentableBlaPositions;
//     }
//
//     private PresentableBlaPosition CreatePresBlaPosition(char pointGlyph, double position, int decan, int houseNr)
//     {
//         var (longTxt, glyph) = doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(position);
//         var houseTxt = GetHouseInRomanNumerals(houseNr);
//         var decanateGlyph = GetDecanateGlyph(decan);
//         return new PresentableBlaPosition(pointGlyph, longTxt, glyph, houseTxt, decanateGlyph);
//     }
//
//     private string GetHouseInRomanNumerals(int house)
//     {
//         return house switch
//         {
//             1 => "I",
//             2 => "II",
//             3 => "III",
//             4 => "IV",
//             5 => "V",
//             6 => "VI",
//             7 => "VII",
//             8 => "VIII",
//             9 => "IX",
//             10 => "X",
//             11 => "XI",
//             12 => "XII"
//         };
//     }
//
//     private char GetDecanateGlyph(int decan)
//     {
//         return decan switch
//         {
//             1 => 'f', // Mars
//             2 => 'a', // Sun
//             3 => 'd', // Venus
//             4 => 'c', // Mercury
//             5 => 'b', // Moon
//             6 => 'h', // Saturn
//             7 => 'g', // Jupiter
//             _ => '?'
//         };
//     }
//}








public record PresentableDecanCount(string Decan, int Count);

public class BlaPresDecanCountFactory()
{
    public List<PresentableDecanCount> CreatePresDecans(BlaChartDetails blaChartDetails)
    {
        var counts = new int[7];
        foreach (var sdh in blaChartDetails.SignsDecansHouses)
        {
            
            counts[sdh.Decan - 1]++;
        }
        var presDecans = new List<PresentableDecanCount>
        {
            new PresentableDecanCount(FindGlyphForDecan(1),counts[0]),
            new PresentableDecanCount(FindGlyphForDecan(2),counts[1]),
            new PresentableDecanCount(FindGlyphForDecan(3),counts[2]),
            new PresentableDecanCount(FindGlyphForDecan(4),counts[3]),
            new PresentableDecanCount(FindGlyphForDecan(5),counts[4]),
            new PresentableDecanCount(FindGlyphForDecan(6),counts[5]),
            new PresentableDecanCount(FindGlyphForDecan(7),counts[6])
        };
        return presDecans;   
    }
    
    private string FindGlyphForDecan(int decan)
    {
        switch (decan)
        {
            case 1: return "f";  // Mars
            case 2: return "a";  // Sun
            case 3: return "d";  // Venus
            case 4: return "c";  // Mercury
            case 5: return "b";  // Moon
            case 6: return "h";  // Saturn
            case 7: return "g";  // Jupiter
            default: return " ";

        }
    }
}
