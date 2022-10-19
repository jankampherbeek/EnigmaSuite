// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Engima.Research.Domain;
using Enigma.Domain.CalcVars;
using Enigma.Domain.Messages;

namespace Enigma.Research.Interfaces;


public interface IResearchProject
{
    public string Name { get; }
    public string Identification { get; }
    public string Description { get; }
    public string DataName {  get; }
    public string CreationDate { get; }
    public ControlGroupTypes ControlGroupType { get; }

}


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
    public Ayanamshas Ayanamsha { get;  }
    public ObserverPositions ObserverPosition { get; }
    public HouseSystems HouseSystem { get; }
    public ProjectionTypes ProjectionType { get; }
}

public interface IControlGroupCreator
{
    public ResultMessage DefineControlGroup(string dataName, ControlGroupTypes controlGroupType);

}