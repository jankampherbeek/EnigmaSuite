// Enigma Astrology Research.
// Unit tests for JdForPositionFinder
// Jan Kampherbeek, 2025

using System;
using Enigma.Core.Slices.Solar;
using Enigma.Core.Calc;
using FakeItEasy;
using NUnit.Framework;

namespace Enigma.Test.Core.Slices.Solar;

[TestFixture]
public class TestJdForPositionFinder
 {
//     private ICelPointSeCalc _fakeCalc;
//     private JdForPositionFinder _finder;
//
//     [SetUp]
//     public void SetUp()
//     {
//         _fakeCalc = A.Fake<ICelPointSeCalc>();
//         _finder = new JdForPositionFinder(_fakeCalc);
//     }
//
//     [Test]
//     public void FindsExactJdForSimpleCase()
//     {
//         // Arrange: simulate the Sun's longitude increasing linearly from 10.0 to 20.0 over 1 day, target 15.0 at 2450000.0
//         double startJd = 2450000.0;
//         double targetLongitude = 15.0;
//         int flags = 258;
//         A.CallTo(() => _fakeCalc.CalculateCelPoint(A<int>._, A<double>._, A<int>._))
//             .ReturnsLazily((int seId, double jd, int f) =>
//             {
//                 double fraction = (jd - (startJd - 0.5)); // 0 to 1 over the search period
//                 double longitude = 10.0 + 10.0 * fraction; // 10.0 at start, 20.0 at end
//                 return new[] { new Enigma.Domain.Dtos.PosSpeed(longitude, 0.0), new Enigma.Domain.Dtos.PosSpeed(0, 0), new Enigma.Domain.Dtos.PosSpeed(0, 0) };
//             });
//         // Act
//         double resultJd = _finder.FindJulianDay(targetLongitude, startJd, flags);
//         // Assert
//         Assert.That(resultJd, Is.EqualTo(startJd).Within(0.0001));
//     }
//
//     [Test]
//     public void HandlesZeroCrossing()
//     {
//         // Arrange: simulate the Sun's longitude increases linearly from 359.0 to 1.0 over the search period, target 0.0 at 2450000.0
//         double startJd = 2450000.0;
//         double targetLongitude = 0.0;
//         int flags = 258;
//         A.CallTo(() => _fakeCalc.CalculateCelPoint(A<int>._, A<double>._, A<int>._))
//             .ReturnsLazily((int seId, double jd, int f) =>
//             {
//                 double fraction = (jd - (startJd - 0.5)); // 0 to 1
//                 double longitude = (359.0 + 2.0 * fraction) % 360.0; // 359.0 at start, 1.0 at end, 0.0 at midpoint
//                 return new[] { new Enigma.Domain.Dtos.PosSpeed(longitude, 0.0), new Enigma.Domain.Dtos.PosSpeed(0, 0), new Enigma.Domain.Dtos.PosSpeed(0, 0) };
//             });
//         // Act
//         double resultJd = _finder.FindJulianDay(targetLongitude, startJd, flags);
//         // Assert
//         Assert.That(resultJd, Is.EqualTo(startJd).Within(0.0001));
//     }
//
//     [Test]
//     public void HandlesReverseZeroCrossing()
//     {
//         // Arrange: simulate the Sun's longitude decreases linearly from 1.0 to 359.0 over the search period, target 0.0 at 2450000.0
//         double startJd = 2450000.0;
//         double targetLongitude = 0.0;
//         int flags = 258;
//         A.CallTo(() => _fakeCalc.CalculateCelPoint(A<int>._, A<double>._, A<int>._))
//             .ReturnsLazily((int seId, double jd, int f) =>
//             {
//                 double fraction = (jd - (startJd - 0.5)); // 0 to 1
//                 double longitude = (1.0 - 2.0 * fraction + 360.0) % 360.0; // 1.0 at start, 359.0 at end, 0.0 at midpoint
//                 return new[] { new Enigma.Domain.Dtos.PosSpeed(longitude, 0.0), new Enigma.Domain.Dtos.PosSpeed(0, 0), new Enigma.Domain.Dtos.PosSpeed(0, 0) };
//             });
//         // Act
//         double resultJd = _finder.FindJulianDay(targetLongitude, startJd, flags);
//         // Assert
//         Assert.That(resultJd, Is.EqualTo(startJd).Within(0.0001));
//     }
//
//     [Test]
//     public void ReturnsStartJdIfAlreadyAtTarget()
//     {
//         // Arrange: simulate the Sun's longitude is always at the target
//         double startJd = 2450000.0;
//         double targetLongitude = 42.0;
//         int flags = 258;
//         A.CallTo(() => _fakeCalc.CalculateCelPoint(A<int>._, A<double>._, A<int>._))
//             .Returns(new[] { new Enigma.Domain.Dtos.PosSpeed(targetLongitude, 0.0), new Enigma.Domain.Dtos.PosSpeed(0, 0), new Enigma.Domain.Dtos.PosSpeed(0, 0) });
//         // Act
//         double resultJd = _finder.FindJulianDay(targetLongitude, startJd, flags);
//         // Assert
//         Assert.That(resultJd, Is.EqualTo(startJd).Within(1e-6));
//     }
} 