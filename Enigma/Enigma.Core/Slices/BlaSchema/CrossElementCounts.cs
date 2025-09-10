// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Slices.BlaSchema;


/// <summary>
/// Counts for crosses and elements in signs and houses
/// </summary>
public static class CrossElementCounts
{
    /// <summary>
    /// Create the counts for crosses and elements
    /// </summary>
    /// <param name="signCounts">Counts for signs</param>
    /// <param name="houseCounts">Counts for houses</param>
    /// <param name="signsOnCusps">Signs on housecusps</param>
    /// <returns>Two dictionaries, the first for crosses, the second for elements.Dictionary with for the cusp (1..12) and for the sign (1..12)</returns>
    public static (Dictionary<int, BlaSignHouseCountLine>, Dictionary<int, BlaSignHouseCountLine>) CreateCrossesElementsCounts(
        Dictionary<int, int> signCounts,
        Dictionary<int, int> houseCounts,
        Dictionary<int, int> signsOnCusps)
    {
        var allLines = CreateAllLines(signCounts, houseCounts, signsOnCusps);
        var crossLines = CreateLinesForCrosses(allLines);
        var elementLines = CreateLinesForElements(allLines);
        return (crossLines, elementLines);
    }

    private static Dictionary<int, BlaSignHouseCountLine> CreateAllLines(        
        Dictionary<int, int> signCounts,
        Dictionary<int, int> houseCounts,
        Dictionary<int, int> signsOnCusps)
    {
        var countLines = new Dictionary<int, BlaSignHouseCountLine>();
        foreach (var (signIndex, signCount) in signCounts)
        {
            var houseCount = houseCounts[signIndex];
            var sum = signCount + houseCount;
            var hCusp = 0;
            foreach (var soc in signsOnCusps)
            {
                if (soc.Key == signIndex) // one sign can rule multiple houses
                {
                    hCusp += soc.Value;
                }
            }
            var total = sum + hCusp;
            var shCountLine = new BlaSignHouseCountLine(signCount, houseCount, sum, hCusp, total);
            countLines.Add(signIndex, shCountLine);
        }
        return countLines;
    }

    private static Dictionary<int, BlaSignHouseCountLine> CreateLinesForElements(Dictionary<int, BlaSignHouseCountLine> allCounts)
    {
        var fireLines = new List<BlaSignHouseCountLine>();
        var earthLines = new List<BlaSignHouseCountLine>();
        var airLines = new List<BlaSignHouseCountLine>();
        var waterLines = new List<BlaSignHouseCountLine>();
        foreach (var line in allCounts)
        {
            if (line.Key is 1 or 5 or 9) fireLines.Add(line.Value);
            if (line.Key is 2 or 6 or 10) earthLines.Add(line.Value);
            if (line.Key is 3 or 7 or 11) airLines.Add(line.Value);
            if (line.Key is 4 or 8 or 12) waterLines.Add(line.Value);
        }

        var elementLines = new Dictionary<int, BlaSignHouseCountLine>()
        {
            { 1, CreateLineWithTotals(fireLines) },
            { 2, CreateLineWithTotals(earthLines) },
            { 3, CreateLineWithTotals(airLines) },
            { 4, CreateLineWithTotals(waterLines) }
        };
        return elementLines;
    }

    private static Dictionary<int, BlaSignHouseCountLine> CreateLinesForCrosses(Dictionary<int, BlaSignHouseCountLine> allCounts)
    {
        var cardinalLines = new List<BlaSignHouseCountLine>();
        var fixedLines = new List<BlaSignHouseCountLine>();
        var mutableLines = new List<BlaSignHouseCountLine>();
        foreach (var line in allCounts)
        {
            if (line.Key is 1 or 4 or 7 or 10) cardinalLines.Add(line.Value);
            if (line.Key is 2 or 5 or 8 or 11) fixedLines.Add(line.Value);
            if (line.Key is 3 or 6 or 9 or 12) mutableLines.Add(line.Value);
        }

        var crossLines = new Dictionary<int, BlaSignHouseCountLine>()
        {
            { 1, CreateLineWithTotals(cardinalLines) },
            { 2, CreateLineWithTotals(fixedLines) },
            { 3, CreateLineWithTotals(mutableLines) }
        };
        return crossLines;
    }
    
    
    private static BlaSignHouseCountLine CreateLineWithTotals(List<BlaSignHouseCountLine> lines)
    {
        var signCount = 0;
        var houseCount = 0;
        var hCuspCount = 0;
        foreach (var line in lines)
        {
            signCount += line.Sign;
            houseCount += line.House;
            hCuspCount += line.HCusp;
        }

        var sum = signCount + houseCount;
        var total = sum + hCuspCount;
        var totalLine = new BlaSignHouseCountLine(signCount, houseCount, sum, hCuspCount, total);
        return totalLine;

    }
    
}