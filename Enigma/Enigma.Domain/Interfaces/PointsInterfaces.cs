// Jan Kampherbeek, (c) 2023.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;
using Enigma.Domain.Enums;
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

    /// <summary>Find CelPoint for index from GeneralPoint.</summary>
    /// <param name="index">Index, including offset.</param>
    /// <exception cref="ArgumentException">Thrown if index is out of range.</exception>
    /// <returns>If found: the CelPoint for the given index.</returns>
    public CelPoints CelPointForIndex(int index);

    /// <summary>Find ZodiacPoint for index from GeneralPoint.</summary>
    /// <param name="index">Index, including offset.</param>
    /// <exception cref="ArgumentException">Thrown if index is out of range.</exception>
    /// <returns>If found: the ZodiacPoint for the given index.</returns>
    public ZodiacPoints ZodiacPointForIndex(int index);

    /// <summary>Find ArabicPoint for index from GeneralPoint.</summary>
    /// <param name="index">Index, including offset.</param>
    /// <exception cref="ArgumentException">Thrown if index is out of range.</exception>
    /// <returns>If found: the ArabicPoint for the given index.</returns>
    public ArabicPoints ArabicPointForIndex(int index);

    /// <summary>Find Mundane Point for index from GeneralPoint.</summary>
    /// <param name="index">Index, including offset.</param>
    /// <exception cref="ArgumentException">Thrown if index is out of range.</exception>
    /// <returns>If found: the Mundane Point for the given index.</returns>
    public MundanePoints MundanePointForIndex(int index);



}

/// <summary>Mapping to support analysis.</summary>
public interface IAnalysisPointsMapping
{
    /// <summary>
    /// Maps values from a calculated chart to a list of AnalysisPoint.
    /// </summary>
    /// <remarks>Does not yet support fix stars, or horizontal coordiantes. 
    /// For mundane positions only supports Mc and Asc.
    /// For zodiacal points only supports Zero Aries.</remarks>
    public List<AnalysisPoint> ChartToSingleAnalysisPoints(List<PointGroups> pointGroups, CoordinateSystems coordinateSystem, bool mainCoord, CalculatedChart chart);
}

public interface ICelPointToAnalysisPointMap
{
    public AnalysisPoint MapToAnalysisPoint(FullCelPointPos fullCelPointPos, PointGroups pointGroup, CoordinateSystems coordinateSystem, bool mainCoord);
}

public interface IMundanePointToAnalysisPointMap
{
    public AnalysisPoint MapToAnalysisPoint(MundanePoints mundanePoint, CuspFullPos cuspPos, PointGroups pointGroup, CoordinateSystems coordinateSystem, bool mainCoord);
}
