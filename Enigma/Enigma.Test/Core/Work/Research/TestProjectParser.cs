// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Work.Conversions;
using Enigma.Core.Work.Research;
using Enigma.Core.Work.Research.Interfaces;
using Enigma.Domain.Research;

namespace Enigma.Test.Core.Work.Research;

[TestFixture]
public class TestResearchProjectParser
{
    private IResearchProjectParser _parser;
    private readonly string _projectName = "TestProject";
    private readonly string _description = "Description for test project.";
    private readonly string _dataName = "This_datafile";
    private readonly ControlGroupTypes _controlGroupType = ControlGroupTypes.StandardShift;
    private readonly int _multiplFactor = 10;



    [SetUp]
    public void SetUp()
    {
        _parser = new ResearchProjectParser();

    }

    [Test]
    public void TestMarshallUnmarshall()
    {
        ResearchProject project1 = new(_projectName, _description, _dataName, _controlGroupType, _multiplFactor);
        string jsonText = _parser.Marshall(project1);
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