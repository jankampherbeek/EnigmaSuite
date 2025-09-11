
// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;

/// <summary>
/// Position in a quadrants
/// </summary>
public static class QuadrantPositions
{

    public static Dictionary<int, int> DefineQuadrants(List<BlaHouseDetails> houseDetails)
    {
        var quadrantCounts = new Dictionary<int, int>
        {
            { 1, 0 },
            { 2, 0 },
            { 3, 0 },
            { 4, 0 }
        };
        foreach (var houseDetail in houseDetails)
        {
            switch (houseDetail.HouseNr)
            {
                case 1 or 2 or 3:
                    quadrantCounts[1] += houseDetail.PointsInHouse.Count;
                    break;
                case 4 or 5 or 6:
                    quadrantCounts[2] += houseDetail.PointsInHouse.Count;
                    break;
                case 7 or 8 or 9:
                    quadrantCounts[3] += houseDetail.PointsInHouse.Count;
                    break;
                case 10 or 11 or 12:
                    quadrantCounts[4] += houseDetail.PointsInHouse.Count;
                    break;
            }
        }
        return quadrantCounts;
    }
}