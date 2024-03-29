// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.References;

namespace Enigma.Domain.Graphics;


/// <summary>
/// Required data to plot a glyph for a celestial point in a chart wheel.
/// </summary>
public record GraphicCelPointForWheelPositions


{
    /// <summary>The celestial point to plot.</summary>
    public ChartPoints CelPoint { get; }
    /// <summary>Ecliptic longitude</summary>
    public double EclipticPos { get; }
    /// <summary>Position in mundane frame, measured in degrees from the ascendant, in anti-clockwise direction.</summary>
    public double MundanePos { get; }
    /// <summary>Position to plot, defined as an angle. Initially the same as MundanePos, but can be overridden to prevent glyphs to overlap.</summary>
    public double PlotPos { get; set; }
    /// <summary>Textual presentation of the longitude in degrees and minutes, (0 .. 29 degrees).</summary>
    public string LongitudeText { get; }

    public GraphicCelPointForWheelPositions(ChartPoints celPoint, double eclipticPos, double mundanePos, string longitudeText)
    {
        CelPoint = celPoint;
        EclipticPos = eclipticPos;
        MundanePos = mundanePos;
        PlotPos = mundanePos;
        LongitudeText = longitudeText;

    }
}


/// <summary>Required data to plat a glyph and position in a declination diagram.</summary>
/// <param name="Glyph">Symbol for the point.</param>
/// <param name="Longitude">Ecliptical longitude.</param>
/// <param name="Declination">Declination.</param>
/// <param name="DeclinationSpeed">Daily speed of declination.</param>
/// <param name="LongitudeText">Textual presentation of the longitude in degrees and minutes, (0 .. 29 degrees).</param>
/// <param name="DeclinationText">Textual presentation of the longitude in degrees and minutes.</param>
public record GraphicCelPointForDeclDiagram(char Glyph, double Longitude, double Declination, double DeclinationSpeed, 
    string LongitudeText, string DeclinationText);
