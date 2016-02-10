using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin
{
    using System;
    using System.Collections.Generic;

    public interface IAppBuilder
    {
        object Build(Type returnType);

        IAppBuilder New();

        IAppBuilder Use(object middleware, params object[] args);

        IDictionary<string, object> Properties { get; }
    }

}
