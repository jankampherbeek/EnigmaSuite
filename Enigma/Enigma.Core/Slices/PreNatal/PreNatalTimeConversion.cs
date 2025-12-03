// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Slices.PreNatal;

public static class PreNatalTimeConversion
{
    const double conversionFactor = 365.2421 * (70.0/273.217);
    
    public static double ConvertToActualTime(double preNatalJd, double conceptionJd, double radixJd)
    {

        var dateTimeSpan = preNatalJd - conceptionJd;
        var actualTime = dateTimeSpan * conversionFactor + radixJd;
        return actualTime ;
    }

 
    /// <summary>
    /// Find the Julian Day for a corrected conception
    /// </summary>
    /// <param name="radixJd">JD for the radix</param>
    /// <param name="baseConceptionJd">The default conception jd</param>
    /// <param name="eventJd">JD for the event</param>
    /// <param name="astronJd">JD for the astronomical moment</param>
    /// <returns>JD of the corrected conception</returns>
    public static double FindCorrectedConceptionJd(double radixJd, double baseConceptionJd, double eventJd, double astronJd)
    {
        var offset = astronJd - eventJd;
        var correctedOffset = offset / conversionFactor;
        var correctedConceptionJd = baseConceptionJd + correctedOffset;  
        return correctedConceptionJd; 
    }
}