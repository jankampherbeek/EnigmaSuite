# Enigma global planning

[TOC]




## Release 2022.0.3

- Data input for the calculation of charts, using databinding.
- Calculation of planetary positions and houses (Sun .. Pluto, Chiron, Nodes).
- Using one standard modus: Western Astrology.
- Calculated results are shown in a textual table.
  - Tabbed pages for ecliptical, equatorial and horizontal positions.
  - Where applicable, positions are given with glyphs.
- Add help screens, using the look-and-feel as used in RadixPro (2008 version).
- Add hints (bi-lingual).
- Use a separate file to populate texts, for now only in English.
- Create a class to handle reading of texts (Rosetta).
- Use mutex to force single instance being run. See Chowdhury, WPF dev. p. 46.
- Unit testing:
  - Improve mocking: add matchers.
  - Create tests for viewmodels.


## Release 2022.0.4

Drawing of chart, with equal signs, with equal houses and French variant.

Add persistency for charts. (Retrieving, saving). Select database and possible use of Ado.net.

Standardize input for date/time: same in calculations as in charts.	

Add data binding to calculations.



## Release 2022.0.5

Add oblique longitude, hypotheticals for School of Ram and Uranian astrology. 

Initial version of modi. Persistency and management for modi. (Retrieving, saving, editing).

Selection between Eris and Persephone.

## Release 2022.0.6

Aspects, midpoints and suggestion Paula Schreurs: houses and lords using anonymous planets. Use lists and graphic representation.



## Backlog Common

## Backlog Charts

- Structures

  - Dominantenverkettung (Chain of rulers)
  - Spiderweb (School of Ram)
  - Cyclings (School of Ram)
  
- Graphics

  - Drawing midpoint disc
  - Drawing as square
  - Drawing declination chart (Kt Böhrer)

- Input support

  - Location
    - Using Google Maps
    - Alternatively using list with coordinates
  - Date/time
    - Using tz Database

- Using modi

  - Create standard modi
    - Western standard
    - Hellenistic
    - School of Ram
    - Gansten
    - Uranian astrology
    - Ebertin
  - Selection of a specific modus
  - User defined modi

- Analysis

  - Harmonics
  - Declinations
    - Parallels
    - Declination midpoints
    - OOB positions
  - Parans
  - Mirror points (Jan de Jong)
  - Distance values
  - Decanates
  - Dodecatemoria
  - Monomoiria
  - Faces, terms
  - I Ching degrees
  - Trutina Hermetica
  - Encadrement (Volguine)
  - Chart-ruler
    - According to Volguine
    - Other theories
  - KAS system by Jack Chandu
  - Rationelle Astrologie by Wilhelm Hof

- Progressive

  - Transits
  - Secundary progressions
  - Primary directions
  - Solars
  - Profections
  - Symbolic progressions
  - Tertiary directions
  - Graphical presentation of progressions
  - Find matches between progressive and radix
  - Prenatal/points
    - Prenatal Eg Sneek
    - Prenatal Willi Sucher
    - Prenatal Leo Knegt
    - Logarithmic timesacle Tad Mann
    - Agepoints Bruno and Lousie Huber

- Calculations

  - Support heliocentric

  - Asteroids and Plutoids

  - Fixed stars

    

## Backlog Counting

- Data handling
  - Import data
  - Conversion
  - Generate control groups
  - Calculation of positions
- Analysis
  - Positions in signs, houses
  - Specific positions
  - Principles
  - Functionality as used in Enigma DedVM
  - Speed
  - Aspects
  - Exactness of aspects
  - Midpoints
  - Exactness Midpoints
  - Harmonics
  - Distance values
  - OOB
  - Declinations
  - Encadrement
  - Cycles (School of Ram)
  - Parans
- Configurables
  - Oblique longitude
  - Heliocentric
  - Sidereal

## Backlog Cycles

- Data input
- Calculations and screens as in existing version
- Waves
- Apsides
- Min/max speed
- Min/max declination
- Min/max latitude
- Generate ephemeris
- Epsilon

## Backlog Statistics

- As in Enigma DedVM
  - Planets in signs
  - Planets in houses
  - Planets at corners
  - Elevated planets
  - Prominent planets
  - Unaspected planets
  - Maximal planets
  - Principles
- Zodiacal divisions
  - Dodecatemoria
  - Terms
  - Decanates
  - Monomoiria
- Midpoints
- Harmonics
- Rulership / houses
- KAS system Jack Chandu
- Rationelle Astrologie by Wilhelm Hoff

## Backlog Calculations

- Coordinate conversions
- Calculations of sidereal time
- Cusps from ST
- Search for date/tome based on positions
- Compare calendars

## Backlog meta

- Installer
  - User can select a specific location to install
  - User can select a location for data
  - Application checks availability of SE files and downloads them, if required

## Finished releases

### Release 2022.0.1

Feasibility: make sure that the SE can be accessed and create a walking skeleton from UI to backend, and back again.

- Dashboard: startup screen. Contains 1 image (Calculations) and a button. Clicking on image or button shows dashboard for calculations. Close button.
- Calculations Dashboard. One selectable item: Julian Day Number.
- Screen for Julian Day Calculation. Entry of date, time and calendar. Assumes astronomical year-count. No validation. Show result after calling service.
- Backend: First version of access to Swiss Ephemeris.
- Service to access calculation of JD.
- Domain: structs for SimpleDate and result containg only a double, a bool for success and a text for the reason of a failure.
- Unit tests for all functionality.
- Introduce mocking.
- Create GitHub repo.
- Use simple DI: constructor injection and building the object tree at the start of the application.
- No i18N yet, all texts in English.
- Use interfaces except for VO's and UI.

### Release 2022.0.2

Finishing the general approach and adding a calculator for obliquity.

- Add validations to screens.
- Add backend support for validations
- Define architecture: check MVVM
- Add proper DI: either Pure DI or a framework. Check for the best solution (read van Deursen/Seeman).
- Add possibility to start Calculation of obliquity to Dashboard for Calculations.
- Create screen for Calculation of obliquity. Entry of date, time and calendar. Assumes astronomical year-count. Show results after calling service. Show both mean and true obliquity.
- Service to access calculation of obliquity.
- Unit tests.

