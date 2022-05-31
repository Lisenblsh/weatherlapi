using weatherApi.Data.Model;
using weatherApi.Data.Network;

namespace weatherApi.Data
{
    public class Repository
    {
        public YandexWeatherModel GetYandexWeather(double lat, double lon)
        {
            var yandexWeather = new Service().GetYandexData(lat, lon).GetAwaiter().GetResult();
            return yandexWeather;
        }

        public OpenWeatherModel GetOpenWeather(double lat, double lon)
        {
            var openWeather = new Service().GetOpenWeatherData(lat,lon).GetAwaiter().GetResult();
            return openWeather;
        }
    }
}
