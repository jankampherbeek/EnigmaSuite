// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api;
using Enigma.Domain.Responses;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for ChartsImport screen.</summary>
public sealed class ChartsImportModel
{
    private readonly IPdDataImportExportApi _importApi;

    public ChartsImportModel(IPdDataImportExportApi importApi)
    {
        _importApi = importApi;
    }

    /// <summary>Start processing a csv file with PlanetDance data and, if no errors occurred, add the charts to the database.</summary>
    /// <param name="inputFile">Csv to read.</param>
    /// <returns>ResultMessage with a descriptive text and an error_code (possibly zero: no error).</returns>
    public string PerformImport(string inputFile)
    {
        bool result = _importApi.ImportPdDataToRdbms(inputFile);
        return result ? "Import successfully completed" : "Data could not be imported. Please check C:\\enigma_ar\\data\\errors.txt";
    }
}