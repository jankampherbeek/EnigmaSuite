// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using api.handlers;
using E4C.api.handlers;
using E4C.calc.seph;
using E4C.calc.seph.secalculations;
using E4C.domain.shared.positions;
using E4C.domain.shared.references;
using E4C.domain.shared.reqresp;
using E4C.domain.shared.specifications;
using E4C.exceptions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace E4CTest.testsrc.calc.seph.secalculations;

/// <summary>
/// Test for MundanePosHandler
/// </summary>
/// <remarks>
/// MundanePosHandler pakcs an already calculated instance of FullMundanePositions in a validated response. 
/// The contents of FullMundanePositions are not tested, this is already done in the tests for MundanePositionsCalc.
/// Tests are only for the handling of error conditions.
/// </remarks>
[TestFixture]
public class TestMundanePosHandler
{

    private double _jdUt = 1232456.789;
    private double _obliquity = 23.447;
    private int _flags = 2;
    private Location _location = new("", 1.0, 2.0);
    private HouseSystems _houseSystem = HouseSystems.Placidus;
    private int _seErrorResult = -3;
    private string _classAndMethod = "classname.methodname";
    private string _paramSummary = "list of params";
    FullMundanePosRequest request;
    
    
    [Test]
    public void TestCalculateAllMundanePositionsHappyFlowIsSuccess()
    {
        IMundanePosHandler handler = defineHandlerHappyFlow();
        request = new FullMundanePosRequest(_jdUt, _location, _houseSystem);
        FullMundanePosResponse response = handler.CalculateAllMundanePositions(request);
        Assert.IsTrue(response.Success);
    }

    [Test]
    public void TestCalculateAllMundanePositionsHappyFlowNoErrorText()
    {
        IMundanePosHandler handler = defineHandlerHappyFlow();
        request = new FullMundanePosRequest(_jdUt, _location, _houseSystem);
        FullMundanePosResponse response = handler.CalculateAllMundanePositions(request);
        Assert.AreEqual("", response.ErrorText);
    }
    
    [Test]
    public void TestCalculateAllMundanePositionsErrorSuccessFalse()
    {
        IMundanePosHandler handler = defineHandlerError();
        request = new FullMundanePosRequest(_jdUt, _location, _houseSystem);
        FullMundanePosResponse response = handler.CalculateAllMundanePositions(request);
        Assert.IsFalse(response.Success);
    }

    [Test]
    public void TestCalculateAllMundanePositionsErrorErrorText()
    {
        IMundanePosHandler handler = defineHandlerError();
        request = new FullMundanePosRequest(_jdUt, _location, _houseSystem);
        FullMundanePosResponse response = handler.CalculateAllMundanePositions(request);
        string expectedErrorText = "-3/classname.methodname/list of params";
        Assert.AreEqual(expectedErrorText, response.ErrorText);
    }
  
    
    private IMundanePosHandler defineHandlerHappyFlow()
    {
        var mockFlagDefs = new Mock<IFlagDefinitions>();
        mockFlagDefs.Setup(p => p.DefineFlags(request)).Returns(_flags);
        var mockOblHandler = new Mock<IObliquityHandler>();
        var oblRequest = new ObliquityRequest(_jdUt, true);
        mockOblHandler.Setup(p => p.CalcObliquity(oblRequest)).Returns(new ObliquityResponse(_obliquity, true, ""));
        var mockMundPosCalc = new Mock<IMundanePositionsCalculator>();
        List<CuspFullPos> cusps = new List<CuspFullPos>();
        CuspFullPos mc = new CuspFullPos(100.0, 101.0, 5.0, new HorizontalPos(0.0, 70.0));
        CuspFullPos ascendant = new CuspFullPos(190.0, 191.0, 5.5, new HorizontalPos(90.0, 0.0));
        CuspFullPos vertex = new CuspFullPos(5.0, 6.0, 8.0, new HorizontalPos(280.0, 10.0));
        CuspFullPos eastPoint = new CuspFullPos(199.0, 200.0, -12.0, new HorizontalPos(99.0, -4.4));
        FullMundanePositions fullMundPos = new FullMundanePositions(cusps, mc, ascendant, vertex, eastPoint);
        mockMundPosCalc.Setup(p => p.CalculateAllMundanePositions(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<int>(), It.IsAny<Location>(), It.IsAny<HouseSystems>())).Returns(fullMundPos);
        return new MundanePosHandler(mockMundPosCalc.Object, mockOblHandler.Object, mockFlagDefs.Object);
    }
    
    private IMundanePosHandler defineHandlerError()
    {
        var mockFlagDefs = new Mock<IFlagDefinitions>();
        mockFlagDefs.Setup(p => p.DefineFlags(request)).Returns(_flags);
        var mockOblHandler = new Mock<IObliquityHandler>();
        var oblRequest = new ObliquityRequest(_jdUt, true);
        mockOblHandler.Setup(p => p.CalcObliquity(oblRequest)).Returns(new ObliquityResponse(_obliquity, true, ""));
        var mockMundPosCalc = new Mock<IMundanePositionsCalculator>();
        mockMundPosCalc.Setup(p => p.CalculateAllMundanePositions(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<int>(), It.IsAny<Location>(), It.IsAny<HouseSystems>())).Throws(new SwissEphException(string.Format("{0}/{1}/{2}", _seErrorResult, _classAndMethod, _paramSummary)));
        return new MundanePosHandler(mockMundPosCalc.Object, mockOblHandler.Object, mockFlagDefs.Object);
    }




}


