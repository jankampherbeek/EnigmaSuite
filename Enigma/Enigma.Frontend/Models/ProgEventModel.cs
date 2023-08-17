using Enigma.Api.Interfaces;
using Enigma.Frontend.Helpers.Interfaces;

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
  
}