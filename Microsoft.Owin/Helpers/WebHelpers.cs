using Microsoft.Owin.Infrastructure;

namespace Microsoft.Owin.Helpers
{
    public static class WebHelpers
    {
        public static IFormCollection ParseForm(string text)
        {
            return OwinHelpers.GetForm(text);
        }
    }
}
