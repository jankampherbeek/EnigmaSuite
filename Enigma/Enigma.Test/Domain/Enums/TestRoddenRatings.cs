// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;

namespace Enigma.Test.Domain.Enums;


[TestFixture]
public class TestRoddenRatings
{

    [Test]
    public void TestRetrievingDetails()
    {
        RoddenRatings roddenRating = RoddenRatings.C;
        RoddenRatingDetails details = roddenRating.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Rating, Is.EqualTo(roddenRating));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.roddenrating.c"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (RoddenRatings roddenRating in Enum.GetValues(typeof(RoddenRatings)))
        {
            RoddenRatingDetails details = roddenRating.GetDetails();
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int roddenRatingIndex = 2;
        RoddenRatings roddenRating = RoddenRatings.C.RoddenRatingForIndex(roddenRatingIndex);
        Assert.That(roddenRating, Is.EqualTo(RoddenRatings.A));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int roddenRatingIndex = 1000;
        Assert.That(() => _ = RoddenRatings.XX.RoddenRatingForIndex(roddenRatingIndex), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllDetailsForRating()
    {
        List<RoddenRatingDetails> allDetails = RoddenRatings.AA.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(8));
            Assert.That(allDetails[0].Rating, Is.EqualTo(RoddenRatings.Unknown));
            Assert.That(allDetails[1].Rating, Is.EqualTo(RoddenRatings.AA));
            Assert.That(allDetails[7].TextId, Is.EqualTo("ref.enum.roddenrating.xx"));
        });
    }
}