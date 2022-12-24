// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Persistency;
using Enigma.Domain.Research;
using Enigma.Research.Domain;

namespace Enigma.Core.Handlers.Research.Interfaces;

/// <summary>Calculates the positions for multiple charts to be used in research methods.</summary>
public interface ICalculatedResearchPositions
{
    /// <summary>Calculate the positions.</summary>
    /// <param name="standardInput">Contains the positions.</param>
    /// <returns>The calculated charts.</returns>
    public List<CalculatedResearchChart> CalculatePositions(StandardInput standardInput);
}


/// <summary>Counting for points in parts of the zodiac (e.g. signs, decanates etc.</summary>
public interface IPointsInZodiacPartsCounting
{
    /// <summary>Perform a count for points in parts of the zodiac.</summary>
    /// <param name="charts">The calculatred charts to check.</param>
    /// <param name="request">The original request.</param>
    /// <returns>The calculated counts.</returns>
    public CountOfPartsResponse CountPointsInParts(List<CalculatedResearchChart> charts, GeneralCountRequest request);
}