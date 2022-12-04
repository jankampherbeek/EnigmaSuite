// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Constants;
using Enigma.Domain.Enums;

namespace Enigma.Test.Domain.AstronCalculations;

[TestFixture]
public class TestProjectionTypes
{
    [Test]
    public void TestRetrievingDetails()
    {
        ProjectionTypes projType = ProjectionTypes.ObliqueLongitude;
        ProjectionTypeDetails details = projType.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.ProjectionType, Is.EqualTo(ProjectionTypes.ObliqueLongitude));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.projectiontype.obliquelongitude"));
        });
    }


    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (ProjectionTypes projType in Enum.GetValues(typeof(ProjectionTypes)))
        {
            ProjectionTypeDetails details = projType.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int index = 0;
        ProjectionTypes projType = ProjectionTypes.ObliqueLongitude.ProjectionTypeForIndex(index);
        Assert.That(projType, Is.EqualTo(ProjectionTypes.TwoDimensional));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int index = 500;
        Assert.That(() => _ = ProjectionTypes.TwoDimensional.ProjectionTypeForIndex(index), Throws.TypeOf<ArgumentException>());
    }


    [Test]
    public void TestAllPrfojectionTypeDetails()
    {
        List<ProjectionTypeDetails> allDetails = ProjectionTypes.TwoDimensional.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(2));
            Assert.That(allDetails[0].ProjectionType, Is.EqualTo(ProjectionTypes.TwoDimensional));
            Assert.That(allDetails[1].ProjectionType, Is.EqualTo(ProjectionTypes.ObliqueLongitude));
            Assert.That(allDetails[0].TextId, Is.EqualTo("ref.enum.projectiontype.twodimensional"));
        });
    }
}
