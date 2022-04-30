// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace E4C.Core;

using SimpleInjector;
using E4C.Core.Api.Astron;
using E4C.Core.Api.Datetime;

public interface IApiFactory
{
    public IChartAllPositionsApi GetChartAllPositionsApi();
    public ICoordinateConversionApi GetCoordinateConversionApi();
    public IHorizontalApi GetHorizontalApi();
    public IHousesApi GetHousesApi();
    public IObliquityApi GetObliquityApi();
    public ICalcDateTimeApi GetCalcDateTimeApi();
    public ICheckDateTimeApi GetCheckDateTimeApi();
    public IJulianDayApi GetJulianDayApi();

}

public class ApiFactory : IApiFactory
{

    private readonly ApiFrontEnd _apiFrontEnd;
    private readonly Container _container;

    public ApiFactory()
    {
        _apiFrontEnd = ApiFrontEnd.Instance;
        _container = _apiFrontEnd.GetContainer();
    }

    public ICalcDateTimeApi GetCalcDateTimeApi()
    {
        return _container.GetInstance<ICalcDateTimeApi>();
    }

    public IChartAllPositionsApi GetChartAllPositionsApi()
    {
        return _container.GetInstance<IChartAllPositionsApi>();
    }

    public ICheckDateTimeApi GetCheckDateTimeApi()
    {
        return _container.GetInstance <ICheckDateTimeApi> ();
    }

    public ICoordinateConversionApi GetCoordinateConversionApi()
    {
        return _container.GetInstance<ICoordinateConversionApi>();
    }

    public IHorizontalApi GetHorizontalApi()
    {
        return _container.GetInstance<IHorizontalApi>();
    }

    public IHousesApi GetHousesApi()
    {
        return _container.GetInstance<IHousesApi>();
    }

    public IJulianDayApi GetJulianDayApi()
    {
        return _container.GetInstance<IJulianDayApi>();
    }

    public IObliquityApi GetObliquityApi()
    {
        return _container.GetInstance<IObliquityApi>();
    }

}