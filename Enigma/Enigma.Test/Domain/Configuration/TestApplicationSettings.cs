// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Configuration;

namespace Enigma.Test.Domain.Configuration;


[TestFixture]
public class TestApplicationSettings
{
    private const string DEFAULT_LOC_DATA = @"c:\enigma_ar\data";
    private const string NEW_PROJ_DATA = @"d:\somewhere\projdata";

    [Test]
    public void TestDefaultValues()
    {
        ApplicationSettings settings = ApplicationSettings.Instance;
        Assert.Multiple(() =>
        {
            Assert.That(settings, Is.Not.Null);
            Assert.That(ApplicationSettings.LocationDataFiles, Is.EqualTo(DEFAULT_LOC_DATA));
        });
    }

    [Test]
    public void TestChangingData()
    {
        ApplicationSettings settings = ApplicationSettings.Instance;
        settings.LocationProjectFiles = NEW_PROJ_DATA;
        Assert.That(settings.LocationProjectFiles, Is.EqualTo(NEW_PROJ_DATA));
    }

    [Test]
    public void TestChangeAfterNewRetrievalOfSingletonInstance()
    {
        ApplicationSettings settings = ApplicationSettings.Instance;
        settings.LocationProjectFiles = NEW_PROJ_DATA;
        ApplicationSettings newSettings = ApplicationSettings.Instance;
        Assert.That(newSettings.LocationProjectFiles, Is.EqualTo(NEW_PROJ_DATA));
    }






}


