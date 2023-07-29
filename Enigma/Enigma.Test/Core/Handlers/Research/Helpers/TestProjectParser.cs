// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Research.Helpers;
using Enigma.Core.Handlers.Research.Interfaces;
using Enigma.Domain.Research;

namespace Enigma.Test.Core.Handlers.Research.Helpers;

[TestFixture]
public class TestResearchProjectParser
{
    private IResearchProjectParser? _parser;
    private const string ProjectName = "TestProject";
    private const string Description = "Description for test project.";
    private const string DataName = "This_datafile";
    private const ControlGroupTypes ControlGroupType = ControlGroupTypes.StandardShift;
    private const int MultiplFactor = 10;


    [SetUp]
    public void SetUp()
    {
        _parser = new ResearchProjectParser();
    }

    [Test]
    public void TestMarshallUnmarshall()
    {
        ResearchProject project1 = new(ProjectName, Description, DataName, ControlGroupType, MultiplFactor);
        string jsonText = _parser!.Marshall(project1);
        ResearchProject project2 = _parser.UnMarshall(jsonText);
        Assert.That(project2, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(project2.Name, Is.EqualTo(project1.Name));
            Assert.That(project2.Description, Is.EqualTo(project1.Description));
            Assert.That(project2.DataName, Is.EqualTo(project1.DataName));
            Assert.That(project2.ControlGroupType, Is.EqualTo(project1.ControlGroupType));
            Assert.That(project2.ControlGroupMultiplication, Is.EqualTo(project1.ControlGroupMultiplication));
        });
    }
}