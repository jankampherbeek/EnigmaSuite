# Configuration and settings


## Settings
You can access *Settings* from both modules *Charts* and *Research* and check them,
but in this release of Enigma, you can not change anything.

Select the menu option **General - Settings**.
You will see a window with information about the location of the following files:

![](settings.png) {width="400"}

- **Data files**. This folder contains the data files you imported.

- The location of **Projects**. All projects will have a sub folder with the name of the project. These sub folders contain all data that is generated for this project.

- Enigma does not yet use **Exported files**. In the future, it will contain the results of exporting data.

- Enigma writes remarks to **Log files**. In case of an error, these log files will be helpful.

- The folder **Database** contains database for calculated charts and other relevant data.

## Standard configuration

You can check and change the configuration via the module *Charts* or via the module *Research*.
Select the menu option **General - Configuration**.

![](configuration-general.png) {width="400"}


This will show a window with a wide range of configurable items. The window comprises 4 tabs:

- The tab **General** shows astronomical settings and settings for orbs. Enigma initially shows this tab.
- **Points** shows the most planets and other points.
- **Aspects** shows all available aspects.
- **Colors** shows the colors for aspect lines.

### General selections


If you select the tab *General*, you can define the following preferences:

**House system**. Currently, Enigma supports only house systems with 12 houses. 
The default selection is Regiomontanus.
Select *None* if you do not want to use a house system.

**Type of zodiac**. Select either *sidereal* or *tropical*.
If you select tropical, the Ayanamsha will always be None.
If you select another Ayanamsha, the setting for Type of zodiac will automatically change into *sidereal*.

**Ayanamsha**. Select one of the available Ayanamshas.
In the paragraph *Appendix: Ayanamshas*, you will find a brief explanation of the distinct possibilities.
See also the remarks at the point above: *Type of zodiac*.

**Observer position**. The standard approach is *topocentric*, so Enigma takes parallax into account.
If you do not want to use parallax correction, select *geocentric*.

**Projection type**. Select *Standard (two-dimensional)* for most approaches.
*Oblique longitude* provides an alternative calculation as supported by the School of Ram.
It is also called *True Astrological Longitude Location*.

**Base orb for aspects**. Enigma corrects the base orb for aspects with the percentages for celestial bodies and aspects.
See the paragraph *Appendix: Defining orbs*.
The value shows the maximum orb for the most important aspect and the most important celestial body.

The **Base orb for midpoints** is the effective orb for midpoints.


### Points

![](configuration-points.png) {width="400"}

The tab *Points* gives access to the configuration for all celestial points that Enigma supports.
For each point, you will find a checkbox.
Check this box if you want to take the celestial point into account, deselect it if you do not want to use it.

There is also a value *Orb%* (Orb percentage) that you can change.
Click on the number for the orb and a small pop-up screen appears where you can edit the value.
You can enter a percentage from 0 up to 100, make sure you use only whole numbers.
It is possible to define a percentage for a point that is not selected,
so it is easy to remember a percentage if you later decide to include the point.
If you want to use a point but not calculate aspects for that point, enter a percentage of zero.

Please note that you cannot deselect the classic points and also not MC or Ascendant.

See the paragraph *Appendix: Planets and other celestial points* for more information.

### Aspects

![](configuration-aspects.png) {width="400"}

*Aspects* is the third tab for the configuration.
At the top of the screen, you can select the type of orb.
In the current release, there is only one method to define orbs: Weighted orb.
It uses percentages for celestial bodies and for aspects to define the actual orb.
You can leave it that way.

Just as with celestial points, you can select and deselect the aspects you want to use.
And there is also an orb-percentage that you can edit by clicking it.

See the paragraph *Appendix: Defining orbs* for more information.

### Aspect colors

![configuration-colors.png](configuration-colors.png) {width="400"}

The last tab is *Aspect colors*. 
Here you can define the color that you want to use for each separate aspect in the chart wheel. 
Enigma uses 15 colors that are all clearly visible in a drawing. 
You can define colors for all aspects, even if you do not use them. 
If you change the configuration and include an aspect, the selected color will automatically be used.
The right column shows the available colors. 
You can change the colors in the middle column, with the label *Color*, by typing the color name. 
You need to type the name of the color exactly as shown in the right column, including the used capitalization. 
There is never a space in a color name.

Enigma checks the color names before saving the configuration and shows a warning if it sees a typo.


## Configuration for progressive astrology

You can access the configuration for progressive astrology via the module Charts or via the module Research. 
Select the menu option **General** - **Config progressions**.

You will see a window with three tabs.
In the tab _Transits_, you define which celestial points you want to include in transits.
You can also define the orb for transits.

![transits.png](transits.png) {width="400"}

The tab _Sec dir_ shows the configuration for secondary directions.
It works the same as for transits. 
You can select celestial points and define an orb.

![secondary-directions.png](secondary-directions.png) {width="400"}

The third tab _Symb dir_ gives access to the configuration for symbolic directions.
Just as with secondary directions and transits, you can select celestial points that you want to use and an orb.
You can also define a time key in the roll-down menu.

![symbolic-directions.png](symbolic-directions.png) {width="400"}

