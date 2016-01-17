using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LtePlatform.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LtePlatform.Controllers.Account
{
    public class ApplicationRolesController : ApiController
    {
        [HttpGet]
        public IEnumerable<IdentityRole> Get()
        {
            var context = ApplicationDbContext.Create();
            return context.Roles;
        }

        [HttpPost]
        public void Post(RoleUsersDto dto)
        {
            var context = ApplicationDbContext.Create();
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));

            if (!roleManager.RoleExists(dto.RoleName)) return;
            foreach (
                var user in
                    dto.UserNames.Select(userName => userManager.FindByName(userName))
                        .Where(user => user != null)
                        .Where(user => !userManager.IsInRole(user.Id, dto.RoleName)))
            {
                userManager.AddToRole(user.Id, dto.RoleName);
            }
        }
    }
}
