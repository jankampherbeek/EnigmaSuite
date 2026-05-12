# Release notes Enigma Astrology Research



## Release 0.8.2 beta - February 25, 2026

Enigma could not find city names with accented characters. Replaced all accented characters in city names with plain characters.

Improved the visibility of a selected line in PreNatal.



## Release 0.8.1 beta - January 20, 2026

Extended supported period for PreNatal to 120 years.

Fixed problem with chart wheel. If an extremely large amount of planets was selected an endless loop could occur because not enough positions were available. Now it is accepted that symbols can partly overlap so that there is always space.

## Release 0.8.0 beta - December 12, 2025

Adding Venus Star Point, Schema for Invisible Luminaries Astrology, Prenatal technique (Eg Sneek), Progressive calendar, changing the startpoint of the zodiac and updated data for time changes and coordinates.

### Updated time changes and coordinates

Data for the calculation for time changes and for coordinates is updated to the most recent version (November, 29, 2025).

### Alternative starting point for zodiac

An alternative startpoint for a zodiac. You can use this to create a draconic zodiac or any zodiac relative to a celestial point.

### Venus Star Point

The Venus Star Point, as defined by Arielle Guttman. The star point figure is shown based on suggestions by Arielle Guttman.

### Prenatal technique

The prenatal technique, as proposed by Eg Sneek. 

### Invisible Luminaries Astrology Schema

A schema with an extensive overview of Invisible Luminaries Astrology, as defined by Cees Jansen.

### Progressive calendar

A progressive calendar that shows both transits and secundary directions, both as list and as a graphic.



### Bugfixes

- The calculation of the position for the Interpolated Black Moon is now correct. It showed the Mean Black Moon instead.
- Symbols for planets in the chart wheel could, in rare circumstances, overlap. This has been fixed.
- An incorrect symbol was shown for the Quintile aspect.



## Release 0.7.2 beta - August 12, 2025

Bugfix

### Fixed remaining error in time rule

In release 0.7.1 most errors for time rules were solved with one exception. In some cases DST was not recognized like for October 21, 1986 in Glasgow. The DST rule 6>=23 was interpreted as 6>=2. This had also to be solved in a separate program tz-coord that prepares the timezone database for Enigma. This has now been solved.

## Release 0.7.1 beta - August 10, 2025

Bugfixes

### Errors in time rules 

The DST was calculated twice, resulting in a wrong time offset. This is corrected.

For large countries with many cities, the lookup of cities took several seconds. As a side effect (because of caching), the offsets were wrong. The lookup is now instant and the caching problem has been addressed.

There are still errors in time rules but they are relatively rare and will be addressed in release 0.7.2.

### Missing name of location

The location name was not shown in the windows for the chart wheel and for positions. This has been fixed.



## Release 0.7.0 beta - August 6, 2025

Zodiac divisions, solar returns and solar.

### Zodiac divisions

Calculation of:

- decans, both based on signs and based on planets.
- dodecatemoria (dwads), the original version and the 13th harmonic variant.
- bounds (terms), the Egyptian version and the version according to Ptolemy. 

### Solar returns

Calculation of a solar. Enigma supports relocation and the use of a sidereal return in a chart that is tropical. The solar is shown as a chart figure and as a list of positions (including declinations). Enigma also calculates the aspects between the solar and the radix. 

### Enneagram

Calculation of the relative strengths of the 9 Enneagram types, using on positions in the chart. The underlying theory is from the dutch researcher Sjoerd Visser. Enneargram supports two versions, including the '2012' version that has not yet been published.

### Export figures to an image on disk

You can now export all figures to an image in PNG format to disk. You can use this image in your word processor, website of just print it. This functionality is available for the chart wheel (including the solar), the declination diagram, the declination strip and the Enneagram. 

### Coordinates and time zone offset can be edited

Enigma automatically calculates the coordinates and the time zone offset. You can now override these values. 

### Regions for cities are shown

If you select a city when entering a chart (or an event), the region is automatically shown. This comes in handy if a country has multiple cities with the same name.

### Background colors for signs based on elements

The zodiac signs in the chart wheel are shown with a color, based on the elements. You can also choose a neutral background.

### Fix: take time into account for events

The time is not taken into account when defining an event for transits, secondary directions or symbolic directions.

### Fix: earth is disabled (for now)

In previous versions, you could select the earth in the configuration. this was premature as Enigma does not yet support heliocentric charts, so the earth has been temporarily removed.



## Release 0.6.2  beta - June 25, 2025

Several bugfixes

### Vertex, Eastpoint, Pars Fortunae, aspects with zero orb, orb with fraction in config

Vertex, Eastpoint and Pars Fortunae were not shown in the chart. Vertex and Eastpoint do have a glyph now.

If a celestial point has in the configuration an orb defined as zero, it will be ignored.

An orb in the configuration that has a fraction is now handled correctly.



## Release 0.6.1 beta - June 20, 2025

Bugfix

### Include timezone and coordinate data

In version 0.6 the data for timezones and geographic coordinates was not included, making it impossible to enter a new chart.



## Release 0.6 beta - June 18, 2025

Support for geographic coordinates and timezones. 



### Automatic coordinates and timezones

- When entering a new chart, you can select the country and the city from a drop down list and Enigma will automatically fill in the coordinates.
- All cities and villages in the world with a population of at least 500 people are supported
- After entering the country and city, Enigma will show the offset and the DST as defined in the TimeZone database.

### New points for Black Lights Astrology

- Support for Dragon and Beast (Black Lights astrology) and for southern lunar node.

  

### User defined work folder

- You can now define a work folder that will contain the results for tests and the log files.
- Other items, database and configuration, are saved in the user folder of Windows.
- The standard location c:/enigma-ar is not required anymore
- An existing database is automatically copied to the new location



### Research

- Added mean values for totals in tests for sign positions, house positions, aspects.
- Changed result files from Json to csv, which drastically reduces the file size.
- You can now easily import the calculated results into a spreadsheet.
- Improved memory handling so that even millions of charts can be calculated.
- You can now remove projects and data files via the user interface.



### Fix

- Added Lot of Fortune to research results, this was missing in previous versions





## Release 0.5.1 beta - February 18, 2025

Bugfix

### Research module: size of control data

An Out of memory error occurred when processing a large control group. Typically at a size of 20,000 charts (the size of the test group multiplied with the factor for the control group). This problem has been solved.



## Release 0.5.0 beta - September 10, 2024

Focus on primary directions

### Minor changes

- Position lines in the declination diagram now use different colors that correspond with the celestial points.
- The wheel diagram now has a checkbox that disables cusps and angles and moves 0 Aries to the left.

### Primary directions

- Support for primary directions for Placidus and Regiomontanus, both zodiacal and mundane, conjunctions and oppositions, and using 5 different time keys. No support yet for other aspects nor for converse directions.
- The configuration for progressions has an additional section for primary directions.  

### User manual

- The user manual now contains a description how to export the results of the research module to Excel.



## Release 0.4.1 beta - May 18, 2024

Corrected the size for the OOB region in the Declination Strip. Thanks to Wendy guy for the tip.



## Release 0.4.0 beta - May 17, 2024

Focus on declinations and improvements for data import.

### General

- All orbs and orb percentages are now sortable (Code by Gökhan Yu).
- The obliquity of the earth's axis is now always the true obliquity (including the effects of nutation).

### Declinations

New functionality, added:

- Parallels in declination.
- Midpoints in declination.
- Longitude equivalents according to Kt Boehrer.
- Declination diagram according to Kt Boehrer.
- Declination strip.
- OOB calendar.

### Research

- Research method for parallels in declination.
- Research method for midpoints in declination.
- Research method for OOB positions.
- The multiplication factor for control groups is now either 1, 10, 100 or 1000.

### User manual

Recreated HTML version of user manual so that it can be used offline.





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

  