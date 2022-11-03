namespace PredictorModel.Training.Datasets.Tools.Elspot.Models;

internal record ElspotData(
    DateTime HourUTC,
    DateTime HourDK,
    string PriceArea,
    decimal SpotPriceDKK,
    decimal SpotPriceEUR
);
