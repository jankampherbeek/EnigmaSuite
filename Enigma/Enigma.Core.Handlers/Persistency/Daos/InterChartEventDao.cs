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
public sealed class InterChartEventDao: IInterChartEventDao
{
    readonly string dbInterChartsEventsFullPath = ApplicationSettings.Instance.LocationDatabase + EnigmaConstants.DATABASE_NAME_INTER_CHARTS_EVENTS;

    /// <inheritdoc/>
    public void Insert(int chartId, int eventId)
    {
        PerformInsert(chartId, eventId);
    }

    /// <inheritdoc/>
    public List<InterChartEvent> ReadAll()
    {
        return ReadRecordsFromJson();
    }

    public bool Delete(int chartId)
    {
        return PerformDelete(chartId);
    }

    private bool CheckDatabaseInterChartsEvents()
    {
        return File.Exists(dbInterChartsEventsFullPath);
    }

    private void PerformInsert(int chartId, int eventId)
    {
        List<InterChartEvent> recordsAsList = ReadRecordsFromJson();
        try
        {
            recordsAsList.Add(new InterChartEvent(chartId, eventId));

            InterChartEvent[] extendedRecords = recordsAsList.ToArray();
            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
            var newJson = JsonSerializer.Serialize(extendedRecords, options);
            File.WriteAllText(dbInterChartsEventsFullPath, newJson);
        }
        catch (Exception ex)
        {
            string errorTxt = "InterChartEventDao.PerformInsert() using chartId " + chartId + " and eventId " + eventId + "encountered an exception.";
            Log.Error(errorTxt, ex);
            throw new Exception(errorTxt, ex);
        };
    }



    private List<InterChartEvent> ReadRecordsFromJson()
    {
        List<InterChartEvent> records = new();
        if (CheckDatabaseInterChartsEvents())
        {
            var json = File.ReadAllText(dbInterChartsEventsFullPath);
            try
            {
                InterChartEvent[] persistableInterChartEventDatas = JsonSerializer.Deserialize<InterChartEvent[]>(json)!;
                records = persistableInterChartEventDatas.ToList();
            }
            catch (Exception ex)
            {
                string errorTxt = "InterChartEventDataDao.ReadRecordsFromJson() encountered an exception.";
                Log.Error(errorTxt, ex);
                throw new Exception(errorTxt, ex);
            };
        }
        return records;
    }

    private bool PerformDelete(int chartId)
    {
        bool success = false;
        List<InterChartEvent> newRecordSet = new();
        var records = ReadRecordsFromJson();
        foreach (var record in records)
        {
            if (record.ChartId == chartId)
            {
                success = true;
            }
            else newRecordSet.Add(record);
        }
        try
        {
            InterChartEvent[] newRecords = newRecordSet.ToArray();
            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
            var newJson = JsonSerializer.Serialize(newRecords, options);
            File.WriteAllText(dbInterChartsEventsFullPath, newJson);
        }
        catch (Exception ex)
        {
            string errorTxt = "InterChartEventDataDao.PerformDelete() using chartId " + chartId.ToString() + "encountered an exception.";
            Log.Error(errorTxt, ex);
            throw new Exception(errorTxt, ex);
        };
        return success;
    }

}
