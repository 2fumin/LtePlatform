using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.AspNet.Identity
{
    internal static class Rfc6238AuthenticationService
    {
        private static readonly Encoding _encoding = new UTF8Encoding(false, true);
        private static readonly TimeSpan _timestep = TimeSpan.FromMinutes(3.0);
        private static readonly DateTime _unixEpoch = new DateTime(0x7b2, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static byte[] ApplyModifier(byte[] input, string modifier)
        {
            if (string.IsNullOrEmpty(modifier))
            {
                return input;
            }
            var bytes = _encoding.GetBytes(modifier);
            var dst = new byte[input.Length + bytes.Length];
            Buffer.BlockCopy(input, 0, dst, 0, input.Length);
            Buffer.BlockCopy(bytes, 0, dst, input.Length, bytes.Length);
            return dst;
        }

        private static int ComputeTotp(HashAlgorithm hashAlgorithm, ulong timestepNumber, string modifier)
        {
            var bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((long)timestepNumber));
            var buffer2 = hashAlgorithm.ComputeHash(ApplyModifier(bytes, modifier));
            var index = buffer2[buffer2.Length - 1] & 15;
            var num2 = ((((buffer2[index] & 0x7f) << 0x18) | ((buffer2[index + 1] & 0xff) << 0x10)) |
                        ((buffer2[index + 2] & 0xff) << 8)) | (buffer2[index + 3] & 0xff);
            return (num2 % 0xf4240);
        }

        public static int GenerateCode(SecurityToken securityToken, string modifier = null)
        {
            if (securityToken == null)
            {
                throw new ArgumentNullException(nameof(securityToken));
            }
            var currentTimeStepNumber = GetCurrentTimeStepNumber();
            using (var hmacsha = new HMACSHA1(securityToken.GetDataNoClone()))
            {
                return ComputeTotp(hmacsha, currentTimeStepNumber, modifier);
            }
        }

        private static ulong GetCurrentTimeStepNumber()
        {
            var span = (TimeSpan)(DateTime.UtcNow - _unixEpoch);
            return (ulong)(span.Ticks / _timestep.Ticks);
        }

        public static bool ValidateCode(SecurityToken securityToken, int code, string modifier = null)
        {
            if (securityToken == null)
            {
                throw new ArgumentNullException(nameof(securityToken));
            }
            var currentTimeStepNumber = GetCurrentTimeStepNumber();
            using (var hmacsha = new HMACSHA1(securityToken.GetDataNoClone()))
            {
                for (var i = -2; i <= 2; i++)
                {
                    if (ComputeTotp(hmacsha, currentTimeStepNumber + (ulong)i, modifier) == code)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
