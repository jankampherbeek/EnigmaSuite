// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Ui.Interfaces;

namespace Enigma.Frontend.Ui.Support;



public class RangeCheck : IRangeCheck
{
    public double InRange360(double angle)
    {
        double angleInRange = angle;
        while (angleInRange < 0.0)
        {
            angleInRange += 360.0;
        }
        while (angleInRange >= 360.0)
        {
            angleInRange -= 360.0;
        }
        return angleInRange;
    }
}
