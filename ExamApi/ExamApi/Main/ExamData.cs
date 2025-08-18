using ExamApi.Management;
using Newtonsoft.Json;

namespace ExamApi.Main
{
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
        public override object LoadFile()
        {
            content = (string)base.LoadFile();
            ExamData data = JsonConvert.DeserializeObject<ExamData>(content);

            return data;
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
