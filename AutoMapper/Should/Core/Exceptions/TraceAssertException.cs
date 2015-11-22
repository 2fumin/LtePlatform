using System;

namespace AutoMapper.Should.Core.Exceptions
{
    /// <summary>
    /// Exception that is thrown when a call to Debug.CustomizeAssert() fails.
    /// </summary>
    public class TraceAssertException : AssertException
    {
        /// <summary>
        /// Creates a new instance of the <see cref="TraceAssertException"/> class.
        /// </summary>
        /// <param name="assertMessage">The original assert message</param>
        /// <param name="assertDetailedMessage">The original assert detailed message</param>
        public TraceAssertException(string assertMessage,
                                    string assertDetailedMessage = "")
        {
            this.AssertMessage = assertMessage ?? "";
            this.AssertDetailedMessage = assertDetailedMessage ?? "";
        }

        /// <summary>
        /// Gets the original assert detailed message.
        /// </summary>
        public string AssertDetailedMessage { get; }

        /// <summary>
        /// Gets the original assert message.
        /// </summary>
        public string AssertMessage { get; }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        public override string Message
        {
            get
            {
                var result = "Debug.CustomizeAssert() Failure";

                if (AssertMessage == "") return result;
                result += " : " + AssertMessage;

                if (AssertDetailedMessage != "")
                    result += Environment.NewLine + "Detailed Message:" + Environment.NewLine + AssertDetailedMessage;

                return result;
            }
        }
    }
}