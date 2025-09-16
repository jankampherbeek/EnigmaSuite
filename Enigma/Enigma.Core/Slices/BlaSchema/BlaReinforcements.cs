// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;

public static class BlaReinforcements
{
    public static Reinforcements CreateReinforcements(Dictionary<ChartPoints, int> planetsInSigns,
        Dictionary<ChartPoints, int> planetsInHouses)
    {
        var pointInOwnSign = ReinforcementCalc.FindPointsInOwnSign(planetsInSigns);
        var pointsInOwnHouse = new Dictionary<ChartPoints, int>();
        var pointsInOwnMundaneHouse = new Dictionary<ChartPoints, int>();
        
        return new Reinforcements(pointInOwnSign, pointsInOwnHouse, pointsInOwnMundaneHouse);

    }

    

    
}