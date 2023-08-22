// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Persistency;
using Enigma.Domain.Progressive;
using Enigma.Frontend.Ui.Interfaces;

namespace Enigma.Frontend.Ui.Support;

/// <inheritdoc/>
public class PeriodDataConverter: IPeriodDataConverter

{
    /// <inheritdoc/>
    public ProgPeriod FromPersistablePeriodData(PersistablePeriodData persistablePeriodData)
    {
        return HandleConversion(persistablePeriodData);
    }

    /// <inheritdoc/>
    public PersistablePeriodData ToPersistableEventData(ProgPeriod progPeriod)
    {
        return HandleConversion(progPeriod);
    }
    
    private static ProgPeriod HandleConversion(PersistablePeriodData persistablePeriodData)
    {
        const string timeText = "00:00:00";
        string description = persistablePeriodData.Description;
        FullDateTime startDateTime = new(persistablePeriodData.StartDateText, timeText,  persistablePeriodData.StartJulianDayEt);
        FullDateTime endDateTime = new(persistablePeriodData.EndDateText, timeText, persistablePeriodData.EndJulianDayEt);
        return new ProgPeriod(persistablePeriodData.Id, description, startDateTime, endDateTime);
    }

    private static PersistablePeriodData HandleConversion(ProgPeriod progPeriod)
    {
        return new PersistablePeriodData(

            progPeriod.Description,
            progPeriod.StartDate.JulianDayForEt,
            progPeriod.EndDate.JulianDayForEt,
            progPeriod.StartDate.DateText,
            progPeriod.EndDate.DateText
        );
    }
}

