using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Owin.Infrastructure
{
    [StructLayout(LayoutKind.Sequential), GeneratedCode("App_Packages", "")]
    internal struct HeaderSegmentCollection : IEnumerable<HeaderSegment>, IEquatable<HeaderSegmentCollection>
    {
        private readonly string[] _headers;

        public HeaderSegmentCollection(string[] headers)
        {
            _headers = headers;
        }

        public bool Equals(HeaderSegmentCollection other)
        {
            return Equals(_headers, other._headers);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return ((obj is HeaderSegmentCollection) && Equals((HeaderSegmentCollection)obj));
        }

        public override int GetHashCode()
        {
            return _headers?.GetHashCode() ?? 0;
        }

        public static bool operator ==(HeaderSegmentCollection left, HeaderSegmentCollection right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HeaderSegmentCollection left, HeaderSegmentCollection right)
        {
            return !left.Equals(right);
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(_headers);
        }

        IEnumerator<HeaderSegment> IEnumerable<HeaderSegment>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct Enumerator : IEnumerator<HeaderSegment>, IDisposable, IEnumerator
        {
            private readonly string[] _headers;
            private int _index;
            private string _header;
            private int _headerLength;
            private int _offset;
            private int _leadingStart;
            private int _leadingEnd;
            private int _valueStart;
            private int _valueEnd;
            private int _trailingStart;
            private Mode _mode;
            private static readonly string[] NoHeaders;
            public Enumerator(string[] headers)
            {
                _headers = headers ?? NoHeaders;
                _header = string.Empty;
                _headerLength = -1;
                _index = -1;
                _offset = -1;
                _leadingStart = -1;
                _leadingEnd = -1;
                _valueStart = -1;
                _valueEnd = -1;
                _trailingStart = -1;
                _mode = Mode.Leading;
            }

            public HeaderSegment Current
                =>
                    new HeaderSegment(new StringSegment(_header, _leadingStart, _leadingEnd - _leadingStart),
                        new StringSegment(_header, _valueStart, _valueEnd - _valueStart));

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (_mode == Mode.Produce)
                {
                    _leadingStart = _trailingStart;
                    _leadingEnd = -1;
                    _valueStart = -1;
                    _valueEnd = -1;
                    _trailingStart = -1;
                    if (((_offset == _headerLength) && (_leadingStart != -1)) && (_leadingStart != _offset))
                    {
                        _leadingEnd = _offset;
                        return true;
                    }
                    _mode = Mode.Leading;
                }
                if (_offset == _headerLength)
                {
                    _index++;
                    _offset = -1;
                    _leadingStart = 0;
                    _leadingEnd = -1;
                    _valueStart = -1;
                    _valueEnd = -1;
                    _trailingStart = -1;
                    if (_index == _headers.Length)
                    {
                        return false;
                    }
                    _header = _headers[_index] ?? string.Empty;
                    _headerLength = _header.Length;
                }
                Label_00F0:
                _offset++;
                var c = (_offset == _headerLength) ? '\0' : _header[_offset];
                var attr = char.IsWhiteSpace(c)
                    ? Attr.Whitespace
                    : ((c == '"') ? Attr.Quote : (((c == ',') || (c == '\0')) ? Attr.Delimiter : Attr.Value));
                switch (_mode)
                {
                    case Mode.Leading:
                        switch (attr)
                        {
                            case Attr.Value:
                                _leadingEnd = _offset;
                                _valueStart = _offset;
                                _mode = Mode.Value;
                                goto Label_02ED;

                            case Attr.Quote:
                                _leadingEnd = _offset;
                                _valueStart = _offset;
                                _mode = Mode.ValueQuoted;
                                goto Label_02ED;

                            case Attr.Delimiter:
                                _leadingEnd = _offset;
                                _mode = Mode.Produce;
                                goto Label_02ED;
                        }
                        break;

                    case Mode.Value:
                        switch (attr)
                        {
                            case Attr.Quote:
                                _mode = Mode.ValueQuoted;
                                goto Label_02ED;

                            case Attr.Delimiter:
                                _valueEnd = _offset;
                                _trailingStart = _offset;
                                _mode = Mode.Produce;
                                goto Label_02ED;

                            case Attr.Whitespace:
                                _valueEnd = _offset;
                                _trailingStart = _offset;
                                _mode = Mode.Trailing;
                                goto Label_02ED;
                        }
                        break;

                    case Mode.ValueQuoted:
                        switch (attr)
                        {
                            case Attr.Quote:
                                _mode = Mode.Value;
                                goto Label_02ED;

                            case Attr.Delimiter:
                                if (c == '\0')
                                {
                                    _valueEnd = _offset;
                                    _trailingStart = _offset;
                                    _mode = Mode.Produce;
                                }
                                goto Label_02ED;
                        }
                        break;

                    case Mode.Trailing:
                        switch (attr)
                        {
                            case Attr.Value:
                                _trailingStart = -1;
                                _valueEnd = -1;
                                _mode = Mode.Value;
                                goto Label_02ED;

                            case Attr.Quote:
                                _trailingStart = -1;
                                _valueEnd = -1;
                                _mode = Mode.ValueQuoted;
                                goto Label_02ED;

                            case Attr.Delimiter:
                                _mode = Mode.Produce;
                                goto Label_02ED;
                        }
                        break;
                }
                Label_02ED:
                if (_mode != Mode.Produce)
                {
                    goto Label_00F0;
                }
                return true;
            }

            public void Reset()
            {
                _index = 0;
                _offset = 0;
                _leadingStart = 0;
                _leadingEnd = 0;
                _valueStart = 0;
                _valueEnd = 0;
            }

            static Enumerator()
            {
                NoHeaders = new string[0];
            }
            private enum Attr
            {
                Value,
                Quote,
                Delimiter,
                Whitespace
            }

            private enum Mode
            {
                Leading,
                Value,
                ValueQuoted,
                Trailing,
                Produce
            }
        }
    }
}
