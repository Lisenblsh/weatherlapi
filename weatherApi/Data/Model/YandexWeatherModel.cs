using Newtonsoft.Json;

namespace weatherApi.Data.Model
{
    public partial class YandexWeatherModel
    {

        [JsonProperty("code")]
        public System.Net.HttpStatusCode Code { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("now")]
        public long Now { get; set; }

        [JsonProperty("now_dt")]
        public DateTimeOffset NowDt { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }

        [JsonProperty("geo_object")]
        public GeoObject GeoObject { get; set; }

        [JsonProperty("yesterday")]
        public Yesterday Yesterday { get; set; }

        [JsonProperty("fact")]
        public Fact Fact { get; set; }

        [JsonProperty("forecasts")]
        public Forecast[] Forecasts { get; set; }
    }

    public partial class Fact
    {
        [JsonProperty("obs_time", NullValueHandling = NullValueHandling.Ignore)]
        public long? ObsTime { get; set; }

        [JsonProperty("uptime", NullValueHandling = NullValueHandling.Ignore)]
        public long? Uptime { get; set; }

        [JsonProperty("temp", NullValueHandling = NullValueHandling.Ignore)]
        public int Temp { get; set; }

        [JsonProperty("feels_like")]
        public int FeelsLike { get; set; }

        [JsonProperty("temp_water")]
        public int TempWater { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("condition")]
        public string Condition { get; set; }

        [JsonProperty("cloudness")]
        public double Cloudness { get; set; }

        [JsonProperty("prec_type")]
        public long PrecType { get; set; }

        [JsonProperty("prec_prob")]
        public long PrecProb { get; set; }

        [JsonProperty("prec_strength")]
        public double PrecStrength { get; set; }

        [JsonProperty("is_thunder", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsThunder { get; set; }

        [JsonProperty("wind_speed")]
        public double WindSpeed { get; set; }

        [JsonProperty("wind_dir")]
        public string WindDir { get; set; }

        [JsonProperty("pressure_mm")]
        public int PressureMm { get; set; }

        [JsonProperty("pressure_pa")]
        public long PressurePa { get; set; }

        [JsonProperty("humidity")]
        public long Humidity { get; set; }

        [JsonProperty("daytime", NullValueHandling = NullValueHandling.Ignore)]
        public string Daytime { get; set; }

        [JsonProperty("polar", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Polar { get; set; }

        [JsonProperty("season", NullValueHandling = NullValueHandling.Ignore)]
        public string Season { get; set; }

        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public string FactSource { get; set; }

        [JsonProperty("soil_moisture")]
        public double SoilMoisture { get; set; }

        [JsonProperty("soil_temp")]
        public long SoilTemp { get; set; }

        [JsonProperty("uv_index", NullValueHandling = NullValueHandling.Ignore)]
        public int? UvIndex { get; set; }

        [JsonProperty("wind_gust")]
        public double WindGust { get; set; }

        [JsonProperty("hour", NullValueHandling = NullValueHandling.Ignore)]
        public int? Hour { get; set; }

        [JsonProperty("hour_ts", NullValueHandling = NullValueHandling.Ignore)]
        public long HourTs { get; set; }

        [JsonProperty("prec_mm", NullValueHandling = NullValueHandling.Ignore)]
        public double? PrecMm { get; set; }

        [JsonProperty("prec_period", NullValueHandling = NullValueHandling.Ignore)]
        public long? PrecPeriod { get; set; }

        [JsonProperty("_source", NullValueHandling = NullValueHandling.Ignore)]
        public string Source { get; set; }

        [JsonProperty("temp_min", NullValueHandling = NullValueHandling.Ignore)]
        public int? TempMin { get; set; }

        [JsonProperty("temp_avg", NullValueHandling = NullValueHandling.Ignore)]
        public int? TempAvg { get; set; }

        [JsonProperty("temp_max", NullValueHandling = NullValueHandling.Ignore)]
        public int? TempMax { get; set; }
    }

    public partial class Forecast
    {
        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("date_ts")]
        public long DateTs { get; set; }

        [JsonProperty("week")]
        public long Week { get; set; }

        [JsonProperty("sunrise")]
        public string Sunrise { get; set; }

        [JsonProperty("sunset")]
        public string Sunset { get; set; }

        [JsonProperty("rise_begin")]
        public string RiseBegin { get; set; }

        [JsonProperty("set_end")]
        public string SetEnd { get; set; }

        [JsonProperty("moon_code")]
        public int MoonCode { get; set; }

        [JsonProperty("moon_text")]
        public string MoonText { get; set; }

        [JsonProperty("parts")]
        public Parts Parts { get; set; }

        [JsonProperty("hours")]
        public Fact[] Hours { get; set; }

        [JsonProperty("biomet", NullValueHandling = NullValueHandling.Ignore)]
        public Biomet Biomet { get; set; }
    }

    public partial class Biomet
    {
        [JsonProperty("index")]
        public long Index { get; set; }

        [JsonProperty("condition")]
        public string Condition { get; set; }
    }

    public partial class Parts
    {
        [JsonProperty("day")]
        public Fact Day { get; set; }

        [JsonProperty("night_short")]
        public Fact NightShort { get; set; }

        [JsonProperty("evening")]
        public Fact Evening { get; set; }

        [JsonProperty("morning")]
        public Fact Morning { get; set; }

        [JsonProperty("night")]
        public Fact Night { get; set; }

        [JsonProperty("day_short")]
        public Fact DayShort { get; set; }
    }

    public partial class GeoObject
    {
        [JsonProperty("district")]
        public Country District { get; set; }

        [JsonProperty("locality")]
        public Country Locality { get; set; }
    }

    public partial class Country
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Info
    {
        [JsonProperty("n")]
        public bool N { get; set; }

        [JsonProperty("geoid")]
        public long Geoid { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("tzinfo")]
        public Tzinfo Tzinfo { get; set; }

        [JsonProperty("def_pressure_mm")]
        public long DefPressureMm { get; set; }

        [JsonProperty("def_pressure_pa")]
        public long DefPressurePa { get; set; }

        [JsonProperty("slug")]
        public long Slug { get; set; }

        [JsonProperty("zoom")]
        public long Zoom { get; set; }

        [JsonProperty("nr")]
        public bool Nr { get; set; }

        [JsonProperty("ns")]
        public bool Ns { get; set; }

        [JsonProperty("nsr")]
        public bool Nsr { get; set; }

        [JsonProperty("p")]
        public bool P { get; set; }

        [JsonProperty("f")]
        public bool F { get; set; }

        [JsonProperty("_h")]
        public bool H { get; set; }
    }

    public partial class Tzinfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("abbr")]
        public string Abbr { get; set; }

        [JsonProperty("dst")]
        public bool Dst { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }
    }

    public partial class Yesterday
    {
        [JsonProperty("temp")]
        public long Temp { get; set; }
    }

}
