// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Globalization;
using Enigma.Core.Slices.VenusStarPoint;
using Enigma.Frontend.Ui.Support.Conversions;
using Enigma.Api.Calc;
using Enigma.Domain.References;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Presentable Venus Star Point position for display in UI</summary>
public class PresentableVspPosition
{
    public int SequenceId { get; }
    public string DateTimeText { get; }
    public string PhenomenonText { get; }
    public string LongitudeText { get; }
    public char SignGlyph { get; }
    
    // Original data for internal use
    public double Jd { get; }
    public VenusPhenomena Phenomenon { get; }
    public double Longitude { get; }
    
    public PresentableVspPosition(VenusStarPointPosition position, IJulianDayApi julianDayApi)
    {
        SequenceId = position.SequenceId;
        Jd = position.Jd;
        Phenomenon = position.Phenomenon;
        Longitude = position.Longitude;
        
        // Convert Julian Day to date/time using JulianDayApi
        var dateTime = julianDayApi.DateTimeFromJd(position.Jd, Calendars.Gregorian);
        DateTimeText = FormatDateTime(dateTime);
        PhenomenonText = GetPhenomenonText(position.Phenomenon);
        
        // Use the ConvertDoubleToDmsWithGlyph method for proper formatting
        var doubleToDmsConversions = new DoubleToDmsConversions();
        var (longitudeText, signGlyph) = doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(position.Longitude);
        LongitudeText = longitudeText;
        SignGlyph = signGlyph;
    }
    
    private static string FormatDateTime(Enigma.Domain.Dtos.SimpleDateTime dateTime)
    {
        // Convert decimal hours to hours, minutes, seconds
        int hours = (int)dateTime.Ut;
        double minutesDecimal = (dateTime.Ut - hours) * 60.0;
        int minutes = (int)minutesDecimal;
        int seconds = (int)((minutesDecimal - minutes) * 60.0);
        
        // Format as yyyy/mm/dd hh:mi:ss
        return $"{dateTime.Year:D4}/{dateTime.Month:D2}/{dateTime.Day:D2} {hours:D2}:{minutes:D2}:{seconds:D2}";
    }
    
    private static string GetPhenomenonText(VenusPhenomena phenomenon)
    {
        return phenomenon switch
        {
            VenusPhenomena.InferiorConjunction => "Inferior",
            VenusPhenomena.SuperiorConjunction => "Superior",
            _ => "Unknown"
        };
    }
}
