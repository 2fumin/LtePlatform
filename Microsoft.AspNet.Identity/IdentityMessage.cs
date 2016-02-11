namespace Microsoft.AspNet.Identity
{
    public class IdentityMessage
    {
        public virtual string Body { get; set; }

        public virtual string Destination { get; set; }

        public virtual string Subject { get; set; }
    }
}
