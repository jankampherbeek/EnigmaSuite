# Enigma Astrology Research - Roadmap

*Jan Kampherbeek, 2023-07-13*

[TOC]

This roadmap contains a description of the functionality in the first releases of *Enigma Astrology Research*. It gives insight into the direction I want to go with Enigma, but it is flexible; changes are possible, but only if there is an important reason for such a change.  

## Objectives

The initial versions of Enigma will supply general support for calculating and analyzing charts, but will also have a focus on seven specific projects:

- Research into suicide (Vivian Muller).
- Research into explosions (Frank Vernooij).
- Research into fybromialgia (Herman Oldenburger, Vivian Muller and Truus van Leeuwen).
- Harmonical aspect theory (Albert Bredenhoff).
- School of Ram techniques (Paula Schreurs).
- Micro astrology (Cemal Cicek).
- 3D-astrology (3D Astrology Technical Working Group).

I will add support for these projects and methods step-by-step to Enigma. 



## Existing releases

I will publish Release 0.1 April 25, 2023. See the release notes for the functionality in this release.



## Planned releases

This roadmap outlines the features that I have planned for the releases so far. 

A point-by-point description of the functionality of each planned release:



### Some additional stuff

Request by Mark Harris (2023/8/22): use different colors for progressions and radix positions in an Uranian disk. Can also be used for a plain chart.



### Release 0.2 - beta

Implementing a first set of progressive techniques. First attempt with 3d charts.

**General**:

- Support for data that describes events. These events are linked to persons in the dataset as mentioned for release 0.1.
- Import data from opengauquelin.org .
- Add orbs for progressive techniques to configuration.
- Add events to database.

**Charts (progressive)**:

- Primary directions. Support for Placidus semi-arc directions and directions under the pole, for Regiomontanus and Campanus, all mundane and zodiacal.  Time keys: Naibod, Cardan, Ptolemy, true solar arc and mean solar arc (tropical and sidereal) (56 possible combinations). This includes support for the system by Wim van Dam (semi-arc, zodiacal, true solar arc). Supports calculation of a 'calendar' with date and time of exactness and also supports the calculation for a specific date/time with matches that are within orb.  
- Primary directions - optionally add support for Topocentric primary directions.
- Secundary progressions with the following time keys:  astronomical days = astronomical year (tropical and sidereal), calendar days = calendar years (using mean calender and days defined in UT). 
- Symbolic progressions. Solar arc directions and variants. Timekeys: Naibod, Cardan, Ptolemy, true solar arc and mean solar arc and user defined fixed values of any size. 
- Transits. 
- Solar returns, tropical and sidereal, supporting relocation.  
- For all progressive techniques: includes matches between progressive and radix positions, defined in aspects, midpoints or harmonics.
- Save events to database and search for events for a chart.

**Charts (3D):**

- First attempt of showing a chart that takes 3d aspects into account for a specific house system (to be defined).

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
- More versions of 3D charts: supporting more house systems. Trying to support Alcabitius, Regiomontanus, Campanus and Placidus.

**Research**:

- Countings for celestial points at corners, elevation (close to MC), prominence (several rulesets, to be defined), principles (combination of sign, house and planet, e.g. Taurus, II and Venus), maximality (to be defined, configurable conditions) and short path (school of Ram).
- Paging function to browse through the calculated charts. 



### Release 0.4 - beta

~~Support for English and Dutch.~~ Adding multiple configurations, including both standard and user definable configurations. Adding functionality for parans. Adding tags to charts in database. <u>Modernized look and feel (using material design).</u>

**General**:

- ~~Bi-linguality: English and Dutch for all texts in the application, the help system, and the user manual.~~
- User can define the location for files.
- Support for multiple configurations. Introduces a set of standard configurations: Common western (western mainstream astrology), School of Ram, Micro astrology, Uranian astrology, Ebertin system, Hellenistic astrology. The user can define additional configurations based on one of the standard configurations.
- Introduction of EAFS: Enigma Astrological Fact Sheets. Documentation for key aspects in astrology.

**Charts**:

- The user can add multiple tags to the saved charts, making it possible to search for specific criteria.
- Support for parans, as in the current program Enigma Parans.
- 3D charts for ecliptical systems (where relevant).

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
- Attempt to construct 3d charts for topocentric houses. Depends on complexity, consider this as optional).

**Calculators:**

- Heliacal rising and setting.



### Release 1.0

The first non-beta release.

**Charts**:

- Full support for micro astrology.
- Show additional information with chart wheel, using mouse-over. The content is configurable.
- Solution for showing a large amount of celestial points in the chart wheel (to be defined).
- 3d charts for Krusinski.

**Cycles**:

- Calculate time series of positions in tropical or sidereal positions, geocentric of heliocentric.
- Show positions in line diagram and a histogram of totals.
- Show distances between specified celestial points in line diagram and a histogram of totals.
- Support for approach by Robert Doolaard.



### Backlog

In the backlog, a list of items I hope to realize in releases that I did not yet plan for. The sequence is random and I can give no guarantee that this functionality will ever make it to an Enigma release. But these points are serious candidates for future development. 

- Additional house systems.
- Prenatal positions according to Eg Sneek.
- Prenatal positions according to Leo Knegt.
- Logarithmic Time Scale according to Tad Mann.
- Age point, according to Louise and Bruno Huber.
- Arabic points, original and self defined.
- Arabic points in right ascension.
- KAS (Chandu).
- Rationelle astrologie (Hof).
- Horoscope rulers (Volguine and others).
- Framing (encadrement Volguine).
- Babylonian approach.
- Hellenistic approach.
- Team functionality: sharing data and results via the Internet.
- Apparent diameter of celestial bodies.
