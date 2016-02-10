namespace Microsoft.Owin.Logging
{
    public interface ILoggerFactory
    {
        ILogger Create(string name);
    }
}
