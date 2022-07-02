using Enigma.Frontend.InputSupport.PresentationFactories;
using Enigma.Frontend.UiDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Enigma.Frontend.Charts
{
    /// <summary>Shows positions in tabular format.</summary>
    public partial class ChartPositions : Window
    {

        private IHousePosForDataGridFactory _housePosForDataGridFactory;
        public CalculatedChart ActiveChart { get; set; }

        public ChartPositions(IHousePosForDataGridFactory housePosForDataGridFactory)
        {
            InitializeComponent();
            _housePosForDataGridFactory = housePosForDataGridFactory;
        }




        public void PopulateHouses()
        {
            if (ActiveChart != null)
            {
                List<PresentableHousePositions> housePositions = _housePosForDataGridFactory.CreateHousePosForDataGrid(ActiveChart.FullHousePositions);
                DGHouses.ItemsSource = housePositions;
            }
        }


    }
}
