namespace PredictorModel.Training.Datasets.Tools;

internal interface IDataReader<T> where T : class
{
    List<T> ReadAllFromFile(string filePath);
}
