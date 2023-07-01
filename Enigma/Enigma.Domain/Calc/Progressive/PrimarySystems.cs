// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.Calc.Progressive;

/// <summary>Enum for systems for primary directions.</summary>
public enum PrimarySystems
{
    None = 0,
    PlacidusMun = 1,
    PlacidusUnderPoleMun = 2,
    RegiomontanusMun = 3,
    CampanusMun = 4,
    PlacidusZod = 5,
    PlacidusUnderPoleZod = 6,
    RegiomontanusZod = 7,
    CampanusZod = 8
}



/// <summary>Details for a primary system.</summary>
/// <param name="PrimarySystem">System for primary directions.</param>
/// <param name="TextId">Id to find a descriptive text in a resource bundle.</param>
public record PrimarySystemDetails(PrimarySystems PrimarySystem, string TextId);


/// <summary>Extension class for enum Primary Systems.</summary>
public static class PrimarySystemExtensions
{
    /// <summary>Retrieve details for primary system.</summary>
    /// <param name="primarySystem">The primary system.</param>
    /// <returns>Details for the primary system.</returns>
    public static PrimarySystemDetails GetDetails(this PrimarySystems primarySystem)
    {
        return primarySystem switch
        {
            PrimarySystems.PlacidusMun => new PrimarySystemDetails(primarySystem, "ref.enum.primarysystem.placidusmun"),
            PrimarySystems.PlacidusUnderPoleMun => new PrimarySystemDetails(primarySystem, "ref.enum.primarysystem.placidusunderpolemun"),
            PrimarySystems.RegiomontanusMun => new PrimarySystemDetails(primarySystem, "ref.enum.primarysystem.regiomontanusmun"),
            PrimarySystems.CampanusMun => new PrimarySystemDetails(primarySystem, "ref.enum.primarysystem.campanusmun"),
            PrimarySystems.PlacidusZod => new PrimarySystemDetails(primarySystem, "ref.enum.primarysystem.placiduszod"),
            PrimarySystems.PlacidusUnderPoleZod => new PrimarySystemDetails(primarySystem, "ref.enum.primarysystem.placidusunderpolezod"),
            PrimarySystems.RegiomontanusZod => new PrimarySystemDetails(primarySystem, "ref.enum.primarysystem.regiomontanuszod"),
            PrimarySystems.CampanusZod => new PrimarySystemDetails(primarySystem, "ref.enum.primarysystem.campanuszod"),


            _ => throw new ArgumentException("Primary system unknown : " + primarySystem.ToString())
        };
    }



    /// <summary>Retrieve details for items in the enum PrimarySystems.</summary>
    /// <returns>All details.</returns>
    public static List<PrimarySystemDetails> AllDetails(this PrimarySystems _)
    {
        var allDetails = new List<PrimarySystemDetails>();
        foreach (PrimarySystems primSysCurrent in Enum.GetValues(typeof(PrimarySystems)))
        {
            if (primSysCurrent != PrimarySystems.None)
            {
                allDetails.Add(primSysCurrent.GetDetails());
            }
        }
        return allDetails;
    }

    /// <summary>Find primary system for a given index.</summary>
    /// <param name="index">The index.</param>
    /// <returns>The primary system.</returns>
    /// <exception cref="ArgumentException">Thrown if the primary system could not be found.</exception>
    public static PrimarySystems PrimarySystemsForIndex(this PrimarySystems _, int index)
    {
        foreach (PrimarySystems primSysCurrent in Enum.GetValues(typeof(PrimarySystems)))
        {
            if ((int)primSysCurrent == index) return primSysCurrent;
        }
        string errorText = "PrimaryKeys.PrimarySystemsForIndex(): Could not find Primary System for index : " + index;
        Log.Error(errorText);
        throw new ArgumentException(errorText);
    }

}
