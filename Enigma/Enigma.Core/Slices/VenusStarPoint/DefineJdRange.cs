// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.Slices.VenusStarPoint;


/// <summary>
/// Define the range of valid JD's for the Venus Star Point
/// </summary>
public class DefineJdRange(IJulDayFacade julDayFacade, ExactConjunctionDate exactConjunctionDate)
{
    private const double MAX_VENUS_PERIOD = 584.0;
    private const double MIN_VENUS_PERIOD = 583.0;
    private const Calendars CAL = Calendars.Gregorian;
    private const double JD_TOLERANCE = 0.1; // Tolerance for considering Julian Days as approximately the same (about 2.4 hours)
    
    public List<Tuple<double, VenusPhenomena>> JdRange(double birthJd, bool prenatal)
    {
        var jdsFound = new List<Tuple<double, VenusPhenomena>>();
        
        
        // calculate all occurrences from starting with 584 days before birthJd, and remember the value for VenusPhenomena
        var lastJdInferior= birthJd - MAX_VENUS_PERIOD;
        var lastJdSuperior = lastJdInferior;        // start JD for inferior and superior conjunctions is the same
        // calculate inferior conjunctiopns
        for (var i = 0; i < 7; i++)
        {
            var year = julDayFacade.DateTimeFromJd(lastJdInferior + MIN_VENUS_PERIOD, CAL).Year;
            var jdNewYear = julDayFacade.JdFromSe(new SimpleDateTime(year, 1, 1, 0, CAL));
            var yearFraction = YearFraction.CalcYearAndFraction(year, lastJdInferior, jdNewYear);
            var jdPhenonomenon = DefineJdPhenomenon(yearFraction, VenusPhenomena.InferiorConjunction);
            lastJdInferior = jdPhenonomenon.Item1;        
            jdsFound.Add(jdPhenonomenon);
        }
        // calculate superior conjunctions
        for (var i = 0; i < 7; i++)
        {
            var year = julDayFacade.DateTimeFromJd(lastJdSuperior + MIN_VENUS_PERIOD, CAL).Year;
            var jdNewYear = julDayFacade.JdFromSe(new SimpleDateTime(year, 1, 1, 0, CAL));
            var yearFraction = YearFraction.CalcYearAndFraction(year, lastJdSuperior, jdNewYear);
            var jdPhenonomenon = DefineJdPhenomenon(yearFraction, VenusPhenomena.SuperiorConjunction);
            lastJdSuperior = jdPhenonomenon.Item1;        
            jdsFound.Add(jdPhenonomenon);
        }
        
        jdsFound.Sort();
        jdsFound = RemoveDuplicateJds(jdsFound);
        
        // if prenatal is true, return the first item before birth and 4 later items
        // if prenatal is false, return 5 items after birth,


        return jdsFound;
    }
    
    /// <summary>
    /// Removes duplicate entries where Julian Days are approximately the same (within tolerance).
    /// If multiple items have approximately the same JD, only the first one is kept.
    /// </summary>
    /// <param name="jdsList">Sorted list of Julian Day and Venus phenomena tuples</param>
    /// <returns>List with duplicates removed</returns>
    private static List<Tuple<double, VenusPhenomena>> RemoveDuplicateJds(List<Tuple<double, VenusPhenomena>> jdsList)
    {
        if (jdsList.Count <= 1)
            return jdsList;

        var result = new List<Tuple<double, VenusPhenomena>> { jdsList[0] };  // Add the first item

        for (var i = 1; i < jdsList.Count; i++)
        {
            var currentJd = jdsList[i].Item1;
            var previousJd = jdsList[i - 1].Item1;
            
            // If the current JD is not approximately the same as the previous one, add it
            if (Math.Abs(currentJd - previousJd) > JD_TOLERANCE)
            {
                result.Add(jdsList[i]);
            }
        }
        return result;
    }
    
    private Tuple<double, VenusPhenomena> DefineJdPhenomenon(double yearFraction, VenusPhenomena phenomenon)
    {
        var estimatedJd = EstimatedConjunctionDate.CalcEstimatedConjunctionDate(yearFraction, phenomenon);
        var exactJd = exactConjunctionDate.CalculateConjunctiondate(estimatedJd);
        return new Tuple<double, VenusPhenomena>(exactJd, phenomenon); 
    }
    
}