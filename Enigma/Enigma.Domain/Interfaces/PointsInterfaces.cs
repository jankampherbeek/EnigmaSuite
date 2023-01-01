// Jan Kampherbeek, (c) 2023.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Points;

namespace Enigma.Domain.Interfaces;

/// <summary>Mapping for GeneralPoints to specific points, v.v.</summary>
public interface IPointMappings
{

    /// <summary>Find index for GeneralPoint from celestial point.</summary>
    /// <param name="point">The celestial point.</param>
    /// <returns>The index.</returns>
    public int IndexForCelPoint(CelPoints point);

    /// <summary>Find index for GeneralPoint from zodiac point.</summary>
    /// <param name="point">The zodiac point.</param>
    /// <returns>The index.</returns>
    public int IndexForZodiacPoint(ZodiacPoints point);

    /// <summary>Find index for GeneralPoint from Arabic Point.</summary>
    /// <param name="point">The Arabic Point.</param>
    /// <returns>The index.</returns>
    public int IndexForArabicPoint(ArabicPoints point);

    /// <summary>Find index for GeneralPoint from mundane point.</summary>
    /// <param name="point">The mundane point.</param>
    /// <returns>The index.</returns>
    public int IndexForMundanePoint(MundanePoints point);

    /// <summary>Find index for GeneralPoint from cusp.</summary>
    /// <param name="point">The number of the cusp.</param>
    /// <returns>The index.</returns>
    public int IndexForCusp(int cuspNr);

    /// <summary>Find GeneralPoint for a specific index.</summary>
    /// <param name="index">The index.</param>
    /// <returns>If the index was found: The GeneralPoint. Otherwise: GeneralPoints.None.</returns>
    public GeneralPoint GeneralPointForIndex(int index);

    /// <summary>Find the point type for a specific index.</summary>
    /// <param name="index">The index.</param>
    /// <returns>If the index was found: The PointType. Otherwise: PointType.None.</returns>
    public PointTypes PointTypeForIndex(int index);


}
