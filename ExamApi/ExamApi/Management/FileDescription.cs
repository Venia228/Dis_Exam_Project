using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApi.Management
{
    abstract partial class FileManager
    {
        public abstract string fileName { get; set; }
        public abstract string filePath { get; }
        public abstract string fileExtension { get; }
    }
}
