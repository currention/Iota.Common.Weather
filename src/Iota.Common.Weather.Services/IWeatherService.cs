using System.Threading.Tasks;

namespace Iota.Common.Weather.Services
{
    public interface IWeatherService
    {
        Task<WeatherResponse> Get(string cityId);
    }
}