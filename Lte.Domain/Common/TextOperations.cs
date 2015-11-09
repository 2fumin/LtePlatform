using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lte.Domain.Common
{
    public static class TextOperations
    {
        public static string[] GetSplittedFields(this string line)
        {
            return line.Split(new[] { '=', ',', '\"', ';' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] GetSplittedFields(this string line, char splitter)
        {
            return line.Split(new[] { splitter }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] GetSplittedFields(this string line, string splitter)
        {
            return line.Split(new[] { splitter }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static bool IsLegalIp(this string ip)
        {
            const string regexText = "^((?:(?:25[0-5]|2[0-4]\\d|((1\\d{2})|([1-9]?\\d)))\\.){3}" +
                                     "(?:25[0-5]|2[0-4]\\d|((1\\d{2})|([1-9]?\\d))))$";

            return Regex.IsMatch(ip, regexText);
        }

    }
}
