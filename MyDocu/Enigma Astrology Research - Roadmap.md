# Enigma Astrology Research - Roadmap

*Jan Kampherbeek, 2023-02-04*

[TOC]

This roadmap contains a description of the functionality in the first releases of *Enigma Astrology Research*. It gives insight in the direction I want to go with Enigma but it is flexible; changes are possible but only if there is a compelling reason for such a change.  

## Objectives

The initial versions of Enigma will supply general support for calculating and analyzing charts but will also have a focus on five specific projects:

- Research into suicide (Vivian Muller).
- Research into explosions (Frank Vernooij).
- Research into fybromialgia (Herman Oldenburger and Vivian Muller).
- Harmonical aspect theory (Albert Bredenhoff).
- School of Ram techniques (Paula Schreurs).
- Micro astrology (Cemal Cicek).

Support for these projects and methods will be added step-by-step to Enigma. 



## Planned releases

This roadmap describes the expected functionality in the first 7 releases. 

A point by point description of the functionality of each planned release:

### Release 0.1 - beta

Basic functionality for charts (calculations, analysis) and research (data, simple tests).

**General**:

- User definable configuration for house systems, zodiac (tropical/sidereal), ayanamsha, observer position, projection to the ecliptic, celestial points to include, aspects to use, orbs for aspects/celestial points.
- Font with astrological symbols.

- Logging of possible errors.

- User manual and help system. Each window that is shown gives access to a help-page.
- Installation program and automatic check for updates.
- Database for charts.

**Charts**:

- Calculation, support for 21 house systems, tropical/sideral zodiac, 40 ayanamsha's, observer position (geocentric, heliocentric, topocentric [using parallax]), classic/modern planets, Chiron, Nessus, Pholus, 9 plutoïds, 6 planetoïds, hypothetical planets (School of Ram (3), Uranian astrology (8) and Transpluto), mathematical points (lunar node (true and mean), apogee (Black Moon, mean and corrected according to the Swiss Ephemeris), Vertex and Eastpoint. Support for oblique longitude (true astrological place, School of Ram). The user still needs to enter location coordinates and timezone manually. 
- Covered period for calculation from 13000 BCE up to 16800 CE (almost 30000 years) for most important celestial points. Chiron however is only supported from 675 CE up to 4650 CE. Several other smaller bodies are supported from 3000 BCE up to 3000 CE.
- High quality graphical presentation of the chart (no 'staircase effect'). The chart figure uses equal signs and variable houses, shows aspects, and is resizable.
- Overview of all calculated positions, including longitude, latitude, right ascension, declination, distance, azimuth and altitude. The daily speed is also shown except for azimuth and altitude.
- Analysis: aspects, a list with actual aspects, aspects to cusps. Midpoints in a 360° circle, represented as list and occupied midpoints for three dial sizes: 360°, 90° and 45°. The user can interactively change the dial size. Harmonics in the form of a list. The user can interactively define and change the harmonic number. There is no limit for the maximum number. Support for fractional harmonics.
- Save charts into database and retrieve charts from database. Searchitem is name/id of chart owner. 

**Research**:

- Import csv-data from a specific format and convert it into Json format.

- Create control groups by shuffling the imported data. Optionally the items in the control group can be multiplied. 

- Use a real random number generator (not a pseudo random number generator).

- Create projects that support research. Within these projects it is possible to calculate a large range of charts, based on inputted data or on data from the control group. The research projects allow some simple countings: positions is signs, positions in houses, aspects, unaspected celestial points, occupied midpoints and harmonic positions that are conjunct radix positions.

  

### Release 0.2 - beta

Implementing a first set of progressive techniques.

**General**:

- Support for data that describes events. These events are linked to persons in the dataset as mentioned for release 0.1.
- Add orbs for progressive techniques to configuration.
- Add events to database.

**Charts (progressive)**:

- Primary directions. Support for Placidus semi-arc directions and directions under the pole, for Regiomontanus and Campanus, all mundane and zodiacal.  Time keys: Naibod, Cardan, Ptolemy, true solar arc and mean solar arc (40 possibe combinations). This includes support for the system by Wim van Dam (semi-arc, zodiacal, true solar arc). Supports calculation of a 'calendar' with date and time of exactness and also supports the calculation for a specific date/time with matches that are within orb.  
- Primary directions - optionally add support for Topocentric primary directions.
- Secundary progressions with the following time keys:  astronomical days = astronomical year (tropical and sidereal), calendar days = calendar years (using mean calender and days defined in UT). 
- Symbolic progressions. Solar arc directions and variants. Timekeys: Naibod, Cardan, Ptolemy, true solar arc and mean solar arc and user defined fixed values of any size. 
- Transits. 
- Solar returns, tropical and sidereal, supporting relocation.  
- For al progressive techniques: includes matches between progressive and radix positions, defined in aspects, midpoints or harmonics.
- Save events to database and search for events for a given chart.

**Research**:

- Construct a control group for progressions, separate shuffling for radix date/time and event date/time.

- Provide calculated totals for progressive positions, using all methods mentioned above, in relation to a chart. Counts aspects between progressed and radix positions, progressive positions that occupy radix midpoints and the other way around, progressive positions that occupy a harmonic point and the other way around. 

  

### Release 0.3 - beta

Adds harmonic and historical orb definitions, celestial objects, dials for midpoints. techniques for the school of Ram and testing methods.

**General**:

- Extend configuration with values for orb for midpoints and harmonics, using different dials, with parameters for historical orbs, for using sect for Pars Fortunae and for rulerships.
- Automatic cleaning of old log files.
- User defined selections for glyphs.

**Charts**:

- Calculation of Pars Fortunae, hypothetical planets as proposed by Carteret and for the apogee (Black Moon) according to Duval. (Approached calculation with formulae by Cees Jansen).
- Harmonic orbs in two versions, according to the approaches by Albert Bredenhoff and David Hamblin. Both a list of values and usage in the actual calculation of aspects.
- Dynamic orbs, an approach to take the development of the orb during a given timespan into account (to be defined in cooperation with Frank Vernooij).
- Midpoints: introducing configurable orb for each dial and adding dials for 22°15' and 11°7'30".
- School of Ram: spiderweb, short path and house-cycles, including images.

**Research**:

- Countings for celestial points at corners, elevation (close to MC), prominence (several rulesets, to be defined), principles (combination of sign, house and planet, e.g. Taurus, II and Venus), maximality (to be defined, configurable conditions) and short path (school of Ram).
- Paging function to browse through the calculated charts.



### Release 0.4 - beta

Support for English and Dutch. Adding multiple configurations, including both standard and user definable configurations. Adding functionality for parans. Adding tags to charts in database.

**General**:

- Bi-linguality: English and Dutch for all texts in the application, the help system, and the user manual.
- User can define the location for files.
- Support for multiple configurations. Introduces a set of standard configurations: Common western (western mainstream astrology), School of Ram, Micro astrology, Uranian astrology, Ebertin system, Hellenistic astrology. The user can define additional configurations based on one of the standard configurations.
- Introduction of EAFS: Enigma Astrological Fact Sheets. Documentation for key aspects in astrology.

**Charts**:

- The user can add multiple tags to the saved charts, making it possible to search for specific criteria.
- Support for parans, as in the current program Enigma Parans.

**Research**:

- Import data: support for files with Gauquelin data and for files containing earthquakes.



### Release 0.5 - beta

Adds several technical possibilities.

**Charts**:

- 3d-aspects: the shortest distance along the globe between two celestial points. Used as an alternative for ecliptical aspects, both in the drawing of the chart and in the list of aspects. 

- Lists with distances between celestial points in both ecliptical and 3d coordinates.

- Declinations: (Contra)-parallel and OOB positions.
- Calculation of 'super-midpoint' for user defined set or celestial points.
- Graphical presentation of midpoints, using several dials
- Graphical presentation of midpoints as planetary pictures.
- Midpoints for declinations.

**Calculators**: 

- Calculator for julian day number.
- Calculator for obliquity.
- Conversion between coördinates.

**Research**:

- Counts for (contra)parallels and occupied declination midpoints, both in radix and in progressive techniques.
- Add histograms and linecharts.



### Release 0.6 - beta

Usability. 

**General**:

- New font for glyphs based on Unicode, adding more glyphs.
- Input location with database or with Google Maps (or alternative).
- Read default time settings from tz database.
- Export images to pdf to support printing.

**Charts**:

- Declinations: longitude equivalent (according to Kt Boehrer).
- Declinations: declination diagram (according to Kt Boehrer).

**Calculators:**

- Heliacal rising and setting.



### Release 1.0

The first non-beta release.

**Charts**:

- Full support for micro astrology.
- Show additional information with chartwheel, using mouse-over. The content is configurable.
- Solution for showing a large amount of celestial points in the chart wheel (to be defined).

**Cycles**:

- Calculate timeseries of positions in tropical or sidereal positions, geocentric of heliocentric.
- Show positions in line diagram and a histogram of totals.
- Show distances between specified celestial points in line diagram and a histogram of totals.
- Support for approach by Robert Doolaard.

