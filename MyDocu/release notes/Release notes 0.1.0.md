# Enigma Astrology Research - Release notes



## Release 0.1.0 beta - April 25, 2023

Basic functionality for charts (calculations, analysis) and research (data, simple tests).

**General**:

- User definable configuration for house systems, zodiac (tropical/sidereal), ayanamsha, observer position, projection to the ecliptic, celestial points to include, aspects to use, orbs for aspects/celestial points.
- Font with astrological symbols.

- Logging of errors.

- User manual and help system. Each window that is shown has access to a help-page.
- Automatic check for updates.
- Json database for charts.

**Charts**:

- Calculation, support for 23 house systems, tropical/sideral zodiac, 40 ayanamsha's, observer position (geocentric, heliocentric, topocentric [using parallax]), classic/modern planets, Chiron, Nessus, Pholus, 9 plutoïds, 6 planetoïds, hypothetical planets (School of Ram (3), Uranian astrology (8) and Transpluto), mathematical points (lunar node (true and mean), apogee (Black Moon, mean and corrected according to the Swiss Ephemeris), Vertex and Eastpoint. Support for oblique longitude (true astrological place, School of Ram). The user still needs to enter location coordinates and time-zone manually. 
- Covered period for calculation from 13000 BCE up to 16800 CE (almost 30000 years) for most important celestial points. For some of these points, Enigma only supports calculation for a shorter period.
- High quality graphical presentation of the chart (no 'staircase effect'). The chart figure uses equal signs and variable houses, shows aspects, and is adjustable in size.
- Overview of all calculated positions, including longitude, latitude, right ascension, declination, distance, azimuth and altitude. It also shows the daily speed, except for azimuth and altitude.
- Analysis
  - Aspects, a list with actual aspects, aspects to cusps. 
  - Midpoints in a 360° circle, represented as list and occupied midpoints for three dial sizes: 360°, 90° and 45°. The user can interactively change the dial size. 
  - Harmonics as a list. The user can interactively define and change the harmonic number. There is no limit for the maximum number. Support for fractional harmonics.

- Save charts in the Json database, and retrieve charts from the database. 

**Research**:

- Import csv-data from a specific format and convert it into Json format.

- Create control groups by shuffling the imported data. You can optionally multiply the items in the control group. Enigma uses a CSPRNG (Cryptographically Secure Pseudo Random Number Generator) when creating control groups, resulting in improved randomness compared to a PRNG  (Pseudo Random Number Generator).

- Create projects that support research. Within these projects, it is possible to calculate a large range of charts based on inputted data or on data from the control group. The research projects allow some simple counting: positions in signs, positions in houses, aspects, unaspected celestial points, occupied midpoints and harmonic positions that are conjunct radix positions.

  