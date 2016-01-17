using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LtePlatform.Models;
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

        public void Post(RoleUsersDto dto)
        {
            var context = ApplicationDbContext.Create();
            var role = context.Roles.FirstOrDefault(x => x.Name == dto.RoleName);
            if (role == null) return;
            foreach (var userName in dto.UserNames)
            {
                var currentUser = context.Users.FirstOrDefault(x => x.UserName == userName);
                var currentRole = currentUser?.Roles.FirstOrDefault(x => x.RoleId == dto.RoleName && x.UserId == userName);
                if (currentRole == null) continue;
                
            }
        }
    }
}
