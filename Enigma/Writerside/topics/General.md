# Module charts

The Charts module supports calculation and analysis of charts.
You can start it by clicking the charts module in the startup screen.
This will open the following screen:

![charts-main.png](charts-main.png) {width="400"}


The very first time you start the Charts module, Enigma will not know which charts you want to use.
You first need to create one or more new charts. Enigma will save all new charts automatically in a database.
You can always delete a chart from the database.

If you entered a new chart, the name of the chart will show underneath **Available charts**.
After selecting the chart by clicking on the name, you can use all buttons to the right and all options in the menu.
The database will remember all charts you added, except the charts you deleted.
You can always retrieve an existing chart from the database by searching for it.

The functions of the buttons and the menu partly overlap. You can perform the most common actions with the buttons.
The menu provides the same actions and somme additional functionality.

## Create a new chart
To create a new chart  you can click the button **New Chart** or select the menu item **Charts** -> **New Chart**.
This action will open a new screen where you can enter the data:

![](data-input.png) {width="400"}

You need to enter the required data and click the button **Calculate**.
If you made an error, Enigma will show a popup with information about what went wrong.
Also, if an error is detected, the underlining of the input field will turn red.

Use the button **Help** for advise on the different fields.

After clicking the button **Calculate**, you will see the newly entered chart in the left column. 
You can select and analyse it.

![charts-main-with-data.png](charts-main-with-data.png) {width="400"}


## Retrieve an existing chart from the database
The main screen for the Charts module shows information about the charts in the database.
The last added chart is mentioned and also the number of charts in the database.

To retrieve a chart from the database you can click the button **Search Chart** or select the menu item **Charts** -> **Search for Chart**.
This opens the following screen:

![](search-chart.png) {width="400"}

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

## Deleting a chart

If you want to delete a chart from the database you can use the button **Delete** and the selected chart will be removed. 
Enigma will ask for a confirmation. 
If you do confirm the data is deleted completely and you can only restore it by reentering the data.

Alternatively you can use the menu option **Charts** - **Delete chart**.


## Showing the chart wheel
If you created a new chart, or retrieved a chart from the database, you can now show a graphic chart wheel. 
Select the chart in the left column and click the button **Wheel**, or use the menu option **Charts** - **Show wheel**.
You will see the figure of the chart and basic information about the chart. 
You can resize the window and the chart will automatically adjust. 
You do not need to close the window with the wheel to continue. 
It is possible to open wheels for different charts simultaneously.
You can also have one or more windows for wheels open and at the same windows for analysis.
You will recognize the chart owner as Enigma shows its name in each window.

![wheel-standard.png](wheel-standard.png) {width="660"}

## Showing the positions of a chart

You can also show the positions for a chart that you selected in the left column. 
Press the button **Positions** or use the menu option **Charts** - **Show positions**.
The screen comprises ecliptical, equatorial and horizontal positions, and the distance, for all celestial bodies. 
You will also see the daily speed for the ecliptical and eqatorial coördinates.

You will also see a list of relevent positions for houses. 
This list does not show latitude as this is always zero for houses.
There is also no daily speed for houses. 

You do not need to close the window with the positions to continue.

![positions.png](positions.png) {width="660"}

A short description of the column headers:
* Longitude: ecliptical longitude, the usual position in the zodiac.
* Long speed: daily speed for the longitude. A negative value means retrogradation.
* Latitude: ecliptical latitude, the deviation from the ecliptic. Positive is north, negative is south.
* Lat speed: daily speed for the latitude. A negative value means going from north to south, otherwise the direction is from south to north.
* RA: right ascension, the position at the equator.
* RA speed: daily speed in right ascension.
* Declination: deviation from the equator. Positive is north, negative is south.
* Decl speed: daily speed for the declination. A negative value means going from north to south, otherwise the direction is from south to north.
* Distance: the distance from the earth measured in AU (Astronomical Units, the mean distance from Earth to Sun).
* Dist speed: the daily change in the distance.
* Azimuth: the position at the horizon, measured from south in the direction of the west etc.
* Altitude: the height - or elevation - above the horizon. 
* A negative value indicates a celestial point that is underneath the horizon.

## Analysis: aspects
 
To calculate the aspects for a chart, you can use the menu option **Analysis** - **Aspects**.
This results in a window with all aspects.

![aspects.png](aspects.png) {width="500"}

In the columns you will find:
* Name and glyph for the first point.
* Name and glyph for the aspect.
* Name and glyph for the second point.
* The actual orb.
* A percentage for the exactness if the orb. The percentage is based on the definitions in the configuration. A high percentage indicates an exact aspect.

You do not need to close the window with aspects to continue.


## Analysis: harmonics

For harmonics you will use the menu option **Analysis** - **Harmonics**.
In the resulting window you can define a value for a harmonic. The default value is 2.
Recalculate the harmonic positions by clicking the button **Calculate**.

![harmonics.png](harmonics.png) {width="400"}

You will see the results in three columns:
* A glyph for the celestial point.
* The positions in the radix .
* The positions according to the calculated harmonic.

A harmonic numer does not need to be an integer. You can enter floating points as harmonic number.

The harmonic number should be at least 1 (this would repeat the radix positions) and not larger than 100.000.
If you do choose such a large number, the results wil suffer from a decrease in exactness. 

You do not need to close the window with harmonics to continue.

## Analysis: midpoints

To calculate midpoints you will use the menu option **Analysis** - **Midpoints**.
In the endpoints window you have the option between three dials: 360, 90 or 45 degrees.
Enigma automatically recalculates the midpoints if you select another dial.

![midpoints.png](midpoints.png) {width="500"}

In the left column, you see all midpoints, indicated by a par of celestial points and a position.
The right column show the occupied midpoints. 
In the image above you see that Uranus is at the midpoint of Sun and Saturn. 
The orb is a bit more than 24 minutes which results in an exactness percentage of 74%.

## Progressive techniques

If you select a chart in the start-window for charts, you can click the button **Progressions**.
This will open a window for progressive techniques. This window is dedicated to the chart you selected.

![prog-main.png](prog-main.png) {width="400"}

For all progressive techniques you need to define an event.

## Progressive: events

If you created a new chart, the window will not show any events. 
An event in Enigma describes the event in the life of the chart owner. 
You need to define an event only once, the program automatically saves it into the database, so you can reuse it in later sessions.
You can use the same event for all supported progressive techniques.

The start-screen fro progressive techniques always shows all available events for the chart you are working on.

### Defining an event
Click the button **New event** to define an event.
You will see the following screen:

![define-event.png](define-event.png) {width="400"}

The functionality is the same as the data input for a chart. 
The description of the event can comprise multiple lines.
Click the button **Save** to put the event into the database.

![prog-main-with-event.png](prog-main-with-event.png) {width="400"}

The newly defined event will automatically be added to the list of available events.

### Deleting an event
Click the button **Delete** to remove the selected event from the database.

## Progressive: secondary directions

Click the button **Secondary dir.** to calculate the secundary directions.

You will see a screen with two tabs.

![secundary-directions.png](secundary-directions.png) {width="400"}

The tab _Positions_ shows the positions for the celestial bodies that you defined in the configuration for progressive techn iques.
For each body, the programs shows longitude, latitude, right ascension and declination.

![sec-dir-aspects.png](sec-dir-aspects.png) {width="400"}

The tab _Aspects with radix_ shows the aspects of the secondary positions with the radix positions and the orb of these aspects.

## Progressive: transits

Click the button **Transits** to calculate the transits.

This works pretty much the same as with secondary directions. 

There will be a screen with two tabs.

![transits.png](transits_1.png) {width="400"}

The tab _Positions_ shows the positions, confirm your definitions in the configuration, using longitude, latitude, right ascension and declination.

![transits-aspects.png](transits-aspects.png) {width="400"}

The tab _Aspects with radix_ shows the aspects of the transit positions with the radix positions and the orb of these aspects.

## Progressive: symbolic directions

Click the button **Symbolic dir.** to calculate symbolic directions.

Again you will see two tabs.

![symbolic-directions.png](symbolic-directions_1.png) {width="400"}

The tab _Positions_ is different. Only the longitude is shown.
The values for Latitude, Right ascension and Declination are not available.
This is because of the nature of symbolic directions. 
The progressive position is not an astronomical position but the result of a simple addition. 
This means that it is not possible to calculate other coördinates than the directed longitude itself.

![sym-dir-aspects.png](sym-dir-aspects.png) {width="400"}

The tab _Aspects with radix_ shows the aspects between the symbolic positions and the radix positions.
