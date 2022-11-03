using Microsoft.Extensions.Configuration;
using PredictorModel.Training;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()
    .Build();


TrainingHandler.Handle(config);
