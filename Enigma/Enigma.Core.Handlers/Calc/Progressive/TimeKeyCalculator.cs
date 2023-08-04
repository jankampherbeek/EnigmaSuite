// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.Progressive;
using Enigma.Domain.Constants;
using Enigma.Domain.Points;
using Serilog;

namespace Enigma.Core.Handlers.Calc.Progressive;



public sealed class FixedTimeKey: IFixedTimeKey
{
    public double DaysFromArc(double arc, double keyLength)
    {
        return arc / keyLength;
    }

    public double ArcFromDays(double days, double keyLength)
    {
        return days * keyLength;
    }
}

public sealed class PlacidusTimeKey: IPlacidusTimeKey
{
    private readonly ISolarArcCalculator _calculator;
    private readonly ISeFlags _seFlags;
    private readonly IPositionFinder _positionFinder;    

    public PlacidusTimeKey(ISolarArcCalculator calculator, ISeFlags seFlags, IPositionFinder positionFinder)
    {
        _calculator = calculator;
        _seFlags = seFlags;
        _positionFinder = positionFinder;
    }

    public double ArcFromDays(double days, double jdRadix, CoordinateSystems coordSys, ObserverPositions observerPos, Location location)
    {
        CheckInput(observerPos, coordSys);
        int flags = _seFlags.DefineFlags(coordSys, observerPos, ZodiacTypes.Tropical);
        return _calculator.CalcSolarArcForTimespan(jdRadix, days, location, flags);
    }

    public double DaysFromArc(double jdRadix, FullPointPos progPosSun, CoordinateSystems coordSys, ObserverPositions observerPos, Location location)
    {
        CheckInput(observerPos, coordSys);
        const double startInterval = 1.0;
        const double maxMargin = 0.000001;
        double posToFind = coordSys == CoordinateSystems.Ecliptical ? progPosSun.Ecliptical.MainPosSpeed.Position : progPosSun.Equatorial.MainPosSpeed.Position;
        double jdForPosition = _positionFinder.FindJdForPositionSun(posToFind, jdRadix, startInterval, maxMargin, coordSys, observerPos, location);
        return jdForPosition - jdRadix;
    }

    private void CheckInput(ObserverPositions observerPos, CoordinateSystems coordSys)
    {
        string errorTxt = string.Empty;
        if (observerPos != ObserverPositions.GeoCentric && observerPos != ObserverPositions.TopoCentric)
        {
            errorTxt+= "PlacidusTimeKey received invalid observerposition: " + observerPos;

        }
        if (coordSys != CoordinateSystems.Ecliptical && coordSys != CoordinateSystems.Equatorial) {
            errorTxt += "PlacidusTimeKey received invalid coordinate system: " + coordSys;
        }

        if (errorTxt == string.Empty) return;
        Log.Error(errorTxt);
        throw new ArgumentException(errorTxt);
    }

}




/// <inheritdoc/>
public sealed class SolarArcCalculator: ISolarArcCalculator
{
    private readonly ICelPointSeCalc _calculator;

    public  SolarArcCalculator(ICelPointSeCalc calc)
    {
        _calculator = calc;
    }

    /// <inheritdoc/>
    public double CalcSolarArcForTimespan(double jdRadix, double timespan, Location location, int flags)
    {
        PosSpeed[] sunStart = _calculator.CalculateCelPoint(ChartPoints.Sun, jdRadix, location, flags);
        PosSpeed[] sunEnd = _calculator.CalculateCelPoint(ChartPoints.Sun, jdRadix + timespan, location, flags);
        return sunEnd[0].Position - sunStart[0].Position;
    }
}



/// <inheritdoc/>
public sealed class TimeKeyCalculator: ITimeKeyCalculator
{
    private readonly IFixedTimeKey _fixedTimeKey;
    private readonly IPlacidusTimeKey _placidusTimeKey;
    private ISeFlags _seFlags;

    public TimeKeyCalculator(IFixedTimeKey fixedTimeKey, IPlacidusTimeKey placidusTimeKey, ISeFlags seFlags)
    {
        _fixedTimeKey = fixedTimeKey;
        _placidusTimeKey = placidusTimeKey;
        _seFlags = seFlags;
    }

    /// <inheritdoc/>
    public double CalculateTotalKey(PrimaryKeys primaryKey, double jdRadix, double jdProgressive, Location location, FullPointPos positionSun, ObserverPositions observerPosition)
    {
        if (jdRadix >= jdProgressive)
        {
            string errorTxt = "TimeKeyCalculator.CalculateTotalKey received jd for event (" + jdProgressive + ")that is earlier than jd for radix (" + jdRadix + ").";
            Log.Error(errorTxt);
            throw new ArgumentException(errorTxt);
        }
        if (observerPosition != ObserverPositions.GeoCentric && observerPosition != ObserverPositions.TopoCentric)
        {
            string errorTxt = "TimeKeyCalculator.CalculateTotalKey received invalid observeerposition: " + observerPosition;
            Log.Error(errorTxt);
            throw new ArgumentException(errorTxt);
        }

        CoordinateSystems coordSys = CoordinateSystems.Ecliptical;
        if ((primaryKey == PrimaryKeys.PtolemyRa) || (primaryKey == PrimaryKeys.PlacidusRa) || (primaryKey == PrimaryKeys.NaibodRa) || (primaryKey == PrimaryKeys.BraheRa))
        {
            coordSys = CoordinateSystems.Equatorial;
        }

        double totalKey = 0.0;
        double timespanInDays = jdProgressive - jdRadix;


        if (timespanInDays > 0.0)
        {
            switch (primaryKey)
            {
                case PrimaryKeys.PtolemyLongitude:
                case PrimaryKeys.PtolemyRa:
                    totalKey = _fixedTimeKey.ArcFromDays(timespanInDays, 1.0);
                    break;
                case PrimaryKeys.NaibodRa:
                case PrimaryKeys.NaibodLongitude:
                    totalKey = _fixedTimeKey.ArcFromDays(timespanInDays, EnigmaConstants.NAIBOD);
                    break;
                case PrimaryKeys.BraheRa:
                    totalKey = _fixedTimeKey.ArcFromDays(timespanInDays, positionSun.Equatorial.MainPosSpeed.Speed);
                    break;
                case PrimaryKeys.BraheLongitude:
                    totalKey = _fixedTimeKey.ArcFromDays(timespanInDays, positionSun.Ecliptical.MainPosSpeed.Speed);
                    break;
                case PrimaryKeys.PlacidusRa:
                case PrimaryKeys.PlacidusLongitude:
                    totalKey = _placidusTimeKey.ArcFromDays(timespanInDays, jdRadix, coordSys, observerPosition, location); 
                    break;
                default:
                    string errorTxt = "TimeKeyCalculator.CalculateTotalKey encountered an unknown key for primary directions: " + primaryKey;
                    Log.Error(errorTxt);
                    throw new ArgumentException(errorTxt);
            }
        }
        return totalKey;
    }

    /// <inheritdoc/>
    public double CalculateDaysFromTotalKey(PrimaryKeys primaryKey, double totalKey, FullPointPos positionSun, double jdRadix, CoordinateSystems coordSys, ObserverPositions observerPos, Location location)
    {
        double timespanInDays = 0.0;

        if (!(totalKey > 0.0)) return timespanInDays;
        switch(primaryKey) {
            case PrimaryKeys.PtolemyLongitude:
            case PrimaryKeys.PtolemyRa:
                timespanInDays = _fixedTimeKey.DaysFromArc(totalKey, 1.0) ;
                break;
            case PrimaryKeys.NaibodRa:
            case PrimaryKeys.NaibodLongitude:
                timespanInDays = _fixedTimeKey.DaysFromArc(totalKey, EnigmaConstants.NAIBOD);
                break;
            case PrimaryKeys.BraheRa:
                timespanInDays = _fixedTimeKey.DaysFromArc(totalKey, positionSun.Equatorial.MainPosSpeed.Speed);
                break;
            case PrimaryKeys.BraheLongitude:
                timespanInDays = _fixedTimeKey.DaysFromArc(totalKey, positionSun.Ecliptical.MainPosSpeed.Speed);
                break;
            case PrimaryKeys.PlacidusRa:
            case PrimaryKeys.PlacidusLongitude:
                timespanInDays = _placidusTimeKey.DaysFromArc(jdRadix, positionSun, coordSys, observerPos, location);
                break;
            default:
                string errorTxt = "TimeKeyCalculator.CalculateDaysFromTotalKey encountered an unknown key for primary directions: " + primaryKey;
                Log.Error(errorTxt);
                throw new ArgumentException(errorTxt);
        }
        return timespanInDays;
    }
    


}



