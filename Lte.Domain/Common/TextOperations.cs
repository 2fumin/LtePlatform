using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
