using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Perform
{
    public static class Helper
    {
        public static IEnumerable<string[]> ReadCsv(string path) {
            var rgex = new Regex("(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)");
            using (var file = new StreamReader(path))
            {
                var line = string.Empty;
                while ((line = file.ReadLine()) != null)
                {
                    var matches = rgex.Matches(line);
                    var cells = new string[matches.Count];
                    for (int i = 0; i < matches.Count; i++)
                    {
                        if (string.IsNullOrEmpty(matches[i].Value) || matches[i].Value == ",") {
                            cells[i] = string.Empty;
                            continue;
                        }

                        cells[i] = matches[i].Value[0] != ',' ? matches[i].Value : matches[i].Value.Substring(1);
                        cells[i] = cells[i][0] != '"' ? cells[i] : cells[i].Substring(1, cells[i].Length - 2).Replace("\"\"", "\"");
                    }
                    
                    yield return cells;
                }
            }
        }

        static readonly Regex mustWrap = new Regex("[\",]");
        public static string FormatCVSstring(string cell)
        {
            if (!mustWrap.IsMatch(cell))
                return cell;
            
            return '"' + cell.Replace("\"", "\"\"") + '"' ;
        }
    }
}