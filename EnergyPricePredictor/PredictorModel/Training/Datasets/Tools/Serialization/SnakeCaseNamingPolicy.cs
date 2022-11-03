using System.Text.Json;

namespace PredictorModel.Training.Datasets.Tools.Serialization;

internal class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        return string.Concat(name.Select((x, i) => i > 0 && (char.IsUpper(x) || char.IsDigit(x)) ? "_" + x : x.ToString())).ToLower();
    }
}
