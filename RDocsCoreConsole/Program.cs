using System;
using System.IO;
using System.Linq;
using Microsoft.ML;

namespace RDocsCoreConsole
{
    class Program
    {
        private static string _appPath => Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
        private static string _trainDataPath => Path.Combine(_appPath, "C:\tmp", "RDox-Samples.tsv");
        private static string _trainTypeDataPath => Path.Combine(_appPath, "C:\tmp", "RDox-Samples-Types.tsv");
        private static string _testDataPath => Path.Combine(_appPath, "C:\tmp", "issues_test.tsv");
        private static string _modelPath => Path.Combine(_appPath, "C:\tmp", "model.zip");

        private static MLContext _mlContext;
        private static PredictionEngine<Document, TypePrediction> _predEngine;
        private static ITransformer _trainedModel;
        static IDataView _trainingDataView;
        static IDataView _typesDataView;

        static void Main(string[] args)
        {
            _mlContext = new MLContext(seed: 0);

            _trainingDataView = _mlContext.Data.LoadFromTextFile<Document>(_trainDataPath, hasHeader: true);
            _typesDataView = _mlContext.Data.LoadFromTextFile<Document>(_trainTypeDataPath, hasHeader: true);

            Console.WriteLine("All set!");
        }
    }
}
