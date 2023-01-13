// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Test.Core.Handlers.Analysis.Helpers;

// TODO 0.1 Analysis test

[TestFixture]
public class TestMidpointHelper
{
    /*   private IMidpointsHelper _midpointsHelper = new MidpointsHelper();
       private readonly double _delta = 0.00000001;

       [Test]
       public void TestConstructEffectiveMidpointHappyFlow()
       {
           AnalysisPoint point1 = new(PointGroups.ZodiacalPoints, 1, 22.0, "a");
           AnalysisPoint point2 = new(PointGroups.ZodiacalPoints, 2, 44.0, "b");
           double expectedMidpoint = 33.0;
           BaseMidpoint actualMidpoint = _midpointsHelper.ConstructEffectiveMidpointInDial(point1, point2, 1.0);    
           Assert.That(actualMidpoint.Position, Is.EqualTo(expectedMidpoint).Within(_delta));
       }

       [Test]
       public void TestConstructEffectiveMidpointLargeDifference()
       {
           AnalysisPoint point1 = new(PointGroups.ZodiacalPoints, 1, 2.0, "a");
           AnalysisPoint point2 = new(PointGroups.ZodiacalPoints, 2, 352.0, "b");
           double expectedMidpoint = 357.0;
           BaseMidpoint actualMidpoint = _midpointsHelper.ConstructEffectiveMidpointInDial(point1, point2, 1.0);
           Assert.That(actualMidpoint.Position, Is.EqualTo(expectedMidpoint).Within(_delta));
       }

       [Test]
       public void TestMeasureMidpointDeviationDial360HappyFlow()
       {
           double division = 1.0;
           double posMidpoint = 10.0;
           double posCelPoint = 11.0;
           double expectedDeviation = 1.0;
           double actualDeviation = _midpointsHelper.MeasureMidpointDeviation(division, posMidpoint, posCelPoint);
           Assert.That(actualDeviation, Is.EqualTo(expectedDeviation).Within(_delta));
       }

       [Test]
       public void TestMeasureMidpointDeviationDial360LargeDifference()
       {
           double division = 1.0;
           double posMidpoint = 10.0;
           double posCelPoint = 348.0;
           double expectedDeviation = 22.0;
           double actualDeviation = _midpointsHelper.MeasureMidpointDeviation(division, posMidpoint, posCelPoint);
           Assert.That(actualDeviation, Is.EqualTo(expectedDeviation).Within(_delta));
       }

       [Test]
       public void TestMeasureMidpointDeviationDial90HappyFlow()
       {
           double division = 0.25;
           double posMidpoint = 10.0;
           double posCelPoint = 101.0;
           double expectedDeviation = 1.0;
           double actualDeviation = _midpointsHelper.MeasureMidpointDeviation(division, posMidpoint, posCelPoint);
           Assert.That(actualDeviation, Is.EqualTo(expectedDeviation).Within(_delta));
       }

       [Test]
       public void TestMeasureMidpointDeviationDial90LargeDifference()
       {
           double division = 0.25;
           double posMidpoint = 10.0;
           double posCelPoint = 281.0;
           double expectedDeviation = 1.0;
           double actualDeviation = _midpointsHelper.MeasureMidpointDeviation(division, posMidpoint, posCelPoint);
           Assert.That(actualDeviation, Is.EqualTo(expectedDeviation).Within(_delta));
       }

       [Test]
       public void TestMeasureMidpointDeviationDial45HappyFlow()
       {
           double division = 0.125;
           double posMidpoint = 10.0;
           double posCelPoint = 56.0;
           double expectedDeviation = 1.0;
           double actualDeviation = _midpointsHelper.MeasureMidpointDeviation(division, posMidpoint, posCelPoint);
           Assert.That(actualDeviation, Is.EqualTo(expectedDeviation).Within(_delta));
       }

       [Test]
       public void TestMeasureMidpointDeviationDial45LargeDifference()
       {
           double division = 0.125;
           double posMidpoint = 10.0;
           double posCelPoint = 326.0;
           double expectedDeviation = 1.0;
           double actualDeviation = _midpointsHelper.MeasureMidpointDeviation(division, posMidpoint, posCelPoint);
           Assert.That(actualDeviation, Is.EqualTo(expectedDeviation).Within(_delta));
       }


       private static List<BaseMidpoint> CreateMidpoints()
       {
           List<BaseMidpoint> midpoints = new();
           PointGroups pointGroup = PointGroups.ZodiacalPoints;
           AnalysisPoint sun = new(pointGroup, 0, 22.0, "a");
           AnalysisPoint moon = new(pointGroup, 1, 178.0, "b");
           AnalysisPoint mars = new(pointGroup, 5, 302.0, "c");
           AnalysisPoint jupiter = new(pointGroup, 6, 42.0, "d");

           midpoints.Add(new BaseMidpoint(sun, moon, 100.0));
           midpoints.Add(new BaseMidpoint(sun, mars, 342.0));
           midpoints.Add(new BaseMidpoint(sun, jupiter, 32.0));
           midpoints.Add(new BaseMidpoint(moon, mars, 240.0));
           midpoints.Add(new BaseMidpoint(moon, jupiter, 110.0));
           midpoints.Add(new BaseMidpoint(mars, jupiter, 352.0));
           return midpoints;
       }

       */
}