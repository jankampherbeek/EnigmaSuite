// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Facades.Se;

namespace Enigma.Core.Calc;

/// <summary>Starts the calculations for mundane positions and cusps.</summary>
public interface IHousesHandler
{
    /// <summary>Calculates all mundane positions.</summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Dictionary<ChartPoints, FullPointPos> CalcHouses(FullHousesPosRequest request);

    /// <summary>Calculate the right ascension of the MC.</summary>
    /// <param name="jdUt">Julian day.</param>
    /// <param name="obliquity">Obliquity.</param>
    /// <param name="location">Actual location.</param>
    /// <returns>The ramc in decimal degrees.</returns>
    public double CalcArmc(double jdUt, double obliquity, Location? location);
}

/// <inheritdoc/>
public sealed class HousesHandler(
    IHousesCalc housesCalc,
    IObliquityHandler obliquityHandler,
    IHorizontalHandler horizontalHandler,
    ICoordinateConversionHandler coordinateConversionHandler)
    : IHousesHandler
{
    public double CalcArmc(double jdUt, double obliquity, Location? location)
    {
        const int flags = EnigmaConstants.SEFLG_SWIEPH;
        var houses = housesCalc.CalculateHouses(jdUt, obliquity, location, 'W', flags);
        return houses[1][2];
    }

    /// <inheritdoc/>
    public Dictionary<ChartPoints, FullPointPos> CalcHouses(FullHousesPosRequest request)
    {
        var houseSystem = request.CalcPrefs.ActualHouseSystem;
        var houseDetails = houseSystem.GetDetails();
        var houseId4Se = houseDetails.SeId;
        const int flags = EnigmaConstants.SEFLG_SWIEPH;
        var location = request.ChartLocation;
        var jdUt = request.JdUt;
        double[][] eclValues;
        Dictionary<ChartPoints, FullPointPos> mundanePositions = new();
        
        var obliquity = obliquityHandler.CalcObliquity(new ObliquityRequest(request.JdUt, true));
        var tropicalValues = housesCalc.CalculateHouses(request.JdUt, obliquity, request.ChartLocation, houseId4Se, flags);
        if (request.CalcPrefs.ActualZodiacType == ZodiacTypes.Sidereal)
        {
            var idAyanamsa = request.CalcPrefs.ActualAyanamsha.GetDetails().SeId;
            SeInitializer.SetAyanamsha(idAyanamsa);
            eclValues = housesCalc.CalculateHouses(request.JdUt, obliquity, request.ChartLocation, houseId4Se, flags + EnigmaConstants.SEFLG_SIDEREAL);
        }
        else
        {
            eclValues = tropicalValues;
        }
        
        var asc = CreateFullChartPointPosForCusp(ChartPoints.Ascendant, tropicalValues[1][0], eclValues[1][0], jdUt, obliquity, location);
        mundanePositions.Add(asc.Key, asc.Value);
        var mc = CreateFullChartPointPosForCusp(ChartPoints.Mc, tropicalValues[1][1], eclValues[1][1], jdUt, obliquity, location);
        mundanePositions.Add(mc.Key, mc.Value);
        if (request.CalcPrefs.ActualChartPoints.Contains(ChartPoints.Vertex))
        {
            var vertex = CreateFullChartPointPosForCusp(ChartPoints.Vertex, tropicalValues[1][3], eclValues[1][3], jdUt, obliquity, location);
            mundanePositions.Add(vertex.Key, vertex.Value);
        }
        if (request.CalcPrefs.ActualChartPoints.Contains(ChartPoints.EastPoint))
        {
            var eastPoint = CreateFullChartPointPosForCusp(ChartPoints.EastPoint, tropicalValues[1][4], eclValues[1][4], jdUt, obliquity, location);
            mundanePositions.Add(eastPoint.Key, eastPoint.Value);
        }
        if (houseSystem != HouseSystems.NoHouses)
        {
            for (var n = 1; n < eclValues[0].Length; n++)
            {
                var cusp = PointsExtensions.PointForIndex(CalculationCats.Mundane, n);
                KeyValuePair<ChartPoints, FullPointPos> cuspPos = CreateFullChartPointPosForCusp(cusp, tropicalValues[0][n], eclValues[0][n], jdUt, obliquity, location);
                mundanePositions.Add(cuspPos.Key, cuspPos.Value);
            }            
        }
        return mundanePositions;
    }

    private KeyValuePair<ChartPoints, FullPointPos> CreateFullChartPointPosForCusp(ChartPoints point, double tropLongitude, double longitude, double jdUt, double obliquity, Location? location)
    {
        const double latitude = 0.0;
        const double speed = 0.0;
        const double distance = 0.0;
        EclipticCoordinates eclCoord = new(tropLongitude, latitude);
        var eqCoord = CalcEquatorialCoordinates(eclCoord, obliquity);
        var horCoord = CalcHorizontalCoordinates(jdUt, location, eqCoord);

        PosSpeed psLongitude = new(longitude, speed);
        PosSpeed psLatitude = new(latitude, speed);
        PosSpeed psRightAscension = new(eqCoord.RightAscension, speed);
        PosSpeed psDeclination = new(eqCoord.Declination, speed);
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
        return coordinateConversionHandler.HandleConversion(coordConvRequest);
    }

    private HorizontalCoordinates CalcHorizontalCoordinates(double jdUt, Location? location, EquatorialCoordinates equCoord)
    {
        HorizontalRequest horizontalRequest = new(jdUt, location, equCoord);
        return horizontalHandler.CalcHorizontal(horizontalRequest);
    }


}


