// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;

namespace Enigma.Core.Calc;

/// <summary>Calculator for Hellenistic lots.</summary>
public interface ILotsCalculator
{
    public Dictionary<ChartPoints, FullPointPos> CalculateAllLots(Dictionary<ChartPoints, FullPointPos> commonPositions, Dictionary<ChartPoints, FullPointPos> mundanePositions, CalculationPreferences prefs,
        double jdUt, double obliquity, Location? location);
}

public class LotsCalculator : ILotsCalculator
{
    private readonly IHousesHandler _housesHandler;
    private readonly IFullPointPosFactory _fullPointPosFactory;
    private readonly IHorizontalHandler _horizontalHandler;
    private readonly ICoordinateConversionCalc _coordinateConversionCalc;

    public LotsCalculator(IHousesHandler housesHandler, IFullPointPosFactory fullPointPosFactory, IHorizontalHandler horizontalHandler, ICoordinateConversionCalc coordinateConversionCalc)
    {
        _housesHandler = housesHandler;
        _fullPointPosFactory = fullPointPosFactory;
        _horizontalHandler = horizontalHandler;
        _coordinateConversionCalc = coordinateConversionCalc;
    }

    public Dictionary<ChartPoints, FullPointPos> CalculateAllLots(Dictionary<ChartPoints, FullPointPos> commonPositions, Dictionary<ChartPoints, FullPointPos> mundanePositions, CalculationPreferences prefs,
        double jdUt, double obliquity, Location? location)
    {
        List<ChartPoints> allPoints = prefs.ActualChartPoints;
        return allPoints.Where(point => point.GetDetails().CalculationCat == CalculationCats.Lots).ToDictionary(point => point, 
            point => CalculateLotPosition(point, jdUt, obliquity, location, prefs, commonPositions, mundanePositions));
    }

    private FullPointPos CalculateLotPosition(ChartPoints lot, double jdUt, double obliquity, Location? location, CalculationPreferences calcPrefs,
        IReadOnlyDictionary<ChartPoints, FullPointPos> commonPoints, IReadOnlyDictionary<ChartPoints, FullPointPos> mundanePoints)
    {
        double lotLongitude = 0.0;
        double ascendant = mundanePoints[ChartPoints.Ascendant].Ecliptical.MainPosSpeed.Position;
        double sun = commonPoints[ChartPoints.Sun].Ecliptical.MainPosSpeed.Position;
        bool dayLight = commonPoints[ChartPoints.Sun].Horizontal.DeviationPosSpeed.Position >= 0.0;
        double moon = commonPoints[ChartPoints.Moon].Ecliptical.MainPosSpeed.Position;
        lotLongitude = lot switch
        {
            ChartPoints.FortunaNoSect => ascendant + moon - sun,
            ChartPoints.FortunaSect => dayLight ? ascendant + moon - sun : ascendant - moon + sun,
            _ => lotLongitude
        };
        while (lotLongitude >= 360.0)
        {
            lotLongitude -= 360.0;
        }
        while (lotLongitude < 0.0)
        {
            lotLongitude += 360.0;
        }
        return ConstructFullPointPos(lotLongitude, jdUt, obliquity, location);

    }

    private FullPointPos ConstructFullPointPos(double longitude, double jdUt, double obliquity, Location? location)
    {
        PosSpeed longPosSpeed = new(longitude, 0.0);
        PosSpeed latPosSpeed = new(0.0, 0.0);
        PosSpeed distPosSpeed = new(0.0, 0.0);
        PosSpeed[] eclipticPosSpeed = { longPosSpeed, latPosSpeed, distPosSpeed };
        EclipticCoordinates eclCoord = new(longitude, 0.0);
        EquatorialCoordinates equCoord = _coordinateConversionCalc.PerformConversion(eclCoord, obliquity);
        PosSpeed raPosSpeed = new(equCoord.RightAscension, 0.0);
        PosSpeed declPosSpeed = new(equCoord.Declination, 0.0);
        PosSpeed[] equatorialPosSpeed = { raPosSpeed, declPosSpeed, distPosSpeed };
        HorizontalRequest horizontalRequest = new(jdUt, location, equCoord);
        HorizontalCoordinates horCoord = _horizontalHandler.CalcHorizontal(horizontalRequest);
        return _fullPointPosFactory.CreateFullPointPos(eclipticPosSpeed, equatorialPosSpeed, horCoord);
    }

}