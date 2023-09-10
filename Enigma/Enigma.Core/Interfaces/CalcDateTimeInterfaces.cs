// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.References;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Interfaces;


/// <summary>Handler for calculation of date and time from JD nr.</summary>
public interface IDateTimeHandler
{
    public DateTimeResponse CalcDateTime(DateTimeRequest request);
    bool CheckDateTime(SimpleDateTime dateTime);
}

/// <summary>Handler for the calculation of a Julian Day Number.</summary>
public interface IJulDayHandler
{
    /// <summary>Starts the calculation for a Julian Day Number.</summary>
    /// <param name="dateTime">Date and time.</param>
    /// <returns>Response with JD related results and an indication if the calculation was successful.</returns>
    public JulianDayResponse CalcJulDay(SimpleDateTime dateTime);
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

/// <summary>Calculations for Julian Day.</summary>
public interface IDateTimeValidator
{
    public bool ValidateDateTime(SimpleDateTime dateTime);
}


/// <summary>Calculations for Julian Day.</summary>
public interface IDateTimeCalc
{
    /// <summary>Calculate Date and time.</summary>
    /// <param name="julDay"/>
    /// <param name="calendar"/>
    /// <returns>Calculated JD for UT.</returns>
    public SimpleDateTime CalcDateTime(double julDay, Calendars calendar);
}