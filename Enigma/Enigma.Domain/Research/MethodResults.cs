// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Configuration;

namespace Enigma.Domain.Research;



public abstract class MethodResult
{

    public GeneralCountRequest methodPerformer { get; set; }
    public AstroConfig appliedConfig { get; set; }
}