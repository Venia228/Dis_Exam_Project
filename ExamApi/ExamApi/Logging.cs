using System.IO;

namespace ExamApi
{
    public class Logging
    {
        public const string loggingPath = "Logs/";
        private string logName = "New Log";
        private string content;

        public Logging(string logName, string content)
        {
            this.logName = logName;
            this.content = content;
        }

        public void SaveLog()
        {
            File.WriteAllText($"{loggingPath}{logName}.txt", content);
        }
        public string GetLogName()
        {
            return logName;
        }
    }
}
