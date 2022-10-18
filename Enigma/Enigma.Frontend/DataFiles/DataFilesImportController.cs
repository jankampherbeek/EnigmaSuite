// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Configuration;
using Enigma.Domain.Constants;
using Enigma.Domain.Messages;
using Enigma.Frontend.Support;
using Enigma.Frontend;
using Enigma.Persistency.FileHandling;
using Enigma.Persistency.Handlers;
using System.IO;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

public class DataFilesImportController
{
    private IDataFilePreparator _dataFilePreparator;
    private ICsvHandler _csvHandler;

    public DataFilesImportController(IDataFilePreparator dataFilePreparator, ICsvHandler csvHandler)
    {
        _dataFilePreparator = dataFilePreparator;
        _csvHandler = csvHandler;
    }

    /// <summary>Chick if a directory does not yet exist.</summary>
    /// <param name="dataName">Name to be used for the data.</param>
    /// <returns>True if a directory for the data with the given name can be created, otherwise false.</returns>
    public bool CheckIfNameCanBeUsed(string dataName)
    {
        string fullPath = ApplicationSettings.Instance.LocationDataFiles + @"\" + dataName;
        return _dataFilePreparator.FolderNameAvailable(fullPath);
    }

    /// <summary>Start processing a csv file and convert it to Json. If no error occurs, save the Json and a copy of the csv.</summary>
    /// <param name="inputFile">Csv to read.</param>
    /// <param name="dataName">Name for data.</param>
    /// <returns>ResultMessage with a descriptive text and an error_code (possibly zero: no error).</returns>
    public ResultMessage PerformImport(string inputFile, string dataName)
    {
        string dataPath = ApplicationSettings.Instance.LocationDataFiles + @"\" + dataName;
        string fullCsvPath = ApplicationSettings.Instance.LocationDataFiles + @"\" + dataName + @"\csv";
        string fullJsonPath = ApplicationSettings.Instance.LocationDataFiles + @"\" + dataName + @"\json";
        ResultMessage receivedResultMessage = _dataFilePreparator.MakeFolderStructure(dataPath);
        if (receivedResultMessage.ErrorCode > ErrorCodes.ERR_NONE)
        {
            return receivedResultMessage;

        }
        receivedResultMessage = _csvHandler.ConvertStandardCsvToJson(dataName, inputFile, fullJsonPath);
        if (receivedResultMessage.ErrorCode == ErrorCodes.ERR_NONE)
        {
            File.Copy(inputFile, fullCsvPath + dataName + "_copy.csv");
        }

        return receivedResultMessage;
    }

    public void ShowHelp()
    {
        HelpWindow helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();
        helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        helpWindow.SetHelpPage("DataImport");
        helpWindow.ShowDialog();
    }


}