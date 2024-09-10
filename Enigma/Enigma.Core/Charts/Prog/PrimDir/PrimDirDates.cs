// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Core.Handlers;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Charts.Prog.PrimDir;

namespace Enigma.Core.Charts.Prog.PrimDir;

public interface IPrimDirDates
{
    public double JdForEvent(double jdStart, double arc, PrimDirTimeKeys key);
}

public class PrimDirDates: IPrimDirDates
{
    
    private ICelPointsHandler _celPointsHandler;

    public PrimDirDates(ICelPointsHandler celPointsHandler)
    {
        _celPointsHandler = celPointsHandler;
    }

    public double JdForEvent(double jdStart, double arc, PrimDirTimeKeys key)
    {
        return JdTraject(jdStart, arc, key);
    }
    
    private double JdTraject(double jdStart, double arc, PrimDirTimeKeys key)
    {
        const double naibodPerYear = 0.985647358006;
        double estimatedJd = jdStart + arc * (360.0 / EnigmaConstants.TROPICAL_YEAR_IN_DAYS);
        double sunEclRadix = CalcSunEcliptical(jdStart);
        double sunEquRadix = CalcSunEquatorial(jdStart);
        switch (key)
        {
            case PrimDirTimeKeys.Ptolemy:
            {
                return jdStart + arc * EnigmaConstants.TROPICAL_YEAR_IN_DAYS;;
            }
            case PrimDirTimeKeys.Naibod:
            {
               return jdStart + arc / naibodPerYear * EnigmaConstants.TROPICAL_YEAR_IN_DAYS;
            }
            case PrimDirTimeKeys.Brahe:
            {
                double sunBefore = CalcSunEquatorial(jdStart - 0.5);
                double sunAfter = CalcSunEquatorial(jdStart + 0.5);
                double raDiff = RangeUtil.ValueToRange(sunAfter - sunBefore, 0.0, 360.0);
                return jdStart + arc / raDiff * EnigmaConstants.TROPICAL_YEAR_IN_DAYS;
            }
            case PrimDirTimeKeys.Placidus:
                double secundaryPosEqu = RangeUtil.ValueToRange(sunEquRadix + arc, 0.0, 360.0);
                double secundaryJdEqu = FindJdForPositionSun(secundaryPosEqu, estimatedJd, CoordinateSystems.Equatorial);
                return (secundaryJdEqu - jdStart) * EnigmaConstants.TROPICAL_YEAR_IN_DAYS + jdStart;
            case PrimDirTimeKeys.VanDam:
                double secundaryPosEcl = RangeUtil.ValueToRange(sunEclRadix + arc, 0.0, 360.0);
                double secundaryJdEcl = FindJdForPositionSun(secundaryPosEcl, estimatedJd, CoordinateSystems.Ecliptical);
                return (secundaryJdEcl - jdStart) * EnigmaConstants.TROPICAL_YEAR_IN_DAYS + jdStart;
            default: return 0.0;
        }
    }

    private double FindJdForPositionSun(double position, double estimatedJd, CoordinateSystems cSys)
    {
        const double MAX_DELTA = 0.0001;
        double tempJd = estimatedJd;
        double guessedPos = CalcSun(tempJd, cSys);
       
        double delta = RangeUtil.ValueToRange(position - guessedPos, -180.0, 180.0);
        bool positionFound = false;
        while (!positionFound)
        {
            guessedPos = CalcSun(tempJd, cSys);
            delta = RangeUtil.ValueToRange(guessedPos - position, -180.0, 180.0);
            if (Math.Abs(delta) < MAX_DELTA) positionFound = true;
            else tempJd-= delta;
        }
        return tempJd;
    }

    private double CalcSun(double jd, CoordinateSystems cSys)
    {
        return cSys == CoordinateSystems.Ecliptical ? CalcSunEcliptical(jd) : CalcSunEquatorial(jd);
    }

    private double CalcSunEquatorial(double jd)
    {
        Location loc = new("", 0.0, 0.0);
        List<ChartPoints> points =
        [
            ChartPoints.Sun
        ];
        var prefs = new CalculationPreferences(
            points, 
            ZodiacTypes.Tropical, 
            Ayanamshas.None,
            CoordinateSystems.Equatorial, 
            ObserverPositions.GeoCentric,
            ProjectionTypes.TwoDimensional, 
            HouseSystems.NoHouses);
        return _celPointsHandler.CalcSinglePointWithSe(ChartPoints.Sun, jd, loc, prefs).Equatorial.MainPosSpeed.Position;
    }
    
    private double CalcSunEcliptical(double jd)
    {
        Location loc = new("", 0.0, 0.0);
        List<ChartPoints> points =
        [
            ChartPoints.Sun
        ];
        var prefs = new CalculationPreferences(
            points, 
            ZodiacTypes.Tropical, 
            Ayanamshas.None,
            CoordinateSystems.Ecliptical, 
            ObserverPositions.GeoCentric,
            ProjectionTypes.TwoDimensional, 
            HouseSystems.NoHouses);
        return _celPointsHandler.CalcSinglePointWithSe(ChartPoints.Sun, jd, loc, prefs).Ecliptical.MainPosSpeed.Position;
    }
    
}