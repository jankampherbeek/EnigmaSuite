// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Handlers.Interfaces;


/// <summary>Starts the calculations for mundane positions and cusps.</summary>
public interface IHousesHandler
{
    /// <summary>Calculates all mundane positions.</summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public FullHousesPositions CalcHouses(FullHousesPosRequest request);
}


/// <summary>Calculations for houses and other mundane positions.</summary>
public interface IHousesCalc
{
    /// <summary>Calculate longitude for houses and other mundane positions.</summary>
    /// <param name="julianDayUt">Julian Day for UT.</param>
    /// <param name="obliquity"/>
    /// <param name="location"/>
    /// <param name="houseSystemId">Id for a housesystem as used by the SE.</param>
    /// <param name="flags"/>
    /// <returns>The calculated positions for the houses and other mundane points.</returns>
    public double[][] CalculateHouses(double julianDayUt, double obliquity, Location location, char houseSystemId, int flags);
}