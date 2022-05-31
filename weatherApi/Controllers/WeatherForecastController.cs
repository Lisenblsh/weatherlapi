using Microsoft.AspNetCore.Mvc;
using weatherApi.Data;
using weatherApi.Data.Model;
using weatherApi.Model;

namespace weatherApi.Controllers
{
    [ApiController]
    [Route("forecast")]
    public class WeatherForecastController : ControllerBase
    {
#pragma warning disable CS8629
#pragma warning disable CS8604
        public WeatherModel Get(double lat, double lon, bool? extra = false, string? lang = "en_EN")
        {
            var yandexWeather = new Repository().GetYandexWeather(lat, lon, (bool)extra, lang);
            return ConvertToMainModel(yandexWeather);
        }

        private WeatherModel ConvertToMainModel(YandexWeatherModel yandexWeather)
        {
            var data = new WeatherModel
            {
                Code = (int)yandexWeather.Code,
                Message = yandexWeather.Message
            };
            if (yandexWeather.Code != System.Net.HttpStatusCode.OK)
            {
                return data;
            }
            var infoLocation = new InfoLocation
            {
                Locality = yandexWeather.GeoObject.Locality?.Name,
                District = yandexWeather.GeoObject.District?.Name,
                Lat = yandexWeather.Info.Lat,
                Lon = yandexWeather.Info.Lon
            };

            var factForecast = new FactForecast
            {
                Temp = yandexWeather.Fact.Temp,
                FeelsLike = yandexWeather.Fact.FeelsLike,
                ConditionEng = yandexWeather.Fact.Condition,
                Cloudness = yandexWeather.Fact.Cloudness,
                WindSpeed = yandexWeather.Fact.WindSpeed,
                WindDirection = yandexWeather.Fact.WindDir,
                PressureMm = yandexWeather.Fact.PressureMm,
                Humidity = yandexWeather.Fact.Humidity,
                UvIndex = yandexWeather.Fact.UvIndex

            };

            var dailyForecast = new List<DailyForecast>();

            foreach (var forecast in yandexWeather.Forecasts)
            {
                dailyForecast.Add(new DailyForecast(forecast.MoonCode)
                {
                    DateTs = forecast.DateTs,
                    Sunrise = forecast.Sunrise,
                    Sunset = forecast.Sunset,
                    PartsForecast = new PartsForecast
                    {
                        Morning = GetPartDayForecast(forecast.Parts.Morning),
                        Day = GetPartDayForecast(forecast.Parts.Day),
                        Evening = GetPartDayForecast(forecast.Parts.Evening),
                        Night = GetPartDayForecast(forecast.Parts.Night)
                    },
                    HoursForecast = GetHoursForecastList(forecast.Hours)
                });
            }

            data.Information = infoLocation;
            data.Fact = factForecast;
            data.DailyForecast = dailyForecast;


            return data;
        }

        private List<HoursForecast> GetHoursForecastList(Fact[] hours)
        {
            var listHours = new List<HoursForecast>();
            foreach (var hour in hours)
            {
                listHours.Add(new HoursForecast
                {
                    Hour = $"{hour.Hour}",
                    HourTs = hour.HourTs,
                    Temp = hour.Temp,
                    FeelsLikeTemp = hour.FeelsLike,
                    ConditionEng = hour.Condition,
                    Cloudness = hour.Cloudness,
                    WindSpeed = hour.WindSpeed,
                    WindGust = hour.WindGust,
                    WindDir = hour.WindDir,
                    PressureMm = hour.PressureMm,
                    Humidity = hour.Humidity,
                    UvIndex = hour.UvIndex

                });
            }

            return listHours;
        }

        private PartDayForecast GetPartDayForecast(Fact forecast)
        {
            return new PartDayForecast
            {
                TempMin = forecast.TempMin,
                TempAvg = forecast.TempAvg,
                TempMax = forecast.TempMax,
                FeelsLike = forecast.FeelsLike,
                WindSpeed = forecast.WindSpeed,
                WindGust = forecast.WindGust,
                WindDir = forecast.WindDir,
                PressureMm = forecast.PressureMm,
                Humidity = forecast.Humidity,
                Cloudness = forecast.Cloudness,
                ConditionEng = forecast.Condition,
                UvIndex = forecast.UvIndex
            };
        }
    }
}