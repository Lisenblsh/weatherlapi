using Microsoft.AspNetCore.Mvc;
using weatherApi.Data;
using weatherApi.Data.Model;
using weatherApi.Model;

namespace weatherApi.Controllers
{
    [ApiController]
    [Route("yandex")]
    public class YandexWeatherController : ControllerBase
    {
#pragma warning disable CS8629
#pragma warning disable CS8604

        [HttpGet]
        public WeatherModel Get(double lat, double lon)
        {
            var yandexWeather = new Repository().GetYandexWeather(lat, lon);
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
                ConditionRu = getConditionRu(yandexWeather.Fact.Condition),
                SystemIconName = getSystemIconName(yandexWeather.Fact.Condition),
                Cloudness = yandexWeather.Fact.Cloudness,
                WindSpeed = yandexWeather.Fact.WindSpeed,
                WindDirection = yandexWeather.Fact.WindDir,
                PressureMm = yandexWeather.Fact.PressureMm,
                Humidity = yandexWeather.Fact.Humidity,
                UvIndex = yandexWeather.Fact.UvIndex,
                HoursForecast = GetHoursForecastList(yandexWeather.Forecasts.First().Hours)

            };

            var dailyForecast = new List<DailyForecast>();

            foreach (var forecast in yandexWeather.Forecasts)
            {
                dailyForecast.Add(new DailyForecast(forecast.MoonCode)
                {
                    DateTs = forecast.DateTs,
                    Sunrise = DateTime.Parse(forecast.Sunrise).Second,
                    Sunset = DateTime.Parse(forecast.Sunset).Second,
                    ConditionEng = forecast.Parts.DayShort.Condition,
                    ConditionRu = getConditionRu(forecast.Parts.DayShort.Condition),
                    SystemIconName = getSystemIconName(forecast.Parts.DayShort.Condition),
                    PartsForecast = new PartsForecast
                    {
                        Morning = GetPartDayForecast(forecast.Parts.Morning),
                        Day = GetPartDayForecast(forecast.Parts.Day),
                        Evening = GetPartDayForecast(forecast.Parts.Evening),
                        Night = GetPartDayForecast(forecast.Parts.Night)
                    }
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
                    ConditionRu = getConditionRu(hour.Condition),
                    SystemIconName = getSystemIconName(hour.Condition),
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
                ConditionRu = getConditionRu(forecast.Condition),
                SystemIconName = getSystemIconName(forecast.Condition),
                UvIndex = forecast.UvIndex
            };
        }

        public string getSystemIconName(string ConditionEng)
        {
            switch (ConditionEng)
            {
                case "clear":
                    return "sun.max";
                case "partly-cloudy":
                    return "cloud.sun";
                case "cloudy":
                    return "cloud";
                case "overcast":
                    return "cloud";
                case "drizzle":
                    return "cloud.drizzle";
                case "light-rain":
                    return "cloud.drizzle";
                case "rain":
                    return "cloud.rain";
                case "moderate-rain":
                    return "cloud.rain";
                case "heavy-rain":
                    return "cloud.heavyrain";
                case "continuous-heavy-rain":
                    return "cloud.heavyrain";
                case "showers":
                    return "cloud.heavyrain";
                case "wet-snow":
                    return "cloud.sleet";
                case "light-snow":
                    return "cloud.snow";
                case "snow":
                    return "cloud.snow";
                case "snow-showers":
                    return "cloud.snow";
                case "hail":
                    return "cloud.hail";
                case "thunderstorm":
                    return "cloud.bolt";
                case "thunderstorm-with-rain":
                    return "cloud.bolt.rain";
                case "thunderstorm-with-hail":
                    return "cloud.bolt.rain";
                default:
                    return "photo";
            }
        }

        public string getConditionRu(string ConditionEng)
        {
            return System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes("ясно"));

            switch (ConditionEng)
            {
                case "clear":
                    return "ясно";
                case "partly-cloudy":
                    return "малооблачно";
                case "cloudy":
                    return "облачно с прояснениями";
                case "overcast":
                    return "пасмурно";
                case "drizzle":
                    return "морось";
                case "light-rain":
                    return "небольшой дождь";
                case "rain":
                    return "дождь";
                case "moderate-rain":
                    return "умеренно сильный дождь";
                case "heavy-rain":
                    return "сильный дождь";
                case "continuous-heavy-rain":
                    return "длительный сильный дождь";
                case "showers":
                    return "ливень";
                case "wet-snow":
                    return "дождь со снегом";
                case "light-snow":
                    return "небольшой снег";
                case "snow":
                    return "снег";
                case "snow-showers":
                    return "снегопад";
                case "hail":
                    return "град";
                case "thunderstorm":
                    return "гроза";
                case "thunderstorm-with-rain":
                    return "дождь с грозой";
                case "thunderstorm-with-hail":
                    return "гроза с градом";
                default:
                    return "";
            }
        }
    }

    [ApiController]
    [Route("openweather")]
    public class OpenWeatherController: ControllerBase
    {
        [HttpGet]
        public WeatherModel Get(double lat, double lon)
        {
            var openWeather = new Repository().GetOpenWeather(lat, lon);
            return ConvertToMainModel(openWeather);
        }

        private WeatherModel ConvertToMainModel(OpenWeatherModel openWeather)
        {
            var data = new WeatherModel
            {
                Code = (int)openWeather.Code,
                Message = openWeather.Message
            };
            if (openWeather.Code != System.Net.HttpStatusCode.OK)
            {
                return data;
            }
            var infoLocation = new InfoLocation
            {
                Locality = null,
                District = null,
                Lat = openWeather.Lat,
                Lon = openWeather.Lon
            };

            var factForecast = new FactForecast
            {
                Temp = (int)openWeather.Current.Temp,
                FeelsLike = (int)openWeather.Current.FeelsLike,
                ConditionRu = openWeather.Current.Weather.First().Description,
                SystemIconName = iconName(openWeather.Current.Weather.First().Icon),
                Cloudness = openWeather.Current.Clouds,
                WindSpeed = openWeather.Current.WindSpeed,
                WindDirection = GetWindDir(openWeather.Current.WindDeg),
                PressureMm = openWeather.Current.Pressure,
                Humidity = openWeather.Current.Humidity,
                UvIndex = (int)openWeather.Current.Uvi,
                HoursForecast = GetHoursForecastList(openWeather.Hourly)

            };

            var dailyForecast = new List<DailyForecast>();

            foreach (var forecast in openWeather.Daily)
            {
                dailyForecast.Add(new DailyForecast(moonPhaseToCode(forecast.MoonPhase))
                {
                    DateTs = forecast.Dt,
                    Sunrise = forecast.Sunrise,
                    Sunset = forecast.Sunset,
                    ConditionRu = forecast.Weather.First().Description,
                    SystemIconName = iconName(forecast.Weather.First().Icon),
                    PartsForecast = new PartsForecast
                    {
                        Morning = new PartDayForecast
                        {
                            TempAvg = (int)forecast.Temp.Morn,
                            TempMax = (int)forecast.Temp.Max,
                            TempMin = (int)forecast.Temp.Min,
                            FeelsLike = (int)forecast.FeelsLike.Morn
                        },
                        Day = new PartDayForecast
                        {
                            TempAvg = (int)forecast.Temp.Day,
                            TempMax = (int)forecast.Temp.Max,
                            TempMin = (int)forecast.Temp.Min,
                            FeelsLike = (int)forecast.FeelsLike.Day
                        },
                        Evening = new PartDayForecast
                        {
                            TempAvg = (int)forecast.Temp.Eve,
                            TempMax = (int)forecast.Temp.Max,
                            TempMin = (int)forecast.Temp.Min,
                            FeelsLike = (int)forecast.FeelsLike.Eve
                        },
                        Night = new PartDayForecast
                        {
                            TempAvg = (int)forecast.Temp.Night,
                            TempMax = (int)forecast.Temp.Max,
                            TempMin = (int)forecast.Temp.Min,
                            FeelsLike = (int)forecast.FeelsLike.Night
                        }
                    }
                });
            }

            data.Information = infoLocation;
            data.Fact = factForecast;
            data.DailyForecast = dailyForecast;


            return data;
        }

        private int moonPhaseToCode(double moonPhase)
        {
            switch (moonPhase)
            {
                case 0:
                    return 8;
                case > 0 and < 0.25:
                    return 9;
                case 0.25:
                    return 12;
                case > 0.25 and < 0.5:
                    return 13;
                case 0.5:
                    return 0;
                case > 0.5 and < 0.75:
                    return 1;
                case 0.75:
                    return 4;
                case > 0.75 and < 1:
                    return 5;
                default:
                    return 0;
            }
        }

        private string GetWindDir(long windDeg)
        {
            switch (windDeg)
            {
                case >= 350 and <= 360 and >= 0 and > 10:
                    return "n";
                case >= 10 and < 80:
                    return "nw";
                case >= 80 and < 100:
                    return "w";
                case >= 100 and < 170:
                    return "sw";
                case >= 170 and < 190:
                    return "s";
                case >= 190 and < 260:
                    return "se";
                case >= 260 and < 280:
                    return "e";
                case >= 280 and < 350:
                    return "ne";
            }
            return "";
        }


        private List<HoursForecast> GetHoursForecastList(Current[] hours)
        {
            var listHours = new List<HoursForecast>();
            foreach (var hour in hours)
            {
                var hourForecast = new HoursForecast
                {
                    Hour = TimeSpan.FromSeconds(hour.Dt).Hours.ToString(),
                    HourTs = hour.Dt,
                    Temp = (int)hour.Temp,
                    FeelsLikeTemp = (int)hour.FeelsLike,
                    Cloudness = hour.Clouds,
                    WindSpeed = hour.WindSpeed,
                    WindGust = hour.WindGust,
                    WindDir = GetWindDir(hour.WindDeg),
                    PressureMm = hour.Pressure,
                    Humidity = hour.Humidity,
                    UvIndex = (int)hour.Uvi,
                    ConditionRu = hour.Weather.First().Description,
                    SystemIconName = iconName(hour.Weather.First().Icon)
                };
                listHours.Add(hourForecast);

            }

            return listHours;
        }

        private string iconName(string icon)
        {
            switch (icon)
            {
                case "01d":
                    return "sun.max";
                case "01n":
                    return "moon.stars";
                case "02d":
                    return "cloud.sun";
                case "02n":
                    return "cloud.moon";
                case "03d":
                    return "cloud.fill";
                case "03n":
                    return "cloud.fill";
                case "04d":
                    return "cloud";
                case "04n":
                    return "cloud";
                case "09d":
                    return "cloud.heavyrain";
                case "09n":
                    return "cloud.heavyrain.fill";
                case "10d":
                    return "cloud.sun.rain";
                case "10n":
                    return "cloud.moon.rain";
                case "11d":
                    return "cloud.sun.bolt";
                case "11n":
                    return "cloud.moon.bolt";
                case "13d":
                    return "snow";
                case "13n":
                    return "snow";
                case "50d":
                    return "cloud.fog";
                case "50n":
                    return "cloud.fog.fill";
                default:
                    return "photo";
            }
        }


    }
}