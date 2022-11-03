using Microsoft.Extensions.Configuration;
using PredictorModel.Models;
using PredictorModel.Training.Datasets.Tools.Elspot;
using PredictorModel.Training.Datasets.Tools.Elspot.Models;
using PredictorModel.Training.Datasets.Tools.OpenWeatherMap;
using PredictorModel.Training.Datasets.Tools.OpenWeatherMap.Models;

namespace PredictorModel.Training.Datasets.Tools;

internal class DataProcessor
{
    private readonly string _basePath;
    private readonly string _elspotDataFile;
    private readonly string _openWeatherMapDataFile;


    public DataProcessor(IConfiguration configuration)
    {
        _basePath = configuration["Training:UnprocessedBasePath"]!;
        _elspotDataFile = Path.Join(_basePath, configuration["Training:ElspotDataFile"]!);
        _openWeatherMapDataFile = Path.Join(_basePath, configuration["Training:OpenWeatherMapDataFile"]!);
    }

    public List<DataPoint> GetProcessedDataPoints()
    {
        var elspotProcessor = new ElspotDataReader();
        var weatherProcessor = new OpenWeatherMapDataReader();

        Dictionary<DateTime, ElspotData> elspotData = elspotProcessor.ReadAllFromFile(_elspotDataFile)
            .ToDictionary(x => x.HourUTC);
        Dictionary<DateTime, OpenWeatherMapData> weatherData = weatherProcessor.ReadAllFromFile(_openWeatherMapDataFile)
            .ToDictionary(x => x.DtIso);

        var result = new List<DataPoint>();

        foreach (var elspotEntry in elspotData)
        {
            var weatherEntry = weatherData[elspotEntry.Key];

            result.Add(new DataPoint(
                Date: elspotEntry.Key,
                Location: weatherEntry.CityName,
                Temperature: weatherEntry.Main.Temp,
                Weather: IntToWeatherType(weatherEntry.Weather[0].Id),
                Cloudiness: weatherEntry.Clouds.All,
                WindSpeed: weatherEntry.Wind.Speed,
                EnergyPriceDKK: elspotEntry.Value.SpotPriceDKK
            ));
        }
        return result;
    }

    private static WeatherType IntToWeatherType(int code)
    {
        switch (code)
        {
            case 200: // thunderstorm with light rain
            case 201: // thunderstorm with rain
            case 202: // thunderstorm with heavy rain
            case 210: // light thunderstorm
            case 211: // thunderstorm
            case 212: // heavy thunderstorm
            case 221: // ragged thunderstorm
            case 230: // thunderstorm with light drizzle
            case 231: // thunderstorm with drizzle
            case 232: // thunderstorm with heavy drizzle
                return WeatherType.Thunderstorm;
            case 300: // light intensity drizzle
            case 301: // drizzle
            case 302: // heavy intensity drizzle
            case 310: // light intensity drizzle rain
            case 311: // drizzle rain
            case 312: // heavy intensity drizzle rain
            case 313: // shower rain and drizzle
            case 314: // heavy shower rain and drizzle
            case 321: // shower drizzle
                return WeatherType.Drizzle;
            case 500: // light rain
            case 501: // moderate rain
            case 502: // heavy intensity rain
            case 503: // very heavy rain
            case 504: // extreme rain
            case 511: // freezing rain
            case 520: // light intensity shower rain
            case 521: // shower rain
            case 522: // heavy intensity shower rain
            case 531: // ragged shower rain
                return WeatherType.Rain;
            case 600: // light snow
            case 601: // Snow
            case 602: // Heavy snow
            case 611: // Sleet
            case 612: // Light shower sleet
            case 613: // Shower sleet
            case 615: // Light rain and snow
            case 616: // Rain and snow
            case 620: // Light shower snow
            case 621: // Shower snow
            case 622: // Heavy shower snow
                return WeatherType.Snow;
            case 701:
                return WeatherType.Mist;
            case 711:
                return WeatherType.Smoke;
            case 721:
                return WeatherType.Haze;
            case 731:
            case 761:
                return WeatherType.Dust;
            case 741:
                return WeatherType.Fog;
            case 751:
                return WeatherType.Sand;
            case 762:
                return WeatherType.Ash;
            case 771:
                return WeatherType.Squall;
            case 781:
                return WeatherType.Tornado;
            case 800:
                return WeatherType.Clear;
            case 801:
                return WeatherType.FewClouds;
            case 802:
                return WeatherType.ScatteredClouds;
            case 803:
                return WeatherType.BrokenClouds;
            case 804:
                return WeatherType.OvercastClouds;
            default: throw new ArgumentException("Code has no mapping", nameof(code));
        }
    }
}
