using System;
using LtePlatform.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace LtePlatform
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // �����û�������֤�߼�
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // �����������֤�߼�
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // �����û�����Ĭ��ֵ
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // ע��˫�������֤�ṩ���򡣴�Ӧ�ó���ʹ���ֻ��͵����ʼ���Ϊ����������֤�û��Ĵ����һ������
            // ����Ա�д�Լ����ṩ���򲢽�����뵽�˴���
            manager.RegisterTwoFactorProvider("�绰����", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "��İ�ȫ������ {0}"
            });
            manager.RegisterTwoFactorProvider("�����ʼ�����", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "��ȫ����",
                BodyFormat = "��İ�ȫ������ {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}