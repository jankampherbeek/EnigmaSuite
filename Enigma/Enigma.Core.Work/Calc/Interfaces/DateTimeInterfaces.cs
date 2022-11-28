// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;

namespace Enigma.Core.Work.Calc.Interfaces;



/// <summary>Calculations for Julian Day.</summary>
public interface ICheckDateTimeValidator
{
    public bool ValidateDateTime(SimpleDateTime dateTime);
}



/// <summary>Calculations for Julian Day.</summary>
public interface IDateTimeCalc
{
    public SimpleDateTime CalcDateTime(double julDay, Calendars calendar);
}



/// <summary>Calculations for Julian Day.</summary>
public interface IJulDayCalc
{
    /// <summary>Calculate Julian Day for Universal Time.</summary>
    /// <param name="dateTime"/>
    /// <returns>Calculated JD for UT.</returns>
    public double CalcJulDayUt(SimpleDateTime dateTime);

    /// <summary>Calculate Delta T</summary>
    /// <param name="juldayUt">Julian Day for UT.</param>
    /// <returns>The value for delta t in seconds and fractions of seconds.</returns>
    public double CalcDeltaT(double juldayUt);
}


