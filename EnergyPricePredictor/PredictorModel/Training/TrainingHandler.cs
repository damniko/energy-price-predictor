using Microsoft.Extensions.Configuration;
using PredictorModel.Models;
using PredictorModel.Training.Datasets.Tools;

namespace PredictorModel.Training;

public class TrainingHandler
{
    public static void Handle(IConfiguration configuration)
    {
        var x = new DataProcessor(configuration);
        List<DataPoint> data = x.GetProcessedDataPoints();
        Console.WriteLine(data.Count);
    }
}
