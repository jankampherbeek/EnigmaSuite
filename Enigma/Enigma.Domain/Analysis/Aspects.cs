// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Analysis;

/// <summary>
/// Supported aspects.
/// </summary>
public enum Aspects
{
    Conjunction,
    Opposition,
    Triangle,
    Square,
    Septile,
    Sextile,
    Quintile,
    SemiSextile,
    SemiSquare,
    BiQuintile,
    Inconjunct,
    SesquiQuadrate,
    Decile,
    TriDecile,
    BiSeptile,
    TriSeptile,
    Novile,
    BiNovile,
    QuadraNovile,
    Undecile,
    Centile,
    Vigintile
}

/// <summary>
/// Details for an aspect
/// </summary>
public record AspectDetails
{
    public readonly Aspects Aspect;
    public readonly double Angle;
    public readonly string TextId;
    public readonly string Glyph;
    public readonly double OrbFactor;

    /// <summary>
    /// Constructs details for an aspect.
    /// </summary>
    /// <param name="aspect">Aspect from enum 'Aspects'.</param>
    /// <param name="angle">Angle for this aspect.</param>
    /// <param name="textId">Text for this aspect in the resource bundle.</param>
    /// <param name="glyph">Default glyph.</param>
    /// <param name="orbFactor">Default weighting Factor for the calculation of the orb. Zero is the aspect should not be used.</param>
    public AspectDetails(Aspects aspect, double angle, string textId, string glyph, double orbFactor)
    {
        Aspect = aspect;
        Angle = angle;
        TextId = textId;
        Glyph = glyph;
        OrbFactor = orbFactor;
    }
}

/// <summary>
/// Specifications for an aspect.
/// </summary>
public interface IAspectSpecifications
{
    /// <summary>
    /// Defines the specifications for an aspect.
    /// </summary>
    /// <param name="aspect">The aspect for which the details are defined.</param>
    /// <returns>A record AspectDetaisl with the required information.</returns>
    public AspectDetails DetailsForAspect(Aspects aspect);
}


/// <inheritdoc/>
public class AspectSpecifications : IAspectSpecifications
{
    public AspectDetails DetailsForAspect(Aspects aspect)
    {
        return aspect switch
        {
            Aspects.Conjunction => new AspectDetails(aspect, 0.0, "ref.enum.aspect.conjunction", "B", 1.0),
            Aspects.Opposition => new AspectDetails(aspect, 180.0, "ref.enum.aspect.opposition", "C", 1.0),
            Aspects.Triangle => new AspectDetails(aspect, 120.0, "ref.enum.aspect.triangle", "D", 0.85),
            Aspects.Square => new AspectDetails(aspect, 90.0, "ref.enum.aspect.square", "E", 0.85),
            Aspects.Septile => new AspectDetails(aspect, 51.42857143, "ref.enum.aspect.septile", "N", 0.0),
            Aspects.Sextile => new AspectDetails(aspect, 60.0, "ref.enum.aspect.sextile", "F", 0.7),
            Aspects.Quintile => new AspectDetails(aspect, 72.0, "ref.enum.aspect.quintile", "K", 0.2),
            Aspects.SemiSextile => new AspectDetails(aspect, 30.0, "ref.enum.aspect.semisextile", "G", 0.2),
            Aspects.SemiSquare => new AspectDetails(aspect, 45.0, "ref.enum.aspect.semisquare", "I", 0.2),
            Aspects.BiQuintile => new AspectDetails(aspect, 144.0, "ref.enum.aspect.biquintile", "L", 0.2),
            Aspects.Inconjunct => new AspectDetails(aspect, 150.0, "ref.enum.aspect.inconjunct", "H", 0.3),
            Aspects.SesquiQuadrate => new AspectDetails(aspect, 135.0, "ref.enum.aspect.sesquiquadrate", "J", 0.2),
            Aspects.Decile => new AspectDetails(aspect, 36.0, "ref.enum.aspect.decile", "Ö", 0.0),
            Aspects.TriDecile => new AspectDetails(aspect, 108.0, "ref.enum.aspect.tridecile", "Õ", 0.0),
            Aspects.BiSeptile => new AspectDetails(aspect, 102.85714286, "biseptile", "Ú", 0.0),
            Aspects.TriSeptile => new AspectDetails(aspect, 154.28571429, "triseptile", "Û", 0.0),
            Aspects.Novile => new AspectDetails(aspect, 40.0, "novile", "Ü", 0.0),
            Aspects.BiNovile => new AspectDetails(aspect, 80.0, "binovile", "Ñ", 0.0),
            Aspects.QuadraNovile => new AspectDetails(aspect, 160.0, "quadranovile", "|", 0.0),
            Aspects.Undecile => new AspectDetails(aspect, 33.0, "undecile", "ç", 0.0),
            Aspects.Centile => new AspectDetails(aspect, 100.0, "centile", ",Ç", 0.0),
            Aspects.Vigintile => new AspectDetails(aspect, 18.0, "vigintile", "Ï", 0.0),
            _ => throw new ArgumentException("Aspect unknown : " + aspect.ToString())
        };
    }
}

