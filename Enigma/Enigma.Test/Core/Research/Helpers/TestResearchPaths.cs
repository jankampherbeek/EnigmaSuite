// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Research.Helpers;
using Enigma.Core.Research.Interfaces;

namespace Enigma.Test.Core.Research.Helpers;

[TestFixture]
public class TestResearchPaths
{
    private IResearchPaths? _researchPaths;

    [SetUp]
    public void SetUp()
    {
        _researchPaths = new ResearchPaths();
    }


    [Test]
    public void TestDataPathNoControlGroup()
    {
        const string projName = "Project Name";
        const bool useControlGroup = false;
        string pathText = _researchPaths!.DataPath(projName, useControlGroup);
        const string expectedPathText = @"c:\enigma_ar\project\Project Name\testdata.json";
        Assert.That(pathText, Is.EqualTo(expectedPathText));
    }

    [Test]
    public void TestDataPathWithControlGroup()
    {
        const string projName = "Project Name";
        const bool useControlGroup = true;
        string pathText = _researchPaths!.DataPath(projName, useControlGroup);
        const string expectedPathText = @"c:\enigma_ar\project\Project Name\controldata.json";
        Assert.That(pathText, Is.EqualTo(expectedPathText));
    }

    [Test]
    public void TestResultPathNoControlGroup()
    {
        const string projName = "Project Name";
        const string methodName = "CountPosInSigns";
        const bool useControlGroup = false;
        string pathText = _researchPaths!.ResultPath(projName, methodName, useControlGroup);
        const string expectedStartOfPathText = @"c:\enigma_ar\project\Project Name\results\testdataresult_";
        Assert.That(pathText, Does.Contain(expectedStartOfPathText));
    }

    [Test]
    public void TestResultPathWithControlGroup()
    {
        const string projName = "Project Name";
        const string methodName = "CountPosInSigns";
        const bool useControlGroup = true;
        string pathText = _researchPaths!.ResultPath(projName, methodName, useControlGroup);
        const string expectedStartOfPathText = @"c:\enigma_ar\project\Project Name\results\controldataresult_";
        Assert.That(pathText, Does.Contain(expectedStartOfPathText));
    }

    [Test]
    public void TestCountResultsPathNoControlGroup()
    {
        const string projName = "Project Name";
        const string methodName = "CountPosInSigns";
        const bool useControlGroup = false;
        string pathText = _researchPaths!.CountResultsPath(projName, methodName, useControlGroup);
        const string expectedStartOfPathText = 
            @"c:\enigma_ar\project\Project Name\results\testdataresult_CountPosInSigns_counts_";
        Assert.That(pathText, Does.Contain(expectedStartOfPathText));
    }
    
    [Test]
    public void TestCountResultsPathWithControlGroup()
    {
        const string projName = "Project Name";
        const string methodName = "CountPosInSigns";
        const bool useControlGroup = true;
        string pathText = _researchPaths!.CountResultsPath(projName, methodName, useControlGroup);
        const string expectedStartOfPathText = 
            @"c:\enigma_ar\project\Project Name\results\controldataresult_CountPosInSigns_counts_";
        Assert.That(pathText, Does.Contain(expectedStartOfPathText));
    }
    
    [Test]
    public void TestSummedResultsPathNoControlGroup()
    {
        const string projName = "Project Name";
        const string methodName = "CountPosInSigns";
        const bool useControlGroup = false;
        string pathText = _researchPaths!.SummedResultsPath(projName, methodName, useControlGroup);
        const string expectedStartOfPathText = 
            @"c:\enigma_ar\project\Project Name\results\testsummedresult_CountPosInSigns_counts_";
        Assert.That(pathText, Does.Contain(expectedStartOfPathText));
    }
    
    [Test]
    public void TestSummedResultsPathWithControlGroup()
    {
        const string projName = "Project Name";
        const string methodName = "CountPosInSigns";
        const bool useControlGroup = true;
        string pathText = _researchPaths!.SummedResultsPath(projName, methodName, useControlGroup);
        const string expectedStartOfPathText = 
            @"c:\enigma_ar\project\Project Name\results\controlsummedresult_CountPosInSigns_counts_";
        Assert.That(pathText, Does.Contain(expectedStartOfPathText));
    }
}