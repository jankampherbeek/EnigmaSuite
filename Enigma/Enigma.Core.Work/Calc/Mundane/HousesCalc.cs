// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Facades.Se.Interfaces;
using Enigma.Core.Work.Calc.Interfaces;
using Enigma.Domain.AstronCalculations;


namespace Enigma.Api.Calc;

public class HousesCalc : IHousesCalc
{
    private readonly IHousesFacade _seHousesFacade;

    public HousesCalc(IHousesFacade seHousesFacade)
    {
        _seHousesFacade = seHousesFacade;
    }

    public double[][] CalculateHouses(double julianDayUt, double obliquity, Location location, char houseSystemId, int flags)
    {
        return _seHousesFacade.RetrieveHouses(julianDayUt, flags, location.GeoLat, location.GeoLong, houseSystemId);
    }
}

