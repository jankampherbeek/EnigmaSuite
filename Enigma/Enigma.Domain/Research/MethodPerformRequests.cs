// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.



using Enigma.Domain.Configuration;
using Enigma.Research.Domain;

namespace Enigma.Domain.Research;


/// <summary>Request to perform a test.</summary>
/// <param name="Method">The research method to be used.</param>
/// <param name="Points">All points that need to be tested.</param>
public record MethodPerformRequest(ResearchMethods Method, List<ResearchPoint> Points);


/// <summary>Request to perform a test using aspects.</summary>
/// <param name="Method">The research method to be used.</param>
/// <param name="Points">All points that need to be tested.</param>
/// <param name="Aspects">All aspects that need to be tested.</param>
public record CountAspectsPerformRequest(ResearchMethods Method, List<ResearchPoint> Points, List<AspectSpecs> Aspects) : MethodPerformRequest(Method, Points);

/// <summary>Request to perform a test using midpoints. Checks for occupied midpoints (conjunction and opposition).</summary>
/// <param name="Method">The research method to be used.</param>
/// <param name="Points">All points that need to be tested.</param>
/// <param name="DivisionForDial">The division to construct the dial: 4 = 90 degrees etc.</param>
public record CountMidpointsPerformRequest(ResearchMethods Method, List<ResearchPoint> Points, int DivisionForDial) : MethodPerformRequest(Method, Points);

/// <summary>Request to perform a test using harmonics. Checks for harmonic positions conjunct radix positions.</summary>
/// <param name="Method">The research method to be used.</param>
/// <param name="Points">All points that need to be tested.</param>
/// <param name="HarmonicNumber">The harmonic number.</param>
public record CountHarmonicConjunctionsPerformRequest(ResearchMethods Method, List<ResearchPoint> Points, double HarmonicNumber) : MethodPerformRequest(Method, Points);

