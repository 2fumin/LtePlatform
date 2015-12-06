using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtePlatform.Areas.TestPage.Models
{
    public class SimpleTypeClass
    {
        public int A { get; set; }

        public int B { get; set; }
    }

    public class SimpleTypeContainer
    {
        public SimpleTypeClass SimpleTypeClass { get; set; }
    }

    public class ListTypeContainer
    {
        public List<SimpleTypeClass> List { get; set; } 
    }
}
