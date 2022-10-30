// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Enums;

namespace Enigma.Research.Domain;

public record CelPointPerSign
{
    public SolarSystemPoints CelPoint { get; }
    int[] PositionsPerSign { get; }

    public CelPointPerSign(SolarSystemPoints celPoint, int[] positionsPerSign)
    {
        CelPoint = celPoint;
        PositionsPerSign = positionsPerSign;
    }
}

public record SignPerCelPoint
{
    public int SignIndex { get; }
    public int[] CelPointIndexes { get; }

    public SignPerCelPoint(int signIndex, int[] celPointIndexes )
    {
        SignIndex = signIndex;
        CelPointIndexes = celPointIndexes;
    }

}
