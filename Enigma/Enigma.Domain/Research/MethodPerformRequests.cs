// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.



using Enigma.Domain.Configuration;
using Enigma.Research.Domain;

namespace Enigma.Domain.Research;


/// <summary>Request to perform a test.</summary>
/// <param name="ProjectName">Name for project, used to find data files.</param>
/// <param name="Method">The research method to be used.</param>
/// <param name="UseControlGroup">True if controlgroup data needs to be calculated, false for test data.</param>
/// <param name="PointsSelection">All points that need to be tested.</param>
/// <param name="Config">Currently active configuration.</param>
public record GeneralCountRequest(string ProjectName, ResearchMethods Method, bool UseControlGroup, ResearchPointsSelection PointsSelection, AstroConfig Config);


/// <summary>Request to perform a test using aspects.</summary>
/// <param name="ProjectName">Name for project, used to find data files.</param>
/// <param name="Method">The research method to be used.</param>
/// <param name="UseControlGroup">True if controlgroup data needs to be calculated, false for test data.</param>
/// <param name="PointsSelection">All points that need to be tested.</param>
/// <param name="Config">Currently active configuration.</param>
/// <param name="Aspects">All aspects that need to be tested.</param>
public record CountAspectsPerformRequest(string ProjectName, ResearchMethods Method, bool UseControlGroup, ResearchPointsSelection PointsSelection, AstroConfig Config, List<AspectSpecs> Aspects) :
    GeneralCountRequest(ProjectName, Method, UseControlGroup, PointsSelection, Config);

/// <summary>Request to perform a test using midpoints. Checks for occupied midpoints (conjunction and opposition).</summary>
/// <param name="ProjectName">Name for project, used to find data files.</param>
/// <param name="Method">The research method to be used.</param>
/// <param name="UseControlGroup">True if controlgroup data needs to be calculated, false for test data.</param>
/// <param name="PointsSelection">All points that need to be tested.</param>
/// <param name="Config">Currently active configuration.</param>
/// <param name="DivisionForDial">The division to construct the dial: 4 = 90 degrees etc.</param>
public record CountMidpointsPerformRequest(string ProjectName, ResearchMethods Method, bool UseControlGroup, ResearchPointsSelection PointsSelection, AstroConfig Config, int DivisionForDial) :
    GeneralCountRequest(ProjectName, Method, UseControlGroup, PointsSelection, Config);

/// <summary>Request to perform a test using harmonics. Checks for harmonic positions conjunct radix positions.</summary>
/// <param name="ProjectName">Name for project, used to find data files.</param>
/// <param name="Method">The research method to be used.</param>
/// <param name="UseControlGroup">True if controlgroup data needs to be calculated, false for test data.</param>
/// <param name="PointsSelection">All points that need to be tested.</param>
/// <param name="Config">Currently active configuration.</param>
/// <param name="HarmonicNumber">The harmonic number.</param>
public record CountHarmonicConjunctionsPerformRequest(string ProjectName, ResearchMethods Method, bool UseControlGroup, ResearchPointsSelection PointsSelection, AstroConfig Config, double HarmonicNumber) :
    GeneralCountRequest(ProjectName, Method, UseControlGroup, PointsSelection, Config);

