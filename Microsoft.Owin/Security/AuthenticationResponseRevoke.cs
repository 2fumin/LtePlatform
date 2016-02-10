namespace Microsoft.Owin.Security
{
    public class AuthenticationResponseRevoke
    {
        public AuthenticationResponseRevoke(string[] authenticationTypes) : this(authenticationTypes, new AuthenticationProperties())
        {
        }

        public AuthenticationResponseRevoke(string[] authenticationTypes, AuthenticationProperties properties)
        {
            AuthenticationTypes = authenticationTypes;
            Properties = properties;
        }

        public string[] AuthenticationTypes { get; private set; }

        public AuthenticationProperties Properties { get; private set; }
    }
}
