// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;


[TestFixture]
public class TestRoddenRatings
{

    [Test]
    public void TestRetrievingDetails()
    {
        const RoddenRatings roddenRating = RoddenRatings.C;
        RoddenRatingDetails details = roddenRating.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Rating, Is.EqualTo(roddenRating));
            Assert.That(details.Text, Is.EqualTo("C - Caution, no source"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (RoddenRatings roddenRating in Enum.GetValues(typeof(RoddenRatings)))
        {
            RoddenRatingDetails details = roddenRating.GetDetails();
            Assert.That(details.Text, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        const int roddenRatingIndex = 2;
        RoddenRatings roddenRating = RoddenRatingsExtensions.RoddenRatingForIndex(roddenRatingIndex);
        Assert.That(roddenRating, Is.EqualTo(RoddenRatings.A));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int roddenRatingIndex = 1000;
        Assert.That(() => _ = RoddenRatingsExtensions.RoddenRatingForIndex(roddenRatingIndex), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllDetailsForRating()
    {
        List<RoddenRatingDetails> allDetails = RoddenRatingsExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(8));
            Assert.That(allDetails[0].Rating, Is.EqualTo(RoddenRatings.Unknown));
            Assert.That(allDetails[1].Rating, Is.EqualTo(RoddenRatings.AA));
            Assert.That(allDetails[7].Text, Is.EqualTo("XX - No data of birth"));
        });
    }
}