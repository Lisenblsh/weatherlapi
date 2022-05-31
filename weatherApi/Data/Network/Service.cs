using Newtonsoft.Json;
using weatherApi.Data.Model;

namespace weatherApi.Data.Network
{
    public class Service
    {
        public async Task<YandexWeatherModel> GetYandexData(double lat, double lon)
        {
            YandexWeatherModel yandexWeatherModels = new YandexWeatherModel() ;

            var url = $"https://api.weather.yandex.ru/v2/forecast";
            var parametrs = $"?lat={lat.ToString().Replace(',','.')}&lon={lon.ToString().Replace(',', '.')}&extra=true&lang=ru_RU";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Add("X-Yandex-API-Key", "93b2979e-c971-46b8-b020-4011f3bde74d");

            HttpResponseMessage response = await client.GetAsync(parametrs).ConfigureAwait(false);
            
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var WeatherList = JsonConvert.DeserializeObject<YandexWeatherModel>(jsonString);

                if(WeatherList != null)
                {
                    yandexWeatherModels = WeatherList;
                }
            }
            yandexWeatherModels.Code = response.StatusCode;
            yandexWeatherModels.Message = response.ReasonPhrase;

            return yandexWeatherModels;
        }

        public async Task<OpenWeatherModel> GetOpenWeatherData(double lat, double lon)
        {
            OpenWeatherModel openWeatherModel = new OpenWeatherModel() ;

            var url = $"https://api.openweathermap.org/data/2.5/onecall";
            var parametrs = $"?lat={lat}&lon={lon}&exclude=minutely,alerts&units=metric&lang=ru";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Add("x-api-key", "ce6e9a2a62fff23ff89c00f16b028762");

            HttpResponseMessage response = await client.GetAsync(parametrs).ConfigureAwait (false);

            if(response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var weatherList = JsonConvert.DeserializeObject<OpenWeatherModel>(jsonString);

                if (weatherList != null)
                {
                    openWeatherModel = weatherList;
                }
            }

            openWeatherModel.Code = response.StatusCode;
            openWeatherModel.Message = response.ReasonPhrase;
            
            return openWeatherModel;
        }
    }
}
