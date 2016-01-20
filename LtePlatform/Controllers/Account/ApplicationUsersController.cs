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
    [Authorize]
    [ApiControl("应用程序用户管理控制器")]
    public class ApplicationUsersController : ApiController
    {
        [HttpGet]
        [ApiDoc("获得目前所有用户信息列表")]
        [ApiResponse("应用程序中已注册的所有用户信息列表")]
        public IEnumerable<ApplicationUserViewModel> Get()
        {
            var context = ApplicationDbContext.Create();
            return context.Users.Select(x => new ApplicationUserViewModel
            {
                UserName = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Hometown = x.Hometown
            });
        } 
    }
}
