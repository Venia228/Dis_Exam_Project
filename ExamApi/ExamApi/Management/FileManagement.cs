using System.IO;


namespace ExamApi.Management
{
    public partial class FileManager
    {
        protected string content { get; set; }

        public virtual void SaveFile()
        {
            File.WriteAllText($"{filePath}{fileName}.{fileExtension}", content);
        }
        public virtual object LoadFile()
        {
            return File.ReadAllText($"{filePath}{fileName}.{fileExtension}");
        }
    }
}
