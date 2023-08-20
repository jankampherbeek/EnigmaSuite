// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Progressive;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.Models;

public class ProgEventModel: DateTimeLocationModelBase
{
    private readonly IJulianDayApi _julianDayApi;
    
    public ProgEventModel(IGeoLongInputParser geoLongInputParser, IGeoLatInputParser geoLatInputParser,
        IDateInputParser dateInputParser, ITimeInputParser timeInputParser, IJulianDayApi julianDayApi) 
        : base(dateInputParser, timeInputParser, geoLongInputParser, geoLatInputParser)
    {
        _julianDayApi = julianDayApi;
    }

    public void CreateEventData(string description, string locationName, Location location, FullDateTime dateTime)
    {
        ProgEvent progEvent = new(0, description, locationName, location, dateTime);
        DataVault.Instance.CurrentProgEvent = progEvent;
    }
    
}