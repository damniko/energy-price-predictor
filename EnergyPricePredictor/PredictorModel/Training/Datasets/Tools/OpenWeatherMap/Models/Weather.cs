namespace PredictorModel.Training.Datasets.Tools.OpenWeatherMap.Models;

internal record Weather(
    int Id,
    string Main,
    string Description,
    string Icon
);
