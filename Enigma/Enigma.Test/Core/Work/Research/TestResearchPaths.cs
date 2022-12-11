// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Work.Research;
using Enigma.Core.Work.Research.Interfaces;

namespace Enigma.Test.Core.Work.Research;

[TestFixture]
public class TestResearchPaths
{
    private IResearchPaths _researchPaths;

    [SetUp]
    public void SetUp()
    {
        _researchPaths= new ResearchPaths();
    }


    [Test]
    public void TestDataPathNoControlGroup()
    {
        string projName = "Project Name";
        bool useControlGroup = false;
        string pathText = _researchPaths.DataPath(projName, useControlGroup);
        string expectedPathText = @"c:\enigma_ar\project\Project Name\testdata.json";
        Assert.That(pathText, Is.EqualTo(expectedPathText));
    }

    [Test]
    public void TestDataPathWithControlGroup()
    {
        string projName = "Project Name";
        bool useControlGroup = true;
        string pathText = _researchPaths.DataPath(projName, useControlGroup);
        string expectedPathText = @"c:\enigma_ar\project\Project Name\controldata.json";
        Assert.That(pathText, Is.EqualTo(expectedPathText));
    }

    [Test]
    public void TestResultPathNoControlGroup()
    {
        string projName = "Project Name";
        string methodName = "CountPosInSigns";
        bool useControlGroup = false;
        string pathText = _researchPaths.ResultPath(projName, methodName, useControlGroup);
        string expectedStartOfPathText = @"c:\enigma_ar\project\Project Name\results\testdataresult_";
        Assert.That(pathText, Does.Contain(expectedStartOfPathText));
    }

    [Test]
    public void TestResultPathWithControlGroup()
    {
        string projName = "Project Name";
        string methodName = "CountPosInSigns";
        bool useControlGroup = true;
        string pathText = _researchPaths.ResultPath(projName, methodName, useControlGroup);
        string expectedStartOfPathText = @"c:\enigma_ar\project\Project Name\results\controldataresult_";
        Assert.That(pathText, Does.Contain(expectedStartOfPathText));
    }
}