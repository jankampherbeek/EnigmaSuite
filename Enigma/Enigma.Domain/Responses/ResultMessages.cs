// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Responses;

/// <summary>General result information.</summary>
/// <param name="ErrorCode">Errorcode, a constant retrieved from the class Enigma.Domain.Constants.ErrorCodes. Zero (ERR_NONE) if no error occurred.</param>
/// <param name="Message">Textual description of the result. Can contain error Message but also other texts (if no error occurs).</param>
/// [Obsolete("Use Key Value pair instead")]
public record ResultMessage(int ErrorCode, string Message);


