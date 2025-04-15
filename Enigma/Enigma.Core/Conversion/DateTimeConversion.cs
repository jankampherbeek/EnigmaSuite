// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Globalization;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Conversion;

/// <summary>Conversions for date/time</summary>
public static class DateTimeConversion
{

    private const double MINUTES_PER_HOUR = 60.0;
    private const double SECONDS_PER_MINUTE = 60.0;
    private const double SECONDS_PER_HOUR = 3600.0;

    /// <summary>Converts an array of strings into an instance of SimpleDateTime.</summary>
    /// <remarks>Optional items default to 0.
    /// Always uses Gregorian calendar. </remarks>
    /// <param name="items">At least 3 items for year, month, date and optionally items for hour, minute and second.</param>
    /// <returns>Constructed record SimpleDateTime.</returns>
    /// <exception cref="ArgumentException">For insufficient input.</exception>
    /// <exception cref="FormatException">For inputitems that cannnot be parsed.</exception>
    public static SimpleDateTime ParseDateTimeFromText(string[] items)
    {
        if (items.Length < 3)
        {
            throw new ArgumentException("Not enough items to define a date");
        }
        
        int h = 0, mi = 0, s = 0;

        if (!int.TryParse(items[0], out var y))
            throw new FormatException($"Invalid year format: {items[0]}");
        if (!int.TryParse(items[1], out var mo))
            throw new FormatException($"Invalid month format: {items[1]}");
        if (mo is < 1 or > 12)
            throw new ArgumentOutOfRangeException($"Month ({mo}) must be between 1 and 12");
        if (!int.TryParse(items[2], out var d))
            throw new FormatException($"Invalid day format: {items[2]}");
        if (d is < 1 or > 31)
            throw new ArgumentOutOfRangeException($"Day ({d}) must be betwee 1 and 31");
        
        switch (items.Length)
        {
            // items for time are optional
            case > 3 when !int.TryParse(items[3], out h):
                throw new FormatException($"Invalid hour format: {items[3]}");
            case > 4 when !int.TryParse(items[4], out mi):
                throw new FormatException($"Invalid minute format: {items[4]}");
            case > 5 when !int.TryParse(items[5], out s):
                throw new FormatException($"Invalid second format: {items[5]}");
        }
        if (h is <-23 or >23)
            throw new ArgumentOutOfRangeException($"Hour ({h}) must be between -23 and 23");
        if (mi is < 0 or >59)
            throw new ArgumentOutOfRangeException($"Minute ({mi}) must be between 0 and 59");
        if (s is < 0 or >59)
            throw new ArgumentOutOfRangeException($"Second ({s}) must be between 0 and 59");
        
        var time = h + mi / MINUTES_PER_HOUR + s / SECONDS_PER_HOUR;
        
        var sdt = new SimpleDateTime(y, mo, d, time, Calendars.Gregorian);
        return sdt;
    }

    /// <summary>Define time or coordinate from strings for hour/degree, minutes, and seconds.</summary>
    /// <remarks>If items are invalid or missing, a value of 0 is used.</remarks>
    /// <param name="hdTxt">Hour or degree</param>
    /// <param name="mTxt">Minute</param>
    /// <param name="sTxt">Second</param>
    /// <returns>Decimale value of time.</returns>
    public static double ParseDHmsToDoubleFromText(string hdTxt, string mTxt, string sTxt)
    {
        var dhms = DefineDhms(hdTxt, mTxt, sTxt);
        return dhms.Item1 + dhms.Item2 / MINUTES_PER_HOUR + dhms.Item3 / SECONDS_PER_HOUR;
    }

    public static Tuple<int, int, int> ParseDhmsToTupleFromText(string hdTxt, string mTxt, string sTxt)
    {
        return DefineDhms(hdTxt, mTxt, sTxt);
    }

    private static Tuple<int, int, int> DefineDhms(string hdTxt, string mTxt, string sTxt)
    {
        int dh = 0, m = 0, s = 0;

        if (!string.IsNullOrEmpty(hdTxt))
        {
            if (!int.TryParse(hdTxt, out dh))
                throw new FormatException($"Invalid hour/degree format: {hdTxt}");
        }
        var posNegFactor = dh >= 0.0 ? 1 : -1;

        if (!string.IsNullOrEmpty(mTxt))
        {
            if (!int.TryParse(mTxt, out m))
                throw new FormatException($"Invalid minute format: {mTxt}");
            if (m is < 0 or > 59)
                throw new ArgumentOutOfRangeException($"Minute ({m}) must be between 0 and 59");
            m *= posNegFactor;
        }
        if (string.IsNullOrEmpty(sTxt)) sTxt = "0";
        if (!int.TryParse(sTxt, out s))
            throw new FormatException($"Invalid second format: {sTxt}");
        if (s is < 0 or > 59)
            throw new ArgumentOutOfRangeException($"Second ({s}) must be between 0 and 59");
        s *= posNegFactor;
        return new Tuple<int, int, int>(dh, m, s);
    }
    
    
    /// <summary>Create sexagesimal test from double.</summary>
    /// <param name="value">The value to parse.</param>
    /// <returns>Text with sexagesimal parts, separated by a colon :</returns>
    public static string ParseSexTextFromFloat(double value)
    {
        var preSign = value >= 0.0 ? "":"-";
        value = Math.Abs(value);
        var hd = Math.Truncate(value);
        var remaining = value - hd + 1e-12; // add minor amount to prevent rounding problems
        var mFrac = remaining * MINUTES_PER_HOUR;
        var m = Math.Truncate(mFrac);
        remaining = mFrac - m;
        var s = Math.Truncate(remaining * SECONDS_PER_MINUTE);

        var mPre = m < 10.0 ? "0" : "";
        var sPre = s < 10.0 ? "0" : "";
        
        var hdTxt = hd.ToString("F0", CultureInfo.InvariantCulture);
        var mTxt = m.ToString("F0", CultureInfo.InvariantCulture);
        var sTxt = s.ToString("F0", CultureInfo.InvariantCulture);

        return $"{preSign}{hdTxt}:{mPre}{mTxt}:{sPre}{sTxt}";
    }
}