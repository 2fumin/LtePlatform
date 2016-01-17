using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtePlatform.Models
{
    public class RoleUsersDto
    {
        public string RoleName { get; set; }

        public IEnumerable<string> UserNames { get; set; }
    }
}
