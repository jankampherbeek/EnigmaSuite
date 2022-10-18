// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Enigma.Frontend.DataFiles
{
    /// <summary>
    /// Interaction logic for DataFilesExportWindow.xaml
    /// </summary>
    public partial class DataFilesExportWindow : Window
    {
        private DataFilesExportController _controller;
        private IRosetta _rosetta;

        public DataFilesExportWindow()
        {
            InitializeComponent();
            _controller = App.ServiceProvider.GetRequiredService<DataFilesExportController>();
            _rosetta = App.ServiceProvider.GetRequiredService<IRosetta>();
        //   PopulateTexts();


        }
    }
}
