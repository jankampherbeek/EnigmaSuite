// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Engima.Research.Domain;
using Enigma.Research.Interfaces;

namespace Enigma.Research.Projects;

public class CountProject : IResearchProject
{
    public string Name { get;  }
    public string Description { get; }
    public string DataName { get; } 
    public string CreationDate { get; } 
    public ControlGroupTypes ControlGroupType { get; }
    public ResearchMethods ResearchMethods { get; }

    public ResearchMethods ResearchMethod => throw new NotImplementedException();

    public CountProject(string name, string description, string dataName, string creationDate, 
        ControlGroupTypes controlGroupType, ResearchMethods researchMethod) 
    {
        Name = name;
        Description = description;
        DataName = dataName;
        CreationDate = creationDate;
        ControlGroupType = controlGroupType;
        ResearchMethods = researchMethod;
    }

    public IResearchResult PerformResearch(IMethodDefinitions methodDefinition)
    {

        return null;
    }

}