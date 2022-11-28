// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.



using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Handlers.Interfaces;


public interface IHousesHandler
{
    public FullHousesPosResponse CalcHouses(FullHousesPosRequest request);
}
