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

        public static string[] GetSplittedFields(this string line, char[] splitters)
        {
            return line.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
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

        public static string GetSubStringInFirstPairOfChars(this string line, char first, char second)
        {
            int index1 = line.IndexOf(first);
            int index2 = line.IndexOf(second);
            if (index2 == -1) { index2 = line.Length; }
            string ipData = line.Substring(index1 + 1, index2 - index1 - 1);
            return ipData;
        }

        public static string GetSubStringInFirstBracket(this string line)
        {
            return line.GetSubStringInFirstPairOfChars('(', ')');
        }

    }
}
