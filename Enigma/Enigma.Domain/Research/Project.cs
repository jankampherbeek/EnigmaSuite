// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


namespace Enigma.Domain.Research;

/// <summary>Representation of ResearchProject. Also used for persistency with JSON.</summary>
public class ResearchProject
{
    public string Name { get; }
    public string Description { get; }
    public string DataName { get; }
    public string CreationDate { get; }
    public ControlGroupTypes ControlGroupType { get; }

    public int ControlGroupMultiplication { get; }

    public ResearchProject(string name, string description, string dataName, ControlGroupTypes controlGroupType, int controlGroupMultiplication)
    {
        Name = name;
        Description = description;
        DataName = dataName;
        ControlGroupType = controlGroupType;
        ControlGroupMultiplication = controlGroupMultiplication;
        CreationDate = DateTime.Now.ToString("F");
    }

}