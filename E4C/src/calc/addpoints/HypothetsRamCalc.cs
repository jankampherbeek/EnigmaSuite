// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.calc.util;
using E4C.Models.Domain;
using System;

namespace E4C.calc.addpoints;

public interface ICalcHelioPos
{
    public double[] CalcEclipticPosition(double factorT, OrbitDefinition orbitDefinition);
}


public class CalcHelioPos : ICalcHelioPos
{
    public double[] CalcEclipticPosition(double factorT, OrbitDefinition orbitDefinition)
    {
        double m, e, ea, inc, v, a, b;
        int count;
        double[] anomaly_vec = new double[3];
        double[] helio_vec = new double[3];
        double[] anomaly_pol = new double[3];
        double[] helio_pol = new double[3];

        //m = rad(gra(rad(terms(T, thisorbit.M))));
        m = DegreeRadianUtil.DegToRad(terms(factorT, orbitDefinition.MeanAnomaly));
        e = terms(factorT, orbitDefinition.Eccentricity);

        /* solve Kepler's equation */
        ea = m;
        for (count = 1; count < 6; count++)
            ea = m + e * Math.Sin(ea);

        /* calculate true anomaly */
        anomaly_vec[0] = orbitDefinition.SemiMajorAxis * (Math.Cos(ea) - e);
        anomaly_vec[1] = orbitDefinition.SemiMajorAxis * Math.Sin(ea) * Math.Sqrt(1 - e * e);
        anomaly_vec[2] = 0;
        anomaly_pol = MathExtra.Rectangular2Polar(anomaly_vec);

        /* reduce to ecliptic */
        a = DegreeRadianUtil.RadToDeg(anomaly_pol[0]) + terms(factorT, orbitDefinition.ArgumentPerihelion);
        m = DegreeRadianUtil.DegToRad(terms(factorT, orbitDefinition.AscendingNode));
        //v = gra(rad(a + gra(m)));
        v = a + DegreeRadianUtil.RadToDeg(m);
        b = DegreeRadianUtil.DegToRad(v);
        inc = DegreeRadianUtil.DegToRad(terms(factorT, orbitDefinition.Inclination));
        a = Math.Atan(Math.Cos(inc) * Math.Tan(b - m));
        if (a < Math.PI) a = a + Math.PI;
        a = DegreeRadianUtil.RadToDeg(a + m);
        if (Math.Abs(v - a) > 10) a = a - 180;

        /* heliocentric lat & long */
        //helio_pol[0] = rad(gra(rad(a)));
        helio_pol[0] = DegreeRadianUtil.DegToRad(a);
        helio_pol[1] = Math.Atan(Math.Sin(helio_pol[0] - m) * Math.Tan(inc));
        helio_pol[2] = DegreeRadianUtil.DegToRad(orbitDefinition.SemiMajorAxis) * (1 - e * Math.Cos(ea));

        /* helio polar to rect */
        helio_vec = MathExtra.Polar2Rectangular(helio_pol);

        return helio_vec;
    }

    double terms(double T, double[] thiselement)
    { return thiselement[0] + thiselement[1] * T + thiselement[2] * T * T; }
}





