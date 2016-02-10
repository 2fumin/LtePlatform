using System.Collections.Generic;

namespace Microsoft.Owin
{
    public interface IHeaderDictionary : IReadableStringCollection, IDictionary<string, string[]>
    {
        void Append(string key, string value);

        void AppendCommaSeparatedValues(string key, params string[] values);

        void AppendValues(string key, params string[] values);

        IList<string> GetCommaSeparatedValues(string key);

        void Set(string key, string value);

        void SetCommaSeparatedValues(string key, params string[] values);

        void SetValues(string key, params string[] values);

        string this[string key] { get; set; }
    }
}
