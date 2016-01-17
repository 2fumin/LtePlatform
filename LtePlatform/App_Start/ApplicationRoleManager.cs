using LtePlatform.Models;
using Microsoft.AspNet.Identity;

namespace LtePlatform
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store) : base(store)
        {
            
        }
    }
}
