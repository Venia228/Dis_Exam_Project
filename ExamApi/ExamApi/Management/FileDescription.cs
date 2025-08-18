namespace ExamApi.Management
{
    abstract partial class FileManager
    {
        public abstract string fileName { get; set; }
        public abstract string filePath { get; }
        public abstract string fileExtension { get; }
    }
}
