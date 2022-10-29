// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Messages;
using Enigma.Frontend.Support;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;


namespace Enigma.Frontend.DataFiles
{
    /// <summary>
    /// Interaction logic for DataFilesImportWindow.xaml
    /// </summary>
    public partial class DataFilesImportWindow : Window
    {
        private IRosetta _rosetta;
        private DataFilesImportController _controller;

        public DataFilesImportWindow()
        {
            InitializeComponent();
            _controller = App.ServiceProvider.GetRequiredService<DataFilesImportController>();
            _rosetta = App.ServiceProvider.GetRequiredService<IRosetta>();
                PopulateTexts();
        }


        
        private void PopulateTexts()
        {
            FormTitle.Text = _rosetta.TextForId("datafilesimportwindow.title");
            tbSelectFile.Text = _rosetta.TextForId("datafilesimportwindow.selectfile");
            tbNameForData.Text = _rosetta.TextForId("datafilesimportwindow.namefordata");
            tbResultLabel.Text = _rosetta.TextForId("datafilesimportwindow.resultlabel");
            tbResultText.Text = _rosetta.TextForId("datafilesimportwindow.resulttext");
            btnBrowse.Content = _rosetta.TextForId("datafilesimportwindow.btnbrowse");
            btnImport.Content = _rosetta.TextForId("datafilesimportwindow.btnimport");
            btnHelp.Content = _rosetta.TextForId("common.btnhelp");
            btnCancel.Content = _rosetta.TextForId("common.btncancel");
            btnClose.Content = _rosetta.TextForId("common.btnclose");
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
                MessageBox.Show(_rosetta.TextForId("datafilesimportwindow.errorsnamepathfound"));
            } else
            {
                if (_controller.CheckIfNameCanBeUsed(dataName))
                {
                    ResultMessage resultMsg = _controller.PerformImport(inputFile, dataName);
                    if (resultMsg.ErrorCode > ErrorCodes.ERR_NONE)
                    {
                        MessageBox.Show(_rosetta.TextForId("datafilesimportwindow.errorsproblemfilesystem") + " " + resultMsg.Message);
                    } else
                    {
                        tbResultText.Text = resultMsg.Message;
                    }
                } else
                {
                    MessageBox.Show(_rosetta.TextForId("datafilesimportwindow.errorsdatanamenotunique"));
                }
            }
        }


        public void HelpClick(object sender, RoutedEventArgs e)
        {
            _controller.ShowHelp();
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
