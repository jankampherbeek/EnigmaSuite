// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;

[TestFixture]
public class TestProjectionTypes
{
    [Test]
    public void TestRetrievingDetails()
    {
        const ProjectionTypes projType = ProjectionTypes.ObliqueLongitude;
        ProjectionTypeDetails details = projType.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.ProjectionType, Is.EqualTo(ProjectionTypes.ObliqueLongitude));
            Assert.That(details.Text, Is.EqualTo("Oblique Longitude"));
        });
    }


    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (ProjectionTypes projType in Enum.GetValues(typeof(ProjectionTypes)))
        {
            ProjectionTypeDetails details = projType.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Text, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        const int index = 0;
        ProjectionTypes projType = ProjectionTypesExtensions.ProjectionTypeForIndex(index);
        Assert.That(projType, Is.EqualTo(ProjectionTypes.TwoDimensional));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = 500;
        Assert.That(() => _ = ProjectionTypesExtensions.ProjectionTypeForIndex(index), Throws.TypeOf<ArgumentException>());
    }


    [Test]
    public void TestAllPrfojectionTypeDetails()
    {
        List<ProjectionTypeDetails> allDetails = ProjectionTypesExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(2));
            Assert.That(allDetails[0].ProjectionType, Is.EqualTo(ProjectionTypes.TwoDimensional));
            Assert.That(allDetails[1].ProjectionType, Is.EqualTo(ProjectionTypes.ObliqueLongitude));
            Assert.That(allDetails[0].Text, Is.EqualTo("Standard (2-dimensional)"));
        });
    }
}
