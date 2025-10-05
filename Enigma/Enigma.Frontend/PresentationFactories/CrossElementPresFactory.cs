// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;

namespace Enigma.Frontend.Ui.PresentationFactories;

public class CrossElementPresFactory
{
    private int _cardinalSCount, _fixedSCount, _mutableSCount, _fireSCount, _earthSCount, _airSCount, _waterSCount;
    private int _cardinalHCount, _fixedHCount, _mutableHCount, _fireHCount, _earthHCount, _airHCount, _waterHCount;
    private int _cardinalCCount, _fixedCCount, _mutableCCount, _fireCCount, _earthCCount, _airCCount, _waterCCount;


    public List<PresentableCrossElementsCount> CreatePresCrossesCounts(Dictionary<int, int> signCounts,
        Dictionary<int, int> houseCounts,
        Dictionary<ChartPoints, int> planetsInHouses,
        Dictionary<int, int> signOnCusps)
    {
        ResetCounts();
        CreateBlaItemsForCrosses(signCounts, houseCounts, planetsInHouses, signOnCusps);
        List<PresentableCrossElementsCount> counts =
        [
            CreateSinglePresCrossElementsCount("Cardinal", _cardinalSCount, _cardinalHCount, _cardinalCCount),
            CreateSinglePresCrossElementsCount("Fixed", _fixedSCount, _fixedHCount, _fixedCCount),
            CreateSinglePresCrossElementsCount("Mutable", _mutableSCount, _mutableHCount, _mutableCCount),
        ];
        return counts;
    }


    
    public List<PresentableCrossElementsCount> CreatePresElementsCounts(Dictionary<int, int> signCounts,
        Dictionary<ChartPoints, int> planetsInHouses,
        Dictionary<int, int> signOnCusps)
    {
        ResetCounts();
        CreateBlaItemsForElements(signCounts, planetsInHouses, signOnCusps);
        List<PresentableCrossElementsCount> counts =
        [
            CreateSinglePresCrossElementsCount("Fire", _fireSCount, _fireHCount, _fireCCount),
            CreateSinglePresCrossElementsCount("Earth", _earthSCount, _earthHCount, _earthCCount),
            CreateSinglePresCrossElementsCount("Air", _airSCount, _airHCount, _airCCount),
            CreateSinglePresCrossElementsCount("Water", _waterSCount, _waterHCount, _waterCCount)
        ];
        return counts;
    }


    private void ResetCounts()
    {
        _cardinalSCount = _fixedSCount = _mutableSCount = _fireSCount = _earthSCount = _airSCount = _waterSCount = 0;
        _cardinalHCount = _fixedHCount = _mutableHCount = _fireHCount = _earthHCount = _airHCount = _waterHCount = 0;
        _cardinalCCount = _fixedCCount = _mutableCCount = _fireCCount = _earthCCount = _airCCount = _waterCCount = 0;
    }


    private void CreateBlaItemsForCrosses(Dictionary<int, int> signCounts,
        Dictionary<int, int> houseCounts,
        Dictionary<ChartPoints, int> planetsInHouses,
        Dictionary<int, int> signOnCusps)
    {
        foreach (var sign in signCounts)
        {
            switch (sign.Key)
            {
                case 1 or 4 or 7 or 10:
                    _cardinalSCount+= sign.Value;
                    break;
                case 2 or 5 or 8 or 11:
                    _fixedSCount+= sign.Value;
                    break;
                case 3 or 6 or 9 or 12:
                    _mutableSCount+= sign.Value;
                    break;
            }
        }

        foreach (var pos in planetsInHouses)
        {
            if (pos.Key.GetDetails().PointCat != PointCats.Angle)
            {
                switch (pos.Value)
                {
                    case 1 or 4 or 7 or 10:
                        _cardinalHCount++;
                        break;
                    case 2 or 5 or 8 or 11:
                        _fixedHCount++;
                        break;
                    case 3 or 6 or 9 or 12:
                        _mutableHCount++;
                        break;
                }
            }
        }

        foreach (var hrs in signOnCusps)
        {
            var count = 0;
            var sign = hrs.Value;
            foreach (var pih in houseCounts)
            {
                if (pih.Key == hrs.Key) count = pih.Value;
            }

            switch (sign)
            {
                case 1 or 4 or 7 or 10:
                    _cardinalCCount += count;
                    break;
                case 2 or 5 or 8 or 11:
                    _fixedCCount += count;
                    ;
                    break;
                case 3 or 6 or 9 or 12:
                    _mutableCCount += count;
                    break;
            }
        }
    }


    private void CreateBlaItemsForElements(Dictionary<int, int> signCounts,
        Dictionary<ChartPoints, int> planetsInHouses,
        Dictionary<int, int> signOnCusps)
    {
        foreach (var sign in signCounts)
        {
            switch (sign.Key)
            {
                case 1 or 5 or 9:
                    _fireSCount+= sign.Value;
                    break;
                case 2 or 6 or 10:
                    _earthSCount+= sign.Value;
                    break;
                case 3 or 7 or 11:
                    _airSCount+= sign.Value;
                    break;
                case 4 or 8 or 12:
                    _waterSCount+= sign.Value;
                    break;
            }
        }

        foreach (var pos in planetsInHouses)
        {
            if (pos.Key.GetDetails().PointCat != PointCats.Angle)
            {
                switch (pos.Value)
                {
                    case 1 or 5 or 9:
                        _fireHCount++;
                        break;
                    case 2 or 6 or 10:
                        _earthHCount++;
                        break;
                    case 3 or 7 or 11:
                        _airHCount++;
                        break;
                    case 4 or 8 or 12:
                        _waterHCount++;
                        break;
                }
            }
        }

        var pointsInHouses = CalcNrOfPointsInHouses(planetsInHouses);

        foreach (var hrs in signOnCusps)
        {
            var count = 0;
            var sign = hrs.Value;
            foreach (var pih in pointsInHouses)
            {
                if (pih.Key == hrs.Key) count = pih.Value;
            }

            switch (sign)
            {
                case 1 or 5 or 9:
                    _fireCCount += count;
                    break;
                case 2 or 6 or 10:
                    _earthCCount += count;
                    ;
                    break;
                case 3 or 7 or 11:
                    _airCCount += count;
                    break;
                case 4 or 8 or 12:
                    _waterCCount += count;
                    ;
                    break;
            }
        }
    }


    private static PresentableCrossElementsCount CreateSinglePresCrossElementsCount(string name, int sCount, int hCount,
        int hcusp)
    {
        var sum = sCount + hCount;
        var total = sum + hcusp;
        var count = new PresentableCrossElementsCount(name, sCount, hCount, sum, hcusp, total);
        return count;
    }

    private static Dictionary<int, int> CalcNrOfPointsInHouses(Dictionary<ChartPoints, int> planetsInHouse)
    {
        var counts = new Dictionary<int, int>
        {
            { 1, 0 },
            { 2, 0 },
            { 3, 0 },
            { 4, 0 },
            { 5, 0 },
            { 6, 0 },
            { 7, 0 },
            { 8, 0 },
            { 9, 0 },
            { 10, 0 },
            { 11, 0 },
            { 12, 0 }
        };

        foreach (var point in planetsInHouse)
        {
            if (point.Key.GetDetails().PointCat == PointCats.Angle) continue;
            var houseNr = point.Value;
            counts[houseNr]++;
        }

        return counts;
    }
}