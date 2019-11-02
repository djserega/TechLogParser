using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechLogParser.Model
{
    public class DataLog
    {
        public DataLog(string name, string text)
        {
            DateTime.TryParseExact(
                name + text.Substring(0, text.IndexOf('-') - 7),
                "yyMMddHHmm:ss",
                null,
                new System.Globalization.DateTimeStyles(),
                out DateTime date);

            Date = date;
            FullText = text;

            ProcessName = FindParseText("p:processName");
            ComputerName = FindParseText("t:computerName");
            User = FindParseText("Usr");
        }

        private string FindParseText(string parameter)
        {
            string value = string.Empty;

            int indexStartParameter = FullText.IndexOf(parameter);
            if (indexStartParameter > 0)
            {
                indexStartParameter += parameter.Length + 1;
                int indexEndParameter = FullText.IndexOf(",", indexStartParameter);
                value = FullText.Substring(indexStartParameter, indexEndParameter - indexStartParameter);
            }

            return value;
        }

        public DateTime Date { get; set; }
        public string FullText { get; set; }
        public string ProcessName { get; set; }
        public string ComputerName { get; set; }
        public string User { get; set; }
    }
}
