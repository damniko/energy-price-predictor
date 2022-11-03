using System.Text.Json;
using PredictorModel.Training.Datasets.Tools.OpenWeatherMap.Models;
using PredictorModel.Training.Datasets.Tools.Serialization;

namespace PredictorModel.Training.Datasets.Tools.OpenWeatherMap;

internal class OpenWeatherMapDataReader : IDataReader<OpenWeatherMapData>
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
        PropertyNameCaseInsensitive = true,
        Converters =
        {
            new DateTimeJsonConverter()
        }
    };

    public List<OpenWeatherMapData> ReadAllFromFile(string filePath)
    {
        using var reader = new StreamReader(File.OpenRead(filePath));
        var result = JsonSerializer.Deserialize<List<OpenWeatherMapData>>(reader.BaseStream, _jsonSerializerOptions);
        return result ?? new List<OpenWeatherMapData>();
    }
}
