// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Slices.PreNatal;

public static class PreNatalToActualTimeConversion
{
    public static double ConvertToActualTime(double preNatalJd, double conceptionJd)
    {
        const double conversionFactor = 365.2421 * (70.0/273.0);
        var dateTimeSpan = preNatalJd - conceptionJd;
        var actualTime = dateTimeSpan * conversionFactor + conceptionJd;
        return actualTime ;
    }
    
}