// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Interfaces;

namespace Enigma.Test.Domain.Enums;


[TestFixture]
public class TestRoddenRatings
{
    private IRoddenRatingSpecifications specifications;

    [SetUp]
    public void SetUp()
    {
        specifications = new RoddenRatingSpecifications();
    }


    [Test]
    public void TestRetrievingDetails()
    {
        RoddenRatings roddenRating = RoddenRatings.C;
        RoddenRatingDetails details = specifications.DetailsForRating(roddenRating);
        Assert.IsNotNull(details);
        Assert.That(details.Rating, Is.EqualTo(roddenRating));
        Assert.That(details.TextId, Is.EqualTo("ref.enum.roddenrating.c"));
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (RoddenRatings roddenRating in Enum.GetValues(typeof(RoddenRatings)))
        {
            RoddenRatingDetails details = specifications.DetailsForRating(roddenRating);
            Assert.IsNotNull(details);
            Assert.IsTrue(details.TextId.Length > 0);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int roddenRatingIndex = 2;
        RoddenRatings roddenRating = specifications.RoddenRatingForIndex(roddenRatingIndex);
        Assert.That(roddenRating, Is.EqualTo(RoddenRatings.A));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int roddenRatingIndex = 1000;
        Assert.That(() => _ = specifications.RoddenRatingForIndex(roddenRatingIndex), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllDetailsForRating()
    {
        List<RoddenRatingDetails> allDetails = specifications.AllDetailsForRating();
        Assert.That(allDetails.Count, Is.EqualTo(8));
        Assert.That(allDetails[0].Rating, Is.EqualTo(RoddenRatings.Unknown));
        Assert.That(allDetails[1].Rating, Is.EqualTo(RoddenRatings.AA));
        Assert.That(allDetails[7].TextId, Is.EqualTo("ref.enum.roddenrating.xx"));
    }

}