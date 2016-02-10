namespace Microsoft.Owin.Security
{
    public class AuthenticationResponseChallenge
    {
        public AuthenticationResponseChallenge(string[] authenticationTypes, AuthenticationProperties properties)
        {
            AuthenticationTypes = authenticationTypes;
            Properties = properties ?? new AuthenticationProperties();
        }

        public string[] AuthenticationTypes { get; private set; }

        public AuthenticationProperties Properties { get; private set; }
    }
}

