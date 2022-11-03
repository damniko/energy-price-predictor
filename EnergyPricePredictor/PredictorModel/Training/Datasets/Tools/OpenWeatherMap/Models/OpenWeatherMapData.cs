namespace PredictorModel.Training.Datasets.Tools.OpenWeatherMap.Models;

internal record OpenWeatherMapData(
    long Dt,
    DateTime DtIso,
    int Timezone,
    string CityName,
    double Lat,
    double Lon,
    Main Main,
    Cloud Clouds,
    List<Weather> Weather,
    Wind Wind
);
