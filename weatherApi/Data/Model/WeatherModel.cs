using Newtonsoft.Json;

namespace weatherApi.Model
{
    public class WeatherModel
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("information")]
        public InfoLocation? Information { get; set; }

        [JsonProperty("fact")]
        public FactForecast? Fact { get; set; }

        [JsonProperty("dailyForecast")]
        public List<DailyForecast>? DailyForecast { get; set; }
    }

    public class FactForecast
    {
        [JsonProperty("temp")]
        public int Temp { get; set; }

        [JsonProperty("feelsLike")]
        public int FeelsLike { get; set; }

        [JsonProperty("systemIconName")]
        public string SystemIconName
        {
            get
            {
                return new CastFunction().getSystemIconName(ConditionEng);
            }
            set { }
        }

        [JsonProperty("conditionEng")]
        public string? ConditionEng { get; set; }

        [JsonProperty("conditionRu")]
        public string ConditionRu
        {
            get
            {
                return new CastFunction().getConditionRu(ConditionEng);
            }
            set { }

        }

        [JsonProperty("cloudness")]
        public double Cloudness { get; set; }

        [JsonProperty("windSpeed")]
        public double WindSpeed { get; set; }

        [JsonProperty("windDirection")]
        public string? WindDirection { get; set; }

        [JsonProperty("pressureMm")]
        public int PressureMm { get; set; }

        [JsonProperty("humidity")]
        public double Humidity { get; set; }

        [JsonProperty("uvIndex")]
        public int? UvIndex { get; set; }
    }

    public class InfoLocation
    {

        [JsonProperty("locality")]
        public string? Locality { get; set; }

        [JsonProperty("district")]
        public string? District { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }

    }

    public class DailyForecast
    {
        private int moonCode;
        public DailyForecast(int moonCode)
        {
            this.moonCode = moonCode;
        }

        [JsonProperty("dateTs")]
        public long DateTs { get; set; }

        [JsonProperty("sunrise")]
        public string? Sunrise { get; set; }

        [JsonProperty("sunset")]
        public string? Sunset { get; set; }

        [JsonProperty("moonPhase")]
        public string MoonPhase
        {
            get { return new CastFunction().getMoonPhase(moonCode); }
            set { }
        }

        [JsonProperty("parts")]
        public PartsForecast? PartsForecast { get; set; }

        [JsonProperty("hours")]
        public List<HoursForecast>? HoursForecast { get; set; }

    }

    public class PartsForecast
    {

        [JsonProperty("morning")]
        public PartDayForecast? Morning { get; set; }

        [JsonProperty("day")]
        public PartDayForecast? Day { get; set; }

        [JsonProperty("evening")]
        public PartDayForecast? Evening { get; set; }

        [JsonProperty("night")]
        public PartDayForecast? Night { get; set; }
    }

    public class PartDayForecast
    {

        [JsonProperty("tempMin")]
        public int? TempMin { get; set; }

        [JsonProperty("tempAvg")]
        public int? TempAvg { get; set; }

        [JsonProperty("tempMax")]
        public int? TempMax { get; set; }

        [JsonProperty("feelsLike")]
        public int FeelsLike { get; set; }

        [JsonProperty("windSpeed")]
        public double WindSpeed { get; set; }

        [JsonProperty("windGust")]
        public double WindGust { get; set; }

        [JsonProperty("windDir")]
        public string? WindDir { get; set; }

        [JsonProperty("pressureMm")]
        public double PressureMm { get; set; }

        [JsonProperty("humidity")]
        public double Humidity { get; set; }

        [JsonProperty("cloudness")]
        public double Cloudness { get; set; }

        [JsonProperty("systemIconName")]
        public string SystemIconName
        {
            get
            {
                return new CastFunction().getSystemIconName(ConditionEng);
            }
            set { }
        }

        [JsonProperty("conditionEng")]
        public string? ConditionEng { get; set; }

        [JsonProperty("conditionRu")]
        public string ConditionRu
        {
            get
            {
                return new CastFunction().getConditionRu(ConditionEng);
            }
            set { }

        }

        [JsonProperty("uvIndex")]
        public int? UvIndex { get; set; }
    }

    public class HoursForecast
    {

        [JsonProperty("hour")]
        public string? Hour { get; set; }

        [JsonProperty("hourTs")]
        public long HourTs { get; set; }

        [JsonProperty("temp")]
        public int Temp { get; set; }

        [JsonProperty("feelsLikeTemp")]
        public int FeelsLikeTemp { get; set; }

        [JsonProperty("systemIconName")]
        public string SystemIconName
        {
            get
            {
                return new CastFunction().getSystemIconName(ConditionEng);
            }
            set { }
        }

        [JsonProperty("conditionEng")]
        public string? ConditionEng { get; set; }

        [JsonProperty("conditionRu")]
        public string ConditionRu
        {
            get
            {
                return new CastFunction().getConditionRu(ConditionEng);
            }
            set { }

        }

        [JsonProperty("cloudness")]
        public double Cloudness { get; set; }

        [JsonProperty("windSpeed")]
        public double WindSpeed { get; set; }

        [JsonProperty("windDir")]
        public string? WindDir { get; set; }

        [JsonProperty("pressureMm")]
        public int PressureMm { get; set; }

        [JsonProperty("humidity")]
        public double Humidity { get; set; }

        [JsonProperty("uvIndex")]
        public int? UvIndex { get; set; }

        [JsonProperty("windGust")]
        public double WindGust { get; set; }
    }
    
    public class CastFunction
    {

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

        public string getMoonPhase(int moonCode)
        {
            switch (moonCode)
            {
                case 0: return "полнолуние";
                case 1 or 2 or 3: return "убывающая Луна";
                case 4: return "последняя четверть";
                case 5 or 6 or 7: return "убывающая Луна";
                case 8: return "новолуние";
                case 9 or 10 or 11: return "растущая Луна";
                case 12: return "первая четверть";
                case 13 or 14 or 15: return "растущая Луна";
                default: return "";
            }
        }
    }

}
