# User Manual Enigma Research 0.1

[TOC]



## About Enigma Research

Enigma Research is free software to aid research into astrology. The program does support the calculation and analysis of horoscopes but for the general astrologer it makes sense to use software that is aimed at the practice of astrology. For practical astrology, the free programs  *PlanetDance*, *Astrolog* or *Morinus* will come in handy.

Enigma Research supports the analysis of multiple charts. It uses control groups to support the validation of results and taking care of the pitfalls of both astronomical and demographic artifacts. The program also produces clear graphics that can be used in publications.

This version 0.1 is a preliminary version. It is not public and only available to a select group of researchers.

### Requirements

You need a computer that runs Windows. Enigma has been tested on Windows 11. It probably runs on older versions, but not older than Windows 7. Enigma will likely run in a Windows emulator on Apple hardware but that has not yet been tested.

The program required about 200 mb diskspace and about 4 GB internal memory (currently this is a very rough estimation).



## Installation

[todo]



## Starting Enigma

[todo]





## Using Enigma

After starting Enigma you will see a screen with a menu on top.

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
- **Location of export files**: `c:\enigma_ar\export` Files that you export, currently only in csv-format.
- **Location of Swiss Ephemeris files**: `c:\enigma_ar\se` The files as supplied by the Swiss Ephemeris that contain the data for the calculations. These files will (almost) never change.

*Csv* (comma delimited values) is the format that is used by spreadsheet software like Microsoft Excel and Libre Office Calc to export data.

*Json* is a format that is mostly used by software but can be read by humans. 

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

There is also a value *Percentage for orb* that you can change. You can enter a percentage from 0 up to 100, make sure you use only whole numbers. It is possible to define a percentage for a point that is not selected so it is easy to remember a percentage if you later decide to include the point. If you want to use a point but not calculate aspects for that point, enter a percentage of zero.

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
