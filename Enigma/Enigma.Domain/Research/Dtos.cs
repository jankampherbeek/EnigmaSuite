// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Engima.Research.Domain;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;

namespace Enigma.Research.Domain;


/// <summary>Identification for a point that has a position planets, houses, zodiac points etc.</summary>
public record ResearchPoint
{
    public int Id { get; }
    public PointTypes PointTypes { get; }
    public CalculationTypes CalculationType { get; }

    /// <summary>Create a point and use a redefinition for the original id.</summary>
    /// <param name="id">Id that identifies the point.</param>
    /// <param name="pointType">The type of point.</param>
    /// <parma name="calculationType">The type of calculation for this point.</parma>
    /// <remarks>For Celestial pionts the id is the same as the original id, 
    /// for special mundane points the id's are 1000 plus the value as in the enum MundanePoints,
    /// for cusps the values are 2000 plus the value of the cusp (1..n),
    /// for zodiacal points the values are 3000 plus the value as in the enum ZodiacalPoints.</remarks>
    public ResearchPoint(int id, PointTypes pointType, CalculationTypes calculationType)
    {
        Id = id;
        PointTypes = pointType;
        CalculationType = calculationType;
    }
}

public record CelPointPerSign
{
    public CelPoints CelPoint { get; }
    int[] PositionsPerSign { get; }

    public CelPointPerSign(CelPoints celPoint, int[] positionsPerSign)
    {
        CelPoint = celPoint;
        PositionsPerSign = positionsPerSign;
    }
}

public record SignPerCelPoint
{
    public int SignIndex { get; }
    public int[] CelPointIndexes { get; }

    public SignPerCelPoint(int signIndex, int[] celPointIndexes)
    {
        SignIndex = signIndex;
        CelPointIndexes = celPointIndexes;
    }

}


/// <summary>Resulting counts from a test.</summary>
/// <param name="Point">The research point for which the counting has been performed.</param>
/// <param name="Counts">The totals in the sequence of the elements that have been checked.</param>
public record ResearchPointCounts(ResearchPoint Point, List<int> Counts);
