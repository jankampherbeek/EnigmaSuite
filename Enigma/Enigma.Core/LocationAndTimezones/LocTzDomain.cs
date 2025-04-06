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

