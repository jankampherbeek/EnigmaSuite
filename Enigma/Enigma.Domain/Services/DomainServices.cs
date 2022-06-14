// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.CalcVars;
using Enigma.Domain.DateTime;
using Enigma.Domain.Locational;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Domain.Services;

public static class DomainServices
{
    public static void RegisterDomainServices(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IAyanamshaSpecifications, AyanamshaSpecifications>();
        serviceCollection.AddTransient<ICalendarSpecifications, CalendarSpecifications>();
        serviceCollection.AddTransient<IChartCategorySpecifications, ChartCategorySpecifications>();
        serviceCollection.AddTransient<IDirections4GeoLatSpecifications, Directions4GeoLatSpecifications>();
        serviceCollection.AddTransient<IDirections4GeoLongSpecifications, Directions4GeoLongSpecifications>();
        serviceCollection.AddTransient<IHouseSystemSpecs, HouseSystemSpecs>();
        serviceCollection.AddTransient<IObserverPositionSpecifications, ObserverPositionSpecifications>();
        serviceCollection.AddTransient<IProjectionTypeDetailSpecifications, ProjectionTypeDetailSpecifications>();
        serviceCollection.AddTransient<IRoddenRatingSpecifications, RoddenRatingSpecifications>();
        serviceCollection.AddTransient<ISolarSystemPointSpecifications, SolarSystemPointSpecifications>();
        serviceCollection.AddTransient<ISolSysPointCatSpecifications, SolSysPointCatSpecifications>();
        serviceCollection.AddTransient<ITimeZoneSpecifications, TimeZoneSpecifications>();
        serviceCollection.AddTransient<IYearCountSpecifications, YearCountSpecifications>();
        serviceCollection.AddTransient<IZodiacTypeSpecifications, ZodiacTypeSpecifications>();
    }
}

