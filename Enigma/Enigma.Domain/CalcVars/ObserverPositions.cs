// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;

namespace Enigma.Domain.CalcVars;

/// <summary>Observer positions, the center points for the calculation of positions of celestial bodies.</summary>
public enum ObserverPositions
{
    HelioCentric, GeoCentric, TopoCentric
}

/// <summary>Details for an observer position.</summary>
public record ObserverPositionDetails
{
    readonly public ObserverPositions ObserverPosition;
    readonly public int ValueForFlag;
    readonly public string TextId;

    /// <param name="position">The observer position.</param>
    /// <param name="valueForFlag">The value to construct the flags, as defined by the Swiss Ephemeris.</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
    public ObserverPositionDetails(ObserverPositions position, int valueForFlag, string textId)
    {
        ObserverPosition = position;
        ValueForFlag = valueForFlag;
        TextId = textId;
    }
}

/// <summary>Specifications for an observer position.</summary>
public interface IObserverPositionSpecifications
{
    /// <summary>Returns the specification for an observer position.</summary>
    /// <param name="observerPosition">The observer positions, from the enum ObserverPositions.</param>
    /// <returns>A record ObserverPositionDetails with the specification of the coordinate system.</returns>
    public ObserverPositionDetails DetailsForObserverPosition(ObserverPositions observerPosition);
}

/// <inheritdoc/>
public class ObserverPositionSpecifications : IObserverPositionSpecifications
{
    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Is thrown if the Observer Position was not recognized.</exception>
    ObserverPositionDetails IObserverPositionSpecifications.DetailsForObserverPosition(ObserverPositions observerPosition)
    {
        return observerPosition switch
        {
            // No specific flags for geocentric.
            ObserverPositions.HelioCentric => new ObserverPositionDetails(observerPosition, EnigmaConstants.SEFLG_HELCTR, "observerPosHelioCentric"),
            ObserverPositions.GeoCentric => new ObserverPositionDetails(observerPosition, 0, "observerPosGeoCentric"),
            ObserverPositions.TopoCentric => new ObserverPositionDetails(observerPosition, EnigmaConstants.SEFLG_TOPOCTR, "observerPosTopoCentric"),
            _ => throw new ArgumentException("Observer position unknown : " + observerPosition.ToString())
        };
    }

}