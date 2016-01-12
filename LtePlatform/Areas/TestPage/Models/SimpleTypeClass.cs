using System.Collections.Generic;
using Lte.Domain.Regular.Attributes;

namespace LtePlatform.Areas.TestPage.Models
{
    [TypeDoc("简单类（用于测试）")]
    public class SimpleTypeClass
    {
        [MemberDoc("A")]
        public int A { get; set; }

        [MemberDoc("B")]
        public int B { get; set; }
    }

    [TypeDoc("简单类容器（用于测试）")]
    public class SimpleTypeContainer
    {
        [MemberDoc("简单类（用于测试）")]
        public SimpleTypeClass SimpleTypeClass { get; set; }
    }

    [TypeDoc("列表类容器（用于测试）")]
    public class ListTypeContainer
    {
        [MemberDoc("简单类列表（用于测试）")]
        public List<SimpleTypeClass> List { get; set; } 
    }
}
