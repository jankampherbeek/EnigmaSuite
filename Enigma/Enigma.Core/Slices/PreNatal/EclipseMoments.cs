// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.Slices.PreNatal;

/// <summary>
/// Handle eclipse moments
/// </summary>
public static class EclipseMoments
{
    private static readonly CalcUtFacade CalcUtFacade = new CalcUtFacade();
    private static readonly EclipseFacade EclipseFacade = new EclipseFacade();
    
    /// <summary>
    /// Find eclipses within a given period
    /// </summary>
    /// <param name="startJd">JD number for the start of the period</param>
    /// <param name="endJd">JD number for the end of the period</param>
    /// <returns>A sorted list (sorted on JD) with solar and lunar eclipses</returns>
    public static List<Eclipse> FindEclipses(double startJd, double endJd)
    {
        var eclipseMoments = new List<Eclipse>();
        var runningJd = startJd;
        while (runningJd < endJd)
        {
            var newJdd = EclipseFacade.NextSolarEclipse(runningJd);
            runningJd = newJdd;
            if (runningJd <= endJd)
            {
                var longitude = CalcLongitude(ChartPoints.Sun, runningJd);
                eclipseMoments.Add(new Eclipse(runningJd, longitude, 'S'));
            }
        }
        runningJd = startJd;
        while (runningJd < endJd)
        {
            var newJdd = EclipseFacade.NextLunarEclipse(runningJd);
            runningJd = newJdd;
            if (runningJd <= endJd)
            {
                var longitude = CalcLongitude(ChartPoints.Sun, runningJd);
                eclipseMoments.Add(new Eclipse(runningJd, longitude, 'L'));
            }
        }
        return eclipseMoments;
    }

    private static double CalcLongitude(ChartPoints factor, double jd)
    {
        var seId = factor.GetDetails().CalcId;
        var flags = 2;      // Use SE, no speed
        var positions = CalcUtFacade.PositionFromSe(jd, seId, flags);
        return positions[0];
    }
    
}