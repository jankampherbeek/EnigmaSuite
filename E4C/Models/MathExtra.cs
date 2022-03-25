using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E4C.Models;

internal class MathExtra
{

    public double[] Rectangular2Polar(double[] rectangularValues)
    {
        if ((rectangularValues == null) || (rectangularValues.Length != 3))
        {
            throw new ArgumentException("Invalud input for rectangularValues.");
        }
        double x = rectangularValues[0];
        double y = rectangularValues[1];
        double z = rectangularValues[2];
        double r = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y,2) + Math.Pow(z,2));
        if (r == 0) r = double.MinValue;
        if (x == 0) x = double.MinValue;
        double phi = Math.Atan2(y, x);
        double theta = Math.Asin(z / r);

        return new double[]{phi, theta, r };
    }

    public double[] Polar2Rectangular(double[] polarValues)
    {
        double phi = polarValues[0];
        double theta = polarValues[1];
        double r = polarValues[2];


        double x = r * Math.Cos(theta) * Math.Cos(phi);
        double y = r * Math.Cos(theta) * Math.Sin(phi);
        double z = r * Math.Sin(theta);

        return new double[] {x, y, z };

    }

}
