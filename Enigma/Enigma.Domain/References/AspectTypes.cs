﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.References;

/// <summary>Enumeration of supported aspects.</summary>
public enum AspectTypes
{
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
/// <param name="Text">Name for this Aspect.</param>
/// <param name="Glyph">Default Glyph.</param>
/// <param name="OrbFactor">Default weighting Factor for the calculation of the orb. Zero if the Aspect should not be used.</param>
public record AspectDetails(AspectTypes Aspect, double Angle, string Text, char Glyph, double OrbFactor);



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
            AspectTypes.Conjunction => new AspectDetails(aspect, 0.0, "Conjunction (0°)", 'B', 1.0),
            AspectTypes.Opposition => new AspectDetails(aspect, 180.0, "Opposition (180°)", 'C', 1.0),
            AspectTypes.Triangle => new AspectDetails(aspect, 120.0, "Triangle (120°)", 'D', 0.85),
            AspectTypes.Square => new AspectDetails(aspect, 90.0, "Square (90°)", 'E', 0.85),
            AspectTypes.Septile => new AspectDetails(aspect, 51.42857143, "Septile (51°25′43″)", 'N', 0.1),
            AspectTypes.Sextile => new AspectDetails(aspect, 60.0, "Sextile (60°)", 'F', 0.7),
            AspectTypes.Quintile => new AspectDetails(aspect, 72.0, "Quintile (72°)", 'K', 0.1),
            AspectTypes.SemiSextile => new AspectDetails(aspect, 30.0, "Semi-sextile (30°)", 'G', 0.2),
            AspectTypes.SemiSquare => new AspectDetails(aspect, 45.0, "Semi-square (45°)", 'I', 0.2),
            AspectTypes.BiQuintile => new AspectDetails(aspect, 144.0, "Bi-quintile (144°)", 'L', 0.1),
            AspectTypes.SemiQuintile => new AspectDetails(aspect, 36.0, "Semi-quintile (36°)", 'Ö', 0.0),
            AspectTypes.Inconjunct => new AspectDetails(aspect, 150.0, "Inconjunct (150°)", 'H', 0.2),
            AspectTypes.SesquiQuadrate => new AspectDetails(aspect, 135.0, "Sesquiquadrate (135°)", 'J', 0.2),
            AspectTypes.TriDecile => new AspectDetails(aspect, 108.0, "Tri-decile (108°)", 'Õ', 0.0),
            AspectTypes.BiSeptile => new AspectDetails(aspect, 102.85714286, "Bi-septile (102°51′26″)", 'Ú', 0.0),
            AspectTypes.TriSeptile => new AspectDetails(aspect, 154.28571429, "Tri-septile (154°17′09″)", 'Û', 0.0),
            AspectTypes.Novile => new AspectDetails(aspect, 40.0, "Novile (40°)", 'Ü', 0.0),
            AspectTypes.BiNovile => new AspectDetails(aspect, 80.0, "Bi-novile(80°)", 'Ñ', 0.0),
            AspectTypes.QuadraNovile => new AspectDetails(aspect, 160.0, "Quadra-novile(160°)", '|', 0.0),
            AspectTypes.Undecile => new AspectDetails(aspect, 33.0, "Undecile (33°)", 'ç', 0.0),
            AspectTypes.Centile => new AspectDetails(aspect, 100.0, "Centile (100°)", 'Ç', 0.0),
            AspectTypes.Vigintile => new AspectDetails(aspect, 18.0, "Vigintile (18°)", 'Ï', 0.0),
            _ => throw new ArgumentException("Aspect unknown : " + aspect)
        };
    }
 
    /// <summary>Retrieve details for items in the enum AspectTypes.</summary>
    /// <returns>All details.</returns>
    public static IEnumerable<AspectDetails> AllDetails()
    {
        return (from AspectTypes currentAspect in Enum.GetValues(typeof(AspectTypes)) select currentAspect.GetDetails()).ToList();
    }


    /// <summary>Find aspect type for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The aspect type for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static AspectTypes AspectTypeForIndex(int index)
    {
        foreach (AspectTypes currAspect in Enum.GetValues(typeof(AspectTypes)))
        {
            if ((int)currAspect == index) return currAspect;
        }
        throw new ArgumentException("Could not find aspect type");
    }

}


