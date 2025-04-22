// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using System.Globalization;
using Enigma.Core.Research;
using Enigma.Domain.References;
using Enigma.Domain.Research;

namespace Enigma.Test.Core.Research;

[TestFixture]
public class TestResearchProjectParser
{
    private IResearchProjectParser? _parser;
    private const string PROJECT_NAME = "TestProject";
    private const string DESCRIPTION = "Description for test project.";
    private const int DATA_INDEX = 3;
    private const ControlGroupTypes CONTROL_GROUP_TYPE = ControlGroupTypes.StandardShift;
    private const int MULTIPL_FACTOR = 10;


    [SetUp]
    public void SetUp()
    {
        _parser = new ResearchProjectParser();
    }

    [Test]
    public void TestMarshallUnmarshall()
    {
        
        ResearchProject project1 = new(PROJECT_NAME, DESCRIPTION, DATA_INDEX,  DateTime.Now.ToString(CultureInfo.InvariantCulture),
            CONTROL_GROUP_TYPE, MULTIPL_FACTOR);
        string jsonText = _parser!.Marshall(project1);
        ResearchProject project2 = _parser.UnMarshall(jsonText);
        Assert.That(project2, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(project2.Name, Is.EqualTo(project1.Name));
            Assert.That(project2.Description, Is.EqualTo(project1.Description));
            Assert.That(project2.IndexDataFile, Is.EqualTo(project1.IndexDataFile));
            Assert.That(project2.ControlGroupType, Is.EqualTo(project1.ControlGroupType));
            Assert.That(project2.ControlGroupMultiplication, Is.EqualTo(project1.ControlGroupMultiplication));
        });
    }
}