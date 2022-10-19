// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Engima.Research.Domain;
using Enigma.Research.Interfaces;

namespace Enigma.Research.Projects;

public class CountProject : IResearchProject
{
    public string Name { get;  }
    public string Identification { get; }
    public string Description { get; }
    public string DataName { get; } 
    public string CreationDate { get; } 
    public ControlGroupTypes ControlGroupType { get; }


    public CountProject(string name, string identification, string description, string dataName, string creationDate, ControlGroupTypes controlGroupType) 
    {
        Name = name;
        Description = description;
        DataName = dataName;
        CreationDate = creationDate;
        ControlGroupType = controlGroupType;

    }


}