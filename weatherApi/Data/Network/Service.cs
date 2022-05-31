using Newtonsoft.Json;
using weatherApi.Data.Model;

namespace weatherApi.Data.Network
{
    public class Service
    {
        public async Task<YandexWeatherModel> GetYandexData(double lat, double lon, bool extra, string lang = "ru_RU")
        {
            YandexWeatherModel yandexWeatherModels = new YandexWeatherModel() ;

            var url = $"https://api.weather.yandex.ru/v2/forecast";
            var parametrs = $"?lat={lat.ToString().Replace(',','.')}&lon={lon.ToString().Replace(',', '.')}&extra={extra}&lang={lang}";

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
    }
}
