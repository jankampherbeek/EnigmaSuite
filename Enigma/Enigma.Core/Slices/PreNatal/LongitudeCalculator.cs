// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.Slices.PreNatal;

public static class LongitudeCalculator
{
    private static readonly CalcUtFacade CalcUtFacade = new CalcUtFacade();
    
    public static double CalcLongitude(ChartPoints factor, double jd)
    {
        var seId = factor.GetDetails().CalcId;
        const int flags = 2;      // Use SE, no speed
        var positions = CalcUtFacade.PositionFromSe(jd, seId, flags);
        return positions[0];
    }
}