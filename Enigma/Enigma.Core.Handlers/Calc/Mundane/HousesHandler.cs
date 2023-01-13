// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.ChartItems.Coordinates;
using Enigma.Domain.Calc.Specials;
using Enigma.Domain.Constants;
using Enigma.Domain.Points;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Calc;

/// <inheritdoc/>
public sealed class HousesHandler : IHousesHandler
{
    private readonly IHousesCalc _housesCalc;
    private readonly IObliquityHandler _obliquityHandler;
    private readonly IHorizontalHandler _horizontalHandler;
    private readonly ICoordinateConversionHandler _coordinateConversionHandler;

    public HousesHandler(IHousesCalc housesCalc, IObliquityHandler obliquityHandler, IHorizontalHandler horizontalHandler, ICoordinateConversionHandler coordinateConversionHandler)
    {
        _housesCalc = housesCalc;
        _obliquityHandler = obliquityHandler;
        _horizontalHandler = horizontalHandler;
        _coordinateConversionHandler = coordinateConversionHandler;
    }


    /// <inheritdoc/>
    public FullHousesPositions CalcHouses(FullHousesPosRequest request)
    {
        HouseSystems houseSystem = request.HouseSystem;
        HouseSystemDetails houseDetails = houseSystem.GetDetails();
        char houseId4Se = houseDetails.SeId;
        int _flags = EnigmaConstants.SEFLG_SWIEPH;
        Location location = request.ChartLocation;
        double jdUt = request.JdUt;
        double[][] _eclValues;
        List<CuspFullPos> allCusps = new();
        double obliquity = _obliquityHandler.CalcObliquity(new ObliquityRequest(request.JdUt, true));
        _eclValues = _housesCalc.CalculateHouses(request.JdUt, obliquity, request.ChartLocation, houseId4Se, _flags);
        for (int n = 1; n < _eclValues[0].Length; n++)
        {
            allCusps.Add(CreateCuspFullPos("Cusp " + n, _eclValues[0][n], jdUt, obliquity, location));
        }
        CuspFullPos ascendant = CreateCuspFullPos(ChartPoints.Ascendant.ToString(), _eclValues[1][0], jdUt, obliquity, location);
        CuspFullPos mc = CreateCuspFullPos(ChartPoints.Mc.ToString(), _eclValues[1][1], jdUt, obliquity, location);
        CuspFullPos vertex = CreateCuspFullPos(ChartPoints.Vertex.ToString(), _eclValues[1][2], jdUt, obliquity, location);
        CuspFullPos eastPoint = CreateCuspFullPos(ChartPoints.EastPoint.ToString(), _eclValues[1][4], jdUt, obliquity, location);
        return new FullHousesPositions(allCusps, mc, ascendant, vertex, eastPoint);
    }

    private CuspFullPos CreateCuspFullPos(string name, double longitude, double jdUt, double obliquity, Location location)
    {
        double latitude = 0.0;
        EclipticCoordinates eclCoord = new(longitude, latitude);
        EquatorialCoordinates eqCoord = CalcEquatorialCoordinates(eclCoord, obliquity);
        HorizontalCoordinates horCoord = CalcHorizontalCoordinates(jdUt, location, eclCoord);
        return new CuspFullPos(name, eclCoord.Longitude, eqCoord, horCoord);

    }

    private EquatorialCoordinates CalcEquatorialCoordinates(EclipticCoordinates eclCoord, double obliquity)
    {
        CoordinateConversionRequest coordConvRequest = new(eclCoord, obliquity);
        return _coordinateConversionHandler.HandleConversion(coordConvRequest);
    }

    private HorizontalCoordinates CalcHorizontalCoordinates(double jdUt, Location location, EclipticCoordinates eclCoord)
    {
        HorizontalRequest horizontalRequest = new(jdUt, location, eclCoord);
        return _horizontalHandler.CalcHorizontal(horizontalRequest);
    }


}


