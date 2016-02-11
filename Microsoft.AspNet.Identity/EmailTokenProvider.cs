using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public class EmailTokenProvider<TUser> : EmailTokenProvider<TUser, string> where TUser : class, IUser<string>
    {
    }
    public class EmailTokenProvider<TUser, TKey> : TotpSecurityStampBasedTokenProvider<TUser, TKey> where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
    {
        private string _body;
        private string _subject;

        public async override Task<string> GetUserModifierAsync(string purpose, UserManager<TUser, TKey> manager, TUser user)
        {
            var email = await manager.GetEmailAsync(user.Id).WithCurrentCulture<string>();
            return ("Email:" + purpose + ":" + email);
        }

        public async override Task<bool> IsValidProviderForUserAsync(UserManager<TUser, TKey> manager, TUser user)
        {
            var introduced12 = false;
            bool ReflectorVariable0;
            var email = await manager.GetEmailAsync(user.Id).WithCurrentCulture<string>();
            if (!string.IsNullOrWhiteSpace(email))
            {
                introduced12 = await manager.IsEmailConfirmedAsync(user.Id).WithCurrentCulture<bool>();
                ReflectorVariable0 = true;
            }
            else
            {
                ReflectorVariable0 = false;
            }
            return (ReflectorVariable0 && introduced12);
        }

        public override Task NotifyAsync(string token, UserManager<TUser, TKey> manager, TUser user)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return manager.SendEmailAsync(user.Id, Subject, string.Format(CultureInfo.CurrentCulture, BodyFormat, new object[] { token }));
        }

        public string BodyFormat
        {
            get
            {
                return (_body ?? "{0}");
            }
            set
            {
                _body = value;
            }
        }

        public string Subject
        {
            get
            {
                return (_subject ?? string.Empty);
            }
            set
            {
                _subject = value;
            }
        }
        
    }
}
