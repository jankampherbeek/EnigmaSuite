// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Shared.Domain;

namespace E4C.Shared.ReqResp;

///<inheritdoc/>
/// <summary>
/// Response with the results for the calculation of FullMundanePositions.
/// </summary>

public record FullHousesPosResponse : ValidatedResponse
{

    ///<inheritdoc/>
    public FullHousesPositions? FullHousesPositions { get; }

    /// <param name="fullHousesPositions">Nullable: Calculated positions if no error occurred, otherwise null.</param>
    /// <param name="success">True if no error occurred, otherwise false.</param>
    /// <param name="errorText">Text about error(s), if they occurred.</param>
    public FullHousesPosResponse(FullHousesPositions? fullHousesPositions, bool success, string errorText) : base(success, errorText)
    {
        FullHousesPositions = fullHousesPositions;
    }

}