// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Research;

namespace Enigma.Domain.RequestResponse.Research;

/// <summary>Response for counting of aspects.</summary>
/// <param name="Request">The original request.</param>
/// <param name="Counts">All counted values.</param>
/// <param name="Totals">Totals of all aspects.</param>
public record CountAspectsResponse(CountAspectsRequest Request, List<AspectsPerChart> Counts, AspectTotals Totals);