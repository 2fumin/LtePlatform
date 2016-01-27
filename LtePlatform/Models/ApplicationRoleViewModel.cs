using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular.Attributes;

namespace LtePlatform.Models
{
    [TypeDoc("角色视图模版")]
    public class ApplicationRoleViewModel
    {
        [MemberDoc("角色名称")]
        public string Name { get; set; }

        [MemberDoc("角色编号")]
        public string RoleId { get; set; }
    }
}
