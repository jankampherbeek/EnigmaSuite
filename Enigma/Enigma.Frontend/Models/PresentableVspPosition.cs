// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Globalization;
using Enigma.Core.Slices.VenusStarPoint;
using Enigma.Frontend.Ui.Support.Conversions;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Presentable Venus Star Point position for display in UI</summary>
public class PresentableVspPosition
{
    public int SequenceId { get; }
    public string DateText { get; }
    public string TimeText { get; }
    public string PhenomenonText { get; }
    public string LongitudeText { get; }
    public char SignGlyph { get; }
    
    // Original data for internal use
    public double Jd { get; }
    public VenusPhenomena Phenomenon { get; }
    public double Longitude { get; }
    
    public PresentableVspPosition(VenusStarPointPosition position, string dateText, string timeText)
    {
        SequenceId = position.SequenceId;
        Jd = position.Jd;
        Phenomenon = position.Phenomenon;
        Longitude = position.Longitude;
        
        DateText = dateText;
        TimeText = timeText;
        PhenomenonText = GetPhenomenonText(position.Phenomenon);
        
        // Use the ConvertDoubleToDmsWithGlyph method for proper formatting
        var doubleToDmsConversions = new DoubleToDmsConversions();
        var (longitudeText, signGlyph) = doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(position.Longitude);
        LongitudeText = longitudeText;
        SignGlyph = signGlyph;
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
