// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Configuration;
using Enigma.Domain.Constants;
using Enigma.Domain.Persistency;
using Serilog;
using System.Text.Json;

namespace Enigma.Core.Handlers.Persistency.Daos;

/// <inheritdoc/>
public sealed class ChartDataDao : IChartDataDao
{
    readonly string dbFullPath = ApplicationSettings.Instance.LocationDatabase + EnigmaConstants.DATABASE_NAME_CHARTS;

    /// <inheritdoc/>
    public int CountRecords()
    {
        return ReadRecordsFromJson().Count;
    }

    /// <inheritdoc/>
    public int HighestIndex()
    {
        return SearchHighestIndex();
    }

    /// <inheritdoc/>
    public PersistableChartData? ReadChartData(int index)
    {
        return PerformRead(index);
    }

    /// <inheritdoc/>
    public List<PersistableChartData> SearchChartData(string partOfName)
    {
        return PerformSearch(partOfName);
    }

    /// <inheritdoc/>
    public List<PersistableChartData> ReadAllChartData()
    {
        return ReadRecordsFromJson();
    }

    /// <inheritdoc/>
    public int AddChartData(PersistableChartData chartData)
    {
        return PerformInsert(chartData);
    }

    /// <inheritdoc/>
    public bool DeleteChartData(int index)
    {
        return PerformDelete(index);
    }


    private int SearchHighestIndex()
    {
        int index = 0;
        var records = ReadRecordsFromJson();
        foreach (var item in records)
        {
            if (item.Id > index) index = item.Id;
        }
        return index;
    }

    private PersistableChartData? PerformRead(int index)
    {
        var records = ReadRecordsFromJson();
        foreach (var record in records)
        {
            if (record.Id == index) return record;
        }
        return null;
    }

    private List<PersistableChartData> PerformSearch(string partOfName)
    {
        List<PersistableChartData> recordsFound = new();
        var records = ReadRecordsFromJson();
        foreach (var record in records)
        {
            if (record.Name.ToLower().Contains(partOfName.ToLower())) recordsFound.Add(record);
        }
        return recordsFound;
    }


    private int PerformInsert(PersistableChartData chartData)
    {
        List<PersistableChartData> recordsAsList = ReadRecordsFromJson();
        try
        {
            int newIndex = SearchHighestIndex() + 1;
            chartData.Id = newIndex;
            recordsAsList.Add(chartData);
            PersistableChartData[] extendedRecords = recordsAsList.ToArray();
            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
            var newJson = JsonSerializer.Serialize(extendedRecords, options);
            File.WriteAllText(dbFullPath, newJson);
            return newIndex;
        }
        catch (Exception ex)
        {
            string errorTxt = "ChartDataDao.PerformInsert() using chartData " + chartData + "encountered an exception.";
            Log.Error(errorTxt, ex);
            throw new Exception(errorTxt, ex);
        };
    }


    private bool PerformDelete(int index)
    {
        bool success = false;
        List<PersistableChartData> newRecordSet = new();
        var records = ReadRecordsFromJson();
        foreach (var record in records)
        {
            if (record.Id == index) success = true;
            else newRecordSet.Add(record);
        }
        try
        {
            PersistableChartData[] newRecords = newRecordSet.ToArray();
            var options = new JsonSerializerOptions { WriteIndented = true , IncludeFields = true };
            var newJson = JsonSerializer.Serialize(newRecords, options);
            File.WriteAllText(dbFullPath, newJson);
        }
        catch (Exception ex)
        {
            string errorTxt = "ChartDataDao.PerformDelete() using index " + index.ToString() + "encountered an exception.";
            Log.Error(errorTxt, ex);
            throw new Exception(errorTxt, ex);
        };
        return success;
    }

    private List<PersistableChartData> ReadRecordsFromJson()
    {
        List<PersistableChartData> records = new();
        if (CheckDatabase())
        {
            var json = File.ReadAllText(dbFullPath);
            try
            {
                PersistableChartData[] persistableChartDatas = JsonSerializer.Deserialize<PersistableChartData[]>(json)!;
                records = persistableChartDatas.ToList();
            }
            catch (Exception ex)
            {
                string errorTxt = "ChartDataDao.ReadRecordsFromJson() encountered an exception.";
                Log.Error(errorTxt, ex);
                throw new Exception(errorTxt, ex);
            };
        }
        return records;
    }

    private bool CheckDatabase()
    {
        return File.Exists(dbFullPath);
    }



}