using System;
using System.IO;
using System.Text;
using Lte.Domain.Lz4Net.Core;

namespace ZipLib.Zip
{
    public class ZipNameTransform : INameTransform
    {
        private static readonly char[] InvalidEntryChars;
        private static readonly char[] InvalidEntryCharsRelaxed;
        private string _trimPrefix;

        static ZipNameTransform()
        {
            char[] invalidPathChars = Path.GetInvalidPathChars();
            int num = invalidPathChars.Length + 2;
            InvalidEntryCharsRelaxed = new char[num];
            Array.Copy(invalidPathChars, 0, InvalidEntryCharsRelaxed, 0, invalidPathChars.Length);
            InvalidEntryCharsRelaxed[num - 1] = '*';
            InvalidEntryCharsRelaxed[num - 2] = '?';
            num = invalidPathChars.Length + 4;
            InvalidEntryChars = new char[num];
            Array.Copy(invalidPathChars, 0, InvalidEntryChars, 0, invalidPathChars.Length);
            InvalidEntryChars[num - 1] = ':';
            InvalidEntryChars[num - 2] = '\\';
            InvalidEntryChars[num - 3] = '*';
            InvalidEntryChars[num - 4] = '?';
        }

        public ZipNameTransform()
        {
        }

        public ZipNameTransform(string trimPrefix)
        {
            TrimPrefix = trimPrefix;
        }

        public static bool IsValidName(string name)
        {
            return (((name != null) && (name.IndexOfAny(InvalidEntryChars) < 0)) && (name.IndexOf('/') != 0));
        }

        public static bool IsValidName(string name, bool relaxed)
        {
            bool flag = name != null;
            if (!flag)
            {
                return false;
            }
            if (relaxed)
            {
                return (name.IndexOfAny(InvalidEntryCharsRelaxed) < 0);
            }
            return ((name.IndexOfAny(InvalidEntryChars) < 0) && (name.IndexOf('/') != 0));
        }

        private static string MakeValidName(string name, char replacement)
        {
            int num = name.IndexOfAny(InvalidEntryChars);
            if (num >= 0)
            {
                StringBuilder builder = new StringBuilder(name);
                while (num >= 0)
                {
                    builder[num] = replacement;
                    if (num >= name.Length)
                    {
                        num = -1;
                    }
                    else
                    {
                        num = name.IndexOfAny(InvalidEntryChars, num + 1);
                    }
                }
                name = builder.ToString();
            }
            if (name.Length > 0xffff)
            {
                throw new PathTooLongException();
            }
            return name;
        }

        public string TransformDirectory(string name)
        {
            name = TransformFile(name);
            if (name.Length <= 0)
            {
                throw new ZipException("Cannot have an empty directory name");
            }
            if (!name.EndsWith("/"))
            {
                name = name + "/";
            }
            return name;
        }

        public string TransformFile(string name)
        {
            if (name != null)
            {
                string str = name.ToLower();
                if ((_trimPrefix != null) && (str.IndexOf(_trimPrefix, StringComparison.Ordinal) == 0))
                {
                    name = name.Substring(_trimPrefix.Length);
                }
                name = name.Replace(@"\", "/");
                name = WindowsPathUtils.DropPathRoot(name);
                while ((name.Length > 0) && (name[0] == '/'))
                {
                    name = name.Remove(0, 1);
                }
                while ((name.Length > 0) && (name[name.Length - 1] == '/'))
                {
                    name = name.Remove(name.Length - 1, 1);
                }
                for (int i = name.IndexOf("//", StringComparison.Ordinal); i >= 0; i = name.IndexOf("//", 
                    StringComparison.Ordinal))
                {
                    name = name.Remove(i, 1);
                }
                name = MakeValidName(name, '_');
                return name;
            }
            name = string.Empty;
            return name;
        }

        public string TrimPrefix
        {
            get
            {
                return _trimPrefix;
            }
            set
            {
                _trimPrefix = value;
                _trimPrefix = _trimPrefix?.ToLower();
            }
        }
    }
}
