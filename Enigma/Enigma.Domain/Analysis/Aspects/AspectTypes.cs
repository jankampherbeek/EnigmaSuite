// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Analysis.Aspects;

/// <summary>Enumeration of supported aspects.</summary>
public enum AspectTypes
{
    None = -1,
    Conjunction = 0,
    Opposition = 1,
    Triangle = 2,
    Square = 3,
    Septile = 4,
    Sextile = 5,
    Quintile = 6,
    SemiSextile = 7,
    SemiSquare = 8,
    SemiQuintile = 9,
    BiQuintile = 10,
    Inconjunct = 11,
    SesquiQuadrate = 12,
    TriDecile = 13,
    BiSeptile = 14,
    TriSeptile = 15,
    Novile = 16,
    BiNovile = 17,
    QuadraNovile = 18,
    Undecile = 19,
    Centile = 20,
    Vigintile = 21
}


/// <summary>Details for an Aspect</summary>
/// <param name="Aspect">Aspect from enum 'AspectTypes'.</param>
/// <param name="Angle">Angle for this Aspect.</param>
/// <param name="TextId">Text for this Aspect in the resource bundle.</param>
/// <param name="Glyph">Default Glyph.</param>
/// <param name="OrbFactor">Default weighting Factor for the calculation of the orb. Zero if the Aspect should not be used.</param>
public record AspectDetails(AspectTypes Aspect, double Angle, string TextId, char Glyph, double OrbFactor);



/// <summary>Extension class for enum AspectTypes.</summary>
public static class AspectTypesExtensions
{
    /// <summary>Retrieve details for aspect type.</summary>
    /// <param name="aspect">The aspect type.</param>
    /// <returns>Details for the aspect type.</returns>
    public static AspectDetails GetDetails(this AspectTypes aspect)
    {
        return aspect switch
        {
            AspectTypes.Conjunction => new AspectDetails(aspect, 0.0, "ref.enum.aspect.conjunction", 'B', 1.0),
            AspectTypes.Opposition => new AspectDetails(aspect, 180.0, "ref.enum.aspect.opposition", 'C', 1.0),
            AspectTypes.Triangle => new AspectDetails(aspect, 120.0, "ref.enum.aspect.triangle", 'D', 0.85),
            AspectTypes.Square => new AspectDetails(aspect, 90.0, "ref.enum.aspect.square", 'E', 0.85),
            AspectTypes.Septile => new AspectDetails(aspect, 51.42857143, "ref.enum.aspect.septile", 'N', 0.1),
            AspectTypes.Sextile => new AspectDetails(aspect, 60.0, "ref.enum.aspect.sextile", 'F', 0.7),
            AspectTypes.Quintile => new AspectDetails(aspect, 72.0, "ref.enum.aspect.quintile", 'K', 0.1),
            AspectTypes.SemiSextile => new AspectDetails(aspect, 30.0, "ref.enum.aspect.semisextile", 'G', 0.2),
            AspectTypes.SemiSquare => new AspectDetails(aspect, 45.0, "ref.enum.aspect.semisquare", 'I', 0.2),
            AspectTypes.BiQuintile => new AspectDetails(aspect, 144.0, "ref.enum.aspect.biquintile", 'L', 0.1),
            AspectTypes.SemiQuintile => new AspectDetails(aspect, 36.0, "ref.enum.aspect.semiquintile", 'Ö', 0.0),
            AspectTypes.Inconjunct => new AspectDetails(aspect, 150.0, "ref.enum.aspect.inconjunct", 'H', 0.2),
            AspectTypes.SesquiQuadrate => new AspectDetails(aspect, 135.0, "ref.enum.aspect.sesquiquadrate", 'J', 0.2),
            AspectTypes.TriDecile => new AspectDetails(aspect, 108.0, "ref.enum.aspect.tridecile", 'Õ', 0.0),
            AspectTypes.BiSeptile => new AspectDetails(aspect, 102.85714286, "ref.enum.aspect.biseptile", 'Ú', 0.0),
            AspectTypes.TriSeptile => new AspectDetails(aspect, 154.28571429, "ref.enum.aspect.triseptile", 'Û', 0.0),
            AspectTypes.Novile => new AspectDetails(aspect, 40.0, "ref.enum.aspect.novile", 'Ü', 0.0),
            AspectTypes.BiNovile => new AspectDetails(aspect, 80.0, "ref.enum.aspect.binovile", 'Ñ', 0.0),
            AspectTypes.QuadraNovile => new AspectDetails(aspect, 160.0, "ref.enum.aspect.quadranovile", '|', 0.0),
            AspectTypes.Undecile => new AspectDetails(aspect, 33.0, "ref.enum.aspect.undecile", 'ç', 0.0),
            AspectTypes.Centile => new AspectDetails(aspect, 100.0, "ref.enum.aspect.centile", 'Ç', 0.0),
            AspectTypes.Vigintile => new AspectDetails(aspect, 18.0, "ref.enum.aspect.vigintile", 'Ï', 0.0),
            _ => throw new ArgumentException("Aspect unknown : " + aspect.ToString())
        };
    }

    /// <summary>Retrieve details for items in the enum AspectTypes.</summary>
    /// <returns>All details.</returns>
    public static List<AspectDetails> AllDetails(this AspectTypes _)
    {
        var allDetails = new List<AspectDetails>();
        foreach (AspectTypes currentAspect in Enum.GetValues(typeof(AspectTypes)))
        {
            allDetails.Add(currentAspect.GetDetails());
        }
        return allDetails;
    }


    /// <summary>Find aspect type for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The aspect type for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static AspectTypes AspectTypeForIndex(this AspectTypes _, int index)
    {
        foreach (AspectTypes currAspect in Enum.GetValues(typeof(AspectTypes)))
        {
            if ((int)currAspect == index) return currAspect;
        }
        throw new ArgumentException("Could not find aspect type for index : " + index);
    }

}



