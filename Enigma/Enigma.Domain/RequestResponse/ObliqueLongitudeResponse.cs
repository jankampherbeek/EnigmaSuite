// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;

namespace Enigma.Domain.RequestResponse;

public record ObliqueLongitudeResponse : ValidatedResponse
{
    public List<NamedEclipticLongitude> CelPointLongitudes { get; }

    public ObliqueLongitudeResponse(List<NamedEclipticLongitude> celPointLongitudes, bool success, string errorText) : base(success, errorText)
    {
        CelPointLongitudes = celPointLongitudes;
    }
}
