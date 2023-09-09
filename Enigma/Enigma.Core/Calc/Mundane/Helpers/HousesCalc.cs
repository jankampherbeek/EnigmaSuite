// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Facades.Se.Interfaces;

namespace Enigma.Core.Calc.Mundane.Helpers;

/// <inheritdoc/>
public sealed class HousesCalc : IHousesCalc
{
    private readonly IHousesFacade _seHousesFacade;

    public HousesCalc(IHousesFacade seHousesFacade)
    {
        _seHousesFacade = seHousesFacade;
    }

    /// <inheritdoc/>
    public double[][] CalculateHouses(double julianDayUt, double obliquity, Location location, char houseSystemId, int flags)
    {
        return _seHousesFacade.RetrieveHouses(julianDayUt, flags, location.GeoLat, location.GeoLong, houseSystemId);
    }
}

