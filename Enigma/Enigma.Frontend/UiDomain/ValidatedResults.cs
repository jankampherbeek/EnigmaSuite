// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.DateTime;

namespace Enigma.Frontend.UiDomain;

public abstract record ValidatedResult(bool Validated, string ErrorText);


public record ValidatedDate(FullDate Date, bool Validated, string ErrorText) : ValidatedResult(Validated, ErrorText);

public record ValidatedTime(FullTime Time, bool Validated, string ErrorText) : ValidatedResult(Validated, ErrorText);




