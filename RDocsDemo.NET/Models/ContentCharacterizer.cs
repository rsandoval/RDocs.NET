using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML;

namespace RDocsDemo.NET.Models
{
    public class ContentCharacterizer
    {
        const string modelFolder = "..\\..\\..\\brain";
        private static string _appPath => Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
        private static string _modelPath => Path.Combine(@"brain", "model.zip");

        private static MLContext _mlContext;
        private static PredictionEngine<SimpleDocument, TypePrediction> _predEngine;
        private static List<Tuple<string, string>> _documentTypes => LoadTypes();

        public ContentCharacterizer()
        {
            _mlContext = new MLContext(seed: 0);
            ITransformer loadedModel = _mlContext.Model.Load(_modelPath, out var modelInputSchema);
            _predEngine = _mlContext.Model.CreatePredictionEngine<SimpleDocument, TypePrediction>(loadedModel);
        }

        public string GetDocumentType(string contents)
        {
            SimpleDocument singleDoc = new SimpleDocument() { ID = "-", Contents = contents };
            var prediction = _predEngine.Predict(singleDoc);

            return prediction.Type;
        }

        private static List<Tuple<string, string>> LoadTypes()
        {
            List<Tuple<string, string>> types = new List<Tuple<string, string>>();

            StreamReader reader = new StreamReader(Path.Combine("data", "Types.txt"));

            string line = "";
            while ((line = reader.ReadLine()) != null)
            {
                char[] separators = { '\t' };
                string[] tokens = line.Split(separators);
                types.Add(new Tuple<string, string>(tokens[0], tokens[1]));
            }

            return types;
        }

        public string GetTypeDescription(string typeID)
        {
            foreach (Tuple<string, string> typeTuple in _documentTypes)
                if (typeTuple.Item1.Equals(typeID))
                    return typeTuple.Item2;

            return "-";
        }
    }
}
