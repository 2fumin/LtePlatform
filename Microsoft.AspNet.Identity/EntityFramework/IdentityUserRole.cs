namespace Microsoft.AspNet.Identity.EntityFramework
{
    public class IdentityUserRole : IdentityUserRole<string>
    {
    }

    public class IdentityUserRole<TKey>
    {
        public virtual TKey RoleId { get; set; }

        public virtual TKey UserId { get; set; }
    }
}
