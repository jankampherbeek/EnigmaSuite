// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Configuration;

namespace Enigma.Test.Domain.Configuration;


[TestFixture]
public class TestApplicationSettings
{
    private const string DefaultLocData = @"c:\enigma_ar\data";
    private const string NewProjData = @"d:\somewhere\projdata";

    private const string EnigmaRoot = @"c:\enigma_ar";
    private const string ExportFiles = @"c:\enigma_ar\export";
    private const string LogFiles = @"c:\enigma_ar\logs";
    private const string Database = @"c:\enigma_ar\database";
    
    
    [Test]
    public void TestDefaultValues()
    {
        ApplicationSettings settings = ApplicationSettings.Instance;
        Assert.Multiple(() =>
        {
            Assert.That(settings, Is.Not.Null);
            Assert.That(ApplicationSettings.LocationDataFiles, Is.EqualTo(DefaultLocData));
            Assert.That(settings.LocationProjectFiles, Is.EqualTo(NewProjData));
            Assert.That(ApplicationSettings.LocationEnigmaRoot, Is.EqualTo(EnigmaRoot));
            Assert.That(ApplicationSettings.LocationExportFiles, Is.EqualTo(ExportFiles));
            Assert.That(ApplicationSettings.LocationLogFiles, Is.EqualTo(LogFiles));
            Assert.That(ApplicationSettings.LocationDatabase, Is.EqualTo(Database));
        });
    }

    [Test]
    public void TestChangingData()
    {
        ApplicationSettings settings = ApplicationSettings.Instance;
        settings.LocationProjectFiles = NewProjData;
        Assert.That(settings.LocationProjectFiles, Is.EqualTo(NewProjData));
    }

    [Test]
    public void TestChangeAfterNewRetrievalOfSingletonInstance()
    {
        ApplicationSettings settings = ApplicationSettings.Instance;
        settings.LocationProjectFiles = NewProjData;
        ApplicationSettings newSettings = ApplicationSettings.Instance;
        Assert.That(newSettings.LocationProjectFiles, Is.EqualTo(NewProjData));
    }
    
}


