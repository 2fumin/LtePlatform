using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Abp.Net.Mail.Smtp;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using LtePlatform.Models;

namespace LtePlatform
{
    public class UserEmailSenderConfiguration : ISmtpEmailSenderConfiguration
    {
        public string DefaultFromAddress => "ouyh19@189.cn";

        public string DefaultFromDisplayName => "Ouyang Hui";

        public string Host => "smtp.189.cn";

        public int Port => 25;

        public string UserName => "ouyh19";

        public string Password => "md@287965";

        public string Domain => "";

        public bool EnableSsl => false;

        public bool UseDefaultCredentials => false;
    }

    public class EmailService : IIdentityMessageService
    {
        private readonly SmtpEmailSender _smtpEmailSender;

        public EmailService()
        {
            var configuration = new UserEmailSenderConfiguration();
            _smtpEmailSender = new SmtpEmailSender(configuration);
        }

        public Task SendAsync(IdentityMessage message)
        {
            // 在此处插入电子邮件服务可发送电子邮件。
            return Task.Run(() =>
            {
                _smtpEmailSender.Send(message.Destination, message.Subject, message.Body);
            });
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // 在此处插入 SMS 服务可发送短信。
            return Task.FromResult(0);
        }
    }

    // 配置要在此应用程序中使用的应用程序用户管理器。

    // 配置要在此应用程序中使用的应用程序登录管理器。
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager)
        { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
