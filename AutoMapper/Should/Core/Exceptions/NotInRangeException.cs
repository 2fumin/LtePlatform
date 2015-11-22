namespace AutoMapper.Should.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when a value is unexpectedly in the given range.
    /// </summary>
    public class NotInRangeException : AssertException
    {
        /// <summary>
        /// Creates a new instance of the <see cref="NotInRangeException"/> class.
        /// </summary>
        /// <param name="actual">The actual object value</param>
        /// <param name="low">The low value of the range</param>
        /// <param name="high">The high value of the range</param>
        public NotInRangeException(object actual,
                                   object low,
                                   object high)
            : base("CustomizeAssert.NotInRange() Failure")
        {
            this.Low = low?.ToString();
            this.High = high?.ToString();
            this.Actual = actual?.ToString();
        }

        /// <summary>
        /// Gets the actual object value
        /// </summary>
        public string Actual { get; }

        /// <summary>
        /// Gets the high value of the range
        /// </summary>
        public string High { get; }

        /// <summary>
        /// Gets the low value of the range
        /// </summary>
        public string Low { get; }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        /// <returns>The error message that explains the reason for the exception, or an empty string("").</returns>
        public override string Message => $"{base.Message}\r\nRange:  ({Low} - {High})\r\nActual: {Actual ?? "(null)"}";
    }
}