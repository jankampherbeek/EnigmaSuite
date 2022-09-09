// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Settings;

namespace Enigma.Test.Domain.Settings;


[TestFixture]
public class TestApplicationSettings
{
    private readonly string _defaultLocData = @"c:\enigma\data";
    private readonly string _defaultLocSe = @"c:\enigma\se";
    private readonly string _newProjData = @"d:\somewhere\projdata";

    [Test]
    public void TestDefaultValues()
    {
        ApplicationSettings settings = ApplicationSettings.Instance;
        Assert.IsNotNull(settings);
        Assert.That(_defaultLocData, Is.EqualTo(settings.LocationDataFiles));
        Assert.That(_defaultLocSe, Is.EqualTo(settings.LocationSeFiles));
    }

    [Test]
    public void TestChangingData()
    {
        ApplicationSettings settings = ApplicationSettings.Instance;
        settings.LocationProjectFiles = _newProjData;
        Assert.That(_newProjData, Is.EqualTo(settings.LocationProjectFiles));
    }

    [Test]
    public void TestChangeAfterNewRetrievalOfSingletonInstance()
    {
        ApplicationSettings settings = ApplicationSettings.Instance;
        settings.LocationProjectFiles = _newProjData;
        ApplicationSettings newSettings = ApplicationSettings.Instance;
        Assert.That(_newProjData, Is.EqualTo(newSettings.LocationProjectFiles));
    }





}


