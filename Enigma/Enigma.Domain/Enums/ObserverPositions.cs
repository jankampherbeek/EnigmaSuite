// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;

namespace Enigma.Domain.Enums;

/// <summary>Observer positions, the center points for the calculation of positions of celestial bodies.</summary>
public enum ObserverPositions
{
    HelioCentric = 0, GeoCentric = 1, TopoCentric = 2
}

public static class ObserverPositionsExtensions
{
    /// <summary>Retrieve dtails for observer Position.</summary>
    /// <param name="obsPos">The observer Position, is automatically filled.</param>
    /// <returns>Details for the observer Position.</returns>
    public static ObserverPositionDetails GetDetails(this ObserverPositions obsPos)
    {
        return obsPos switch
        {
            // No specific flags for geocentric.
            ObserverPositions.GeoCentric => new ObserverPositionDetails(obsPos, 0, "ref.enum.observerposition.geocentric"),
            ObserverPositions.HelioCentric => new ObserverPositionDetails(obsPos, EnigmaConstants.SEFLG_HELCTR, "ref.enum.observerposition.heliocentric"),
            ObserverPositions.TopoCentric => new ObserverPositionDetails(obsPos, EnigmaConstants.SEFLG_TOPOCTR, "ref.enum.observerposition.topocentric"),
            _ => throw new ArgumentException("Observer Position unknown : " + obsPos.ToString())
        };
    }


    /// <summary>Retrieve details for items in the enum ObserverPositions.</summary>
    /// <param name="...">The ObserverPosition, is automatically filled.</param>
    /// <returns>All details.</returns>
    public static List<ObserverPositionDetails> AllDetails(this ObserverPositions position)
    {
        var allDetails = new List<ObserverPositionDetails>();
        foreach (ObserverPositions currentPosition in Enum.GetValues(typeof(ObserverPositions)))
        {
            allDetails.Add(currentPosition.GetDetails());
        }
        return allDetails;
    }

    public static ObserverPositions ObserverPositionForIndex(this ObserverPositions observerPosition, int index)
    {
        foreach (ObserverPositions currentObsPos in Enum.GetValues(typeof(ObserverPositions)))
        {
            if ((int)currentObsPos == index) return currentObsPos;
        }
        throw new ArgumentException("Could not find Observer Position for index : " + index);
    }

}


/// <summary>Details for an observer Position.</summary>
/// <param name="Position">The observer Position.</param>
/// <param name="ValueForFlag">The value to construct the flags, as defined by the Swiss Ephemeris.</param>
/// <param name="TextId">Id to find a descriptive text in a resource bundle.</param>
public record ObserverPositionDetails(ObserverPositions Position, int ValueForFlag, string TextId);







