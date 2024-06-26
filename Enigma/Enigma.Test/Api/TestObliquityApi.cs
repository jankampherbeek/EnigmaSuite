﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api;
using Enigma.Core.Handlers;
using Enigma.Domain.Requests;
using FakeItEasy;

namespace Enigma.Test.Api;


[TestFixture]
public class TestObliquityApi
{
    private const double JD_UT = 123456.789;
    private const double DELTA = 0.00000001;
    private const double EXPECTED_TRUE_OBLIQUITY = 23.447;
    private ObliquityRequest? _obliquityRequest;
    private IObliquityHandler? _obliquityHandlerFake;
    private IObliquityApi? _obliquityApi;


    [SetUp]
    public void SetUp()
    {
        _obliquityRequest = new ObliquityRequest(JD_UT, true);
        _obliquityHandlerFake = A.Fake<IObliquityHandler>();
        A.CallTo(() => _obliquityHandlerFake.CalcObliquity(_obliquityRequest)).Returns(EXPECTED_TRUE_OBLIQUITY);
        _obliquityApi = new ObliquityApi(_obliquityHandlerFake);
    }


    [Test]
    public void TestObliquityHappyFlow()
    {
        double response = _obliquityApi!.GetObliquity(_obliquityRequest!);
        Assert.That(response, Is.EqualTo(EXPECTED_TRUE_OBLIQUITY).Within(DELTA));
    }

    [Test]
    public void TestObliquityNullRequest()
    {
        ObliquityRequest? request = null;
        Assert.That(() => _obliquityApi!.GetObliquity(request!), Throws.TypeOf<ArgumentNullException>());
    }


}








