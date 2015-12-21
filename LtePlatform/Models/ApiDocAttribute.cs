using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtePlatform.Models
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ApiDocAttribute : Attribute
    {
        public ApiDocAttribute(string doc)
        {
            Documentation = doc;
        }
        public string Documentation { get; }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class ApiResponseAttribute : Attribute
    {
        public ApiResponseAttribute(string doc)
        {
            Documentation = doc;
        }
        public string Documentation { get; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ApiControlAttribute : Attribute
    {
        public ApiControlAttribute(string doc)
        {
            Documentation = doc;
        }
        public string Documentation { get; }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ApiParameterDocAttribute : Attribute
    {
        public ApiParameterDocAttribute(string param, string doc)
        {
            Parameter = param;
            Documentation = doc;
        }
        public string Parameter { get; }
        public string Documentation { get; }
    }
}
