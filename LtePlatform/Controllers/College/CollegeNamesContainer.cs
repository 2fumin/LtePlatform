using System.Collections.Generic;
using Lte.Domain.Regular.Attributes;

namespace LtePlatform.Controllers.College
{
    [TypeDoc("校园名称容器")]
    public class CollegeNamesContainer
    {
        [MemberDoc("校园名称列表")]
        public IEnumerable<string> Names { get; set; } 
    }
}