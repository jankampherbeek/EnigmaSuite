// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;

/// <summary>
/// Handle the construction of all refinforcement items
/// </summary>
public static class BlaReinforcements
{
    public static Reinforcements CreateReinforcements(Dictionary<ChartPoints, int> planetsInSigns,
        Dictionary<ChartPoints, int> planetsInHouses,
        Dictionary<int, int> signsOnCusps)
    {
        var pointInOwnSign = ReinforcementCalc.FindPointsInOwnSign(planetsInSigns);
        var pointsInOwnHouse = ReinforcementCalc.FindPointsInOwnHouses(planetsInHouses, signsOnCusps);
        var pointsInOwnMundaneHouse = ReinforcementCalc.FindPointsInMundaneHouses(planetsInHouses);
        
        return new Reinforcements(pointInOwnSign, pointsInOwnHouse, pointsInOwnMundaneHouse);

    }
}