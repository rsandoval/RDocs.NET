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
        private static ContentCharacterizer _single = null;

        const string _modelFolder = @"brain";
        private static string _appPath => Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
        private static string _modelPath => Path.Combine(_modelFolder, "model.zip");

        private MLContext _mlContext;
        private PredictionEngine<SimpleDocument, TypePrediction> _predEngine;
        private List<Tuple<string, string>> _documentTypes = new List<Tuple<string, string>>();

        private ContentCharacterizer()
        {
            _mlContext = new MLContext(seed: 0);
            ITransformer loadedModel = _mlContext.Model.Load(_modelPath, out var modelInputSchema);
            _predEngine = _mlContext.Model.CreatePredictionEngine<SimpleDocument, TypePrediction>(loadedModel);

            _documentTypes = LoadTypes();
        }

        public static ContentCharacterizer GetInstance()
        {
            if (_single == null) _single = new ContentCharacterizer();
            return _single;
        }

        private List<Tuple<string, string>> LoadTypes()
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


        public string GetDocumentType(string contents)
        {
            SimpleDocument singleDoc = new SimpleDocument() { ID = "-", Contents = contents };
            var prediction = _predEngine.Predict(singleDoc);

            return prediction.Type;
        }

        public DateTime GetIssuingDate(string contents)
        {
            return new DateTime(2019, 1, 1);
        }

        public List<string> GetNames(string contents)
        {
            List<string> foundNames = new List<string>();

            foundNames.Add("RSOLVER SpA");
            foundNames.Add("R. Sandoval U.");

            return foundNames;
        }

        public string GetConcatenatedNames(List<string> names)
        {
            string concatNames = "";

            foreach (string name in names)
                concatNames += name + " ";

            return concatNames.Trim();
        }

        public string GetTypeDescription(string typeID)
        {
            foreach (Tuple<string, string> typeTuple in _documentTypes)
                if (typeTuple.Item1.Equals(typeID))
                    return typeTuple.Item2;

            return "-";
        }

        public string GetDocumentDescription(string filename, string contents)
        {
            string docCharacterization = "";
            string typeId = GetDocumentType(contents);
            string typeDesc = GetTypeDescription(typeId);
            string docDate = GetIssuingDate(contents).ToString("dd MMM yyyy");
            string docNames = GetConcatenatedNames(GetNames(contents));

            docCharacterization = "El documento \"" + filename + "\" es de tipo " + typeDesc
                + ", emitido el " + docDate + (!String.IsNullOrEmpty(docNames) ? " y menciona a " + docNames + "." : ".");

            return docCharacterization;
        }
    }
}
