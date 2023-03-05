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
using Enigma.Facades.Se;


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

    public double CalcArmc(double jdUt, double obliquity, Location location)
    {
        int flags = EnigmaConstants.SEFLG_SWIEPH;
        double[][] houses = _housesCalc.CalculateHouses(jdUt, obliquity, location, 'W', flags);
        return houses[1][2];
    }

    /// <inheritdoc/>
    public Dictionary<ChartPoints, FullPointPos> CalcHouses(FullHousesPosRequest request)
    {
        HouseSystems houseSystem = request.calcPrefs.ActualHouseSystem;
        HouseSystemDetails houseDetails = houseSystem.GetDetails();
        char houseId4Se = houseDetails.SeId;
        int _flags = EnigmaConstants.SEFLG_SWIEPH;
        Location location = request.ChartLocation;
        double jdUt = request.JdUt;
        double[][] _eclValues;
        double[][] tropicalValues;
        Dictionary<ChartPoints, FullPointPos> mundanePositions = new();
        double obliquity = _obliquityHandler.CalcObliquity(new ObliquityRequest(request.JdUt, true));
        tropicalValues = _housesCalc.CalculateHouses(request.JdUt, obliquity, request.ChartLocation, houseId4Se, _flags);
        if (request.calcPrefs.ActualZodiacType == ZodiacTypes.Sidereal)
        {
            int idAyanamsa = request.calcPrefs.ActualAyanamsha.GetDetails().SeId;
            SeInitializer.SetAyanamsha(idAyanamsa);
            _eclValues = _housesCalc.CalculateHouses(request.JdUt, obliquity, request.ChartLocation, houseId4Se, _flags + EnigmaConstants.SEFLG_SIDEREAL);
        }
        else
        {
            _eclValues = tropicalValues;
        }

        

        KeyValuePair<ChartPoints, FullPointPos> asc = CreateFullChartPointPosForCusp(ChartPoints.Ascendant, tropicalValues[1][0], _eclValues[1][0], jdUt, obliquity, location);
        mundanePositions.Add(asc.Key, asc.Value);
        KeyValuePair<ChartPoints, FullPointPos> mc = CreateFullChartPointPosForCusp(ChartPoints.Mc, tropicalValues[1][1], _eclValues[1][1], jdUt, obliquity, location);
        mundanePositions.Add(mc.Key, mc.Value);
        if (request.calcPrefs.ActualChartPoints.Contains(ChartPoints.Vertex)) {
            KeyValuePair<ChartPoints, FullPointPos> vertex = CreateFullChartPointPosForCusp(ChartPoints.Vertex, tropicalValues[1][3], _eclValues[1][3], jdUt, obliquity, location);
            mundanePositions.Add(vertex.Key, vertex.Value);
        }
        if (request.calcPrefs.ActualChartPoints.Contains(ChartPoints.EastPoint))
        {
            KeyValuePair<ChartPoints, FullPointPos> eastPoint = CreateFullChartPointPosForCusp(ChartPoints.EastPoint, tropicalValues[1][4], _eclValues[1][4], jdUt, obliquity, location);
            mundanePositions.Add(eastPoint.Key, eastPoint.Value);
        }



        for (int n = 1; n < _eclValues[0].Length; n++)
        {
            int cuspIndex = 2000 + n;
            ChartPoints cusp = ChartPoints.None.PointForIndex(cuspIndex);
            KeyValuePair<ChartPoints, FullPointPos> cuspPos = CreateFullChartPointPosForCusp(cusp, tropicalValues[0][n], _eclValues[0][n], jdUt, obliquity, location);
            mundanePositions.Add(cuspPos.Key, cuspPos.Value);
        }
        return mundanePositions;
    }

    private KeyValuePair<ChartPoints, FullPointPos> CreateFullChartPointPosForCusp(ChartPoints point, double tropLongitude, double longitude, double jdUt, double obliquity, Location location)
    {
        double latitude = 0.0;
        double speed = 0.0;
        double distance = 0.0;
        EclipticCoordinates eclCoord = new(tropLongitude, latitude);
        EquatorialCoordinates eqCoord = CalcEquatorialCoordinates(eclCoord, obliquity);
        HorizontalCoordinates horCoord = CalcHorizontalCoordinates(jdUt, location, eqCoord);

        PosSpeed psLongitude = new(longitude, speed);
        PosSpeed psLatitude = new (latitude, speed);
        PosSpeed psRightAscension = new(eqCoord.RightAscension, speed);
        PosSpeed psDeclination = new (eqCoord.Declination, speed);
        PosSpeed psDistance = new(distance, speed);
        PosSpeed psAzimuth = new(horCoord.Azimuth, speed);
        PosSpeed psAltitude = new(horCoord.Altitude, speed);

        PointPosSpeeds ppsEcliptical = new(psLongitude, psLatitude, psDistance);
        PointPosSpeeds ppsEquatorial = new(psRightAscension, psDeclination, psDistance);
        PointPosSpeeds ppsHorizontal = new(psAzimuth, psAltitude, psDistance);
        FullPointPos fpPos = new(ppsEcliptical, ppsEquatorial, ppsHorizontal);
        return new KeyValuePair<ChartPoints, FullPointPos>(point, fpPos);
    }

    private EquatorialCoordinates CalcEquatorialCoordinates(EclipticCoordinates eclCoord, double obliquity)
    {
        CoordinateConversionRequest coordConvRequest = new(eclCoord, obliquity);
        return _coordinateConversionHandler.HandleConversion(coordConvRequest);
    }

    private HorizontalCoordinates CalcHorizontalCoordinates(double jdUt, Location location, EquatorialCoordinates equCoord)
    {
        HorizontalRequest horizontalRequest = new(jdUt, location, equCoord);
        return _horizontalHandler.CalcHorizontal(horizontalRequest);
    }


}


