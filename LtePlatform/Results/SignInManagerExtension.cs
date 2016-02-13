using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LtePlatform.Models;
using Microsoft.AspNet.Identity.Owin;

namespace LtePlatform.Results
{
    public static class SignInManagerExtension
    {
        public async static Task CheatTwoFactorSingIn(this SignInManager<ApplicationUser, string> manager,
            bool isPersistent, bool rememberBrowser)
        {
            var userId = await manager.GetVerifiedUserIdAsync();
            var user = await manager.UserManager.FindByIdAsync(userId);
            await manager.UserManager.ResetAccessFailedCountAsync(user.Id);
            await manager.SignInAsync(user, isPersistent, rememberBrowser);
        }
    }
}
