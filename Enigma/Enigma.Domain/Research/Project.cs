// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.



using Engima.Domain.Research;

namespace Enigma.Domain.Research;

/// <summary>Representation of ResearchProject. Also used for persistency with JSON.</summary>
public class ResearchProject
{
    public string Name { get; }
    public string Identification { get; }
    public string Description { get; }
    public string DataName { get; }
    public string CreationDate { get; }
    public ControlGroupTypes ControlGroupType { get; }

    public int ControlGroupMultiplication { get; }

    public ResearchProject(string name, string identification, string description, string dataName, ControlGroupTypes controlGroupType, int controlGroupMultiplication)
    {
        Name = name;
        Identification = identification;
        Description = description;
        DataName = dataName;
        ControlGroupType = controlGroupType;
        ControlGroupMultiplication = controlGroupMultiplication;
        CreationDate = System.DateTime.Now.ToString("F");
    }

}