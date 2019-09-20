using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ML.Data;

namespace RDocsCoreConsole
{
    public class Document
    {
        [LoadColumn(0)]
        public string ID { get; set; }
        [LoadColumn(1)]
        public string Contents { get; set; }
        [LoadColumn(2)]
        public int Suptertype { get; set; }
        [LoadColumn(3)]
        public string Filename { get; set; }
        [LoadColumn(4)]
        public string CalculatedSupertype { get; set; }

        public string Type { get; set; }

    }

    public class DocumentType
    {
        [LoadColumn(0)]
        public string ID { get; set; }
        [LoadColumn(1)]
        public string DocumentID { get; set; }
        [LoadColumn(2)]
        public int TypeID { get; set; }
    }




    public class TypePrediction
    {
        [ColumnName("PredictedLabel")]
        public string Type;
    }

}
