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
    public class ApplicationUsersController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var context = ApplicationDbContext.Create();
            return context.Users.Select(x=>x.UserName);
        } 
    }
}
