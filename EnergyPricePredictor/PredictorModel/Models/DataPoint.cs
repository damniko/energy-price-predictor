namespace PredictorModel.Models;

public record DataPoint(
    DateTime Date,
    string Location,
    double Temperature,
    WeatherType Weather,
    int Cloudiness,
    double WindSpeed,
    decimal EnergyPriceDKK
);
