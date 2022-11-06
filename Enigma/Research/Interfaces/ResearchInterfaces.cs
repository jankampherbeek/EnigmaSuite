// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;


namespace Enigma.Research.Interfaces;



public interface IResearchResult
{
    public bool NoErrors { get; }
    public string Comments { get; }
}

public interface IMethodProcessor
{
    public IResearchResult ProcessData(string dataName);
}


public interface IMethodDefinitions
{
    public Ayanamshas Ayanamsha { get; }
    public ObserverPositions ObserverPosition { get; }
    public HouseSystems HouseSystem { get; }
    public ProjectionTypes ProjectionType { get; }
}








