// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Analysis;

/// <summary>
/// Supported midpoints.
/// </summary>

public enum MidpointTypes
{
    Dial360,
    Dial90,
    Dial45
}

/// <summary>
/// Details for a midpoint.
/// </summary>
public record MidpointDetails
{
    public readonly MidpointTypes Midpoint;
    public readonly int Division;
    public readonly string TextId;
    public readonly double OrbFactor;

    /// <summary>
    /// Constructs details for a midpoint.
    /// </summary>
    /// <param name="aspect">Midpoint from enum 'MidpointTypes'.</param>
    /// <param name="division">Division of the circle before midpoints are calculated.</param>
    /// <param name="textId">Text for this midpoint in the resource bundle.</param>
    /// <param name="orbFactor">Default weighting Factor for the calculation of the orb. Zero if the MidpointType should not be used.</param>
    public MidpointDetails(MidpointTypes midpoint, int division, string textId, double orbFactor)
    {
        Midpoint = midpoint;
        Division = division;
        TextId = textId;
        OrbFactor = orbFactor;
    }
}


/// <summary>
/// Specifications for a midpoint.
/// </summary>
public interface IMidpointSpecifications
{
    /// <summary>
    /// Defines the specifications for a midpoint.
    /// </summary>
    /// <param name="midpoint">The midpoint for which the details are defined.</param>
    /// <returns>A record MidpointDetails with the required information.</returns>
    public MidpointDetails DetailsForMidpoint(MidpointTypes midpoint);
}

/// <inheritdoc/>
public class MidpointSpecifications : IMidpointSpecifications
{
    public MidpointDetails DetailsForMidpoint(MidpointTypes midpoint)
    {
        return midpoint switch
        {
            MidpointTypes.Dial360 => new MidpointDetails(midpoint, 1, "ref.enum.midpoint.dial360", 1),
            MidpointTypes.Dial90 => new MidpointDetails(midpoint, 4, "ref.enum.midpoint.dial90", 0.25),
            MidpointTypes.Dial45 => new MidpointDetails(midpoint, 8, "ref.enum.midpoint.dial45", 0.125),
            _ => throw new ArgumentException("Midpoint unknown : " + midpoint.ToString())
        };
    }
}