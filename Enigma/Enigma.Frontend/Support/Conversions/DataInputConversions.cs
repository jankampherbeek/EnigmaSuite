// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Globalization;
using Serilog;

namespace Enigma.Frontend.Ui.Support.Conversions;

/// <summary>Conversions to assist data input</summary>
public interface IDataInputConverter
{
    /// <summary>Creates a formatted string of the type d:mm:ss from a textual presentation of a double</summary>
    /// <remarks>Ignores plus or minus, this should be handled separately.
    /// Returns empty string for invalid inputs.
    /// </remarks>
    /// <param name="input">Textual presentation of a double representing coordinates</param>
    /// <returns>/// A formatted string in the format "d:mm:ss" where:
    /// d = degrees
    /// mm = minutes (00-59)
    /// ss = seconds (00-59)
    /// Returns empty string if parsing fails</returns>
    public string ValueTxtToFormattedCoordinate(string input);
}


/// <inheritdoc/>
public class DataInputConverter: IDataInputConverter
{
    private const double MINUTES_IN_DEGREE = 60.0;
    private const double SECONDS_IN_MINUTE = 60.0;
    private const string SEPARATOR = ":";
    private const string ZERO_PREFIX = "0";
    private const double EPSILON = 1E-8;
    
    /// <inheritdoc/>
    public string ValueTxtToFormattedCoordinate(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            Log.Information("Empty of null input provided");
            return string.Empty;
        }
        
        var result = "";
        var normalizedInput = input.Replace(',', '.');
        if (double.TryParse(normalizedInput, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
        {
            value = Math.Abs(value); 
           var d = (int)value;
           var remaining = (value - d + EPSILON) * MINUTES_IN_DEGREE;
           var m = (int)remaining;
           remaining = (remaining - m + EPSILON) * SECONDS_IN_MINUTE;
           var s = (int)remaining;
           var mPrefix = m > 9 ? "" : ZERO_PREFIX;
           var sPrefix = s > 9 ? "" : ZERO_PREFIX;
           result = $"{d}{SEPARATOR}{mPrefix}{m}{SEPARATOR}{sPrefix}{s}";
        }
        else
        {
            Log.Error($"Could not convert coordinate value: {input}");
        }
        return result;
    }
}