# Configuration and settings


## Settings
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

## Configuration

You can check and change the configuration via the module *Charts* or via the module *Research*.
Select the menu option **General - Configuration**.

![](configuration-general.png)


This will show a window with a wide range of configurable items. The window comprises 3 tabs:

- The tab **General** shows astronomical settings and settings for orbs. This tab is initially shown.

- **Points** shows the most planets and other points.

- **Aspects** shows all available aspects.

### General selections


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


### Points

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

### Aspects

![](configuration-aspects.png)

*Aspects* is the last tab for the configuration.
At the top of the screen you can select the type of orb.
In the current release, there is only one method to define orbs: Weighted orb.
It uses percentages for celestial bodies and for aspects to define the actual orb.
You can leave it that way.

Just as with celestial points, you can select and deselect the aspects you want to use.
And there is also an orb-percentage that you can edit by clicking it.

See the paragraph *Appendix: Defining orbs* for more information.
