using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Owin.Host.SystemWeb.CallEnvironment
{
    internal class TraceTextWriter : TextWriter
    {
        internal static readonly TraceTextWriter Instance = new TraceTextWriter();

        public TraceTextWriter() : base(CultureInfo.InvariantCulture)
        {
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern void OutputDebugString(string message);

        public override void Write(char value)
        {
            Write(value.ToString());
        }

        public override void Write(string value)
        {
            if (Debugger.IsLogging())
            {
                Debugger.Log(0, null, value);
            }
            else
            {
                OutputDebugString(value ?? string.Empty);
            }
        }

        public override void Write(char[] buffer, int index, int count)
        {
            Write(new string(buffer, index, count));
        }

        public override void WriteLine(string value)
        {
            Write(value + Environment.NewLine);
        }

        public override Encoding Encoding => Encoding.Default;
    }
}
