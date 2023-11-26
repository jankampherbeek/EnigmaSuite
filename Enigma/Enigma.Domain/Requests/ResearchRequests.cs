// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Research;

namespace Enigma.Domain.Requests;


/// <summary>Request to perform a test where the occurrence of specific aspects, that involve a specific set of points, are counted.</summary>
/// <param name="ProjectName">Name for project, used to find data files.</param>
/// <param name="Method">The research method to be used.</param>
/// <param name="UseControlGroup">True if controlgroup data needs to be calculated, false for test data.</param>
/// <param name="PointSelection">All points that need to be tested.</param>
/// <param name="Aspects">All aspects that need to be tested.</param>
/// <param name="Config">Currently active configuration.</param>
public record CountAspectsRequest(string ProjectName, ResearchMethods Method, bool UseControlGroup, ResearchPointSelection PointSelection, List<AspectConfigSpecs> Aspects, AstroConfig Config);
