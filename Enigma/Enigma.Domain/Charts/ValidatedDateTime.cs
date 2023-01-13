// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Calc.DateTime;

namespace Enigma.Domain.Charts;


public record ValidatedDate(FullDate Date, bool Validated, string ErrorText);

public record ValidatedTime(FullTime Time, bool Validated, string ErrorText);




