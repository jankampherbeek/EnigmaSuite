// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;


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
        }

        private void PopulateHouses()
        {
            DGHouses.ItemsSource = _controller.GetHousePositionsCurrentChart();
            DGHouses.Columns[0].MaxWidth = 50;
            DGHouses.Columns[2].MaxWidth = 20;
            DGHouses.Columns[2].CellStyle = FindResource("glyphColumnStyle") as Style;
        }


    }
}
