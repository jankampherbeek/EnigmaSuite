// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.CalcVars;

namespace Enigma.Test.Domain.CalcVars;


[TestFixture]
public class TestRoddenRatings
{
    [Test]
    public void TestRetrievingDetails()
    {
        RoddenRatings roddenRating = RoddenRatings.C;
        IRoddenRatingSpecifications specifications = new RoddenRatingSpecifications();
        RoddenRatingDetails details = specifications.DetailsForRating(roddenRating);
        Assert.IsNotNull(details);
        Assert.That(details.Rating, Is.EqualTo(roddenRating));
        Assert.That(details.TextId, Is.EqualTo("ref.enum.roddenrating.c"));
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        IRoddenRatingSpecifications specifications = new RoddenRatingSpecifications();

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
        IRoddenRatingSpecifications specifications = new RoddenRatingSpecifications();
        int roddenRatingIndex = 2;
        RoddenRatings roddenRating = specifications.RoddenRatingForIndex(roddenRatingIndex);
        Assert.That(roddenRating, Is.EqualTo(RoddenRatings.A));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        IRoddenRatingSpecifications specifications = new RoddenRatingSpecifications();
        int roddenRatingIndex = 1000;
        Assert.That(() => _ = specifications.RoddenRatingForIndex(roddenRatingIndex), Throws.TypeOf<ArgumentException>());
    }

}