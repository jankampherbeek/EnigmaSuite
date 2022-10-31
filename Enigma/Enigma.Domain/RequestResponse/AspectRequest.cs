// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Charts;

namespace Enigma.Domain.RequestResponse;

public record AspectRequest
{
    public CalculatedChart CalcChart { get; }

    public AspectRequest(CalculatedChart calculatedChart)
    {
        CalcChart = calculatedChart;
    }
}

