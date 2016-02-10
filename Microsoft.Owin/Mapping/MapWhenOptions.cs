using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Owin.Mapping
{
    public class MapWhenOptions
    {
        public Func<IDictionary<string, object>, Task> Branch { get; set; }

        public Func<IOwinContext, bool> Predicate { get; set; }

        public Func<IOwinContext, Task<bool>> PredicateAsync { get; set; }
    }
}
