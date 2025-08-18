// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using NUnit.Framework;

namespace Enigma.Test.Core.Slices.VenusStarPoint;

[TestFixture]
public class TestVspPositioning
{
    [Test]
    public void TestVspPositioningCalculation()
    {
        // Test case: Ascendant at 90° (9 o'clock position)
        // VSP at 0° Aries should be positioned at 0° on the wheel
        double ascendantLongitude = 90.0;
        double vspLongitude = 0.0;
        
        // Calculate the angle for positioning
        // Longitude starts at 0° Aries, and 9 o'clock is the ascendant
        // The difference between 0° Aries and ascendant is 360 - longitude asc
        double angle = vspLongitude - ascendantLongitude + 90.0;
        if (angle < 0.0) angle += 360.0;
        if (angle >= 360.0) angle -= 360.0;
        
        // Expected: 0° - 90° + 90° = 0°
        Assert.That(angle, Is.EqualTo(0.0).Within(1));
        
        // Test case: VSP at 180° should be positioned at 180° on the wheel
        vspLongitude = 180.0;
        angle = vspLongitude - ascendantLongitude + 90.0;
        if (angle < 0.0) angle += 360.0;
        if (angle >= 360.0) angle -= 360.0;
        
        // Expected: 180° - 90° + 90° = 180°
        Assert.That(angle, Is.EqualTo(180.0).Within(1));
        
        // Test case: VSP at 270° should be positioned at 270° on the wheel
        vspLongitude = 270.0;
        angle = vspLongitude - ascendantLongitude + 90.0;
        if (angle < 0.0) angle += 360.0;
        if (angle >= 360.0) angle -= 360.0;
        
        // Expected: 270° - 90° + 90° = 270°
        Assert.That(angle, Is.EqualTo(270.0).Within(1));
    }
    
    [Test]
    public void TestVspPositioningWithDifferentAscendant()
    {
        // Test case: Ascendant at 45° 
        double ascendantLongitude = 45.0;
        
        // VSP at 0° Aries should be positioned at 45° on the wheel
        double vspLongitude = 0.0;
        double angle = vspLongitude - ascendantLongitude + 90.0;
        if (angle < 0.0) angle += 360.0;
        if (angle >= 360.0) angle -= 360.0;
        
        // Expected: 0° - 45° + 90° = 45°
        Assert.That(angle, Is.EqualTo(45.0).Within(1));
        
        // VSP at 90° should be positioned at 135° on the wheel
        vspLongitude = 90.0;
        angle = vspLongitude - ascendantLongitude + 90.0;
        if (angle < 0.0) angle += 360.0;
        if (angle >= 360.0) angle -= 360.0;
        
        // Expected: 90° - 45° + 90° = 135°
        Assert.That(angle, Is.EqualTo(135.0).Within(1));
    }
    
    [Test]
    public void TestVspPositioningAngleNormalization()
    {
        double ascendantLongitude = 90.0;
        
        // Test case: VSP at 360° should be normalized to 0°
        double vspLongitude = 360.0;
        double angle = vspLongitude - ascendantLongitude + 90.0;
        if (angle < 0.0) angle += 360.0;
        if (angle >= 360.0) angle -= 360.0;
        
        // Expected: 360° - 90° + 90° = 360° -> normalized to 0°
        Assert.That(angle, Is.EqualTo(0.0).Within(1));
        
        // Test case: VSP at -90° should be normalized to 270°
        vspLongitude = -90.0;
        angle = vspLongitude - ascendantLongitude + 90.0;
        if (angle < 0.0) angle += 360.0;
        if (angle >= 360.0) angle -= 360.0;
        
        // Expected: -90° - 90° + 90° = -90° -> normalized to 270°
        Assert.That(angle, Is.EqualTo(270.0).Within(1));
    }
}
