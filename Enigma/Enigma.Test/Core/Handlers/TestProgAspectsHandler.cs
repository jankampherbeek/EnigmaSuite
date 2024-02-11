// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis;
using Enigma.Core.Handlers;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Test.Core.Handlers;

/// <summary>Unit tests for PRogAspectsHandler.</summary>
/// <remarks>Actually a small integration test, as the constructor uses real objects for CalculatedDistance and
/// CheckedProgAspects.</remarks>
[TestFixture]
public class TestProgAspectsHandler
{
    private const double DELTA = 0.00000001;
    private readonly IProgAspectsHandler _handler = 
        new ProgAspectsHandler(new CalculatedDistance(), new CheckedProgAspects());
    
    [Test]
    public void TestFindProgAspectsHappyFlow()
    {
        const double orb = 10.0;
        ProgAspectsRequest request = new(CreateChartPoints(), CreateProgPoints(), CreateAspectTypes(), orb);
        (List<DefinedAspect>? aspectsFound, int resultCode) = _handler.FindProgAspects(request);
        Assert.That(resultCode, Is.EqualTo(ResultCodes.OK));
        // expected aspects:
        // r Sun conj p Uranus
        // r Moon opp p Saturn
        // r Moon quintile p Uranus
        // r Chiron conj p MakeMake
        Assert.Multiple(() =>
        {
            Assert.That(aspectsFound, Has.Count.EqualTo(4));
            Assert.That(aspectsFound[0].Aspect.Aspect, Is.EqualTo(AspectTypes.Conjunction));
            Assert.That(aspectsFound[0].Point1, Is.EqualTo(ChartPoints.Uranus));
            Assert.That(aspectsFound[0].Point2, Is.EqualTo(ChartPoints.Sun));
            Assert.That(aspectsFound[0].ActualOrb, Is.EqualTo(3.0).Within(DELTA));
            Assert.That(aspectsFound[1].Aspect.Aspect, Is.EqualTo(AspectTypes.Opposition));
            Assert.That(aspectsFound[3].Point2, Is.EqualTo(ChartPoints.Chiron));
        });
    }

    [Test]
    public void TestFindProgAspectsOverlapping()
    {
        const double orb = 30.0;
        Dictionary<ChartPoints, double> cPoints = new()
        {
            { ChartPoints.Sun, 1.0 }
        };
        Dictionary<ChartPoints, double> pPoints = new()
        {
            { ChartPoints.Saturn, 80.0 }
        };
        List<AspectTypes> aspectTypes = new()
        {
            AspectTypes.Square,
            AspectTypes.Quintile
        };
        ProgAspectsRequest request = new(cPoints, pPoints, aspectTypes, orb);
        (List<DefinedAspect>? aspectsFound, int resultCode) = _handler.FindProgAspects(request);
        Assert.Multiple(() =>
        {
            Assert.That(resultCode, Is.EqualTo(ResultCodes.OK));
            Assert.That(aspectsFound, Has.Count.EqualTo(2));            
            Assert.That(aspectsFound[0].Aspect.Aspect, Is.EqualTo(AspectTypes.Square));
            Assert.That(aspectsFound[0].Point1, Is.EqualTo(ChartPoints.Saturn));
            Assert.That(aspectsFound[0].Point2, Is.EqualTo(ChartPoints.Sun));
            Assert.That(aspectsFound[0].ActualOrb, Is.EqualTo(11.0).Within(DELTA));
            Assert.That(aspectsFound[1].Aspect.Aspect, Is.EqualTo(AspectTypes.Quintile));
            Assert.That(aspectsFound[1].Point1, Is.EqualTo(ChartPoints.Saturn));
            Assert.That(aspectsFound[1].Point2, Is.EqualTo(ChartPoints.Sun));
            Assert.That(aspectsFound[1].ActualOrb, Is.EqualTo(7.0).Within(DELTA));
        });
    }

    [Test]
    public void TestFindProgAspectsWrongInput()
    {
        const double orb = 10.0;
        ProgAspectsRequest request = new(CreateChartPoints(), CreateProgPointsWithError(), CreateAspectTypes(), orb);
        ProgAspectsResponse response = _handler.FindProgAspects(request);
        Assert.That(response.ResultCode, Is.EqualTo(ResultCodes.WRONG_ARGUMENTS));
    }
    
    private static Dictionary<ChartPoints, double> CreateChartPoints()
    {
        Dictionary<ChartPoints, double> cPoints = new()
        {
            { ChartPoints.Sun, 100.0 },
            { ChartPoints.Moon, 22.0 },
            { ChartPoints.Chiron, 355.0 }
        };
        return cPoints;
    }

    private static Dictionary<ChartPoints, double> CreateProgPoints()
    {
        Dictionary<ChartPoints, double> pPoints = new()
        {
            { ChartPoints.Saturn, 204.0 },
            { ChartPoints.Uranus, 97.0 },
            { ChartPoints.Makemake, 1.0 }
        };
        return pPoints;
    }

    private static Dictionary<ChartPoints, double> CreateProgPointsWithError()
    {
        Dictionary<ChartPoints, double> pPoints = new()
        {
            { ChartPoints.Saturn, 204.0 },
            { ChartPoints.Uranus, 97.0 },
            { ChartPoints.Makemake, -1.0 }      // Error: negative values not allowed.
        };
        return pPoints;
    }
    
    private static List<AspectTypes> CreateAspectTypes()
    {
        List<AspectTypes> aspectTypes = new()
        {
            AspectTypes.Conjunction,
            AspectTypes.Quintile,
            AspectTypes.Opposition
        };
        return aspectTypes;
    }
    

}