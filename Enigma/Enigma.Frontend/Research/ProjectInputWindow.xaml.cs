// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Research;
using Enigma.Frontend.Helpers.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Media;

namespace Enigma.Frontend.Ui.Research
{
    /// <summary>Window for the input of project data.</summary>
    public partial class ProjectInputWindow : Window
    {
        private readonly ProjectInputController _controller;

        public ProjectInputWindow()
        {
            InitializeComponent();
            _controller = App.ServiceProvider.GetRequiredService<ProjectInputController>();
            PopulateTexts();
            PopulateData();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HelpClick(object sender, RoutedEventArgs e)
        {
            ProjectInputController.ShowHelp();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            TransferValues();
            if (_controller.ProcessInput())
            {
                Close();
            }
            else
            {
                HandleErrors();
            }
        }


        private void PopulateTexts()
        {
            Title = Rosetta.TextForId("projectinputwindow.title");
            FormTitle.Text = Rosetta.TextForId("projectinputwindow.formtitle");
            GeneralInfoTxt.Text = Rosetta.TextForId("projectinputwindow.generalinfo");
            NameTxt.Text = Rosetta.TextForId("projectinputwindow.name");
            Descr.Text = Rosetta.TextForId("projectinputwindow.description");
            ControlGroupInfoTxt.Text = Rosetta.TextForId("projectinputwindow.controlgroupinfo");
            ControlGroup.Text = Rosetta.TextForId("projectinputwindow.controlgrouptype");
            MultiplicTxt.Text = Rosetta.TextForId("projectinputwindow.multiplication");
            DataInfoTxt.Text = Rosetta.TextForId("projectinputwindow.datainfo");
            DataFile.Text = Rosetta.TextForId("projectinputwindow.datafile");
            BtnCancel.Content = Rosetta.TextForId("common.btncancel");
            BtnHelp.Content = Rosetta.TextForId("common.btnhelp");
            BtnSave.Content = Rosetta.TextForId("common.btnsave");
        }

        private void PopulateData()
        {
            comboControlGroup.Items.Clear();
            foreach (var cgDetail in ControlGroupTypes.StandardShift.AllDetails())
            {
                comboControlGroup.Items.Add(Rosetta.TextForId(cgDetail.TextId));
            }
            comboControlGroup.SelectedIndex = 0;

            comboDataFile.Items.Clear();
            foreach (var presDataName in _controller.GetDataNames())
            {
                comboDataFile.Items.Add(presDataName.DataName);
            }
            comboDataFile.SelectedIndex = 0;
        }

        private void HandleErrors()
        {
            NameValue.Background = _controller.ActualErrorCodes.Contains(ErrorCodes.ERR_RESEARCH_NAME_INVALID) ? Brushes.Yellow : Brushes.White;
            DescrValue.Background = _controller.ActualErrorCodes.Contains(ErrorCodes.ERR_RESEARCH_DESCRIPTION) ? Brushes.Yellow : Brushes.White;
            MultiplicValue.Background = _controller.ActualErrorCodes.Contains(ErrorCodes.ERR_RESEARCH_MULTIPLICATION) ? Brushes.Yellow : Brushes.White;
        }

        private void TransferValues()
        {
            _controller.ProjectName = NameValue.Text;
            _controller.ProjectDescription = DescrValue.Text;
            _controller.ControlGroupMultiplication = MultiplicValue.Text;
            ControlGroupTypes controlGroupType = ControlGroupTypes.StandardShift.ControlGroupTypeForIndex(comboControlGroup.SelectedIndex);
            _controller.ControlGroupType = controlGroupType;
            _controller.DataFileName = comboDataFile.SelectedItem.ToString()!;
        }
    }
}
