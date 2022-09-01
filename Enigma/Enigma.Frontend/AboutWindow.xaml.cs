using Enigma.Frontend.Support;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Enigma.Frontend
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        private IRosetta _rosetta;
        public AboutWindow(IRosetta rosetta)
        {
            InitializeComponent();
            _rosetta = rosetta;
            PopulateTexts();
        }
        private void PopulateTexts()
        {
            Title.Text = _rosetta.TextForId("aboutwindow.title");
            Description.Text = _rosetta.TextForId("aboutwindow.description");
            CopyrightTitle.Text = _rosetta.TextForId("aboutwindow.copyright.title");
            CopyrightText.Text = _rosetta.TextForId("aboutwindow.copyright.text");
            MoreInfoTitle.Text = _rosetta.TextForId("aboutwindow.moreinfo.title");
            MoreInfoText.Text = _rosetta.TextForId("aboutwindow.moreinfo.text");
            BtnClose.Content = _rosetta.TextForId("common.btnclose");
         }


        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
