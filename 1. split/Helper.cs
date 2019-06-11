using System.Text.RegularExpressions;

namespace Split
{
    public static class Helper
    {
        static readonly Regex isNumber = new Regex("^[a-fA-F0-9]+$");
        public static bool IsNumber(string data)
        {
            return isNumber.IsMatch(data);
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