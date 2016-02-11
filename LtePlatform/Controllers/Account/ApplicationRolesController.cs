using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LtePlatform.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LtePlatform.Controllers.Account
{
    [Authorize]
    [ApiControl("用户角色管理控制器")]
    public class ApplicationRolesController : ApiController
    {
        [HttpGet]
        [ApiDoc("获取所有角色定义视图")]
        [ApiResponse("所有角色定义视图")]
        public IEnumerable<ApplicationRoleViewModel> Get()
        {
            var context = ApplicationDbContext.Create();
            var roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
            return roleManager.Roles.Select(x=>new ApplicationRoleViewModel
            {
                Name = x.Name,
                RoleId = x.Id
            });
        }

        [HttpPost]
        [ApiDoc("批量向用户添加某个角色")]
        [ApiParameterDoc("dto", "角色添加信息，包含角色名称和待添加的用户列表")]
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

        [HttpGet]
        [ApiDoc("新增一个角色")]
        [ApiParameterDoc("roleName", "角色名称")]
        [ApiResponse("添加是否成功")]
        public bool Get(string roleName)
        {
            var context = ApplicationDbContext.Create();
            var roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
            if (roleManager.RoleExists(roleName)) return false;
            roleManager.Create(new ApplicationRole(roleName));
            return true;
        }
    }
}
