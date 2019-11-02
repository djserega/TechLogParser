using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechLogParser.Model
{
    public class FileLog
    {
        public FileLog(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            Fullname = fileInfo.FullName;
            Name = fileInfo.Name.Replace(fileInfo.Extension, "");
        }

        public string Name { get; set; }
        public string Fullname { get; set; }
        public List<DataLog> DataLogs { get; set; } = new List<DataLog>();
        public bool DataLogsIsEmpty { get => DataLogs.Count == 0; }


        internal void ParseTextFileLog()
        {
            string textFile = GetTextFile();

            if (string.IsNullOrWhiteSpace(textFile))
                return;

            FillListDataLog(textFile);
        }

        private string GetTextFile()
        {
            string textFile = string.Empty;
            using (StreamReader reader = new FileInfo(Fullname).OpenText())
            {
                textFile = reader.ReadToEnd();
            }

            return textFile;
        }

        private void FillListDataLog(string text)
        {
            string[] lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                DataLog dataLog = new DataLog(Name, line);
                DataLogs.Add(dataLog);
            }
        }
    }
}
