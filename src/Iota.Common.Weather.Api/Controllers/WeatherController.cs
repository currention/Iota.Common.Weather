using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Iota.Common.Weather.Services;
using Iota.Common.Weather.Core.Configuration;

namespace Iota.Common.Weather.Api.Controllers
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;
        private readonly WeatherSettings _weatherSettings;

        public WeatherController(IWeatherService weatherService, WeatherSettings weatherSettings)
        {
            _weatherService = weatherService;
            _weatherSettings = weatherSettings;
        }

        // GET api/weather
        [HttpGet("{id}")]
        public async Task<WeatherResponse> Get(string id)
        {
            Console.WriteLine("Weather Url: " + _weatherSettings.WeatherUrl);
            Console.WriteLine("City Id: " + id);
            var response = await  _weatherService.Get(id);

            return response;
        }
    }
}