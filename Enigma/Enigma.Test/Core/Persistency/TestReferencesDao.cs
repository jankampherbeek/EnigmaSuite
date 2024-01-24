// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Persistency;

namespace Enigma.Test.Core.Persistency;

[TestFixture]
public class TestReferencesDao
{

    private IReferencesDao _refDao = new ReferencesDao();

    [Test]
    public void TestReadAllRatings()
    {
        Dictionary<long, string> ratingsResult = _refDao.ReadAllRatings();
        Assert.That(ratingsResult, Has.Count.EqualTo(8));
    }

    [Test]
    public void TestReadNameForRating()
    {
        string ratingsNameResult = _refDao.ReadNameForRating(2);
        Assert.That(ratingsNameResult, Is.EqualTo("AA - Accurate"));
    }

    [Test]
    public void TestReadAllChartCategories()
    {
        Dictionary<long, string> catResult = _refDao.ReadAllChartCategories();
        Assert.That(catResult, Has.Count.EqualTo(7));
    }

    [Test]
    public void TestReadNameForChartCategory()
    {
        string catNameResult = _refDao.ReadNameForChartCategory(4);
        Assert.That(catNameResult, Is.EqualTo("Horary"));
    }

   
}