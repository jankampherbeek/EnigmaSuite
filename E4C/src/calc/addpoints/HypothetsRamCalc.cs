// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.calc.util;
using E4C.Models.Domain;
using System;

namespace E4C.calc.addpoints;

public record OrbitDefinition
{
    public readonly double[] MeanAnomaly;
    public readonly double[] EccentricAnomaly;
    public readonly double SemiMajorAxis;
    public readonly double[] ArgumentPerihelion;
    public readonly double[] AscNode;
    public readonly double[] Inclination;

    public OrbitDefinition(double[] meanAnomaly, double[] eccentricAnomaly, double semiMajorAxis, double[] argumentPerihelion, double[] ascNode, double[] inclination)
    {
        MeanAnomaly = meanAnomaly;
        EccentricAnomaly = eccentricAnomaly;
        SemiMajorAxis = semiMajorAxis;
        ArgumentPerihelion = argumentPerihelion;
        AscNode = ascNode;
        Inclination = inclination;
    }

}

public interface ICalcHelioPos
{
    public double[] CalcEclipticPosition(double factorT, OrbitDefinition orbitDefinition);
}

public interface ICalcHypRamEclPos
{
    public double[] Calculate(SolarSystemPoints planet, double jdUt);
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
        if (m < 0.0) m += (Math.PI * 2);
        e = terms(factorT, orbitDefinition.EccentricAnomaly);

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
        m = DegreeRadianUtil.DegToRad(terms(factorT, orbitDefinition.AscNode));
        //v = gra(rad(a + gra(m)));
        v = a + DegreeRadianUtil.RadToDeg(m);
        if (v < 0.0) v += 360.0;
        b = DegreeRadianUtil.DegToRad(v);
        inc = DegreeRadianUtil.DegToRad(terms(factorT, orbitDefinition.Inclination));
        a = Math.Atan(Math.Cos(inc) * Math.Tan(b - m));
        if (a < Math.PI) a = a + Math.PI;
        a = DegreeRadianUtil.RadToDeg(a + m);
        if (Math.Abs(v - a) > 10) a = a - 180;

        /* heliocentric lat & long */
        //helio_pol[0] = rad(gra(rad(a)));
        helio_pol[0] = DegreeRadianUtil.DegToRad(a);
        if (helio_pol[0] < 0.0) helio_pol[0] += (Math.PI * 2);
        helio_pol[1] = Math.Atan(Math.Sin(helio_pol[0] - m) * Math.Tan(inc));
        helio_pol[2] = DegreeRadianUtil.DegToRad(orbitDefinition.SemiMajorAxis) * (1 - e * Math.Cos(ea));

        /* helio polar to rect */
        helio_vec = MathExtra.Polar2Rectangular(helio_pol);

        return helio_vec;
    }

    double terms(double T, double[] thiselement)
    { return thiselement[0] + thiselement[1] * T + thiselement[2] * T * T; }
}



public class CalcHypRamEclPos : ICalcHypRamEclPos
{
    public double[] Calculate(SolarSystemPoints planet, double jdUt)
    {
        double t = FactorT(jdUt);
        double[] earth_helio;
        double[] planetHelio;
        double[] thisplanet_helio = new double[3];

        double[] thisplanet_geo_vec = new double[3] ;
        double[] thisplanet_geo_pol = new double[3];

        ICalcHelioPos calcHelioPos = new CalcHelioPos();    // TODO use DI
        //  Position thisposition;

        /* calculate heliocentric position of earth: */
        earth_helio = calcHelioPos.CalcEclipticPosition(t, DefineOrbitDefinition(SolarSystemPoints.Earth));

        thisplanet_helio = calcHelioPos.CalcEclipticPosition(t, DefineOrbitDefinition(planet));


     //   if (thisplanet != EARTH)
     //   {
          //  thisplanet_helio = helio_planet(T, orbits[thisplanet]);

            /* calculate geocentric position: */
            thisplanet_geo_vec[0] = thisplanet_helio[0] - earth_helio[0];
        thisplanet_geo_vec[1] = thisplanet_helio[1] - earth_helio[1];
        thisplanet_geo_vec[2] = thisplanet_helio[2] - earth_helio[2];
    //    thisplanet_geo_vec.y = thisplanet_helio.y - earth_helio.y;
    //        thisplanet_geo_vec.z = thisplanet_helio.z - earth_helio.z;
     //   }
     //   else
     //   {
          //  thisplanet_geo_vec = earth_helio;
     //   }

        thisplanet_geo_pol = MathExtra.Rectangular2Polar(thisplanet_geo_vec);

        

        double posLong = DegreeRadianUtil.RadToDeg(thisplanet_geo_pol[0]);
        if (posLong < 0.0) posLong += 360.0;
        double posLat = DegreeRadianUtil.RadToDeg(thisplanet_geo_pol[1]);
        double posDist = DegreeRadianUtil.RadToDeg(thisplanet_geo_pol[2]);

        double[] thisPosition = new double[] {posLong, posLat, posDist };


      //  if (thisplanet == EARTH) thisposition.longitude = gra(thisplanet_geo_pol.phi + pi);
     //   if (thisPosition[1] > 180) thisPosition[1] -= 360;

        return thisPosition;
    }


    private double FactorT(double jdUt)
    {
        return (jdUt - 2415020.5) / 36525;
    }

    private OrbitDefinition DefineOrbitDefinition(SolarSystemPoints planet)
    {
        double[] meanAnomaly;
        double[] eccentricAnomaly = new double[] { 0, 0, 0 };
        double[] argumentPerihelion = new double[] { 0, 0, 0 };
        double[] ascNode = new double[] { 0, 0, 0 };
        double[] inclination = new double[] { 0, 0, 0 }; 
        double semiMajorAxis = 0.0;
        if (planet == SolarSystemPoints.Earth)
        {
            meanAnomaly = new double[] { 358.47584, 35999.0498, -.00015 };
            eccentricAnomaly = new double[] { .016751, -.41e-4, 0 };
            semiMajorAxis = 1.00000013;
            argumentPerihelion = new double[] { 101.22083, 1.71918, .00045 };
        }
        else if (planet == SolarSystemPoints.PersephoneRam)
        {
            meanAnomaly = new double[] { 295.0, 60, 0 };
            semiMajorAxis = 71.137866;
        }
        else if (planet == SolarSystemPoints.HermesRam)
        {
            meanAnomaly = new double[] { 134.7, 50.0, 0 };
            semiMajorAxis = 80.331954;
        }
        else if (planet == SolarSystemPoints.DemeterRam)
        {
            meanAnomaly = new double[] { 114.6, 40, 0 };
            semiMajorAxis = 93.216975;
            ascNode = new double[] { 125, 0, 0 };
            inclination = new double[] { 5.5, 0, 0 };
        }
        else throw new ArgumentException($"Unrecognized planet for OrbitDefinition: {0}", planet.ToString());
        return new OrbitDefinition(meanAnomaly, eccentricAnomaly, semiMajorAxis, argumentPerihelion, ascNode, inclination);
    }

}





