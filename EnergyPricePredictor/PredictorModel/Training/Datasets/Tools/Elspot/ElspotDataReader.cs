using System.Text.Json;
using PredictorModel.Training.Datasets.Tools.Elspot.Models;

namespace PredictorModel.Training.Datasets.Tools.Elspot;

internal class ElspotDataReader : IDataReader<ElspotData>
{
    public List<ElspotData> ReadAllFromFile(string filePath)
    {
        using var reader = new StreamReader(File.OpenRead(filePath));
        var result = JsonSerializer.Deserialize<List<ElspotData>>(reader.BaseStream);

        return result ?? new List<ElspotData>();
    }
}
