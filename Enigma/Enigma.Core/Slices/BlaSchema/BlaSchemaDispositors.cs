// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;


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
