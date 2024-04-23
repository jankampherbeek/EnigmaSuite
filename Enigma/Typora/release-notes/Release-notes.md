# Release notes Enigma Astrology Research



## Release 0.4.0 beta - May 31, 2024

Declinations and improvements for data import. New setup for user manual.

### Declinations

New functionality, added:

- Parallels in declination.
- Midpoints in declination.
- OOB positions.
- OOB calendar [TODO]
- Longitude equivalents according to Kt Boehrer.
- Declination diagram according to Kt Boehrer.
- Diagram with declination ladder [TODO]
- Research method for parallels in declination.
- Research method for midpoints in declination.
- Research method for OOB positions.

### Data import

- Improved speed of import [TODO]
- Show the number of imported charts during import [TODO]

### User manual

Recreated HTML version of user manual so that it can be used offline. [TODO]





## Release 0.3.1 beta - March 5, 2024

Minor fixes: 

- added missing icon in menu for secundary directions.
- removed premature functionality that should have waited for release 0.4.0.

## Release 0.3.0 beta - March 4, 2024

Data import from PlanetDance, additional calculated points, configurable colors for aspects, and bug fixes.

### Data import

- You can now import the charts from a database in PlanetDance into the Enigma database.
- You can also import PlanetDance data and use it as research data in a project.

### Supported celestial points

- The calculation of the longitude of the Apogee (Black Moon) according to Max Duval is now supported, using a calculation as proposed by Cees Jansen..

- The calculation of the longitudes for the hypothetical planets Persephone and Vulcanus, as proposed by Jean Carteret, is now supported.


### Small improvements

- The chart wheel now shows more information about the chart, including date, time and location.

- You can now define the colors for aspect lines in the configuration.

### Bug fixes

- The icon in the menu for secondary directions is now shown.

- The positions for Hygeia and Astraea are now calculated.






## Release 0.2.0 beta - February 10, 2024

Support for several progressive techniques.

### General

**Updated user interface**. A modern style, based on the _Material Design_ approach by Google.

**Updated database**. Replaced the Json database from release 0.1 with a standard database.

**Updated user configuration**. The configuration can now handle updates for future releases. 
This means that the system will automatically perform an update of the configuration from release 0.2 to release 0.3. 
It is not possible to update automatically from the configuration in release 0.1 to release 0.2.

**Added configuration for progressive techniques**. It is now possible to define a configuration for progressive techniques. 

**Integrated user manual**. The user manual is available in both PDF and HTML format. The HTML version is on line and accessible from the application menu.



### Charts progressive

**Events for progressive techniques**. It is now possible to enter events that can be (re-)used in progressive techniques. Enigma saves these events automatically in the database. This means that the user can enter one event and use it with several progressive techniques.

**Calculation and analysis for transits**. The user can calculate transits for an event. 
Enigma shows the results in a table and also calculates the aspects between transits and points in the radix.

**Calculation and analysis for secondary directions**. It is also possible to calculate secondary directions.
Enigma uses separate tables to show the results of the positions and of aspects with points in the radix.

**Calculation and analysis for symbolic directions**. Support for symbolic directions. The user can select from three time keys. 
1 degree per year, mean daily movement of the Sun per year, and the actual movement of the Sun in days, used for each year.






## Release 0.1 beta - April 25, 2023 
Basic functionality for charts (calculations, analysis) and research (data, simple tests).

### General
**User definable configuration** for house systems, zodiac (tropical/sidereal), ayanamsha, observer position, projection 
to the ecliptic, celestial points to include, aspects to use, orbs for aspects/celestial points.

**Font with astrological symbols**.

**Logging of errors**.

**User manual and help system**. Each window that is shown gives access to a help-page.

**Automatic check for updates**.

**Json database for charts**.

### Charts
**Calculation**, support for 23 house systems, tropical/sideral zodiac, 40 ayanamsha's, observer position (geocentric, 
topocentric [using parallax]), classic/modern planets, Chiron, Nessus, Pholus, 9 plutoïds, 6 planetoïds, hypothetical 
planets (School of Ram (3), Uranian astrology (8) and Transpluto), mathematical points (lunar node (true and mean), 
apogee (Black Moon, mean and corrected according to the Swiss Ephemeris), Vertex and Eastpoint). 

Support for oblique longitude (true astrological place, School of Ram). 

The user still needs to enter location coordinates and time-zone manually.
Covered period for calculation from 13000 BCE up to 16800 CE (almost 30000 years) for most important celestial points. Calculation of Chiron is possible from 675 CE to 4650 CE. Enigma supports several other smaller bodies only from 3000 BCE up to 3000 CE.

**High quality graphical presentation of the chart** (no 'staircase effect'). 
The chart figure uses equal signs and variable houses, shows aspects, and is adjustable in size.

**Positions**
Overview of all calculated positions, including longitude, latitude, right ascension, declination, distance, azimuth 
and altitude. It also shows the daily speed, except for azimuth and altitude.


### Analysis
**Aspects**, a list with actual aspects, aspects to cusps.

**Midpoints**, represented as list and occupied midpoints for three dial sizes: 360°, 90° and 45°. 
The user can interactively change the dial size. 

**Harmonics** as a list. The user can interactively define and change the harmonic number. 
There is no limit for the maximum number. Support for fractional harmonics.

### Database

Save charts in the database, and retrieve charts from the database.

### Research

**Import csv-data** from a specific format and convert it into Json format.

**Create control groups** by shuffling the imported data. You can optionally multiply the items in the control group.

**Uses a real random number generator** (not a pseudo-random number generator).

**Create projects that support research**. Within these projects, it is possible to calculate a large range of charts 
based on inputted data or on data from the control group. 
The research projects allow some simple counting: positions in signs, positions in houses, aspects, unaspected celestial 
points, occupied midpoints and harmonic positions that are conjunct radix positions.

  