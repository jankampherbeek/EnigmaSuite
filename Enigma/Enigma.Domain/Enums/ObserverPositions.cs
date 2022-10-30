// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Interfaces;

namespace Enigma.Domain.Enums;

/// <summary>Observer positions, the center points for the calculation of positions of celestial bodies.</summary>
public enum ObserverPositions
{
    HelioCentric = 0, GeoCentric = 1, TopoCentric = 2
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


/// <inheritdoc/>
public class ObserverPositionSpecifications : IObserverPositionSpecifications
{
    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Is thrown if the Observer Position was not recognized.</exception>
    public ObserverPositionDetails DetailsForObserverPosition(ObserverPositions observerPosition)
    {
        return observerPosition switch
        {
            // No specific flags for geocentric.
            ObserverPositions.GeoCentric => new ObserverPositionDetails(observerPosition, 0, "ref.enum.observerposition.geocentric"),
            ObserverPositions.HelioCentric => new ObserverPositionDetails(observerPosition, EnigmaConstants.SEFLG_HELCTR, "ref.enum.observerposition.heliocentric"),
            ObserverPositions.TopoCentric => new ObserverPositionDetails(observerPosition, EnigmaConstants.SEFLG_TOPOCTR, "ref.enum.observerposition.topocentric"),
            _ => throw new ArgumentException("Observer position unknown : " + observerPosition.ToString())
        };
    }

    public List<ObserverPositionDetails> AllObserverPositionDetails()
    {
        var allDetails = new List<ObserverPositionDetails>();
        foreach (ObserverPositions position in Enum.GetValues(typeof(ObserverPositions)))
        {
            allDetails.Add(DetailsForObserverPosition(position));
        }
        return allDetails;
    }

    public ObserverPositions ObserverPositionForIndex(int index)
    {
        foreach (ObserverPositions observerPosition in Enum.GetValues(typeof(ObserverPositions)))
        {
            if ((int)observerPosition == index) return observerPosition;
        }
        throw new ArgumentException("Could not find Observer Position for index : " + index);
    }

}

