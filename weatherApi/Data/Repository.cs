using weatherApi.Data.Model;
using weatherApi.Data.Network;

namespace weatherApi.Data
{
    public class Repository
    {
        public YandexWeatherModel GetYandexWeather(double lat, double lon, bool extra, string lang)
        {
            var yandexWeather = new Service().GetYandexData(lat, lon, extra, lang).GetAwaiter().GetResult();
            return yandexWeather;
        }
    }
}
