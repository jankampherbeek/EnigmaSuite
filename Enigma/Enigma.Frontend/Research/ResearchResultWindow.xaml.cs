﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Research;
using Enigma.Frontend.Helpers.Support;
using System.Windows;

namespace Enigma.Frontend.Ui.Research
{
    /// <summary>Screen to show research results.</summary>
    public partial class ResearchResultWindow : Window
    {
        private readonly ResearchResultController _controller;

        public ResearchResultWindow(ResearchResultController controller)
        {
            InitializeComponent();
            _controller = controller;
            PopulateTexts();
        }

        public void SetResults(CountOfPartsResponse responseTest, CountOfPartsResponse responseControl)
        {
            _controller.SetMethodResponses(responseTest, responseControl);
            PopulateData();
        }

        public void SetResults(CountOfAspectsResponse responseTest, CountOfAspectsResponse responseControl)
        {
            _controller.SetMethodResponses(responseTest, responseControl);
            PopulateData();
        }

        private void PopulateTexts()
        {
            Title = Rosetta.TextForId("researchresultwindow.title");
            tbFormTitle.Text = Rosetta.TextForId("researchresultwindow.formtitle");
            tabTest.Header = Rosetta.TextForId("researchresultwindow.tabtest");
            tabControl.Header = Rosetta.TextForId("researchresultwindow.tabcontrol");
            btnHelp.Content = Rosetta.TextForId("common.btnhelp");
            btnClose.Content = Rosetta.TextForId("common.btnclose");
        }


        private void PopulateData()
        {
            tbTestProject.Text = _controller.ProjectText;
            tbControlProject.Text = _controller.ProjectText;
            tbTestMethod.Text = _controller.TestMethodText;
            tbControlMethod.Text = _controller.ControlMethodText;
            tbTestResult.Text = _controller.TestResultText;
            tbControlResult.Text = _controller.ControlResultText;
        }



        public void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public void HelpClick(object sender, RoutedEventArgs e)
        {
            _controller.ShowHelp();
        }
    }





}