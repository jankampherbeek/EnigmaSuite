# Enigma Astrology Research - Roadmap

2022-11-23 

[TOC]

## General

### Objectives

The initial versions of Enigma will supply general support for calculating and analyzing charts but will also have a focus on five specific projects:

- Research into suicide by Vivian Muller.
- Research into explosions by Frank Vernooij.
- Research into fybromialgia by Herman Oldenburger and Vivian Muller.
- Harmonical aspect theory by Albert Bredenhoff.
- Micro astrology by Cemal Cicek.

Support for these projects and methods will be added step-by-step to Enigma during the first four releases. 



### Planned releases

This roadmap describes the expected functionality in the following releases:

- 0.1 Initial beta-version with support for calculations.
- 0.2 Second beta-version with technical improvements (regarding programming) and more functionality.
- 0.3 First publicly available release but still beta and most of the functionality for the projects mentioned above.
- 1.0 First 'golden code' version (not beta anymore) with fixes and suggestions from the first releases applied.



## Functionality 

A point by point description of the functionality of each planned release.

### Release 0.1

#### General

- User defined settings for:
  - Housesystem (21 options).
  - Zodiac (tropical/sidereal) and Ayanamsha (40 options).
  - Observer position (geocentric, heliocentric, topocentric [using parallax correction]).
  - Type of projection to the ecliptic (standard, oblique longitude [School of Ram]).
  - Base orb for aspects.
  - Base orb for midpoints.
  - Base orb for progressions.
  - Selection of supported celestial points: classic and modern planets, Vertex, Eastpoint, Pars Fortunae (with and without sect), 7 mathematical points (Lunar node, apogee [Black Moon] etc.), 9 Plutoïds, Centaurs (Chiron, Nessus, Pholus), 6 Planetoïds, 14 hypothetical planets (including Uranian TNP's (8), School of Ram (3), Carteret (2) and Transpluto).
  - Weighting factor to define the orb for each celestial point mentioned above.
  - Selection of aspects: 5 major, 7 minor and 10 micro aspects.
  - Weighting factor for each aspect.
  - Specific orbs (e.g. aspects to cusps).
  - Three methods to define an orb.
- Input location and time for charts, coordinates and timezone to be entered by the user.
- Calculation of charts with support of all elements that can be defined in the settings, covering a period of 13000 BCE up to 16800 CE (almost 30000 years) for most important celestial points. Chiron however is only supported from 675 CE up to 4650 CE. Several other smaller bodies are supported from 3000 BCE up to 3000 CE.
- High quality graphical presentation of the chart (no 'staircase effect' in oblique lines or circles). The chart figure uses equal signs and variable houses, shows aspects, and is resizable.
- Font with astrological symbols
- Overview of all calculated positions, including longitude, latitude, right ascension, declination, distance, azimuth and altitude. The daily speed is also shown except for azimuth and altitude.
- Logging of possible errors.
- User manual and help system. Each window that is shown gives access to a help-page.
- Installation program.
- Automatic check for updates.

#### Analysis

- Aspects: a list with actual aspects, calculated based on the settings the user made. 
- Aspects to cusps: a list with aspects form celestial points to cusps, but not between cusps.
- List with all midpoints in a 360° circle, regardless if they are occupied or not.
- List with occupied midpoints for three dial sizes: 360°, 90° and 45°. The user can interactively change the dial size. 
- List with harmonics. The user can interactively define and change the harmonic number. The harmonic number should be at least 1, there is no limit for the maximum number but for extremely high numbers the results will be less reliable.

#### Progressive

- Primary directions. Support for Placidus semi-arc directions, both mundane and zodiacal, with support for the following timekeys: Naibod, Cardan, Ptolemy, true solar arc and mean solar arc. This includes support for the system by Wim van Dam (semi-arc, zodiacal, true solar arc). Supports calculation of a 'calendar' with date and time of exactness and also supports the calculation for a specific date/time with matches that are within orb.
- Secundary directions with the following time keys:  astronomical days = astronomical year (tropical and sidereal), calendar days = calendar years (using mean calender and days defined in UT). Includes matches between radix and secundary positions.
- Transits. Includes matches between radix and transit positions.



#### Research

- Import csv-data from a specific format and convert it into Json format.
- Construct a control group by shuffling the imported data. Optionally the items in the control group can be multiplied.
- Construct a control group for progressions, separate shuffling for radix date/time and event date/time.
- Create projects that support research.
- Calculation of a large range of charts, based on inputted data or on data from the control group.
- Paging function to browse through the calculated charts.
- Counting positions is signs.
- Counting positions in houses.
- Counting for aspects.
- Counting for unaspected celestial points.





### Release 0.2

#### General

- Automatic cleaning of old log files.

#### Analysis

- Harmonic orbs, according to the approach by Albert Bredenhoff. Both a list of values and usage in the actual calculation of aspects.
- Harmonic orbs, according to the approach by David Hamblin. Both a list of values and usage in the actual calculation of aspects.
- Midpoints: introducing configurable orb for each dial and adding dials for 22°15' and 11°7'30".
- Dynamic orbs, an approach to take the development of the orb during a given timespan into account.

#### Progressive

- Primary directions based on Regiomontanus.

#### Research

- Import data: support for files with Gauquelin data and for files containing earthquakes.
- Counting positions in harmonics.
- Counting aspects in harmonics.
- Counting occupied midpoints.
- Counting celestial points at corners.
- Counting elevation (close to MC).
- Counting prominence (several rulesets, to be defined).



### Release 0.3

#### General

- User can define the location for files.
- Support multiple configurations.
- Introduce a set of standard configurations
  - Common western (western mainstream astrology).
  - School of Ram.
  - Micro astrology.
  - Uranian astrology.
  - Ebertin system.
  - Hellenistic astrology.
- User defined configurations based on one of the standard configurations.
- Bi-linguality: English and Dutch for all texts in the application, the help system and the user manual.

#### Analysis

- 3d-aspects: the shortest distance along the globe between two celestial points. Used as an alternative for ecliptical aspects, both in the drawing of the chart and in the list of aspects.
- List with distances between celestial points. In ecliptical and 3d coordinates.
- Declinations: (Contra)-parallel, OOB positions.

#### Progressive

- Primary directions: to be decided, either Placidus under the pole or Campanus.
- Symbolic directions: solar arc, fixed values.
- Solar return, with difference between tropical and sidereal returns.
- Optionally relocation for solar return. 

#### Research

- Counting principles (combination of sign, house and planet, e.g. Taurus, II and Venus), conditions will be configurable.
- Counting maximality (to be defined, configurable conditions).
- Support for progressive positions:
  - Count of progressive aspects at the time of a given event.
  - Count of progressive midpoints at the time of a given event.

#### Calculators

- Calculator for julian day number.
- Calculator for obliquity.
- Conversion between coördinates.

#### Cycles

- Calculate timeseries of positions in tropical or sidereal positions, geocentric of heliocentric.
- Show positions in line diagram and a histogram of counts.
- Show distances between specified celestial points in line diagram and a histogram of counts.



### Release 1.0

#### General

- User defined selections for glyphs.
- New font for glyphs based on Unicode.
- Show additional information with chartwheel, based on user input.
- Solution for showing a large amount of celestial points in the chart wheel (to be defined).
- Input location with database or with Google Maps (or alternative).
- Read default time settings from tz database.

#### Analysis

- Graphical presentation of midpoints, using several dials
- Graphical presentation of midpoints as planetary pictures.
- Midpoints for declinations.
- Full support for micro astrology.
- Calculation of 'super-midpoint' for user defined set or celestial points.

#### Progressive

#### Research

#### Calculators

#### Cycles

- Support for approach by Robert Doolaard.

