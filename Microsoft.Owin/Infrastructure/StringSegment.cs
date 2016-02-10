using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;

namespace Microsoft.Owin.Infrastructure
{
    [StructLayout(LayoutKind.Sequential), GeneratedCode("App_Packages", "")]
    internal struct StringSegment : IEquatable<StringSegment>
    {
        public StringSegment(string buffer, int offset, int count)
        {
            Buffer = buffer;
            Offset = offset;
            Count = count;
        }

        public string Buffer { get; }

        public int Offset { get; }

        public int Count { get; }

        public string Value => Offset != -1 ? Buffer.Substring(Offset, Count) : null;

        public bool HasValue => (((Offset != -1) && (Count != 0)) && (Buffer != null));

        public bool Equals(StringSegment other)
        {
            return ((string.Equals(Buffer, other.Buffer) && (Offset == other.Offset)) && (Count == other.Count));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return ((obj is StringSegment) && Equals((StringSegment)obj));
        }

        public override int GetHashCode()
        {
            var num = Buffer?.GetHashCode() ?? 0;
            num = (num * 0x18d) ^ Offset;
            return ((num * 0x18d) ^ Count);
        }

        public static bool operator ==(StringSegment left, StringSegment right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(StringSegment left, StringSegment right)
        {
            return !left.Equals(right);
        }

        public bool StartsWith(string text, StringComparison comparisonType)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            var length = text.Length;
            return ((HasValue && (Count >= length)) && (string.Compare(Buffer, Offset, text, 0, length, comparisonType) == 0));
        }

        public bool EndsWith(string text, StringComparison comparisonType)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            var length = text.Length;
            return ((HasValue && (Count >= length)) && (string.Compare(Buffer, (Offset + Count) - length, text, 0, length, comparisonType) == 0));
        }

        public bool Equals(string text, StringComparison comparisonType)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            var length = text.Length;
            return ((HasValue && (Count == length)) && (string.Compare(Buffer, Offset, text, 0, length, comparisonType) == 0));
        }

        public string Substring(int offset, int length)
        {
            return Buffer.Substring(Offset + offset, length);
        }

        public StringSegment Subsegment(int offset, int length)
        {
            return new StringSegment(Buffer, Offset + offset, length);
        }

        public override string ToString()
        {
            return (Value ?? string.Empty);
        }
    }
}
