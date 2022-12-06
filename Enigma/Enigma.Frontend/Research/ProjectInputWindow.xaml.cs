// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Research;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Helpers.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Media;

namespace Enigma.Frontend.Ui.Research
{
    /// <summary>Window for the input of project data.</summary>
    public partial class ProjectInputWindow : Window
    {
        private readonly Rosetta _rosetta = Rosetta.Instance;
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
            FormTitle.Text = _rosetta.TextForId("projectinputwindow.formtitle");
            GeneralInfoTxt.Text = _rosetta.TextForId("projectinputwindow.generalinfo");
            NameTxt.Text = _rosetta.TextForId("projectinputwindow.name");
            Descr.Text = _rosetta.TextForId("projectinputwindow.description");
            ControlGroupInfoTxt.Text = _rosetta.TextForId("projectinputwindow.controlgroupinfo");
            ControlGroup.Text = _rosetta.TextForId("projectinputwindow.controlgrouptype");
            MultiplicTxt.Text = _rosetta.TextForId("projectinputwindow.multiplication");
            DataInfoTxt.Text = _rosetta.TextForId("projectinputwindow.datainfo");
            DataFile.Text = _rosetta.TextForId("projectinputwindow.datafile");
            BtnCancel.Content = _rosetta.TextForId("common.btncancel");
            BtnHelp.Content = _rosetta.TextForId("common.btnhelp");
            BtnSave.Content = _rosetta.TextForId("common.btnsave");
        }

        private void PopulateData()
        {
            comboControlGroup.Items.Clear();
            foreach (var cgDetail in ControlGroupTypes.StandardShift.AllDetails())
            {
                comboControlGroup.Items.Add(_rosetta.TextForId(cgDetail.TextId));
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
#pragma warning disable CS8601 // Possible null reference assignment.    
            _controller.DataFileName = comboDataFile.SelectedItem.ToString();
#pragma warning restore CS8601 // Possible null reference assignment.
        }
    }
}
