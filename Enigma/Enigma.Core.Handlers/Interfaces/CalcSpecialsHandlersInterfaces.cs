// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.



using Enigma.Domain.RequestResponse;

public interface IObliquityHandler
{
    ObliquityResponse CalcObliquity(ObliquityRequest obliquityRequest);
}
