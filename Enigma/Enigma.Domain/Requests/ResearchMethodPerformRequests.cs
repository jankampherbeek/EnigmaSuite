// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Domain.Requests;


/// <summary>Request to perform a test.</summary>
/// <param name="ProjectName">Name for project, used to find data files.</param>
/// <param name="Method">The research method to be used.</param>
/// <param name="UseControlGroup">True if controlgroup data needs to be calculated, false for test data.</param>
/// <param name="PointSelection">All points that need to be tested.</param>
/// <param name="Config">Currently active configuration.</param>
public record GeneralResearchRequest(string ProjectName, ResearchMethods Method, bool UseControlGroup, 
    ResearchPointSelection PointSelection, AstroConfig Config);




/// <summary>Request to perform a test using midpoints. Checks for occupied midpoints (conjunction and opposition).</summary>
/// <param name="ProjectName">Name for project, used to find data files.</param>
/// <param name="Method">The research method to be used.</param>
/// <param name="UseControlGroup">True if controlgroup data needs to be calculated, false for test data.</param>
/// <param name="PointSelection">All points that need to be tested.</param>
/// <param name="Config">Currently active configuration.</param>
/// <param name="DivisionForDial">The division to construct the dial: 4 = 90 degrees etc.</param>
/// <param name="Orb">Orb.</param>
public record CountOccupiedMidpointsRequest(string ProjectName, ResearchMethods Method, bool UseControlGroup, 
    ResearchPointSelection PointSelection, AstroConfig Config, int DivisionForDial, double Orb) :
    GeneralResearchRequest(ProjectName, Method, UseControlGroup, PointSelection, Config);

/// <summary>Request to perform a test using midpoints in declination.</summary>
/// <param name="ProjectName">Name for project, used to find data files.</param>
/// <param name="Method">The research method to be used.</param>
/// <param name="UseControlGroup">True if controlgroup data needs to be calculated, false for test data.</param>
/// <param name="PointSelection">All points that need to be tested.</param>
/// <param name="Config">Currently active configuration.</param>
/// <param name="Orb">Orb</param>
public record CountOccupiedMidpointsDeclinationRequest(string ProjectName, ResearchMethods Method, bool UseControlGroup, 
    ResearchPointSelection PointSelection, AstroConfig Config, double Orb) :
    GeneralResearchRequest(ProjectName, Method, UseControlGroup, PointSelection, Config);




/// <summary>Request to perform a test using harmonics. Checks for harmonic positions conjunct radix positions.</summary>
/// <param name="ProjectName">Name for project, used to find data files.</param>
/// <param name="Method">The research method to be used.</param>
/// <param name="UseControlGroup">True if controlgroup data needs to be calculated, false for test data.</param>
/// <param name="PointSelection">All points that need to be tested.</param>
/// <param name="Config">Currently active configuration.</param>
/// <param name="HarmonicNumber">The harmonic number.</param>
/// <param name="Orb">Orb.</param>
public record CountHarmonicConjunctionsRequest(string ProjectName, ResearchMethods Method, bool UseControlGroup, 
    ResearchPointSelection PointSelection, AstroConfig Config, double HarmonicNumber, double Orb) :
    GeneralResearchRequest(ProjectName, Method, UseControlGroup, PointSelection, Config);

