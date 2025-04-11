// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.LocationAndTimeZones;

//Local domain for handling of locations and timezones


/// <summary>A line with time zone information</summary>
/// <param name="Name">Name of the zone, same as tzIdentification</param>
/// <param name="StdOff">Offset from Ut</param>
/// <param name="Rules">Name of rules for DST</param>
/// <param name="Format">Abbreviation of the timezone or an indication of the offset</param>
/// <param name="Until">Julian day when this definition ends</param>
public record TzLine(string Name, double StdOff, string Rules, string Format, double Until);


/// <summary>Definition for date and time</summary>
public record DateTimeHms(int Year, int Month, int Day, int Hour, int Min, int Sec);

/// <summary>Elements for a line with DST information</summary>
/// <param name="Name">Name of the DST rule</param>
/// <param name="From">Start year</param>
/// <param name="To">End year of definition of end year (e.g. 1920, only)</param>
/// <param name="In">Month that change takes effect</param>
/// <param name="On">Day, or definition for day, that change takes effect (e.g. 14, last6)</param>
/// <param name="At">Time that change takes effect</param>
/// <param name="Ut">'u' if UT is used, otherwise 'n'</param>
/// <param name="Save">Value for DST</param>
/// <param name="Letter">Letter that indicates the change (e.g. S, -)</param>
public record DstElementsLine(string Name, int From, int To, int In, string On, double At, string Ut,double Save, string Letter);

/// <summary>Comprised data from a DST line</summary>
/// <param name="StartJd">Julian Day the change takes effect</param>
/// <param name="Offset">Additional offset for DST</param>
/// <param name="Letter">Letter that indicates the change</param>
/// <param name="IsUst">True if UT is used, otherwise false</param>
public record DstLine(double StartJd, double Offset, string Letter, bool IsUt);

/// <summary>
/// 
/// </summary>
/// <param name="Offset">Offset because of DST</param>
/// <param name="Letter">Letter that indicates the change</param>
/// <param name="IsInvalid">True if time is invalid because of dst change</param>
/// <param name="IsAmbiguous">True if time is ambiguous because of dst change</param>
public record DstInfo(double Offset, string Letter, bool IsInvalid, bool IsAmbiguous);