namespace AutoMapper.Should.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when a collection unexpectedly contains the expected value.
    /// </summary>
    public class DoesNotContainException : AssertException
    {
        /// <summary>
        /// Creates a new instance of the <see cref="DoesNotContainException"/> class.
        /// </summary>
        /// <param name="expected">The expected object value</param>
        public DoesNotContainException(object expected)
            : base($"Assert.DoesNotContain() failure: Found: {expected}") { }
    }
}