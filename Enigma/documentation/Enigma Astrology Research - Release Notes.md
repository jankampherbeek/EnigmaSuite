# Enigma Astrology Research - Release Notes 



[TOC]



## Planned release 0.1

This release will only be made available to selected researchers.

### Calculation

Exact calculation of astrological charts is supported whereby the Swiss Ephemeris routines are used.



#### Supported house systems

Placidus, GOH/Koch, Porphyri, Regiomontanus, Campanus, Alcabitius, Topocentric, Krusinski, APC/Abenragel, Morin, Whole Sign, Equal from ascendant, Equal from Mc, Equal from Aries, Vehlow, Axial Rotation, Horizontal, Carter, Gauquelin, Sunshine, Sunshine (Treindl). An option No house is also available.



#### Supported zodiacs

Both the tropical and the sideral zodiac are supported.

The user can use the following ayanamsha's: Fagan, Lahiri, DeLuce, Raman, UshaShashi, Krishnamurti, DjwhahlKhul, Yukteshwar, Bhasin, Kugler (3 versions), Huber, Eta Piscium, Aldebaran 15 Taurus, Hipparchus, Sassanian, Galactic Center 0 Sagittarius,  J2000, J1900, B1950, SuryaSiddhanta, SuryaSiddhanta Mean Sun, Aryabhata, Aryabhata Mean Sun, Ss Revati, Ss Citra, True Citra, True Rivati, True Pushya, Galactic Center (Brand), Galactic Equator IAU 1958, Galactic Equator, Galactic Equator Mid Mula, Skydram, True Mula, Dhruva, Aryabhata 522, Britton, Galactic Center 0 Cap.



#### Supported observer positions

Geocentric, Topocentric (corrected for parallax) and Heliocentric.



#### Supported ecliptical projections

Standard (two-dimensional), the usual projection in a right angle and oblique longitude, the method as used in the School of Ram.



#### Supported celestial points

Mundane points: MC, Ascendant , housecusps, Vertex, Eastpoint.

Lots: Pars Fortunae using sect and without sect.

Mathematical points: Mean Node, True Node, Zero Aries, Mean Apogee, Corrected Apoge, Corrected Apogee method Duval (approximately), Interpolated Apogee, 

Phsycial points: Sun, Moon, Mercury, Venus, Mars, Jupiter, Saturn, Uranus, Neptune, Pluto, Chiron, Nessus, Pholus, Juya, Varuna, Ixion, Quaoar, Haumea, Eris, Sedna, Orcus, Makemake, Ceres, Pallas, Juno, Vesta, Hygieia, Astraea

Hypothetical points: Cupido, Hades, Zeus, Kronos, Apollon, Admetos, Vulcanus (Uranian), Poseidon, Persephone (School of Ram), Hermes, Demeter, Vulcanus (Carteret), Persephone (Carteret), Transpluto.



#### Overview of positions

For celestial bodies the following values are shown:

longitude, latitude, right ascension, declination, distance, azimuth, altitude. The daily speed is given for all coordinates except for azimuth and altitude.



### Graphical

#### Chart drawing

The chart-drawing is of high quality: no 'stair-case effect'. The chart can be enlarged or reduced whereby font-size, thickness of lines etc. are newly dimensioned accordingly.



#### Browse function

The user can step through the chart-drawing of a set of charts.



### Analysis

All functionality for the analysis of a chart is available when using a single chart and when performing statistical analysis.



#### Aspects

##### Supported aspects

Conjunction, Opposition, Triangle, Square, Sextile, Semi-sextile, Inconjunct, Semi-square, Sesquiquadrate, Quintile, Bi-quintile, Septile, Vigintile, Undecile, Semi-quintile, Novile, Ni-novile, Centile, Bi-septile, Tri-decile, Tri-septile, Quadra-novile.

##### Orbs for aspects

###### Weighted orb

A weighting factor is defines for celestial objects and for aspects. These factors are combined with a user definable base-orb.

###### Fixed orb

###### Harmonic orb (Bredenhoff) [R2]

###### Dynamical orb [R2]



##### Specific aspects

###### Aspects to cusps

###### 3D aspects [R3]

Shortest angle between two celestial points.

###### All distances

List with all distances, user can define a subset of celestial points and/or ranges for the distance.





#### Midpoints

Supported swheels: 360 degrees and 90 degrees.

#### Harmonics

Harmonics according to Hamblin [R2]

#### Progressive

##### Transits

##### Secundary progressions

Standard solution [to be defined]

Additional time=keys [R3]



##### Primary directions

Placidus, secundary solar arc, no latitude (van Dam system).

Additional systems [R3]



##### Solars [R3]



##### Symbolic directions [R3]



### Research

#### Import of data

Reading data-files using a specific cvs-format and converting these to an internally used json-format. The data can be used within one or more projects.

#### Research projects

The user can create projects for specific research activities and based on an imported dataset.



 



#### Creation of controlgroups

Creation of controlgroups based on shuffling, using a True Random Number Generator.

Creation of controlgroups for progressions [to be defined].



### Cycles [R3]



### Specific calculations

#### Calculator for Julian Day Number [R3]

#### Calculator for obliquity of earth's axis [R3]

#### Calculator for the conversion of coördinates [R3]



### Configuration

Define personal preferences for calculations.

Define locations of files [R3]



### Meta

#### Installation program

#### Automatic update [R3]

Including check for ephemeris files.
