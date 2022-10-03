// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Support;
using System.Windows;


namespace Enigma.Frontend.DataFiles;

/// <summary>
/// Interaction logic for DataFilesOverview.xaml
/// </summary>
public partial class DataFilesOverviewWindow : Window
{

    private IRosetta _rosetta;
    public DataFilesOverviewWindow(IRosetta rosetta)
    {
        InitializeComponent();
        _rosetta = rosetta;
        PopulateTexts();
    }


    private void PopulateTexts()
    {
        FormTitle.Text = _rosetta.TextForId("datafilesoverviewwindow.title");
    }
}
