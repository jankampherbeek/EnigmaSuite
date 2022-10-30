// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.RequestResponse;

/// <summary>Request to calculate obliquity.</summary>
public record ObliquityRequest
{
    public double JdUt { get; }


    public ObliquityRequest(double jdUt)
    {
        JdUt = jdUt;
    }

}