// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Analysis;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;
using Enigma.Domain.Enums;
using Enigma.Domain.Points;

namespace Enigma.Domain.Interfaces;

public interface IOrbDefinitions
{
    public CelPointOrb DefineCelPointOrb(CelPoints celPoint);
    public MundanePointOrb DefineMundanePointOrb(string mundanePoint);
}




