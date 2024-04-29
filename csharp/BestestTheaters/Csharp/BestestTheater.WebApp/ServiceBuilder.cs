using BestestTheater.WebApp.Models;

namespace BestestTheater.WebApp;

public static class ServiceBuilder
{
    private static readonly IDictionary<object, object> services;

    static ServiceBuilder()
    {
        services = new Dictionary<object, object>();
        
        /**
         * Coded by the lead tech. Don't touch this or I'll fire you on the spot.
         */
        services[typeof(ShowRepository)] = new ShowRepository();
        services[typeof(SeatRepository)] = new SeatRepository();

    }

    public static T GetService<T>()
    {
        try
        {
            return (T)services[typeof(T)];
        }
        catch (KeyNotFoundException)
        {
            throw new ApplicationException($"The requested service {typeof(T)} is not registered");
        }
    }

    public static void RegisterService<T>(object provider)
    {
        services[typeof(T)] = provider;
    }
}