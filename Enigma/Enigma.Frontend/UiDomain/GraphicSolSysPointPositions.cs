// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.CalcVars;


namespace Enigma.Frontend.UiDomain;


/// <summary>
/// Required data to plot a glyph for a solar system point in a chart wheel.
/// </summary>
public record GraphicSolSysPointPositions
{
    /// <summary>The solar system point to plot.</summary>
    public SolarSystemPoints SolSysPoint { get; }
    /// <summary>Ecliptic longitude</summary>
    public double EclipticPos { get; }
    /// <summary>Position in mundane frame, measured in degrees from then ascendant, in anti-clockwise direction.</summary>
    public double MundanePos { get; }
    /// <summary>Position to plot, defined as an angle. Initially the same as MundanePos, but can be overridden to prevent glyphs to overlap.</summary>
    public double PlotPos { get; set; }
    /// <summary>Textual presentation of the longitude in degrees and minutes, (0 .. 29 degrees).</summary>
    public string LongitudeText { get; }

    public GraphicSolSysPointPositions(SolarSystemPoints solSysPoint, double eclipticPos, double mundanePos, string longitudeText)
    {
        SolSysPoint = solSysPoint;
        EclipticPos = eclipticPos;
        MundanePos = mundanePos;
        PlotPos = mundanePos;
        LongitudeText = longitudeText;

    }
}

