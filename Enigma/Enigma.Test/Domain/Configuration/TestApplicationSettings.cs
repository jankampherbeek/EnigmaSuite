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

    [Test]
    public void TestDefaultValues()
    {
        ApplicationSettings settings = ApplicationSettings.Instance;
        Assert.Multiple(() =>
        {
            Assert.That(settings, Is.Not.Null);
            Assert.That(settings.LocationDataFiles, Is.EqualTo(DefaultLocData));
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


