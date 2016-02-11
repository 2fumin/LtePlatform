using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public class PhoneNumberTokenProvider<TUser> : PhoneNumberTokenProvider<TUser, string> where TUser : class, IUser<string>
    {
    }

    public class PhoneNumberTokenProvider<TUser, TKey> : TotpSecurityStampBasedTokenProvider<TUser, TKey> where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
    {
        private string _body;

        public async override Task<string> GetUserModifierAsync(string purpose, UserManager<TUser, TKey> manager, TUser user)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            var phoneNumber = await manager.GetPhoneNumberAsync(user.Id).WithCurrentCulture();
            return ("PhoneNumber:" + purpose + ":" + phoneNumber);
        }

        public async override Task<bool> IsValidProviderForUserAsync(UserManager<TUser, TKey> manager, TUser user)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            var phoneNumber = await manager.GetPhoneNumberAsync(user.Id).WithCurrentCulture();
            var introduced12 = false;
            bool ReflectorVariable0;

            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                introduced12 = await manager.IsPhoneNumberConfirmedAsync(user.Id).WithCurrentCulture();
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
            return manager.SendSmsAsync(user.Id, string.Format(CultureInfo.CurrentCulture, MessageFormat, token));
        }

        public string MessageFormat
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
        
    }
}
