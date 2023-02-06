# User Manual Enigma Research 0.1

[TOC]



## About Enigma Research

Enigma Research is free software to aid research into astrology. The program does support the calculation and analysis of horoscopes but its primary focus is research.

Enigma Research supports the analysis of multiple charts. It uses control groups to support the validation of results and taking care of the pitfalls of both astronomical and demographic artifacts. The program also produces clear graphics that can be used in publications.

This version 0.1 is a preliminary version and soon will be followed with versions that have more functionality.

 

### Requirements

You need a computer that runs Windows. Enigma has been tested on Windows 11. It probably runs on older versions, but not older than Windows 7. Enigma will likely run in a Windows emulator on Apple hardware but that has not yet been tested.

The program requires about 200 mb diskspace and about 4 GB internal memory (currently this is a very rough estimation).



## Installation

[todo]



## Starting Enigma



### Start screen

After starting Enigma you will see a screen *Enigma Astrology Research*.

<img src="D:\dev\proj\EnigmaSuite\Enigma\documentation\img\start-window-en.jpg" alt="start-window-en" style="zoom:67%;" />

<!-- html-help-begin [mainwindow] -->

The screen shows two images indicating a module: Charts and Research. 

In release 0.5 a module for Calculators will be added, followed by a module for Cycles in release 0.6.

You can start a module by clicking one of the images.

<!-- html-help-end [mainwindow] -->



## General

### Celestial points overview

overview

### Orbs for aspects

explanation of method and different approaches

### Ayanamshas overview

overview

### Settings

Menu: General -> Settings

<!-- html-help-begin [settings] -->

Settings define the location of files that Enigma uses. In version 0.1 these locations are fixed.

The current locations are:

- **Location of data files**: `c:\enigma_ar\data`  Data files contain data, currently only in csv-format,  that can be used by projects.
- **Location of project files**: `c:\enigma_ar\project` Project files include files with description of projects and files that are converted to Json format.
- **Location of export files**: `c:\enigma_ar\export` Files that you export.
- **Location of Swiss Ephemeris files**: `c:\enigma_ar\se` The files as supplied by the Swiss Ephemeris that contain the data for the calculations. These files will (almost) never change.

*Csv* (comma delimited values) is the format that is used by spreadsheet software like Microsoft Excel and Libre Office Calc to export data.

*Json* is a format that is mostly used by software but is easy to read for humans. 

<!-- html-help-end [settings] -->



### Configuration

Menu: General -> Configuration

<!-- html-help-begin [configuration] -->

In the configuration, you can define your astrological preferences. Enigma provides a default configuration at install time, but you can override that configuration. 

In the menu you can define your preferences for:

- General selections that have effect on the calculations: Tab *General*.
- Selection of celestial bodies and their orbs: Tabs *Celestial points*, *Minor/mathematical* and *Hypothetical*.
- Selection of aspects and their orbs: Tab *Aspects*.

After changing the configuration, you can save it by clicking on the button **OK**. Saving is done simultaneously for the settings on all tabs. If you made an error you will get a popup with a warning and the data will not be saved. Locate the error, correct it and click OK again.

Click the button **Cancel** to ignore your changes and to close this window.



#### General selections

If you select the tab *General* you can define the following preferences:

**House system**. Currently only house systems with 12 houses are supported. Select *None* if you do not want to use a house system. 

**Type of zodiac**. Select either *sidereal* or *tropical*. If you select tropical, the Ayanamsha will always be None, if you select another Ayanamsha, the setting for Type of zodiac will automatically change into sidereal.

**Ayanamsha**. Select one of the available Ayanamshas. The paragraph *Ayanamshas overview* in the user manual gives a short explanation of the different types. See the remarks at the point above: Type of zodiac.

**Observer position**. The standard approach is *geocentric*, if you want to take parallax into account select *topocentric*. In future releases the option *heliocentric* will be supported.

**Type of projection to the ecliptic**. Select *Standard (two-dimensional)* for most approaches. *Oblique longitude* provides an alternative calculation as supported by the School of Ram. It is also called *True Astrological Longitude Location*.

**Base orbs**. You can define orbs for aspects and for midpoints. 

The base orb for aspects will be corrected with the percentages for celestial bodies and aspects. See the paragraph Orbs for Aspects in the User Manual. The value indicates the maximum orb for the most important aspect and the most important celestial body.

The base orb for midpoints  is the effective orb for midpoints.



#### Celestial points

The tab *Celestial points* is the first of three tabs where you can define points for your chart.

For each point you will find a checkbox. Check this box if you want to take the celestial point into account, deselect it if you do not want to use it. 

There is also a value *Orb perc.* (Orb percentage) that you can change. You can enter a percentage from 0 up to 100, make sure you use only whole numbers. It is possible to define a percentage for a point that is not selected so it is easy to remember a percentage if you later decide to include the point. If you want to use a point but not calculate aspects for that point, enter a percentage of zero.

In this tab you see four types of points: *Classic*, *Modern*, *Mundane points* and *Arabic Parts*. Please note that you cannot deselect the classic points and also not MC or Ascendant. 

See the paragraph *Celestial Points Overview* in the User Manual for an explanation for each point.



#### Minor/Mathematical

The tab *Minor/Mathematical* contains four types of points: Mathematical points (mostly intersections), Centaurs, Plutoids and Planetoids.

Use it the same way as described for the tab *Celestial Points*.



#### Hypothetical points and planets

The tab *Hypothetical* enables selections of different types of hypothetical points/planets.

Use it the same way as described for the tab *Celestial Points*.



#### Aspects

*Aspects* is the last tab for the configuration. First you need to select a *method for defining orbs*. You have three possibilities:

- **Fixed orb**. Use only one orb for all celestial points and all major aspects. For minor aspects and micro aspects you can define a different orb. This is not yet supported in this release.
- **Weighted orb**. Use percentages for celestial bodies and for aspects to define the actual orb.
- **Dynamic/historical orb**. Take the historical development of the orb into account. Not yet supported in this release.

See the paragraph *Orbs for aspects* in the User Manual for more information. 

The aspects are divided into three categories: Major, Minor and Micro. For each aspect you (de)select using the checkbox and define the percentage for the orb. 

<!-- html-help-end [configuration] -->



## Charts



### Calculating a chart

<!-- html-help-begin [chartsdatainput] -->

#### Input for the calculation of a chart

In this sceen you will see three blocks: 

- **General information** is for data that is not required for the calculation but is used to identify a chart. 
- **Location** is for name and coordinates of the location.
- **Date and time** is for all information about the birth-time or event-time. 



##### General information

**Unique name or id for chart**. Enter an identification for this chart.  This can be a name, a number or some other identification.

**Description of the source**. A short explanation about the source of the data. 

**Rodden Rating**. Select one of the Rodden Ratings or leave the default value *Unknown*.

**Category**. Indicate the subject for which the chart is calculated or leave the default value *Unknown*.



##### Location

**Description of the location**. A cityname, address or other identification for the  location. This field is optional.

**Longitude**. Geographic longitude in the format *ddd:mm:ss*. For  *123°45'30"* you need to enter *123:45:30*. You can skip the seconds,  they will default to 0 seconds. So *145:15* is a valid indication for  *145°15'00"*. 

Use the scroll-downbox to select either **E** *(East)* or **W**  *(West)*. 

**Latitude**. Geographic latitude in the format *dd:mm:ss*. For  *52°13'30"* you need to enter *52:13:30*. You can skip the seconds,  they will default to 0 seconds. So *45:15* is a valid indication for  *45°15'00"*. 

Use the scroll-downbox to select either **N** *(North)* or **S**  *(South)*. 

##### Date and time

**Date**. Enter the date in the format *yyyy:mm:dd*, so June 16, 2022  would be entered as *2022/06/16*. Historical dates before the year 0 should  have a negative year if you select the Astronomical yearcount. Otherwise, all  years will be positive.

**Cal. (Calendar)**. The calendar that is applicable, select either  **G** *(Gregorian)* or **J** *(Julian)*. For recent dates you  will always need the Gregorian calendar.

**Yearcount**. There is a difference between a historical and an  astronomical yearcount. Historical dates do not recognize the year 0. So the  historical year 1 CE is preced by the year 1 BCE. The astronomical years would  be +1 for 1 CE, and 0 for 1 BCE, preceded by -1 for 2 BCE.

For years after the year 0, astronomical and historical dates are the  same.

Select one of the following values: 

- **CE**: Common Era (historical). Previously: AD. 
- **BCE**: Before Common Era (historical). Previously BC. 
- **Astronomical**: Positive and negative years. 

**Time**. Enter the time in the format *hh:mm:ss*, using 24 hour  notation. 2h38m30s PM would be *14:38:30*.

**DST**. Check the box DST if Daylight Saving Time is applicable.

**TimeZone**. Select one of the available timezones. The list contains the  offset from UT (Greenwich Time) and the name of the timezone. If the time does  not fit into one of the available timezones, select *LMT: Local Mean Time*.  You will get the possibility to define another offset from UT for the time  used.

**LMT: difference with UT**. This field becomes available if you selected  LMT as timezone. Here you can define the offset from UT (Greenwich) in hours,  minutes, and seconds, using the format hh:mm:ss in the same way as for entering  the clock-time. Also select either **E** *(East)* or **W**  *(West)* to indicate the direction for the offset.



##### Start the calculation

Click the button **Calculate** to perform the calculation. If you made any  errors, the fields that contain an error will return yellow. Correct the input  and try again.

Click the button **Close** if you do not want to continue.

<!-- html-help-end [chartsdatainput] -->



### Analysis of radix charts

#### Aspects



#### Midpoints

After calculating a chart of reading a chart from the database, you will see the window **Charts**.

This window shows a menu. Select *Analysis - Midpoints* to show the window for midpoints.

<!-- html-help-start [midpoints] -->

In this window you will see two tables. The left table contains a list of all midpoints, regardless if they are occupied or not.  The table to the right shows occupied midpoints for a specific dial. 

Both tables will contain only celestial points that you selected in the configuration.

For the occupied midpoints you will also see the actual orb and a percentage for the exactness of the midpoint. The higher the percentage, the smaller the orb.

Default a dial of 360 degrees is used. You can change the dial with the radio buttons to the right of the two tables. Currently, the dials for 360 degrees, 90 degrees and 45 degrees are supported. The orb for all dials is the same.

After changing the selection of the dial, the content of the table with Occupied midpoints will automatically update.

<!-- html-help-end [midpoints] -->



#### Harmonics

In the window **Charts** you will see a menu. Select *Analysis - Harmonics* to show the window for harmonics.

<!-- html-help-start [harmonics] -->

The table contains both the radix positions and the harmonic positions for all celestial points that you selected in the configuration, and additionally for the MC and the Ascendant. 

Initially, the harmonics are calculated for the 2nd harmonic. You can change the harmonic by entering a number underneath *Enter number for new harmonic* and clicking the button **Calculate**. 

After clicking this button, the content of the table with harmonics will automatically update.

Enigma also supports fractional harmonics: harmonics for non-integer numbers. You can enter a fractional number the same way as an integer number. Use the decimal separator (dot or comma) that you normally use on your computer.

The effective harmonic is shown above the table with positions.

<!-- html-help-end [harmonics] -->





## Research



### Data import

Enigma imports data. In the future it will support multiple formats for data but currently only one standard format is supported.

#### Definition of standard format for data import

The standard format is in csv (Comma Separated Values). 

The data should use the following items:

- **Id** Unique identifier, can be a number or any text.
- **Name** A descriptive name, this can be empty but make sure not to skip the comma.
- **Longitude** A text defining the geographic longitude. Format is `ddd:mm:ss:dir` where *ddd* is the number of degrees, *mm* the minutes, *ss* the seconds and *dir* an indication for East or West, respectively E or W. Some examples: `62:13:30:E`,  `5:6:0:W` and `118:0:0:E`. Make sure to add a value - possibly zero - for seconds. 
- **Latitude** A text defining the geographic latitude. Format is the same as for Longitude with the exception for *dir*, which for latitude is an indication for North or South, respectively N or S. 
- **Data** A text defining the date. The format is `yyyy/mm/dd` where *yyyy* is the year, *mm* the month and *dd* the day. For dates before the year zero, use astronomical years. Some examples: `2022/9/26`, `1/1/1` (January 1 in the year 1 CE) and `-10000/2/2` (February 2 in the astronomical year -10000 or the historical year 10001 BCE).
- **Calendar** A character indicating the calender. Use G for the Gregorian calendar or J for the Julian calendar.
- **Time** A text indicting the time. The format is `hh:mm:ss` where *hh* is the hour, *mm* the minute and *ss* the second. The notation is 24-hour based. Some examples `13:49:30`, `1:1:0` and `12:30:00`. Make sure to add a value - possibly zero - for seconds. 
- **Zone** A value that indicates the offset from Greenwich Time. Use a positive or negative number, fractions are allowed, do not use comma's but dots.
- **Dst** An indication for Daylight Saving time. Use a value for indicate the difference in hours. In most cases this will be the value 1. Fractions are allowed, do not use comma's but dots. A zero value indicates that there was no dst.

An example of a valid line:

`1234,Jan,6:52:31:E,52:12:37:N,1953/1/29,G,8:37:30,1,0` 

You can add spaces fore or after the comma's to make the line more readable. Do not pace spaces inside the values.

An example with added spaces:

`1234, Jan, 6:52:31:E, 52:12:37:N, 1953/1/29, G, 8:37:30, 1, 0` 

Enigma expects that the first line of the file will contain header inforamtion, like:

`Id,Name,longitude,latitude,date,cal,time,zone,dst`

This line is always ignored so do not put data on the first line.



#### Location of the files

The files are saved in a folder structure. The start folder is defined in the settings, default it is : c:\enigma_ar\data

A csv-file that has been imported is copied to *c:\enigma_ar\data\\[name]\csv* and a json file to *c:\enigma_ar\data\\[name]\json*.

 The name for the dataset is used instead of *[name]*.



#### Overview of imported data files

Menu: Data -> Overview Datafiles

<!-- html-help-begin [datafilesoverview] -->

Data files that you already imported will show up in this view.

It contains the files that have been imported into the Enigma environment.

The overview only contains the names of the imported data files.

<!-- html-help-end [datafilesoverview] -->





#### Performing an import

Menu: Data -> Import data

<!-- html-help-begin [dataimport] -->

To import data, you need to select a file in the correct csv-format and define a unique name for the data. This name is used to define the data in research projects.

To select a file you can use the button **Browse for file**. This will open a standard window to find and select a file. The selected file will show in the field after 'Select the file with data'.

After completing this information, you can click the button **Import file**.

If you did not enter all required information, or if you used a name for the data that is already in use, you will get a popup window with an explanation of what is wrong.

If no errors occurred, a text that clarifies the result is shown in the filed underneath 'Result of the import'.

After a successful import, you can either perform another import or click the button **Close**.

If you change your mind and want to skip the import, click the button **Cancel**.

<!-- html-help-end [dataimport] -->



### Project import



#### Types of controlgroups

Currently there is one type of controlgroup: *Standard shifting of location, date, and time*. In the document https://nvwoa.nl/txt/20200628controlegroepen.pdf  (in dutch, will soon be translated) you will find a thorough description of this way of generating controlgroups.



#### Randomization

Randomization is an essential part of generating controlgroups. Most programs use a pseudo random number generator, resulting in data being not fully random. That is sufficient for gaming etc., but for research we prefer real random number generators. Enigma uses such a real random number generator, based on the 'entropy' (random internal data) of the system. 



#### Multiplication factor

If a dataset is relatively small this will also result in a small control group. This easily leads to unreal effects. To avoid this, it is possible to multiply the control group data. This has the effect that the control group gives a better estimation of the expected data in the research population. 



#### Performing an import

<!-- html-help-begin [projectimport] -->

 Before you can create a project, a datafile already needs to have been imported. If you did not yet import a datafile, please do so now. Menu: <b>Data - Import Data</b>.

##### Description of the project

To define the project you need to provide a <i>name</i>, an <i>identification</i> and a <i>description</i>. All these fields are obligatory.

- In <b>Name for project</b> you enter the name you want to use for your project.
- In <b>Description</b> you can enter additional information about the project, typically a short description of your intended research.

##### Control group

Defining a project includes the creation of data for a control group. You can define the type of controlgroup and a multiplication factor.
See the initial part of this paragraph in the User manual.

##### Datafile to be used

Select a datafile from the list of available datafiles.

##### Saving the project

If you click the button **Save**, Enigma will check your input and mark any incorrect input fields with a yellow color. If there are no errors, a folder structure for this project, a datafile and a control file are generated. If during this process an error occurs, you will receive a popup about the error. Otherwise you will receive a confirmation popup. After closing this popup you will return to the main screen.

<!-- html-help-end [projectimport] -->



### Doing research

To support research, Enigma needs to:

- Import data (population data).
- Define a project which includes:
  - A project defionition.
  - A copy of the data.
  - A generated control group.
- One or more tests that will be performed using the population data and the control group data.

#### Folder structure

The following folder structure is in use:

- Root of Enigma data, typically c:\enigma_ar\
  - data
    - separate folders for each data set
      - csv: a copy of the original data
      - json: the same data converted to Json format.
  - project
    - separate folders for each project
      - file: controldata.json: the control group data.
      - file: project.json: definition for the project
      - file: testdata.json: the population data.
      - folder: results
        - separate folders for each test, a name that describes the test method that was used, followed by a date/time stamp in the format *yyyy/mm/dd HH:mm:ss*.
          - in each folder the calculated charts for the test-data and for the control-data, respectively *charts_testdata.json* and charts_*controldata.json*. Also the counts: *counts.json*.



#### Projects overview

After selecting Research in the dashboard, Enigma shows a screen with new screen to handle research projects.

<!-- html-help-begin [projectoverview] -->

This screen shows an overview of existing projects. If you did not yet define a project, the overview of projects will be empty.

In the menu you can check the current settings: **General - Settings**, and check of change the Configuration: **General - Configuration**.

To check the available data sets, use the menu **Data - Available data sets**. You can import a new data set via the menu **Data - Import data**. 

Before you can define a new project, you already must have imported a dataset. To define a new project use the menu **Research Projects - Add new project** or the button **New**. A project needs to have access to one data set. If you want to use multiple data sets you will need to define multiple projects.

You can start research on an existing project by selecting the project in the list and clicking the button **Open**. A new screen will appear that will ask you to select a method to perform a test.

<!-- html-help-end [projectoverview] -->



#### Test with project

<!-- html-help-begin [testwithproject] -->

In the upper half of this screen, you will see details about the current project, including the dataset that will be used and the type of control group.

The bottom half of the screen shows the available test methods. You can perform a method, using the data set for this project, by selecting it in the list and clicking the button **Perform test**. In subsequent screens you will be asked for more details. The type of details will depend on the type of the selected test.

Please note that your options will be limited to the definitions as defined in the configuration. If you did not include a specific celestial body or aspect in the configuration, you cannot use it in your test. And orbs in the configuration remain active. If you want to check, or change, the configuration, you can click the button **Configuration**. This will open the standard screen to define a configuration and you can change this. After closing this screen, the new configuration will be immediately active. 

<!-- html-help-end [testwithproject] -->

#### Select points to include for test

<!-- html-help-begin [selectpointsfortest] -->

For each test, you will need to define the points to include in the test. The screen shows Celestial Points (Lights, planets, minor planets, mathematical points) in the list to the left, and Mundane points (related to houses) in the list to the right.

Points that are not selected in the current configuration are not shown.  If you are missing a point you need to redefine the configuration: click the button **Cancel** to close this window and return to the overview of tests, where you can also open the configuration window.

You can select one or more points by clicking them. Deselect by clicking again. You can select/deselect all celestial or all mundane points by clicking one of the checkboxes **Select all celestail points** or **Select all mundane points**.

With the checkbox **Include all cusps** you can include all cusps into the research. This means you can count aspects to cusps or use cusps in midpoints, etc. Please note that Ascendant and MC can also be selected, for quadrant systems you have to be aware of this effect. 

Click the button **OK** to start the calculations.

<!-- html-help-end [selectpointsfortest] -->



#### Research results

The results of the research are show in tabular format. Of course these results depend on the type of test that you performed. You will see two tabs, one for **Test data** and one for **Control data**. Each of these tabs shows the result of the respective countings.

These results are automatically saved to disk as a text file. At the bottom of the screen you will find the location of the files for test data and for control data.



### Details per test

The different tests have some screens in common, for instance for the definition of celestial points that will be part of the test. These screens have been described in the preceding paragraph. 

In this paragraph you will find specifics per type of test.



#### Count positions in signs

todo

#### Count positions in houses

todo

#### Count aspects

todo

#### Count unaspected celestial points

todo

#### Count occupied midpoints

If you perform this test you will see the usual screen to select points to include in the test. The option 'Include all cusps' will be disabled as Enigma does not calculate midpoints for cusps. You can select the mundane points MC and ascendant. If you selected Vertex and/or Eastpoint in the configuration you can also select these points.

You need to select at least three points.

##### Enter details for midpoints

<!-- html-help-begin [midpointdetails] -->

In this screen you define additional details for midpoints. 

###### Dial size

You can calculate midpoints in different *dials*. A dial is a division of the  zodiac in zero, 4 or 8 parts. A finer division is not yet possible in Enigma but  will be added in release 0.3.

You can select one of the following dials: 

- **360°**: no division. 
- **90°**: a division by 4. 
- **45°**: a division by 8. 

###### Orb

You can define your own orb. As the number of midpoints can be quite high, it  makes sense to use a relatively small orb, typically smaller than 2° if you use  a 360° dial and smaller for the other dials.

The orb should be defined in positive numeric values, the minutes should be  between 0 and 59. The maximum total size is 9°59'. However, such a large orb  probably does not make any sense. If your orb is not correctly defined you will  be warned with a popup after pressing the OK-button. 

<!-- html-help-end [midpointdetails] -->

##### Result screen

After entering the details Enigma will calculate the occupied midpoints and show the results in a scrollable list. The format is as follows:

`Sun      / Jupiter       = Moon     12`

This indicates that the Moon is 12 times at the midpoint of Sun and Jupiter. 

Only midpoints with a count larger than zero are shown.

The results are also saved to disk, for the location check the text at the bottom of the list.





#### Count harmonic conjunctions

Starting a test to count harmonic conjunctions will first show the usual screen to select points to include in the test. The option 'Include all cusps' will be disabled as Enigma does not calculate harmonics for cusps. You can select the mundane points MC and ascendant. If you selected Vertex and/or Eastpoint in the configuration you can also select these points.

You need to select at least one point.

##### Enter details for harmonics

<!-- html-help-begin [harmonicdetails] -->

In this screen you define additional details for harmonics. 

###### Harmonic number

You need to enter the number of the harmonic. In most cases this will be an integer value but Enigma also supports fractional numbers. There is no limit,  you can calculate the harmonics in the range of thousands. Of course this might  effect the accuracy of the results.

If your harmonic number is not correctly  defined you will be warned with a popup after pressing the OK-button. 

###### Orb

You can define your own orb in positive numeric values, the minutes should be  between 0 and 59. The maximum total size is 9°59'. 

If your orb is not correctly  defined you will be warned with a popup after pressing the OK-button. 

<!-- html-help-end [harmonicdetails] -->

##### Result screen

After entering the details Enigma will calculate the harmonic conjunctions and show the results in a scrollable list. The format is as follows:

`Harmonic Saturn      /  Radix Neptune     8`

This indicates that the harmonic position of Saturn forms a conjunction with the radix position of Neptune in 8 of the charts. 

Only harmonic conjunctions with a count larger than zero are shown.

The results are also saved to disk, for the location check the text at the bottom of the list.







## Cycles





## Calculators



## Saved text

 The following actions are available:

- **Settings**. Menu *General - Setttings*. Check the currently used settings.
- **Configuration**. Menu *General - Configuration*. Define the configuration that is used for your calculations.
- **Charts**.
  - **New chart**. Menu *Charts - New chart* or button *New Chart*. Calculate a new chart. 
  - **Overview of charts**. Menu *Charts - Overview of charts*. List of available charts.
  - **Search chart**. Button *Search chart* and the accompanying input field. You can search after you input a part of the name that you used for the chart.
- **Research**.
  - **Overview data files**. Menu *Research - Overview data files*. List of imported datafiles.
  - **Import data file**. Menu *Research - Import data file* or button *New data*. Add an external file to the Enigma environment.
  - **New project**. Menu *Research - New project* or button *New project*. Define a new research project.
  - **Search project**. Button *Search project* and  the accompanying input field. You can search after you input a part of the name that you used for the project. 
- **Periods**. This functionality is not yet available in version 0.1.0.
- **Calculations**. This functionality is not yet available in version 0.1.0.

Via the **Help** menu or button, you can access the user manual.
