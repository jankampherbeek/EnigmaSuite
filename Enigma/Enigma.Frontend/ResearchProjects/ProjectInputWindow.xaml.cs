// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Engima.Domain.Research;
using Enigma.Domain.Constants;
using Enigma.Frontend.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Media;

namespace Enigma.Frontend.ResearchProjects
{
    /// <summary>
    /// Interaction logic for ProjectInputWindow.xaml
    /// </summary>
    public partial class ProjectInputWindow : Window
    {
        private readonly IRosetta _rosetta;
        private readonly ProjectInputController _controller;
        private readonly IControlGroupTypeSpecifications _controlGroupTypeSpecifications;


        public ProjectInputWindow()
        {
            InitializeComponent();
            _rosetta = App.ServiceProvider.GetRequiredService<IRosetta>();
            _controller = App.ServiceProvider.GetRequiredService<ProjectInputController>();
            _controlGroupTypeSpecifications = App.ServiceProvider.GetRequiredService<IControlGroupTypeSpecifications>();
            PopulateTexts();
            PopulateData();

        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HelpClick(object sender, RoutedEventArgs e)
        {
            // todo
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            TransferValues();
            if (_controller.ProcessInput())
            {
                // create json for project
                // create controlgroups
                // show msgbox
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
            Identif.Text = _rosetta.TextForId("projectinputwindow.identification");
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
            foreach (var cgDetail in _controlGroupTypeSpecifications.AllControlGroupTypeDetails())
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
            IdentifValue.Background = _controller.ActualErrorCodes.Contains(ErrorCodes.ERR_RESEARCH_IDENTIFICATION_INVALID) ? Brushes.Yellow : Brushes.White;
            DescrValue.Background = _controller.ActualErrorCodes.Contains(ErrorCodes.ERR_RESEARCH_DESCRIPTION) ? Brushes.Yellow : Brushes.White;
            MultiplicValue.Background = _controller.ActualErrorCodes.Contains(ErrorCodes.ERR_RESEARCH_MULTIPLICATION) ? Brushes.Yellow : Brushes.White;
        }

        private void TransferValues()
        {
            _controller.ProjectName = NameValue.Text;
            _controller.ProjectIdentifier = IdentifValue.Text;
            _controller.ProjectDescription = DescrValue.Text;
            _controller.ControlGroupMultiplication = MultiplicValue.Text;
            ControlGroupTypes controlGroupType = _controlGroupTypeSpecifications.AllControlGroupTypeDetails()[comboControlGroup.SelectedIndex].ControlGroupType;
            _controller.ControlGroupType = controlGroupType;
            _controller.DataFileName = comboDataFile.SelectedItem.ToString();
        }
    }
}
