// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Domain.Constants;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Interfaces;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for heliacal calculations.</summary>
public class CalcHeliacalModel: DateTimeLocationModelBase
{
    /// <summary>Descriptive texts for celestial event types, in the ame sequence as the corresponding enum.</summary>
    public List<string> AllEventTypes { get; }
    
    /// <summary>Descriptive texts for celestial objects, in the ame sequence as the corresponding enum.</summary>
    public List<string> AllHeliacalObjects { get; }

    public int Altitude;
    
    private readonly IGeoLatInputParser _geoLatInputParser;
    private readonly IGeoLongInputParser _geoLongInputParser;
    private readonly IDateInputParser _dateInputParser;

    

    public CalcHeliacalModel(IGeoLatInputParser geoLatInputParser, 
        IGeoLongInputParser geoLongInputParser, 
        IDateInputParser dateInputParser, 
        ITimeInputParser timeInputParser): 
        base(dateInputParser, timeInputParser, geoLongInputParser, geoLatInputParser)
    {
        _geoLatInputParser = geoLatInputParser;
        _geoLongInputParser = geoLongInputParser;
        _dateInputParser = dateInputParser;
        AllEventTypes = new List<string>();
        AllHeliacalObjects = new List<string>();
        PopulateEventTypes();
        PopulateHeliacalObjects();
    }

    public void CalcHeliacalEvent(HeliacalEventTypes eventType, HeliacalObjects helObject)
    {
        // todo calculate heliacal risings and settings
    }
    
    
    public bool IsAltitudeValid(string altitude)
    {
        if (string.IsNullOrEmpty(altitude)) return false;
        bool result = int.TryParse(altitude, out int altitudeValue);
        bool isValid = (result && altitudeValue is > EnigmaConstants.ALTITUDE_OBSERVER_MIN 
            and < EnigmaConstants.ALTITUDE_OBSERVER_MAX); 
        if (isValid) Altitude = altitudeValue;
        return isValid;
    }

    
    
    private void PopulateEventTypes()
    {
        IEnumerable<HeliacalEventTypeDetails> eventTypeDetails = HeliacalEventTypesExtensions.AllDetails();
        foreach (var eventTypeDetail in eventTypeDetails)
        {
            AllEventTypes.Add(eventTypeDetail.Text);
        }
    }

    private void PopulateHeliacalObjects()
    {
        IEnumerable<HeliacalObjectDetails> heliacalObjectDetails = HeliacalObjectsExtensions.AllDetails();
        foreach (var heliacalObjectDetail in heliacalObjectDetails)
        {
            AllHeliacalObjects.Add(heliacalObjectDetail.ChartPoint.GetDetails().Text);
        }
    }
    
    
}