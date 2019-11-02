using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechLogParser.Model
{
    public class DirectoryLog
    {
        public DirectoryLog(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            FullName = dirInfo.FullName;
            Name = dirInfo.Name;
        }

        public string Name { get; set; }
        public string FullName { get; set; }
        public List<FileLog> FileLogs { get; set; } = new List<FileLog>();
        public bool FileLogsIsEmpty { get => FileLogs.Count == 0; }
    }
}
