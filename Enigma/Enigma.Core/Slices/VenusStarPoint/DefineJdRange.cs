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
    private const Calendars cal = Calendars.Gregorian;
    
    public List<Tuple<double, VenusPhenomena>> JdRange(double birthJd, bool prenatal)
    {
        var jdsFound = new List<Tuple<double, VenusPhenomena>>();
        
        
        // calculate all occurrences from starting with 584 days before birthJd, and remember the value for VenusPhenomena
        var lastJdInferior= birthJd - MAX_VENUS_PERIOD;
        var lastJdSuperior = lastJdInferior;        // start JD for inferior and superior conjunctions is the same
        // calculate inferior conjunctiopns
        for (var i = 0; i < 7; i++)
        {
            var year = julDayFacade.DateTimeFromJd(lastJdInferior + MIN_VENUS_PERIOD, cal).Year;
            var jdNewYear = julDayFacade.JdFromSe(new SimpleDateTime(year, 1, 1, 0, cal));
            var yearFraction = YearFraction.CalcYearAndFraction(year, lastJdInferior, jdNewYear);
            var jdPhenonomenon = DefineJdPhenomenon(yearFraction, VenusPhenomena.InferiorConjunction);
            lastJdInferior = jdPhenonomenon.Item1;        
            jdsFound.Add(jdPhenonomenon);
        }
        // calculate superior conjunctions
        for (var i = 0; i < 7; i++)
        {
            var year = julDayFacade.DateTimeFromJd(lastJdSuperior + MIN_VENUS_PERIOD, cal).Year;
            var jdNewYear = julDayFacade.JdFromSe(new SimpleDateTime(year, 1, 1, 0, cal));
            var yearFraction = YearFraction.CalcYearAndFraction(year, lastJdSuperior, jdNewYear);
            var jdPhenonomenon = DefineJdPhenomenon(yearFraction, VenusPhenomena.SuperiorConjunction);
            lastJdSuperior = jdPhenonomenon.Item1;        
            jdsFound.Add(jdPhenonomenon);
        }
        
        jdsFound.Sort();
        
        // sort list
        // remove duplicates
        // if prenatal is true, return the first item before birth and 4 later items
        // if prenatal is false, return 5 items after birth,


        return jdsFound;
    }
    
    private Tuple<double, VenusPhenomena> DefineJdPhenomenon(double yearFraction, VenusPhenomena phenomenon)
    {
        var estimatedJd = EstimatedConjunctionDate.CalcEstimatedConjunctionDate(yearFraction, phenomenon);
        var exactJd = exactConjunctionDate.CalculateConjunctiondate(estimatedJd);
        return new Tuple<double, VenusPhenomena>(exactJd, phenomenon); 
    }
    
}