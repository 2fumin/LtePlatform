namespace Microsoft.Owin.Security
{
    public abstract class AuthenticationOptions
    {
        private string _authenticationType;

        protected AuthenticationOptions(string authenticationType)
        {
            Description = new AuthenticationDescription();
            AuthenticationType = authenticationType;
            AuthenticationMode = AuthenticationMode.Active;
        }

        public AuthenticationMode AuthenticationMode { get; set; }

        public string AuthenticationType
        {
            get
            {
                return _authenticationType;
            }
            set
            {
                _authenticationType = value;
                Description.AuthenticationType = value;
            }
        }

        public AuthenticationDescription Description { get; set; }
    }
}
