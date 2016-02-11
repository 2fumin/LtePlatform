namespace Microsoft.AspNet.Identity.EntityFramework
{
    public class IdentityUserClaim : IdentityUserClaim<string>
    {
    }

    public class IdentityUserClaim<TKey>
    {
        public virtual string ClaimType { get; set; }

        public virtual string ClaimValue { get; set; }

        public virtual int Id { get; set; }

        public virtual TKey UserId { get; set; }
    }
}
