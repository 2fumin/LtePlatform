using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Microsoft.AspNet.Identity
{
    internal static class Crypto
    {
        private const int PBKDF2IterCount = 0x3e8;
        private const int PBKDF2SubkeyLength = 0x20;
        private const int SaltSize = 0x10;

        [MethodImpl(MethodImplOptions.NoOptimization)]
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }
            if (((a == null) || (b == null)) || (a.Length != b.Length))
            {
                return false;
            }
            var flag = true;
            for (var i = 0; i < a.Length; i++)
            {
                flag &= a[i] == b[i];
            }
            return flag;
        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            using (var bytes = new Rfc2898DeriveBytes(password, SaltSize, PBKDF2IterCount))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(PBKDF2SubkeyLength);
            }
            var dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, SaltSize);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, PBKDF2SubkeyLength);
            return Convert.ToBase64String(dst);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            var src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            var dst = new byte[SaltSize];
            Buffer.BlockCopy(src, 1, dst, 0, SaltSize);
            var buffer3 = new byte[PBKDF2SubkeyLength];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, PBKDF2SubkeyLength);
            using (var bytes = new Rfc2898DeriveBytes(password, dst, PBKDF2IterCount))
            {
                buffer4 = bytes.GetBytes(PBKDF2SubkeyLength);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }
    }
}
