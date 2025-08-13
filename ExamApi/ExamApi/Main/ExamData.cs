using ExamApi.Management;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApi.Main
{
    public struct Interval
    {
        public int _from;
        public int _to;

        public Interval(int _from, int _to) : this()
        {
            this._from = _from;
            this._to = _to;
        }
    }
    public class ExamData : FileManager
    {
        #region Override Properties
        public override string fileName { get; set; } = "ExamFile";
        public override string filePath => "Exams/";
        public override string fileExtension => "json";

        public override void SaveFile()
        {
            content = JsonConvert.SerializeObject(this);

            base.SaveFile();
        }
        #endregion

        public bool useRandomValues { get; set; }
        public string[] questions { get; set; }
        public string[] anwsersOrFormulas { get; set; }
        public int availableQuestions { get; set; }
        public int timeToSolve { get; set; }
        public Interval[][] intervals { get; set; }
    }
}
