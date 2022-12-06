// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.Analysis;

/// <summary>
/// Arabic Points or Hellenistic Lots
/// </summary>
public enum ArabicPoints
{
    FortunaSect = 1, FortunaNoSect = 2
}

public static class ArabicPointsExtensions
{
    /// <summary>Retrieve details for lots/Arabic points.</summary>
    /// <param name="arabicPoint">The arabic point, is automatically filled.</param>
    /// <returns>Details for the Arabic point.</returns>
    public static ArabicPointDetails GetDetails(this ArabicPoints arabicPoint)
    {
        return arabicPoint switch
        {
            ArabicPoints.FortunaSect => new ArabicPointDetails(arabicPoint, "ref.enum.arabicpoint.fortunasect"),
            ArabicPoints.FortunaNoSect => new ArabicPointDetails(arabicPoint, "ref.enum.arabicpoint.fortunanosect"),
            _ => throw new ArgumentException("Arabic Point unknown : " + arabicPoint.ToString())
        } ;
}

    /// <summary>Retrieve details for items in the enum ArabicPoints.</summary>
    /// <param name="arabicPoint">The arabic point, is automatically filled.</param>
    /// <returns>All details.</returns>
    public static List<ArabicPointDetails> AllDetails(this ArabicPoints arabicPoint)
    {
        var allDetails = new List<ArabicPointDetails>();
        foreach (ArabicPoints currentArabicPoint in Enum.GetValues(typeof(ArabicPoints)))
        {
            allDetails.Add(currentArabicPoint.GetDetails());
        }
        return allDetails;
    }	

    private static void HandleError(ArabicPoints arabicPoint)
    {
        string errorTxt = "Arabic Point unknown : " + arabicPoint.ToString();
        Log.Error(errorTxt);
        throw new ArgumentException(errorTxt);
    }
}


/// <summary>Details for Arabic Points.</summary>
/// <param name="ArabicPoint">Instance from enum ArabicPoints.</param>
/// <param name="TextId">Id to find a descriptive text in a resource bundle.</param>
public record ArabicPointDetails(ArabicPoints ArabicPoint, string TextId);

