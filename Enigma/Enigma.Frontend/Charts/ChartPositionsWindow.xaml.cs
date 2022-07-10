// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;
using System.Windows.Controls;

namespace Enigma.Frontend.Charts
{
    /// <summary>Shows positions in tabular format.</summary>
    public partial class ChartPositionsWindow : Window
    {

        private ChartPositionsController _controller;

        public ChartPositionsWindow(ChartPositionsController controller)
        {
            InitializeComponent();
            _controller = controller;
          
        }
         
        public void PopulateAll()
        {
            PopulateHouses();
            PopulateCelPoints();
        }

        private void PopulateHouses()
        {
            DGHouses.ItemsSource = _controller.GetHousePositionsCurrentChart();
            DGHouses.GridLinesVisibility = DataGridGridLinesVisibility.None;
            DGHouses.Columns[0].Header = "";
            DGHouses.Columns[1].Header = "Longitude";
            DGHouses.Columns[2].Header = "";
            DGHouses.Columns[3].Header = "RA";
            DGHouses.Columns[4].Header = "Declination";
            DGHouses.Columns[5].Header = "Azimuth";
            DGHouses.Columns[6].Header = "Altitude";
            DGHouses.Columns[0].MaxWidth = 80;
            DGHouses.Columns[2].MaxWidth = 20;
            DGHouses.Columns[0].CellStyle = FindResource("nameColumnStyle") as Style; 
            DGHouses.Columns[2].CellStyle = FindResource("glyphColumnStyle") as Style;
            DGHouses.HorizontalAlignment = HorizontalAlignment.Right;       
        }

        private void PopulateCelPoints()
        {
            DGCelPoints.ItemsSource = _controller.GetCelPointPositionsCurrentChart();
            DGCelPoints.GridLinesVisibility = DataGridGridLinesVisibility.None;
            DGCelPoints.Columns[0].Header = "";
            DGCelPoints.Columns[1].Header = "Longitude";
            DGCelPoints.Columns[2].Header = "";
            DGCelPoints.Columns[3].Header = "Speed long";
            DGCelPoints.Columns[4].Header = "Latitude";
            DGCelPoints.Columns[5].Header = "Speed lat";
            DGCelPoints.Columns[6].Header = "RA";
            DGCelPoints.Columns[7].Header = "Speed RA";
            DGCelPoints.Columns[8].Header = "Declination";
            DGCelPoints.Columns[9].Header = "Speed decl";
            DGCelPoints.Columns[10].Header = "Distance";
            DGCelPoints.Columns[11].Header = "Speed dist";
            DGCelPoints.Columns[12].Header = "Azimuth";
            DGCelPoints.Columns[13].Header = "Altitude";

            DGCelPoints.Columns[0].CellStyle = FindResource("glyphColumnStyle") as Style;
            DGCelPoints.Columns[2].CellStyle = FindResource("glyphColumnStyle") as Style;
            DGCelPoints.HorizontalAlignment = HorizontalAlignment.Right;
        }


    }
}
