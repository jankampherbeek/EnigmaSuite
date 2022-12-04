// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.Enums;

namespace Enigma.Test.Domain.Analysis;

[TestFixture]
public class TestArabicPoints
{

    [Test]
    public void TestRetrievingDetails()
    {
        ArabicPoints point = ArabicPoints.FortunaSect;
        ArabicPointDetails details = point.GetDetails();
        Assert.That(details, Is.Not.Null);
        Assert.That(details.ArabicPoint, Is.EqualTo(ArabicPoints.FortunaSect));
        Assert.That(details.TextId, Is.EqualTo("ref.enum.arabicpoint.fortunasect"));
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (ArabicPoints point in Enum.GetValues(typeof(ArabicPoints)))
        {
            ArabicPointDetails details = point.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }

 }