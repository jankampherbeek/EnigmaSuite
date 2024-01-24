# Module charts

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

## Create a new chart
To create a new chart  you can click the button **New Chart** or select the menu item **Charts** -> **New Chart**.
This action will open a new screen where you can enter the data:

![](data-input.png)

You need to enter the required data and click the button **Calculate**.
If you made an error, Enigma will show a popup with information about what went wrong.
Also, if an error is detected, the underlining of the input field will turn red.

Use the button **Help** for advise on the different fields.

After clicking the button **Calculate**, the newly entered chart can be selected and analysed.

## Retrieve an existing chart from the database
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
