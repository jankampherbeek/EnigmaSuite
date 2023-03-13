// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Charts;
using Enigma.Domain.Persistency;
using Enigma.Frontend.Ui.Interfaces;


namespace Enigma.Frontend.Ui.SUpport;

/// <inheritdoc/>
public sealed class ChartDataConverter: IChartDataConverter
{
    private readonly ILocationConversion _locationConversion;

    public ChartDataConverter(ILocationConversion locationConversion)
    {
        _locationConversion = locationConversion;
    }

    /// <inheritdoc/>
    public ChartData FromPersistableChartData(PersistableChartData persistableChartData)
    {
        return HandleConversion(persistableChartData);
    }

    /// <inheritdoc/>
    public PersistableChartData ToPersistableChartData(ChartData chartData)
    {
        return HandleConversion(chartData);
    }

    private ChartData HandleConversion(PersistableChartData persistableChartData)
    {
        string name = persistableChartData.Name;
        string description = persistableChartData.Description;
        string source = persistableChartData.Source;
        string locationName = persistableChartData.LocationName;
        ChartCategories chartCategory = persistableChartData.ChartCategory;
        RoddenRatings rating = persistableChartData.Rating;
        MetaData metaData = new(name, description, source, locationName, chartCategory, rating);
        string locationFullName = _locationConversion.CreateLocationDescription(locationName, persistableChartData.GeoLat, persistableChartData.GeoLong); 
        Location location = new(locationFullName, persistableChartData.GeoLong, persistableChartData.GeoLat);
        FullDateTime fullDateTime = new(persistableChartData.DateText, persistableChartData.TimeText, persistableChartData.JulianDayEt);
        return new ChartData(persistableChartData.Id, metaData, location, fullDateTime);
    }

    private static PersistableChartData HandleConversion (ChartData chartData)
    {
        return new PersistableChartData (
            chartData.Id,
            chartData.MetaData.Name,
            chartData.MetaData.Description,
            chartData.MetaData.Source,
            chartData.MetaData.ChartCategory,
            chartData.MetaData.RoddenRating,
            chartData.FullDateTime.JulianDayForEt,
            chartData.FullDateTime.DateText,
            chartData.FullDateTime.TimeText,
            chartData.Location.LocationFullName,
            chartData.Location.GeoLong,
            chartData.Location.GeoLat
        );
    }


} 