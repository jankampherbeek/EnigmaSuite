/*
   HYPOPLAN.C

   calculation of an ephemeris of the three hypothetical planets
   as in use by the Ram school, calculated according to Erlewine,
   written in Microsoft C by Ingmar de Boer (c) 1996, 2000

   This version: orbits according to Ram/Tollenaar.

   Checked: 27 september 2001

*/

#include <stdio.h>
#include <math.h>

#define EARTH     0
#define PERSEFONE 1
#define HERMES    2
#define DEMETER   3


/* global structures */
typedef struct threeterms {double term1,term2,term3;} ThreeTerms;
typedef struct position {double longitude,latitude,distance;} Position;
typedef struct vector {double x,y,z;} Vector;
typedef struct polar {double phi,theta,r;} Polar;
typedef struct orbit {
   ThreeTerms M,e;
   double a;
   ThreeTerms PA,node,incl;
} Orbit;


/* array of orbits of planets according to Ram/Tollenaar a.o. */
Orbit orbits[4] =
{
  {{358.47584,35999.0498,-.00015},{.016751,-.41e-4,0},1.00000013,{101.22083,1.71918,.00045},{0,0,0},{0,0,0}},
  {{295.0,60,0},{0,0,0},71.137866,{0,0,0},{0,0,0},{0,0,0}},
  {{134.7,50,0},{0,0,0},80.331954,{0,0,0},{0,0,0},{0,0,0}},
  {{114.6,40,0},{0,0,0},93.216975,{0,0,0},{125,0,0},{5.5,0,0}}
};


/* array of orbits of planets according to De Beer */
Orbit orbitsDeBeer[4] =
{
  {{358.47584,35999.0498,-.00015},{.016751,-.41e-4,0},1.00000013,{101.22083,1.71918,.00045},{0,0,0},{0,0,0}},
  {{295.0,61.4689,0},{0,0,0},70,{0,0,0},{0,0,0},{0,0,0}},
  {{134.7,50.31153,0},{0,0,0},80,{0,0,0},{0,0,0},{0,0,0}},
  {{114.6,42.44636,0},{0,0,0},89.6,{0,0,0},{125,0,0},{5.5,0,0}}
};


/* utility functions and global variables */
double pi = 3.141592653589792907377;
double rad(double gr) {return pi * gr / 180.0;}
double gra(double ra) {if (ra<0) ra=ra+2*pi; return fmod((180.0 * ra / pi), 360.0);}
double terms(double T, ThreeTerms thiselement)
   {return thiselement.term1 + thiselement.term2 * T + thiselement.term3 * T * T;}


/* rectangular to polar coordinates */
Polar r2p (Vector rect)
{
   Polar pole;

   pole.r = sqrt((rect.x * rect.x + rect.y * rect.y + rect.z * rect.z));
   if (pole.r==0) pole.r = 1.7e-9;
   if (rect.x==0) rect.x = 1.7e-9;

   pole.phi   = atan2(rect.y, rect.x);
   pole.theta = asin(rect.z / pole.r);

   return pole;
}


/* polar to rectangular coordinates */
Vector p2r (Polar pole)
{
   Vector rect;

   rect.x = pole.r * cos(pole.theta) * cos(pole.phi);
   rect.y = pole.r * cos(pole.theta) * sin(pole.phi);
   rect.z = pole.r * sin(pole.theta);
   return rect;
}


/* Julian date */
double jd(int d, int m, int y, double gmt)
{
   if (m <= 2) {m += 12; y--;}
   return 3.0 - floor(y / 100) + floor(floor(y / 100) / 4)
      + floor(y * 365.25) + floor(30.6001 * (m + 1)) + 1720993.5 + d + gmt / 24.0;
}


/* T */
double T(double jd) {return (jd - 2415020.5) / 36525;}


/* heliocentric position vector of planet */
Vector helio_planet(double T, Orbit thisorbit)
{
   double m,e,ea,in,v,a,b;
   int    count;
   Vector anomaly_vec,helio_vec;
   Polar  anomaly_pol,helio_pol;

   m  = rad(gra(rad(terms(T, thisorbit.M))));
   e  = terms(T, thisorbit.e);

   /* solve Kepler's equation */
   ea = m;
   for (count=1;count<6;count++)
      ea = m + e * sin(ea);

   /* calculate true anomaly */
   anomaly_vec.x = thisorbit.a * (cos(ea) - e);
   anomaly_vec.y = thisorbit.a * sin(ea) * sqrt(1 - e * e);
   anomaly_vec.z = 0;
   anomaly_pol   = r2p(anomaly_vec);

   /* reduce to ecliptic */
   a  = gra(anomaly_pol.phi) + terms(T, thisorbit.PA);
   m  = rad(terms(T, thisorbit.node));
   v  = gra(rad(a + gra(m)));
   b  = rad(v);
   in = rad(terms(T, thisorbit.incl));
   a  = atan(cos(in) * tan(b - m));
   if (a<pi) a = a + pi;
   a  = gra(a + m);
   if (fabs(v - a) > 10) a = a - 180;

   /* heliocentric lat & long */
   helio_pol.phi   = rad(gra(rad(a)));
   helio_pol.theta = atan(sin(helio_pol.phi - m) * tan(in));
   helio_pol.r     = rad(thisorbit.a) * (1 - e * cos(ea));

   /* helio polar to rect */
   helio_vec = p2r(helio_pol);

   return helio_vec;
}


/* geocentric longitude, latitude and distance */
Position planet(double T, int thisplanet)
{
   Vector earth_helio;
   Vector thisplanet_helio;

   Vector thisplanet_geo_vec;
   Polar  thisplanet_geo_pol;

   Position thisposition;

   /* calculate heliocentric position of earth: */
   earth_helio = helio_planet(T, orbits[EARTH]);

   if (thisplanet!=EARTH)
   {
      thisplanet_helio = helio_planet(T, orbits[thisplanet]);

      /* calculate geocentric position: */
      thisplanet_geo_vec.x = thisplanet_helio.x - earth_helio.x;
      thisplanet_geo_vec.y = thisplanet_helio.y - earth_helio.y;
      thisplanet_geo_vec.z = thisplanet_helio.z - earth_helio.z;
   }
   else
   {
      thisplanet_geo_vec = earth_helio;
   }

   thisplanet_geo_pol = r2p(thisplanet_geo_vec);

   thisposition.longitude = gra(thisplanet_geo_pol.phi);
   thisposition.latitude  = gra(thisplanet_geo_pol.theta);
   thisposition.distance  = gra(thisplanet_geo_pol.r);

   if (thisplanet==EARTH) thisposition.longitude = gra(thisplanet_geo_pol.phi + pi);
   if (thisposition.latitude>180) thisposition.latitude-=360;

   return thisposition;
}


int main()
{

   double   t;
   int      months;
   int      years;
   Position persefone;
   Position hermes;
   Position demeter;

   years=1800;
   for (months=1;years<2100;months++)
   {
      if (months>12) {months=1;++years;};
      t = T(jd(1, months, years, 0));

      persefone = planet(t, PERSEFONE);
      hermes    = planet(t, HERMES);
      demeter   = planet(t, DEMETER);
      printf("%i,%i,%03.2f,%03.2f,%03.2f,%02.2f\n", months,years,persefone.longitude,hermes.longitude,demeter.longitude,demeter.latitude);
   }
   return 0;
}
