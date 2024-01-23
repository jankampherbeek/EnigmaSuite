using System.Data.SQLite;
using Enigma.Domain.Dtos;

namespace Enigma.Core.Persistency;

public interface ICustomerRepository
{
   // ChartData GetCustomer(long id);
    void AddChartData(ChartData newChart);
}

public class SqLiteBaseRepository
{
    public static string DbFile
    {
        get { return ApplicationSettings.LocationDatabase + "EnigmaSQLite.sqlite"; }
    }

    public static SQLiteConnection SimpleDbConnection()
    {
        return new SQLiteConnection("Data Source=" + DbFile);
    }
}