using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Owin.Mapping
{
    public class MapOptions
    {
        public Func<IDictionary<string, object>, Task> Branch { get; set; }

        public PathString PathMatch { get; set; }
    }
}
