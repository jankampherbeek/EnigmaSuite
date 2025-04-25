// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Persistency;
using Enigma.Core.Research;

namespace Enigma.Test.Core.Research;

[TestFixture]
public class TestResearchPaths
{
    private IResearchPaths? _researchPaths;

    [SetUp]
    public void SetUp()
    {
        ISettingsDao settingsDao = new SettingsDao();
        _researchPaths = new ResearchPaths(settingsDao);
    }


    [Test]
    public void TestDataPathNoControlGroup()
    {
        const string projName = "Project Name";
        const bool useControlGroup = false;
        var pathText = _researchPaths!.DataPath(projName, useControlGroup);
        const string expected = @"\project\Project Name\testdata.json";
        Assert.That(pathText, Does.Contain(expected));
    }

    [Test]
    public void TestDataPathWithControlGroup()
    {
        const string projName = "Project Name";
        const bool useControlGroup = true;
        var pathText = _researchPaths!.DataPath(projName, useControlGroup);
        const string expected = @"\project\Project Name\controldata.json";
        Assert.That(pathText, Does.Contain(expected));
    }

    [Test]
    public void TestResultPathNoControlGroup()
    {
        const string projName = "Project Name";
        const string methodName = "CountPosInSigns";
        const bool useControlGroup = false;
        var pathText = _researchPaths!.ResultPath(projName, methodName, useControlGroup);
        const string expected = @"\project\Project Name\results\testdataresult_";
        Assert.That(pathText, Does.Contain(expected));
    }

    [Test]
    public void TestResultPathWithControlGroup()
    {
        const string projName = "Project Name";
        const string methodName = "CountPosInSigns";
        const bool useControlGroup = true;
        var pathText = _researchPaths!.ResultPath(projName, methodName, useControlGroup);
        const string expected = @"\project\Project Name\results\controldataresult_";
        Assert.That(pathText, Does.Contain(expected));
    }

    [Test]
    public void TestCountResultsPathNoControlGroup()
    {
        const string projName = "Project Name";
        const string methodName = "CountPosInSigns";
        const bool useControlGroup = false;
        var pathText = _researchPaths!.CountResultsPath(projName, methodName, useControlGroup);
        const string expected = @"\project\Project Name\results\testdataresult_CountPosInSigns_counts_";
        Assert.That(pathText, Does.Contain(expected));
    }
    
    [Test]
    public void TestCountResultsPathWithControlGroup()
    {
        const string projName = "Project Name";
        const string methodName = "CountPosInSigns";
        const bool useControlGroup = true;
        var pathText = _researchPaths!.CountResultsPath(projName, methodName, useControlGroup);
        const string expected = @"\project\Project Name\results\controldataresult_CountPosInSigns_counts_";
        Assert.That(pathText, Does.Contain(expected));
    }
    
    [Test]
    public void TestSummedResultsPathNoControlGroup()
    {
        const string projName = "Project Name";
        const string methodName = "CountPosInSigns";
        const bool useControlGroup = false;
        var pathText = _researchPaths!.SummedResultsPath(projName, methodName, useControlGroup);
        const string expected = @"\project\Project Name\results\testsummedresult_CountPosInSigns_counts_";
        Assert.That(pathText, Does.Contain(expected));
    }
    
    [Test]
    public void TestSummedResultsPathWithControlGroup()
    {
        const string projName = "Project Name";
        const string methodName = "CountPosInSigns";
        const bool useControlGroup = true;
        var pathText = _researchPaths!.SummedResultsPath(projName, methodName, useControlGroup);
        const string expected = @"\project\Project Name\results\controlsummedresult_CountPosInSigns_counts_";
        Assert.That(pathText, Does.Contain(expected));
    }
}