// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using domain.shared;
using E4C.Core.Astron.CoordinateConversion;
using E4C.Core.Astron.Horizontal;
using E4C.Core.Astron.Obliquity;
using E4C.Core.Shared.Domain;
using E4C.domain.shared.specifications;
using E4C.Exceptions;
using E4C.Shared.References;
using E4C.Shared.ReqResp;
using System.Collections.Generic;

namespace E4C.Core.Astron.Houses;

public interface IHousesHandler
{
    public FullHousesPosResponse CalcHouses(FullHousesPosRequest request);
}


public class HousesHandler : IHousesHandler
{
    private readonly IHousesCalc _housesCalc;
    private readonly IHouseSystemSpecs _houseSystemSpecs;
    private readonly IObliquityHandler _obliquityHandler;
    private readonly IHorizontalHandler _horizontalHandler;
    private readonly ICoordinateConversionHandler _coordinateConversionHandler;

    public HousesHandler(IHousesCalc housesCalc, IHouseSystemSpecs houseSystemSpecs, IObliquityHandler obliquityHandler, IHorizontalHandler horizontalHandler, ICoordinateConversionHandler coordinateConversionHandler)
    {
        _housesCalc = housesCalc;
        _houseSystemSpecs = houseSystemSpecs;
        _obliquityHandler = obliquityHandler;
        _horizontalHandler = horizontalHandler;
        _coordinateConversionHandler = coordinateConversionHandler;
    }



    public FullHousesPosResponse CalcHouses(FullHousesPosRequest request)
    {
        string errorText = "";
        bool success = true;
        HouseSystems houseSystem = request.HouseSystem;
        HouseSystemDetails houseDetails = _houseSystemSpecs.DetailsForHouseSystem(houseSystem);
        char houseId4Se = houseDetails.SeId;
        int _flags = Constants.SEFLG_SWIEPH;
        Location location = request.ChartLocation;
        double jdUt = request.JdUt;
        FullHousesPositions? fullHousesPos = null;
        try
        {
            double[][] _eclValues;
            List<CuspFullPos> allCusps = new();
            ObliquityResponse oblResponse = _obliquityHandler.CalcObliquity(new ObliquityRequest(request.JdUt, true));
            double obliquity = oblResponse.Obliquity;
            _eclValues = _housesCalc.CalculateHouses(request.JdUt, obliquity, request.ChartLocation, houseId4Se, _flags);
            for (int n = 1; n < _eclValues.Length; n++)
            {
                allCusps.Add(CreateCuspFullPos(_eclValues[0][n], jdUt, obliquity, location));
            }
            CuspFullPos ascendant = CreateCuspFullPos(_eclValues[1][0], jdUt, obliquity, location);
            CuspFullPos mc = CreateCuspFullPos(_eclValues[1][1], jdUt, obliquity, location);
            CuspFullPos vertex = CreateCuspFullPos(_eclValues[1][2], jdUt, obliquity, location);
            CuspFullPos eastPoint = CreateCuspFullPos(_eclValues[1][3], jdUt, obliquity, location);
            fullHousesPos = new(allCusps, mc, ascendant, vertex, eastPoint);
        }
        catch (SwissEphException see)
        {
            errorText = see.Message;
            success = false;
        }
        return new FullHousesPosResponse(fullHousesPos, success, errorText);
    }

    private CuspFullPos CreateCuspFullPos(double longitude, double jdUt, double obliquity, Location location)
    {
        double latitude = 0.0;
        EclipticCoordinates eclCoord = new(longitude, latitude);
        EquatorialCoordinates eqCoord = CalcEquatorialCoordinates(eclCoord, obliquity);
        HorizontalCoordinates horCoord = CalcHorizontalCoordinates(jdUt, location, eclCoord);
        return new CuspFullPos(eclCoord.Longitude, eqCoord, horCoord);

    }

    private EquatorialCoordinates CalcEquatorialCoordinates(EclipticCoordinates eclCoord, double obliquity)
    {
        CoordinateConversionRequest coordConvRequest = new CoordinateConversionRequest(eclCoord, obliquity);
        CoordinateConversionResponse coordConvResponse = _coordinateConversionHandler.HandleConversion(coordConvRequest);
        return coordConvResponse.equatorialCoord;
    }

    private HorizontalCoordinates CalcHorizontalCoordinates(double jdUt, Location location, EclipticCoordinates eclCoord)
    {
        HorizontalRequest horizontalRequest = new HorizontalRequest(jdUt, location, eclCoord);
        HorizontalResponse horizontalResponse = _horizontalHandler.CalcHorizontal(horizontalRequest);
        return horizontalResponse.HorizontalAzimuthAltitude;
    }


}


