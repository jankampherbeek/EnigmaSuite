﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Facades.Se.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;

namespace Enigma.Core.Handlers.Calc.Mundane.Helpers;

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
