// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;

namespace Enigma.Test.Domain.Dtos;


[TestFixture]
public class TestApplicationSettings
{
    private const string DEFAULT_LOC_DATA = @"c:\enigma_ar\data";
    private const string NEW_PROJ_DATA = @"d:\somewhere\projdata";

    private const string ENIGMA_ROOT = @"c:\enigma_ar";
    private const string EXPORT_FILES = @"c:\enigma_ar\export";
    private const string LOG_FILES = @"c:\enigma_ar\logs";
    private const string DATABASE = @"c:\enigma_ar\database";
    
    
    [Test]
    public void TestDefaultValues()
    {
        ApplicationSettings settings = ApplicationSettings.Instance;
        Assert.Multiple(() =>
        {
            Assert.That(settings, Is.Not.Null);
            Assert.That(ApplicationSettings.LocationDataFiles, Is.EqualTo(DEFAULT_LOC_DATA));
            Assert.That(settings.LocationProjectFiles, Is.EqualTo(NEW_PROJ_DATA));
            Assert.That(ApplicationSettings.LocationEnigmaRoot, Is.EqualTo(ENIGMA_ROOT));
            Assert.That(ApplicationSettings.LocationExportFiles, Is.EqualTo(EXPORT_FILES));
            Assert.That(ApplicationSettings.LocationLogFiles, Is.EqualTo(LOG_FILES));
            Assert.That(ApplicationSettings.LocationDatabase, Is.EqualTo(DATABASE));
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


