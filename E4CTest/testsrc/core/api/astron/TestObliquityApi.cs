// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Api.Astron;
using E4C.Core.Astron.Obliquity;
using E4C.Shared.ReqResp;
using Moq;
using NUnit.Framework;
using System;

namespace E4CTest.core.api.astron;


[TestFixture]
public class TestObliquityApi
{
    private readonly double _jdUt = 123456.789;
    private readonly double _delta = 0.00000001;
    private readonly bool _useTrueObliquity = true;
    private readonly double _expectedObliquity = 23.447;
    private readonly bool _expectedSuccess = true;
    private readonly string _expectedErrorText = "";
    private ObliquityRequest _obliquityRequest;
    private Mock<IObliquityHandler> _mockObliquityHandler;

    private IObliquityApi _obliquityApi;


    [SetUp]
    public void SetUp()
    {
        _obliquityRequest = new ObliquityRequest(_jdUt, _useTrueObliquity);
        _mockObliquityHandler = new Mock<IObliquityHandler>();
        _mockObliquityHandler.Setup(p => p.CalcObliquity(_obliquityRequest)).Returns(new ObliquityResponse(_expectedObliquity, _expectedSuccess, _expectedErrorText));
        _obliquityApi = new ObliquityApi(_mockObliquityHandler.Object);
    }


    [Test]
    public void TestObliquityHappyFlow()
    {
        ObliquityResponse response = _obliquityApi.getObliquity(_obliquityRequest);
        Assert.AreEqual(_expectedObliquity, response.Obliquity, _delta);
        Assert.IsTrue(response.Success);
        Assert.AreEqual(_expectedErrorText, response.ErrorText);
    }

    [Test]
    public void TestObliquityNullRequest()
    {
        Assert.That(() => _obliquityApi.getObliquity(null), Throws.TypeOf<ArgumentNullException>());
    }


}








