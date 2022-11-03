namespace PredictorModel.Training.Datasets.Tools.OpenWeatherMap.Models;

internal record Main(
    double Temp,
    double TempMin,
    double TempMax,
    double FeelsLike,
    int Pressure,
    int? SeaLevel,
    int? GrndLevel,
    int Humidity,
    double DewPoint
);
