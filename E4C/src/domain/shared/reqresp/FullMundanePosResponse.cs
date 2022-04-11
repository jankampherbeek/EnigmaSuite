// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.domain.shared.positions;

namespace E4C.domain.shared.reqresp;

///<inheritdoc/>
/// <summary>
/// Response with the results for the calculation of FullMundanePositions.
/// </summary>
/// <param name="FullMundanePositions">Nullable: Calculated positions if no error occurred, otherwise null.</param>
/// <param name="Success">True if no error occurred, otherwise false.</param>
/// <param name="ErrorText">Text about error(s), if they occurred.</param>
public record FullMundanePosResponse: ValidatedResponse
{

    public FullMundanePositions? FullMundanePositions { get; }

    public FullMundanePosResponse(FullMundanePositions? fullMundanePositions, bool success, string errorText) : base(success, errorText)
    {
        FullMundanePositions = fullMundanePositions;
    }

}