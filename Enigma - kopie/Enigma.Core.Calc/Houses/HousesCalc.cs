// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Calc.SeFacades;
using Enigma.Domain.CalcVars;
using Enigma.Domain.Locational;

namespace Enigma.Core.Calc.Houses;

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


public class HousesCalc : IHousesCalc
{
    private readonly IHousesFacade _seHousesFacade;
    private readonly IHouseSystemSpecifications _houseSystemSpecifications;

    public HousesCalc(IHousesFacade seHousesFacade, IHouseSystemSpecifications houseSystemSpecs)
    {
        _seHousesFacade = seHousesFacade;
        _houseSystemSpecifications = houseSystemSpecs;
    }

    public double[][] CalculateHouses(double julianDayUt, double obliquity, Location location, char houseSystemId, int flags)
    {
        return _seHousesFacade.RetrieveHouses(julianDayUt, flags, location.GeoLat, location.GeoLong, houseSystemId);
    }
}

