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
            Text = text;
        }

        public DateTime Date { get; set; }
        public string Text { get; set; }
    }
}
