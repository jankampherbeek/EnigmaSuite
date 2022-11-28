// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Engima.Research.Domain;
using Enigma.Domain.AstronCalculations;
using Enigma.Research.Domain;

namespace Enigma.Domain.Research;

/// <summary>Request to perform a test.</summary>
/// <param name="ResearchPoints">The points that partake in the test.</param>
/// <param name="Settings">The settings for the test, a child of the abstract record ResearchCalcSettings.</param>
/// <param name="DefinedLocation">The location.</param>
/// <param name="ResearchMethod">The research method that will be applied.</param>
/// <param name="PathToProjectFolder">Full path to the project folder where the Json data is located.</param>
public record ResearchRequest(List<ResearchPoint> ResearchPoints,
    ResearchCalcSettings Settings,
    Location DefinedLocation,
    ResearchMethods ResearchMethod,
    string PathToProjectFolder);


/// <summary>Response of a performed test.</summary>
/// <param name="Report">Textual presentation of the results with included references to the resouce bundle.</param>
/// <param name="Counts">All coounted values.</param>
public record ResearchResponse(List<string> Report, List<ResearchPointCounts> Counts);

