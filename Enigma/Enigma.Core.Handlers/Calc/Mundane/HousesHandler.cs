// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Work.Calc.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Constants;
using Enigma.Domain.Enums;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Points;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Calc;

public class HousesHandler : IHousesHandler
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



    public FullHousesPosResponse CalcHouses(FullHousesPosRequest request)
    {
        string errorText = "";
        bool success = true;
        HouseSystems houseSystem = request.HouseSystem;
        HouseSystemDetails houseDetails = houseSystem.GetDetails();
        char houseId4Se = houseDetails.SeId;
        int _flags = EnigmaConstants.SEFLG_SWIEPH;
        Location location = request.ChartLocation;
        double jdUt = request.JdUt;
        FullHousesPositions? fullHousesPos = null;
        try
        {
            double[][] _eclValues;
            List<CuspFullPos> allCusps = new();
            ObliquityResponse oblResponse = _obliquityHandler.CalcObliquity(new ObliquityRequest(request.JdUt));
            double obliquity = oblResponse.ObliquityTrue;
            _eclValues = _housesCalc.CalculateHouses(request.JdUt, obliquity, request.ChartLocation, houseId4Se, _flags);
            for (int n = 1; n < _eclValues[0].Length; n++)
            {
                allCusps.Add(CreateCuspFullPos("Cusp " + n, _eclValues[0][n], jdUt, obliquity, location));
            }
            CuspFullPos ascendant = CreateCuspFullPos(MundanePoints.Ascendant.ToString(), _eclValues[1][0], jdUt, obliquity, location);
            CuspFullPos mc = CreateCuspFullPos(MundanePoints.Mc.ToString(), _eclValues[1][1], jdUt, obliquity, location);
            CuspFullPos vertex = CreateCuspFullPos(MundanePoints.Vertex.ToString(), _eclValues[1][2], jdUt, obliquity, location);
            CuspFullPos eastPoint = CreateCuspFullPos(MundanePoints.EastPoint.ToString(), _eclValues[1][4], jdUt, obliquity, location);
            fullHousesPos = new(allCusps, mc, ascendant, vertex, eastPoint);
        }
        catch (SwissEphException see)
        {
            errorText = see.Message;
            success = false;
        }
        return new FullHousesPosResponse(fullHousesPos, success, errorText);
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
        CoordinateConversionResponse coordConvResponse = _coordinateConversionHandler.HandleConversion(coordConvRequest);
        return coordConvResponse.EquatorialCoord;
    }

    private HorizontalCoordinates CalcHorizontalCoordinates(double jdUt, Location location, EclipticCoordinates eclCoord)
    {
        HorizontalRequest horizontalRequest = new(jdUt, location, eclCoord);
        HorizontalResponse horizontalResponse = _horizontalHandler.CalcHorizontal(horizontalRequest);
        return horizontalResponse.HorizontalAzimuthAltitude;
    }


}


