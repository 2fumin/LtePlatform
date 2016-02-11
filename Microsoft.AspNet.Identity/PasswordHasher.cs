namespace Microsoft.AspNet.Identity
{
    public class PasswordHasher : IPasswordHasher
    {
        public virtual string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        public virtual PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            return Crypto.VerifyHashedPassword(hashedPassword, providedPassword)
                ? PasswordVerificationResult.Success
                : PasswordVerificationResult.Failed;
        }
    }
}
