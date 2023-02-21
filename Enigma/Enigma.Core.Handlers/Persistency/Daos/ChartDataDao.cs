// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Configuration;
using Enigma.Domain.Persistency;
using Newtonsoft.Json;
using Serilog;


namespace Enigma.Core.Handlers.Persistency.Daos;

/// <inheritdoc/>
public sealed class ChartDataDao: IChartDataDao           // TODO 0.1 refactor
{
    readonly string dbFullPath = ApplicationSettings.Instance.LocationDatabase + @"/ChartsDatabase.json";     // todo 0.1, move database name to constants

    /// <inheritdoc/>
    public int CountRecords()
    {
        return PerformCount();
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
        return PerformReadAll();
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

 
    private int PerformCount()
    {
        int count = 0;
        if (CheckDatabase())
        {
            var json = File.ReadAllText(dbFullPath);
            try
            {
                var records = JsonConvert.DeserializeObject<PersistableChartData[]>(json);
                count = records.Length;
            }
            catch (Exception ex) 
            {
                string errorTxt = "ChartDataDao.PerformCount() encountered an exception.";
                Log.Error(errorTxt, ex );
                throw new Exception(errorTxt, ex);
            };
        }
        return count;
    }


    private int SearchHighestIndex()
    {
        int index = 0;
        if (CheckDatabase())
        {
            var json = File.ReadAllText(dbFullPath);
            try
            {
                var records = JsonConvert.DeserializeObject<PersistableChartData[]>(json);
                foreach (var item in records)
                {
                    if (item.Id > index) index = item.Id;
                }
            }
            catch (Exception ex)
            {
                string errorTxt = "ChartDataDao.SearchHighestIndex() encountered an exception.";
                Log.Error(errorTxt, ex);
                throw new Exception(errorTxt, ex);
            };
        }
        return index;
    }

    private PersistableChartData? PerformRead(int index)
    {
        if (CheckDatabase())
        {
            var json = File.ReadAllText(dbFullPath);
            try
            {
                var records = JsonConvert.DeserializeObject<PersistableChartData[]>(json);
                foreach (var record in records)
                {
                    if (record.Id == index)
                    {
                        return record;
                    }
                }
            }
            catch (Exception ex)
            {
                string errorTxt = "ChartDataDao.ReadChartData() encountered an exception.";
                Log.Error(errorTxt, ex);
                throw new Exception(errorTxt, ex);
            };
        }
        return null;
    }

    private List<PersistableChartData> PerformSearch(string partOfName)
    {
        List<PersistableChartData> recordsFound = new();
        if (CheckDatabase())
        {
            var json = File.ReadAllText(dbFullPath);
            try
            {
                var records = JsonConvert.DeserializeObject<PersistableChartData[]>(json);
                foreach (var record in records)
                {
                    if (record.Name.ToLower().Contains(partOfName.ToLower()))
                    {
                        recordsFound.Add(record);
                    }
                }
            }
            catch (Exception ex)
            {
                string errorTxt = "ChartDataDao.PerformSearch() using argument " + partOfName + "encountered an exception.";
                Log.Error(errorTxt, ex);
                throw new Exception(errorTxt, ex);
            };
        }
        return recordsFound;
    }


    private List<PersistableChartData> PerformReadAll()
    {
        List<PersistableChartData> recordsFound = new();
        if (CheckDatabase())
        {
            var json = File.ReadAllText(dbFullPath);
            try
            {
                var records = JsonConvert.DeserializeObject<PersistableChartData[]>(json);
                recordsFound = records.ToList();
            }
            catch (Exception ex)
            {
                string errorTxt = "ChartDataDao.PerformReadAll() encountered an exception.";
                Log.Error(errorTxt, ex);
                throw new Exception(errorTxt, ex);
            };
        }
        return recordsFound;
    }


    private int PerformInsert(PersistableChartData chartData)
    {
        int newIndex = -1;
        List<PersistableChartData> recordsAsList = new();
        if (CheckDatabase())
        {
            var json = File.ReadAllText(dbFullPath);
            recordsAsList = JsonConvert.DeserializeObject<PersistableChartData[]>(json).ToList();
        }
        try
        {
            newIndex = SearchHighestIndex() + 1;
            chartData.Id = newIndex;
            recordsAsList.Add(chartData);
            PersistableChartData[] extendedRecords = recordsAsList.ToArray();
            var newJson = JsonConvert.SerializeObject(extendedRecords);
            File.WriteAllText(dbFullPath, newJson);
        }
        catch (Exception ex)
        {
            string errorTxt = "ChartDataDao.PerformInsert() using chartData " + chartData + "encountered an exception.";
            Log.Error(errorTxt, ex);
            throw new Exception(errorTxt, ex);
        };
        return newIndex;
    }


    private bool PerformDelete(int index)
    {
        bool success = false;
        if (CheckDatabase())
        {
            var json = File.ReadAllText(dbFullPath);
            try
            {
                List<PersistableChartData> newRecordSet = new();
                var records = JsonConvert.DeserializeObject<PersistableChartData[]>(json);

                foreach (var record in records)
                {
                    if (record.Id == index)
                    {
                        success = true;
                    }
                    else
                    {
                        newRecordSet.Add(record);
                    }
                }
                PersistableChartData[] newRecords = newRecordSet.ToArray();
                var newJson = JsonConvert.SerializeObject(newRecords);
                File.WriteAllText(dbFullPath, newJson);
            }
            catch (Exception ex)
            {
                string errorTxt = "ChartDataDao.PerformDelete() using index " + index.ToString() + "encountered an exception.";
                Log.Error(errorTxt, ex);
                throw new Exception(errorTxt, ex);
            };
        }
        return success;
    }



    private bool CheckDatabase()
    {
        return File.Exists(dbFullPath);
    }



}