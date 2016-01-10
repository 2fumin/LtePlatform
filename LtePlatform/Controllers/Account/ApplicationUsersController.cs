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
    public class ApplicationUsersController : ApiController
    {
        [HttpGet]
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
