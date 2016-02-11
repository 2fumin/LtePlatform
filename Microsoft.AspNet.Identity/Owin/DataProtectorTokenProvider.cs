using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin.Security.DataProtection;

namespace Microsoft.AspNet.Identity.Owin
{
    public class DataProtectorTokenProvider<TUser> : DataProtectorTokenProvider<TUser, string> where TUser : class, IUser<string>
    {
        public DataProtectorTokenProvider(IDataProtector protector) : base(protector)
        {
        }
    }

    public class DataProtectorTokenProvider<TUser, TKey> : IUserTokenProvider<TUser, TKey> where TUser : class, IUser<TKey> 
        where TKey : IEquatable<TKey>
    {
        public DataProtectorTokenProvider(IDataProtector protector)
        {
            if (protector == null)
            {
                throw new ArgumentNullException(nameof(protector));
            }
            this.Protector = protector;
            this.TokenLifespan = TimeSpan.FromDays(1.0);
        }

        public async Task<string> GenerateAsync(string purpose, UserManager<TUser, TKey> manager, TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var stream = new MemoryStream();
            using (var writer = stream.CreateWriter())
            {
                writer.Write(DateTimeOffset.UtcNow);
                writer.Write(Convert.ToString(user.Id, CultureInfo.InvariantCulture));
                writer.Write(purpose ?? "");
                string stamp = null;
                if (manager.SupportsUserSecurityStamp)
                {
                    stamp = await manager.GetSecurityStampAsync(user.Id).WithCurrentCulture();
                }
                writer.Write(stamp ?? "");

            }
            var inArray = ((DataProtectorTokenProvider<TUser, TKey>)this).Protector.Protect(stream.ToArray());
            return Convert.ToBase64String(inArray);
        }

        public Task<bool> IsValidProviderForUserAsync(UserManager<TUser, TKey> manager, TUser user)
        {
            return Task.FromResult(true);
        }

        public Task NotifyAsync(string token, UserManager<TUser, TKey> manager, TUser user)
        {
            return Task.FromResult(0);
        }

        public async Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser, TKey> manager, TUser user)
        {
            try
            {
                var buffer = this.Protector.Unprotect(Convert.FromBase64String(token));
                var reader = new MemoryStream(buffer).CreateReader();
                try
                {
                    var creationTime = reader.ReadDateTimeOffset();
                    var expirationTime = creationTime + ((DataProtectorTokenProvider<TUser, TKey>)this).TokenLifespan;
                    if (expirationTime < DateTimeOffset.UtcNow) return false;
                    var a = reader.ReadString();
                    if (!string.Equals(a, Convert.ToString(user.Id, CultureInfo.InvariantCulture))) return false;
                    var purp = reader.ReadString();
                    if (!string.Equals(purp, purpose)) return false;
                    var stamp = reader.ReadString();
                    if (reader.PeekChar() != -1) return false;
                    if (!manager.SupportsUserSecurityStamp)
                    {
                        return (stamp == "");
                    }
                    var expectedStamp = await manager.GetSecurityStampAsync(user.Id).WithCurrentCulture();
                    return (stamp == expectedStamp);
                }
                finally
                {
                    reader?.Dispose();
                }
            }
            catch
            {
                // ignored
            }
            return false;
        }

        public IDataProtector Protector { get; }

        public TimeSpan TokenLifespan { get; set; }
        
    }
}
