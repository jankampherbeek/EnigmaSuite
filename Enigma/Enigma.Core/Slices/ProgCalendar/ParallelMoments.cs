// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.Slices.ProgCalendar;

/// <summary>
/// Find moments that a progressive point forms a parallel or contra-parallel with a radix point
/// </summary>
public static class ParallelMoments
{

    private static readonly CalcUtFacade CalcUtFacade = new CalcUtFacade();

    public static List<ProgCalDeclinationParallelMatch> FindParallelMoments(ProgressionTypes progType,
        CalculatedChart calcChart,
        List<ChartPoints> progPoints, double jdStart, double jdEnd)
    {
        var parallelMoments = new List<ProgCalDeclinationParallelMatch>();

        foreach (var progPoint in progPoints)
        {
            // TODO find moments that a progPointa forms a parallel or contra-parallel with a radixPoint.
            // A parallel is when the declination of the progPoint is the same as the radixPoint
            // A contra-parallel is when the declination of the progPoint the same but with a different sign (positive/negative).
            // The margin should be no more than 0.00001 seconds of time.
            // The moments should be found in the time range from jdStart to jdEnd.
            // The approach should be comparable with the approach for the AspectMoments. 
            // Use the CalcDeclination() method to calculate the declination of a progPoint at a specific Julian Day.
            // Use the ProgCalDeclinationParallelMatch class to store the results. 
        }

        return parallelMoments;
    }

    /// <summary>
    /// Calculate the declination for a given chart point at a specific Julian Day
    /// </summary>
    public static double CalcDeclination(ChartPoints factor, double jd)
    {
        var seId = factor.GetDetails().CalcId;
        const int flags = 2 + 256 + 2048;              // use SE, use speed, equatorial coordinates
        var positions = CalcUtFacade.PositionFromSe(jd, seId, flags);
        return positions[0];
    }



}