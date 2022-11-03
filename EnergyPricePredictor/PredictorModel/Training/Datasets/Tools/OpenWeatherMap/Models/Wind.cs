namespace PredictorModel.Training.Datasets.Tools.OpenWeatherMap.Models;

internal record Wind(
    double Speed,
    double Deg,
    double? Gust
);
