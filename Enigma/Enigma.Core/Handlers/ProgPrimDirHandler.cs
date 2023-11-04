// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Core.Handlers;

public class ProgPrimDirHandler: IProgPrimDirHandler
{
    private readonly ISpeculumCreator _speculumCreator;
    private readonly ITimeKeyCalculator _timeKeyCalculator;

    public ProgPrimDirHandler(ISpeculumCreator speculumCreator, ITimeKeyCalculator timeKeyCalculator)
    {
        _speculumCreator = speculumCreator;
        _timeKeyCalculator = timeKeyCalculator;
    }
    

    public ProgPrimDirResponse CalculatePrimDir(ProgPrimDirRequest request)
    {
        List<TimedProgMatch> allMatches = new();
        Speculum speculum = _speculumCreator.CreateSpeculum(request.PdDirMethod, request.Chart, request.Promissors,
            request.Significators);
        double jdRadix = request.Chart.InputtedChartData.FullDateTime.JulianDayForEt;
        FullPointPos radixSun = request.Chart.Positions[ChartPoints.Sun];
        Location location = request.Chart.InputtedChartData.Location;
        ObserverPositions observerPos = request.ObserverPos;
        AspectTypes aspect = AspectTypes.Conjunction;        // TODO add opposition
        foreach (ChartPoints significator in request.Significators)
        {
            SpeculumItem signItem = speculum.SpeculumItems[significator];
            
            foreach (ChartPoints promissor in request.Promissors)
            {
                if (significator == promissor) continue;
                
                SpeculumItem promItem = speculum.SpeculumItems[promissor];
                double sa = promItem.Hd >= 0.0 ? promItem.Dsa : promItem.Nsa;
                double projMdMovingPoint = signItem.PropSa * sa;
                double arcOfDirection = promItem.Hd > 0.0                       // check this, definition by Gansten unclear and probably wrong
                    ? projMdMovingPoint - promItem.MdMc
                    : projMdMovingPoint - promItem.MdIc;
                double years = _timeKeyCalculator.CalculateDaysFromTotalKey(request.PdKeys, arcOfDirection, radixSun,
                    jdRadix, observerPos, location); 
                double progJd = jdRadix + years / EnigmaConstants.TROPICAL_YEAR_IN_DAYS;
                allMatches.Add(new TimedProgMatch(significator, promissor, aspect, progJd));
            }
        }
        double centralJdNr = request.Jdnr; 
        int nrOfYearsBeforeAndAfter = request.PeriodInYears / 2;
        double startJd = centralJdNr - nrOfYearsBeforeAndAfter * EnigmaConstants.TROPICAL_YEAR_IN_DAYS;
        double endJd = centralJdNr + nrOfYearsBeforeAndAfter * EnigmaConstants.TROPICAL_YEAR_IN_DAYS;
        List<TimedProgMatch> matchesInRange = new();
        foreach (var match in allMatches)
        {
            if (match.Jdnr >= startJd && match.Jdnr <= endJd)
            {
                matchesInRange.Add((match));
            }
        }

        // sort matchesInRange on Jdnr
        matchesInRange.Sort((x, y) => x.Jdnr.CompareTo(y.Jdnr));
        
        

        int resultCode = 0;     // todo give info on possible errors

        return new ProgPrimDirResponse(matchesInRange, speculum, resultCode);

    }



    
}