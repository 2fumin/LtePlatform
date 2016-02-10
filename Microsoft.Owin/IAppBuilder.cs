using System;
using System.Collections.Generic;

namespace Microsoft.Owin
{
    public interface IAppBuilder
    {
        object Build(Type returnType);

        IAppBuilder New();

        IAppBuilder Use(object middleware, params object[] args);

        IDictionary<string, object> Properties { get; }
    }

}
