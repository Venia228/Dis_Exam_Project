using ExamApi.Management;
using System;

namespace ExamApi.Main
{
    public class StudentData : FileManager
    {
        #region Override Properties
        public override string fileName { get; set; } = "New Student";
        public override string filePath => "Logs/";
        public override string fileExtension => "txt";

        public override void SaveFile()
        {
            content = 
                $"Ф.И.О ученика: {Name}\n" +
                $"Класс: {ClassName}\n" +
                $"Экзамен: {exam.fileName}\n" +
                $"Оценка: {Mark}\n" +
                $"Правильных ответов: {rightAnwsersCount}\n" +
                $"Время выполнения: {wastedTimeOnExam} \n" +
                $"Дата выполнения: {DateTime.Now}";

            base.SaveFile();
        }
        #endregion

        public string Name { get; set; }
        public string ClassName { get; set; }
        public ExamData exam { get; set; }
        public sbyte Mark { get; set; }
        public int rightAnwsersCount { get; set; }
        public string wastedTimeOnExam { get; set; }
    }
}
