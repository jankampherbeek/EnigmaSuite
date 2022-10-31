// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Research.Domain;
using Enigma.Research.Interfaces;

namespace Enigma.Research.Results;
public class CountSignResult : IResearchResult
{
    public bool NoErrors { get; }

    public string Comments { get; }

    public List<CelPointPerSign> CelPointsPerSign { get; }

    public List<SignPerCelPoint> SignsPerCelPoint { get; }

}