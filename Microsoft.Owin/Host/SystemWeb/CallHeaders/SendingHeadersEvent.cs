using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Owin.Properties;

namespace Microsoft.Owin.Host.SystemWeb.CallHeaders
{
    internal class SendingHeadersEvent
    {
        private IList<Tuple<Action<object>, object>> _callbacks = new List<Tuple<Action<object>, object>>();

        internal void Fire()
        {
            var list = Interlocked.Exchange(ref _callbacks, null);
            if (list == null) return;
            var count = list.Count;
            for (var i = 0; i != count; i++)
            {
                var tuple = list[(count - i) - 1];
                tuple.Item1(tuple.Item2);
            }
        }

        internal void Register(Action<object> callback, object state)
        {
            if (_callbacks == null)
            {
                throw new InvalidOperationException(Resources.Exception_CannotRegisterAfterHeadersSent);
            }
            _callbacks.Add(new Tuple<Action<object>, object>(callback, state));
        }
    }
}
