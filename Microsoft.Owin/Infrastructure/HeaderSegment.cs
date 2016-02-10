using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;

namespace Microsoft.Owin.Infrastructure
{
    [StructLayout(LayoutKind.Sequential), GeneratedCode("App_Packages", "")]
    internal struct HeaderSegment : IEquatable<HeaderSegment>
    {
        public HeaderSegment(StringSegment formatting, StringSegment data)
        {
            Formatting = formatting;
            Data = data;
        }

        public StringSegment Formatting { get; }

        public StringSegment Data { get; }

        public bool Equals(HeaderSegment other)
        {
            return (Formatting.Equals(other.Formatting) && Data.Equals(other.Data));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return ((obj is HeaderSegment) && Equals((HeaderSegment)obj));
        }

        public override int GetHashCode()
        {
            return ((Formatting.GetHashCode() * 0x18d) ^ Data.GetHashCode());
        }

        public static bool operator ==(HeaderSegment left, HeaderSegment right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HeaderSegment left, HeaderSegment right)
        {
            return !left.Equals(right);
        }
    }
}
