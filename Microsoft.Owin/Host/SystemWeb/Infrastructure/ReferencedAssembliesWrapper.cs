using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

namespace Microsoft.Owin.Host.SystemWeb.Infrastructure
{
    internal class ReferencedAssembliesWrapper : IEnumerable<Assembly>
    {
        public IEnumerator<Assembly> GetEnumerator()
        {
            return BuildManager.GetReferencedAssemblies().Cast<Assembly>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
