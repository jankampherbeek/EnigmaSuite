// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Globalization;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Conversion;

/// <summary>Conversions for date/time as used in the backend.</summary>
public static class DateTimeConversion
{

    private const double MINUTES_PER_HOUR = 60.0;
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
        {
            throw new FormatException($"Invalid year format: {items[0]}");
        }

        if (!int.TryParse(items[1], out var mo))
        {
            throw new FormatException($"Invalid month format: {items[1]}");
        }

        if (!int.TryParse(items[2], out var d))
        {
            throw new FormatException($"Invalid day format: {items[2]}");
        }

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

        var time = h + mi / MINUTES_PER_HOUR + s / SECONDS_PER_HOUR;
        var sdt = new SimpleDateTime(y, mo, d, time, Calendars.Gregorian);
        return sdt;
    }

    /// <summary>Define time from strings for hour, minutes, and seconds.</summary>
    /// <remarks>If items are invalid or missing, a value of 0 is used.</remarks>
    /// <param name="hTxt">Hour</param>
    /// <param name="mTxt">Minute</param>
    /// <param name="sTxt">Second</param>
    /// <returns>Decimale value of time.</returns>
    public static double ParseHmsFromText(string hTxt, string mTxt, string sTxt)
    {
        int h = 0, m = 0, s = 0;

        if (!string.IsNullOrEmpty(hTxt))
        {
            if (!int.TryParse(hTxt, out h))
            {
                throw new FormatException($"Invalid hour format: {hTxt}");
            }
        }

        var posNegFactor = h >= 0.0 ? 1 : -1;

        if (!string.IsNullOrEmpty(mTxt))
        {
            if (!int.TryParse(mTxt, out m))
            {
                throw new FormatException($"Invalid minute format: {mTxt}");
            }

            m *= posNegFactor;
        }

        if (string.IsNullOrEmpty(sTxt)) return h + m / MINUTES_PER_HOUR + s / SECONDS_PER_HOUR;
        if (!int.TryParse(sTxt, out s))
        {
            throw new FormatException($"Invalid second format: {sTxt}");
        }

        s *= posNegFactor;
        return h + m / 60.0 + s / 3600.0;
    }

    /// <summary>Create sexagesimal test from double.</summary>
    /// <param name="value">The value to parse.</param>
    /// <returns>Text with sexagesimal parts, separated by a colon :</returns>
    public static string ParseSexTextFromFloat(double value)
    {
        const string sep = ":";
        var hd = Math.Truncate(value);
        var remaining = value - hd + 1e-12; // add minor amount to prevent rounding problems
        var mFrac = remaining * 60.0;
        var m = Math.Truncate(mFrac);
        remaining = mFrac - m;
        var s = Math.Truncate(remaining * 60.0);

        var mPre = m < 10.0 ? "0" : "";
        var sPre = s < 10.0 ? "0" : "";
        
        var hdTxt = hd.ToString("F0", CultureInfo.InvariantCulture);
        var mTxt = m.ToString("F0", CultureInfo.InvariantCulture);
        var sTxt = s.ToString("F0", CultureInfo.InvariantCulture);

        return hdTxt + sep + mPre + mTxt + sep + sPre + sTxt;
    }
}