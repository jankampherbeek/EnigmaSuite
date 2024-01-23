# User Manual Enigma Astrology Research - release 0.2

## Introduction

Thank you for your interest in Enigma. 
You are about to learn about a program that calculates charts and also supports several types of research into astrology.

You can use it for your astrological calculations. Enigma is accurate and calculates fast. 
It will produce a chart wheel and perform the most important analysis.

The program also supports several types of research: counting aspects, occupied midpoints and more. 
And it generates control groups so that you can cancel out artifacts.

Several other releases will follow and each new release will add more functionality.

Enigma is free: you do not have to pay for it. 
And Enigma is open source: I published all code, and anybody can download it. 
Programmers that want to use my code can do so freely, but only if their own software is also open source.

I hope you enjoy the program!

Jan Kampherbeek.


### Parts of the user manual

This User Manual comprises the following parts:

- **Configuration** and settings. You can tailor Enigma to your own preferences.

- **Charts**. How to calculate and analyze a chart.

- **Progressions**. How to use progressive techniques.

- **Research**. How to set up and perform research.

- **Appendix**. Specifications and background information.

### Related documents
- **Installation manual** which leads through the installation process.
- **Release notes**, an overview of add functionality in this and previous releases.
- **Pogrammers manual**, a separate manual fort those that are interested in the source code of Enigma.

## General information
Enigma consists of several modules, currently a module for charts and a module for research.
In future releases, I will add more modules.

### User assistance
Enigma tries to help you when using the program. 
It shows on-line help, checks your input, writes details to a log-file and prevents wrong results.

#### On-line help

In almost all windows, Enigma shows a button **Help**. 
If you click this button, the program shows specific information for the current window.
To prevent cluttering the screen with many windows, you need to close the help window before you can continue.

#### Input check
Where possible, Enigma checks your input. 
It checks for the correct format and range for date, time and geographical coordinates. 
It also checks if a date is valid and it takes leap years into account while doing so.

If you make an error, Enigma marks the entry with a red underlining. Just correct the input and click again.

Please note that you enter numbers with fractions based on the ‘locality’ (language and other settings) of your computer. 
If you normally use a dot to separate integer and fraction, you also need to use a dot in Enigma. 
The same if your computer expects a comma by default.

#### Log-files
Enigma writes information about its activities to log-files. 
These files are in the folder *c:\enigma_ar\logs*. Each day, the program starts a new log. 
If there are more than 31 log-files, the program deletes the oldest files. 
In case of an error, the log-files can contain important information about the error. 
I might ask you for these log-files if you report an error.

#### Preventing wrong results
Enigma supports almost 30,000 years of planetary calculations. 
But for some celestial points, this period is much shorter. 
Especially for Chiron, which you cannot calculate before 675 CE.

The Swiss Ephemeris, which takes care of all the calculations in Enigma, does not object if a planet is out of the 
supported range, it just returns a zero. 
This translates to zero Aries, and you could happily use this point and calculate aspects and midpoints. 
That makes little sense, so Enigma filters the results: if it cannot calculate a celestial point, it will just omit it.




### Starting Enigma

Click the Enigma icon to start the program.

![](start-icon.png)

You will see a window with two images, one labeled *Charts* and one labeled *Research*.

![](dashboard.png)

Click an image to go to the corresponding module. 
If you finish working in a module, you will return to this start window and can make another selection.
Both modules of Enigma use the same configuration and the same settings.

The settings define the location of files that Enigma uses.
The configuration gives you the opportunity to define how Enigma behaves astrologically: 
which house system, which planets, aspects, and much more.

### Settings
You can access *Settings* from both modules *Charts* and *Research* and check them, 
but in this release of Enigma, you can not change anything.

Select the menu option **General - Settings**. 
You will see a window with information about the location of the following files:

![](settings.png)

- **Data files**. This folder contains the data files you imported.

- The location of **Projects**. All projects will have a sub folder with the name of the project. These sub folders contain all data that is generated for this project.

- Enigma does not yet use **Exported files**. In the future, it will contain the results of exporting data.

- Enigma writes remarks to **Log files**. In case of an error, these log files will be helpful.

- The folder **Database** contains data for calculated charts.

### Configuration

You can check and change the configuration via the module *Charts* or via the module *Research*.
Select the menu option **General - Configuration**.

![](configuration-general.png)


This will show a window with a wide range of configurable items. The window comprises 3 tabs:

- The tab **General** shows astronomical settings and settings for orbs. This tab is initially shown.

- **Points** shows the most planets and other points.

- **Aspects** shows all available aspects.

#### General selections


If you select the tab *General* you can define the following preferences:

**House system**. Currently, Enigma supports only house systems with 12 houses. 
Select *None* if you do not want to use a house system.

**Type of zodiac**. Select either *sidereal* or *tropical*. 
If you select tropical, the Ayanamsha will always be None. 
If you select another Ayanamsha, the setting for Type of zodiac will automatically change into *sidereal*.

**Ayanamsha**. Select one of the available Ayanamshas. 
In the paragraph *Appendix: Ayanamshas* you will find a brief explanation of the different possibilities. 
See also the remarks at the point above: *Type of zodiac*.

**Observer position**. The standard approach is *geocentric*. 
If you want to take parallax into account select *topocentric*.

**Projection type**. Select *Standard (two-dimensional)* for most approaches. 
*Oblique longitude* provides an alternative calculation as supported by the School of Ram. 
It is also called *True Astrological Longitude Location*.

**Base orb for aspects**. The base orb for aspects will be corrected with the percentages for celestial bodies and aspects. 
See the paragraph *Appendix: Defining orbs*. 
The value shows the maximum orb for the most important aspect and the most important celestial body.

The **Base orb for midpoints** is the effective orb for midpoints.


#### Points

![](configuration-points.png)

The tab *Points* gives access to the configuration fo all celestial points that Enigma supports.
For each point you will find a checkbox. 
Check this box if you want to take the celestial point into account, deselect it if you do not want to use it.

There is also a value *Orb%* (Orb percentage) that you can change. 
Click on the number for the orb and a small popup screen appears where you can edit the value.
You can enter a percentage from 0 up to 100, make sure you use only whole numbers. 
It is possible to define a percentage for a point that is not selected, 
so it is easy to remember a percentage if you later decide to include the point. 
If you want to use a point but not calculate aspects for that point, enter a percentage of zero.

Please note that you cannot deselect the classic points and also not MC or Ascendant.

See the paragraph *Appendix: Planets and other celestial points* for more information.

#### Aspects

![](configuration-aspects.png)

*Aspects* is the last tab for the configuration. 
At the top of the screen you can select the type of orb. 
In the current release, there is only one method to define orbs: Weighted orb.
It uses percentages for celestial bodies and for aspects to define the actual orb.
You can leave it that way.

Just as with celestial points, you can select and deselect the aspects you want to use.
And there is also an orb-percentage that you can edit by clicking it. 

See the paragraph *Appendix: Defining orbs* for more information.

## The Charts module

The Charts module supports calculation and analysis of charts. 
You can start it by clicking the charts module in the startup screen.
This will open the following screen:

-- TODO image

The very first time you start the Charts module, Enigma will not know which charts you want to use.
You first need to create one or more new charts. Enigma will save all new charts automatically in a database.
You can always delete a chart from the database.

If you entered a new chart, the name of the chart will show underneath **Available charts**.
After selecting the chart by clicking on the name, you can use all buttons to the right and all options in the menu.
The database will remember all charts you added, except the charts you deleted.
You can always retrieve an existing chart from the database by searching for it.

The functions of the buttons and the menu partly overlap. You can perform the most common actions with the buttons.
The menu provides the same actions and somme additional functionality.

### Create a new chart
To create a new chart  you can click the button **New Chart** or select the menu item **Charts** -> **New Chart**.
This action will open a new screen where you can enter the data:

![](data-input.png)

You need to enter the required data and click the button **Calculate**. 
If you made an error, Enigma will show a popup with information about what went wrong.
Also, if an error is detected, the underlining of the input field will turn red.

Use the button **Help** for advise on the different fields.

After clicking the button **Calculate**, the newly entered chart can be selected and analysed.

### Retrieve an existing chart from the database
The main screen for the Charts module shows information about the charts in the database.
The last added chart is mentioned and also the number of charts in the database.

To retrieve a chart from the database you can click the button **Search Chart** or select the menu item **Charts** -> **Search for Chart**.
This opens the following screen:

![](search-chart.png)

In the field _Search argument_ you can enter a (part of) a name, or just leave the field empty.
After clicking the button **Search**, Enigma will show all charts that have the search argument in the name.
It ignores a difference in lowercase or uppercase.
If you did not enter a search argument, Enigma will show al charts from the database.
However, the number of charts is limited to 100. 
So you always need to enter a searchargument if the number of charts in the dtabase is larger. 

Use the button **Help** for more details.

If you select one of the found charts, the button **Select** becomes available. 
Click this button to start working with the selected chart. 
The search window will close and the chart will be available in the main screen for the Charts module.





## Appendix 

### House systems

Enigma supports the following house systems:

- **Placidus** is based on the proportional time that the point of the house cusp has traveled. Cusp 11 for instance, should have traveled 1/3 of the time it is above the horizon.

- **Koch**, also called *Birthplace Houses* or *GOH,* divides the time for daily movement of the MC and calculates the ascendant for each time.

- **Porphyri** trisects the longitudes of the quadrants that are formed by MC, Ascendant, IC and Descendant.

- **Regiomontanus** divides the equator in equal parts and draws great circles through the division points that intersect the ecliptic.

- **Campanus** divides the celestial globe into equal parts by drawing great circles from north to south. These circles intersect the ecliptic.

- **Alcabitius** is comparable to *Porphyri*, but it trisects the quadrants of the equator.

- The **Topocentric** system constructs and divides a cone that represents the rotation of the earth.

- **Krusinski** is comparable to *Campanus* but it divides the celestial globe looking from points that are perpendicular to the ascendant-descendant.

- **APC** (Ascendant Parallel Circle) divides a small circle through the ascendant and parallel to the equator and projects the results to the ecliptic, looking from the north to the south. Cusps of this system are not oppositional.

- **Morin** divides the equator and draws great circles to the poles of the ecliptic. MC and ascendant are not equal to cusps 10 and 1, and the results do not change if the geographic latitude changes.

- The **Whole sign** system uses the sign on the ascendant as the first house, and then subsequent signs as houses 2, etc. MC and ascendant will not be equal with cusp 10 and 1.

- **Equal from Ascendant** calculates houses of 30 degrees, starting from the ascendant. The MC is not equal to cusp 10.

- **Equal from MC** also calculates houses of 30 degrees, but starts with the MC. The ascendant is not equal to cusp 1.

- **Equal from 0 Aries** considers the houses to be equal to the signs and starts the first house with the sign Aries. MC and ascendant will not be equal with cusp 10 and 1.

- **Vehlow** is comparable with *Equal from Ascendant,* but it starts 15 degrees before the ascendant. MC and ascendant will not be equal with cusp 10 and 1.

- **Axial Rotation** (also called *Zariel*) divides the equator in 12 equal parts, starting from the right ascension of the MC. Position circles from the north and south and perpendicular to the equator, define the cusps. The ascendant is not equal to cusp 1. The results do not change because of geographic latitude.

- The **Horizon system** (also called *Zenith system*) divides the horizon in equal parts, starting in the east. It defines the cusps by drawing great circles, perpendicular to the horizon. Cusp 1 is not equal to the ascendant, cusp 7 is the Vertex.

- **Carter** divides the equator into 12 equal parts, starting with the right ascension of the ascendant. It defines the cusps by converting the positions in right ascension to longitude. The MC is not equal to cusp 10.

- **Gauquelin** has the same approach as *Placidus,* but counts 36 houses and uses a clockwise direction.

- The **SunShine system** by Bob Makranski trisects the semi arcs of the Sun and defines the cusps by drawing great circles from north to south and through the division points.

- **SunShine (Treindl)** has the same approach as the *SunShine system* with a slightly different approach by Alois Treindl.

- **Pullen (sinusoidal delta)**, an approach by Walter Pullen that is comparable to Porphyri. Pullen bases the size of the succeeding houses 2, 5, 8 and 11 on a sine-wave, such that the size of the succeeding houses reflects the relative size of the quadrants.

- **Pullen (sinusoidal ratio)** is an improvement on *Pullen (sinusoidal delta)*.

- **Sripati**, as *Porphyri,* but with the cusps as midpoint between the last and the current house.

 

### Ayanamsha’s

Enigma supports an extensive set of Ayanamsha’s.

The most important ones are:

- **Fagan**, as proposed by Cyril Fagan and Ronald Bradley.

- **Lahiri**, official standard in India.

- **DeLuce**, based on the supposed birth date of Jesus.

- **Raman**, the ayanamsha according to B. V. Raman

- **Krishnamurti**, proposed by K.S. Krishnamurti, assumes that the ayanamsha was zero in 291 CE, probably at the date of the equinox.

- **Djwhal Kuhl**, assumes that the age of Aquarius starts in 2117.

- **Huber**. The mean ayanamsha as found in Babylonian texts and calculated by the historian Peter Huber.

- **Galactic Center 0 Sag**, Dieter Koch proposes to put the Galactic Center at 0 degrees Sagittarius.

- **True Chitrapaksha** starts at 180 degrees from the longitude of Spica.

- **Galactic Center (Brand)**. Rafael Gil Brand proposes to start with the Galactic Center and defines this as the golden section between 0 degrees Scorpio and zero degrees Aquarius.

- **Galactic Center 0 Cap**. David Cochrane puts the Galactic Center at 0 degrees Capricorn.

For more information, check the documentation of the Swiss Ephemeris at [*https://www.astro.com/swisseph/swisseph.htm*](https://www.astro.com/swisseph/swisseph.htm) , chapter 2.8 *Sidereal Ephemerides for Astrology*.


### Observer positions

Enigma supports three *observer positions*. An observer position is the location of a (fictive) observer that registers the positions of the celestial bodies.

You can select one of the following observer positions:

- **Geocentric**: the observer is in the center of the earth. A somewhat unlikely position, but it is the de facto standard in astrology.

- **Topocentric**: the observer stands firmly on the earth crest. This is the only position that is physically possible. The positions of the celestial bodies will differ slightly from the geocentric position because of the effect of parallax. It will affect the Moon (up to about a degree) most. The other celestial bodies will differ only a few arc seconds or less.

- **Heliocentric**: Enigma calculates the positions as seen from the Sun. The positions of the houses, Sun, Moon, lunar nodes and lunar apsides (Black Moon) are not available, but the Earth is.


### Projection to the ecliptic

We calculate the ecliptical position of a celestial body by projecting this body to the ecliptic, using an arc that is perpendicular to the ecliptic.

There is one exception to this rule. The Dutch *School of Ram* calculates the positions with an arc that is oblique to the ecliptic and runs from the north point to the south point. The effect is that it will correctly place the planets in the houses; it solves the latitude problem. The consequence is that the ecliptical positions of planets with higher latitude, typically Moon and Pluto, will fluctuate during the day significantly.

If you want to use the techniques of the School of Ram, you can select in the configuration **Oblique Longitude** as *Type of projection to the ecliptic*. In all other cases, select **Standard (two-dimensional)**.


### Planets and other celestial points

Enigma will always calculate the **classical planets** (Sun up to Saturn), MC and Ascendant. All other points are optional.

You can add many other points to the calculation:

- **Modern planets**: Uranus, Neptune and Pluto. (I’ll call Pluto a planet).

- **Mundane points**: Vertex and Eastpoint.

- **Arabic parts**: Pars Fortunae, both with and without sect.

- **Mathematical points**: Mean Node and True Node, the vernal point (Zero Aries) and three calculations for the apogee of the Moon: Mean, Corrected and Interpolated. The corrected version is according to the most recent lunar theories. I will add the apogee according to Duval in a later release (approximated calculation by Cees Jansen).

- **Centaurs**: Chiron, Nessus and Pholus.

- **Plutoids**: Huya, Varuna, Ixion, Quaoar, Haumea, Eris, Sedna, Orcus and Makemake.

- **Planetoids**: Ceres, Pallas, Juno, Vesta, Hygieia and Astraea.

- **Hypothetical points and planets**:

    - **Uranian**: Cupido, Hades, Zeus and Kronos (according to Alfred Witte). Apollon, Ademetos, Vulcanus and Poseidon (according to Friedrich Sieggrün).

    - **School of Ram**: Persephone, Hermes and Demeter.

    - **Transpluto**, also called Isis, as described by Theodor Landscheidt.

    - **Carteret**: Vulcanus and Perpsephone, as proposed by Jean Carteret, become available in a future release.

### Supported periods for calculations

Enigma can calculate all planets and most other points for a period of almost 30000 years. For some celestial bodies, this period is shorter. Astronomers do not have enough data to calculate these celestial bodies for all periods. If Enigma cannot calculate a point for a specific date, it will omit this point.

| Celestial points                                                          | From         | Until       |
|---------------------------------------------------------------------------|--------------|-------------|
| Sun, Moon and planets and all points not in the others rows of this table | -12999/08/02 | 16799/12/30 |
| Chiron                                                                    | 0675/01/01   | 4650/01/01  |
| Pholus                                                                    | -2958/01/01  | 7308/12/30  |
| Nessus, Huya, Ixion, Orcus, Varuna, MakeMake, Haumea, Quaoar, Eris, Sedna | -3000/03/18  | 2998/08/23  |
| Ceres, Vesta                                                              | -12999/08/02 | 9591/05/23  |

### Defining orbs

#### Orbs for aspects

An orb for an aspect can depend on many factors. Enigma takes two of these factors into account: the points that form an aspect and the aspect itself.

In the configuration, you define a base orb (Configuration, tab general, Base orb for aspects). The base orb is the maximum orb that is possible.

Also in the configuration, you define an orb percentage for each point in the chart. If you want to use the full orb, you enter 100, and for a smaller orb, a smaller percentage. You will probably use a large percentage for fast moving points and a smaller percentage for slower moving points.

To check if an aspect is within orb, Enigma combines the percentages of both points that form the aspect. It chooses the highest value. The idea is that the speed of the fastest point defines the orb.

An example:

The Moon will have a large orb as it moves fast. Pluto will have a small orb. If the percentage of the Moon is 100% and the percentage for Pluto is 50%, you do not want the mean value of 75% as the speed of the Moon is defining the exactness of the aspect. In this case the percentage of 100% is used.

Aspects also have an orb percentage that you define in the configuration.

The effective orb is the percentage of the point with the highest percentage, combined with the percentage of the aspect.

Some examples, using a base-orb of 10 degrees:

Sun: 100%, Neptune 50% —\> 100% for the points.

Conjunction: 100%, effective orb 100% of 10 degrees is 10 degrees.

Uranus: 50%, Eris 40% —\> 50% for the points.

Semi-quintile: 30%, effective orb 15% of 10 degrees is 1.5 degree.

#### Orbs for midpoints

Enigma supports a configurable base orb for midpoints. In a later version, it will be possible to define different orbs for different midpoint dials.

#### Orbs in research

If you perform a test with harmonics, you can enter the orb that you want to use in your research.

In other research projects, you use the orbs as defined in the configuration.

### Format for data-files

Enigma supports only one type of data-file. Please note that the format as used in version 0.1 has been changed in version 0.2.

Later versions will add support for data from the Gauquelin archives and data for progressions.

You can create your own data-file using the csv format (Comma Separated Values). This is a simple text file with one line per chart. You need to separate the different values with a comma. Make sure you use a real text-editor and not Word, LibreOffice Text or another word processor. Examples of a text-editor: NotePad (available in Windows), Notepad++ (more powerful, download it for free from: [*https://notepad-plus-plus.org*](https://notepad-plus-plus.org)).

An example of the first lines of a data-file:

 Id,Name,longitude,latitude,date,cal,time,zone,dst

 107, Leonardo da Vinci, 10:55:0:E, 43:47:0:N, 1452/4/14, J, 21:40, 0.7277778, 0

 108, Albrecht Dürer, 11:04:0:E, 49:27:0:N, 1471/5/21, J, 11:00, 0.7377778, 0

 109, Michelangelo Buonarotti, 11:59:0:E, 43:39:0:N, 1475/3/6, J, 1:45, 0.7988888, 0

You can copy the first line. Do not skip it, as Enigma will always skip this first line automatically.
The lines starting with 107, 108 and 109 contain the real data.

Each line contains 9 fields that correspond to the labels used in the first line:

- **Id**, a unique identifier. It can be a number or other identification.

- **Name**. A description for the chart. The name, or a code, if you would like to keep the data anonymous.

- **Latitude**. Geographical latitude in the format *dd:mm:ss:D*. For *dd* enter the degrees (use 1 or 2 positions), for *mm* the minutes and for *ss* the seconds. Replace *D* with *‘E’* for eastern longitude or *‘W’* for western longitude. Use colons between all items.

- **Longitude**. Geographical longitude in almost the same format as for Latitude: ddd:mm:ss. For *dd* enter the degrees (use 1, 2 or 3 positions), for *mm* the minutes and for *ss* the seconds. Replace *D* with *‘N’* for northern latitude or *‘S’* for southern latitude.

- **Date**. Birthdate, or date for an event in the format *yyyy/mm/dd*. For *yyyy* enter the year, for *mm* the month and for *dd* the date. Use a forward slash between all items.

- **Cal**. The calendar. For most charts, this will be Gregorian: use the character *‘G’*. If the time reckoning was according to the Julian Calendar, use *‘J’*.

- **Time**. Time for the birth or for an event. Use the format *hh:mm:ss* or *hh:mm*. For *hh* enter the hour, for *mm* the minutes and for *ss* the seconds. Seconds are optional. Use colons between all items.

- **Zone**. Enter the correction for the time zone. This is a number, possibly with a fraction. This does not depnd on the locality of your computer: always use a dot between integer part and fraction.

- **DST**. Shows if daylight saving time applies. Use *0* for no DST and *1* for DST.

Save your data-file, preferably with the extension .csv at a location of your liking. You can import the data-file in Enigma. If one or more charts are using a wrong format, Enigma will recognize this and create a report with the offending lines at c:\enigma_ar\data\errors.txt.

**Example file**

You can download an example file from [*https://radixpro.com/enigma*](https://radixpro.com/enigma). This file contains the data of 42 visual artists.

### Using Excel or Libre Office

You can use a spreadsheet to create a data file. Spreadsheets can read csv files, so you can try it with the example file mentioned above.

Make sure that you use a comma as a separator and do not use quotes.

### Result files

If you perform a test, Enigma will create several result files. You can find these files in the folder *c:\enigma_ar\project\\projectname\]\results .*

Replace \[projectname\] with the name you used for the project.

**Counts**

The most important file is a text file that shows the counts for the performed test. This file has the same content as the result window that Enigma shows after performing a test.

The name is *\[type\]summedresult\_\[testmethod\]\_counts\_\[date and time\].text*

- Replace \[type\] with test (for the test data) or control for the control data.

- Replace \[testmethod\] with the name of the method.

- Replace date and time with a representation of the actual date and time (during the test).

An example of results for testdata for the method CountOccupiedMidpoints:

*Testsummedresult_CountOccupiedMidpoints_counts_2023-4-16 11-52-29*

**Positions**

You can check the positions for the calculated charts. Enigma stores these positions in a JSON file, a verbose format, but it is readable for both humans and computers.

The name of the file is *\[type\]dataresult\_\[testmethod\]\_positions\_\[date and time\].json*

Use the same replacements as described for the Counts file.

Please note that this file contains over 1000 lines per chart, you might not want to print it. The file contains the same information as given in the window with calculated positions. Each chart in the file starts with the positions, followed by name and other information about the chart.

## Folder structure

The application itself is by default in the folder *C:\Program Files (x86)\Enigma Astrology Research*, unless you defined another folder during installation.

In the folder where you installed Enigma is a sub-folder **doc***.*

In this folder, you will find the *User Manual*, the *Release Notes* and the *Roadmap* for Enigma.

For data, Enigma uses a separate folder structure. These folders do not exist after install; Enigma creates them the first time you use the application.

The folder structure is as follows:

C:\enigma_ar

\data

\\ \[dataname\] (multiple folders)

\csv

\json

\database

\export

\logs

\project

\\projectname\] (multiple folders\]

\results

In **enigma_ar** you will find the files *enigmaconfig.json* and *enigmaprogconfig.json*. These files contain the configuration for Enigma. Please do not edit these files, but use Enigma itself to change the configuration. If you remove these files, Enigma automatically creates a new default configuration.

The folder **data** contains the imported data files. All data files have a sub-folder with the name you defined for the data. In the folder structure, this is **\[dataname\]**. These folders have two sub-folders: **csv** and **json**. They contain respectively a copy of the imported file and the conversion to JSON format.

In **database** Enigma maintains a database with data for charts and events. It creates this database after the first calculation of a chart.

The folder **export** is for future use.

Enigma saves log files in the folder **logs**. Each day you use Enigma, it creates a new log file. If the number of log files is larger than 31, the program deletes the oldest log file.

The folder **project** contains sub-folders for each project you create. It uses the names for the projects. In the folder structure, you see **\[projectname\]** as placeholder for these names. This folder contains jsonfiles for the definition of the project, the test data and the control data.

The sub-folder **results** contains the results of the different texts.

## Control groups

If you create a project, Enigma will automatically add control groups.

Currently, Enigma supports one type of control group: *Standard shifting of location, date, and time*.

Enigma creates the control group by collecting all different parts of date, time, and location. It randomly combines these parts to new combinations.

Charts in a control group obviously do not describe real living persons or real existing situations. But they have a comparable distribution in data, time and location. This means that the control group reflects any artifacts, astronomically or demographically, that exist in the test data. A remarkable result in the test data should not show in the control group. If it does, it will probably be because of an artifact.

Optionally, you can multiply the number of combinations to create a larger control group. The maximum supported multiplication is 10.

For the control group type *Standard shifting of location, date, and time*, Enigma uses the following algorithm:

- Make separate collections of the following items:

    - year

    - month

    - date

    - hour

    - minute

    - second

    - indications for the use of DST

    - time zones

    - geographic longitudes

    - geographic latitudes

- Sort the values for the days from large to small.

- Shuffle all other items using a randomizer.

- Repeat for each item in the test data, optionally multiplied with a factor to increase the control group:

    - Combine the elements at the top of each collection into a new chart. Make sure that only applicable months combine with dates larger than 28.

    - Remove the used elements.

## More information

### Documentation

- **Release notes.** The release notes contain a description of the added functionality per release.

- **Developers manual.** If you’re a programmer, you might be interested in the Developers Manual.

You can download all documentation from [*https://radixpro.com/enigma*](https://radixpro.com/enigma)

### Facebook group

You can join the Facebook group for Enigma:

[*https://www.facebook.com/groups/246475509388734*](https://www.facebook.com/groups/246475509388734)

### Websites

The website for Enigma - and for other astrological information - is [*https://radixpro.com*](https://radixpro.com/) (English), use [*https://radixpro.org*](https://radixpro.org/) for technical information for programmers.

### Mailing list

You can subscribe to a mailing list. I only send mails for new releases of Enigma.

To subscribe, send a mail with the subject ‘subscribe’ to enigma@radixpro.org

To stop a subscription, send ‘unsubscribe’ to the same address.

### Source code

Enigma is open source. You can use the source in your own open source projects but not in closed source projects. Your program does not have to be free to use the Enigma source, but it has to be open source.

Please look at the copyright details in the main folder of the source.

The source is available via github:

[*https://github.com/jankampherbeek/EnigmaSuite*](https://github.com/jankampherbeek/EnigmaSuite)

### Support

No software is without errors. I heavily tested Enigma, but that does not guarantee the absence of errors.

If you encounter an error, you can contact me at the email address *enigma@radixpro.org*

