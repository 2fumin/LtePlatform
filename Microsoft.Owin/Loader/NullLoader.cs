using System;
using System.Collections.Generic;

namespace Microsoft.Owin.Loader
{
    internal class NullLoader
    {
        private static readonly NullLoader Singleton = new NullLoader();

        public Action<IAppBuilder> Load(string startup, IList<string> errors)
        {
            return null;
        }

        public static Func<string, IList<string>, Action<IAppBuilder>> Instance => Singleton.Load;
    }
}
