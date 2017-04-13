using System;
using Xunit;
using Moq;
using Iota.Common.Weather.Core.Configuration;
using Iota.Common.Weather.Services;

namespace Tests
{
    public class WeatherServiceTests
    {
        [Fact]
        public async void Can_get_weather() 
        {
			 // Arrange
        Mock<WeatherSettings> weatherSettingMock = new Mock<WeatherSettings>();
		
     var settings =   weatherSettingMock.Object;
     settings.WeatherUrl = "http://www.weather.com.cn/data/sk/{0}.html";

     
 Console.WriteLine("SETTINGS: Weather url is " + settings.WeatherUrl);
        // weatherSettingMock(x => x.WeatherUrl).Returns("http://www.weather.com.cn/data/sk/{0}.html");
        IWeatherService service = new WeatherService(settings);
 
        // Act 
        WeatherResponse result = await service.Get("101191201");
 
 Console.WriteLine(result == null);

if(result != null && result.WeatherInfo != null)
{
 Console.WriteLine(result.WeatherInfo.City);
 Console.WriteLine(result.WeatherInfo.Temp);
}
        // Assert
        // Assert.Equal("hello", result);
		
        }
    }
}
