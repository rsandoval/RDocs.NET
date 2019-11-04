using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ML.Data;

namespace RDocsCoreConsole
{
    public class Document
    {
        private string _contents = "";

        [LoadColumn(0)]
        public string ID { get; set; }
        [LoadColumn(1)]
        public string Contents { get { return _contents; } set { _contents = value.ToUpper(); } }
        [LoadColumn(2)]
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
