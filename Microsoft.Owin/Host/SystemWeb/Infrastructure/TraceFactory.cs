namespace Microsoft.Owin.Host.SystemWeb.Infrastructure
{
    internal static class TraceFactory
    {
        public static ITrace Create(string name)
        {
            return Instance.Create(name);
        }

        public static ITraceFactory Instance { get; set; } = new DefaultTraceFactory();
    }
}
