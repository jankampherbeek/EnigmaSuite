// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.RequestResponse;
using Enigma.Frontend.Helpers.Support;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;


namespace Enigma.Frontend.Ui.Research.DataFiles
{
    /// <summary>
    /// Interaction logic for DataFilesImportWindow.xaml
    /// </summary>
    public partial class DataFilesImportWindow : Window
    {
        private readonly DataFilesImportController _controller;

        public DataFilesImportWindow()
        {
            InitializeComponent();
            _controller = App.ServiceProvider.GetRequiredService<DataFilesImportController>();
            PopulateTexts();
        }



        private void PopulateTexts()
        {
            FormTitle.Text = Rosetta.TextForId("datafilesimportwindow.title");
            tbSelectFile.Text = Rosetta.TextForId("datafilesimportwindow.selectfile");
            tbNameForData.Text = Rosetta.TextForId("datafilesimportwindow.namefordata");
            tbResultLabel.Text = Rosetta.TextForId("datafilesimportwindow.resultlabel");
            tbResultText.Text = Rosetta.TextForId("datafilesimportwindow.resulttext");
            btnBrowse.Content = Rosetta.TextForId("datafilesimportwindow.btnbrowse");
            btnImport.Content = Rosetta.TextForId("datafilesimportwindow.btnimport");
            btnHelp.Content = Rosetta.TextForId("common.btnhelp");
            btnCancel.Content = Rosetta.TextForId("common.btncancel");
            btnClose.Content = Rosetta.TextForId("common.btnclose");
        }

        private void ClearValues()
        {
            tboxNameForData.Text = "";
            tboxSelectFile.Text = "";
            tbResultText.Text = "";
        }

        private void BrowseClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new();
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                tboxSelectFile.Text = openFileDlg.FileName;

            }
        }

        private void ImportClick(object sender, RoutedEventArgs e)
        {
            tbResultText.Text = "";
            string dataName = tboxNameForData.Text;
            string inputFile = tboxSelectFile.Text;
            if (String.IsNullOrWhiteSpace(dataName) || String.IsNullOrWhiteSpace(inputFile))
            {
                MessageBox.Show(Rosetta.TextForId("datafilesimportwindow.errorsnamepathfound"));
            }
            else
            {
                if (_controller.CheckIfNameCanBeUsed(dataName))
                {
                    ResultMessage resultMsg = _controller.PerformImport(inputFile, dataName);
                    if (resultMsg.ErrorCode > ErrorCodes.NONE)
                    {
                        MessageBox.Show(Rosetta.TextForId("datafilesimportwindow.errorsproblemfilesystem") + " " + resultMsg.Message);
                    }
                    else
                    {
                        tbResultText.Text = resultMsg.Message;
                    }
                }
                else
                {
                    MessageBox.Show(Rosetta.TextForId("datafilesimportwindow.errorsdatanamenotunique"));
                }
            }
        }


        public void HelpClick(object sender, RoutedEventArgs e)
        {
            DataFilesImportController.ShowHelp();
        }

        public void CancelClick(object sender, RoutedEventArgs e)
        {
            ClearValues();
            Close();
        }

        public void CloseClick(object sender, RoutedEventArgs e)
        {
            ClearValues();
            Close();
        }

    }


}
