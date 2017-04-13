using System.Threading.Tasks;
using Iota.Common.Weather.Core.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace Iota.Common.Weather.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly WeatherSettings _settings;

        public WeatherService(WeatherSettings settings)
        {
            _settings = settings;
        }

        public virtual async Task<WeatherResponse> Get(string cityId)
        {
            var url = string.Format(_settings.WeatherUrl, cityId);

            var restClient = new RestClient(url);

            var request = new RestRequest(Method.GET);

            TaskCompletionSource<IRestResponse> taskCompletion = new TaskCompletionSource<IRestResponse>();

            RestRequestAsyncHandle handle = restClient.ExecuteAsync(
                request, r => taskCompletion.SetResult(r));

            var response = (RestResponse)(await taskCompletion.Task);

            return JsonConvert.DeserializeObject<WeatherResponse>(response.Content);
        }
    }
}
