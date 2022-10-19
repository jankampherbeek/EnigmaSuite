// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.CalcVars;

/// <summary>
/// Arabic Points or Hellenistic Lots
/// </summary>
public enum ArabicPoints
{
    FortunaSect = 1, FortunaNoSect = 2
}

/// <summary>
/// Details for Arabic Points
/// </summary>
public record ArabicPointDetails
{
    readonly public ArabicPoints ArabicPoint;
    readonly public string TextId;

    /// <param name="arabicPoint">Instance from enum ArabicPoints.</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
    public ArabicPointDetails(ArabicPoints arabicPoint, string textId)
    {
        ArabicPoint = arabicPoint;
        TextId = textId;
    }
}

/// <summary>
/// Specifications for Arabic Points/Hellenistic Lots.
/// </summary>
public interface IArabicPointSpecifications
{
    /// <summary>Returns the specification for an Arabic point.</summary>
    /// <param name="arabicPoint">The Arabic point, from the enum ArabicPoints.</param>
    /// <returns>A record ArabicPointDetails with the specification of the Arabic point.</returns>
    public ArabicPointDetails DetailsForArabicPoint(ArabicPoints arabicPoint);
    public List<ArabicPointDetails> AllArabicPointDetails();
}

/// <inheritdoc/>
public class ArabicPointSpecifications : IArabicPointSpecifications
{
    /// <inheritdoc/>
    public ArabicPointDetails DetailsForArabicPoint(ArabicPoints arabicPoint) => arabicPoint switch
    {
        ArabicPoints.FortunaSect => new ArabicPointDetails(arabicPoint, "ref.enum.arabicpoint.fortunasect"),
        ArabicPoints.FortunaNoSect => new ArabicPointDetails(arabicPoint, "ref.enum.arabicpoint.fortunanosect"),
        _ => throw new ArgumentException("Arabic Point unknown : " + arabicPoint.ToString())
    };


    /// <inheritdoc/>
    public List<ArabicPointDetails> AllArabicPointDetails()
    {
        var allDetails = new List<ArabicPointDetails>();
        foreach (ArabicPoints arabicPoint in Enum.GetValues(typeof(ArabicPoints)))
        {
            allDetails.Add(DetailsForArabicPoint(arabicPoint));
        }
        return allDetails;
    }

}


