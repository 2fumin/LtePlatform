using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.OAuth
{
    public abstract class BaseValidatingContext<TOptions> : BaseContext<TOptions>
    {
        protected BaseValidatingContext(IOwinContext context, TOptions options) : base(context, options)
        {
        }

        public virtual void Rejected()
        {
            IsValidated = false;
            HasError = false;
        }

        public void SetError(string error, string errorDescription = null)
        {
            SetError(error, errorDescription, null);
        }

        public void SetError(string error, string errorDescription, string errorUri)
        {
            Error = error;
            ErrorDescription = errorDescription;
            ErrorUri = errorUri;
            Rejected();
            HasError = true;
        }

        public virtual bool Validated()
        {
            IsValidated = true;
            HasError = false;
            return true;
        }

        public string Error { get; private set; }

        public string ErrorDescription { get; private set; }

        public string ErrorUri { get; private set; }

        public bool HasError { get; private set; }

        public bool IsValidated { get; private set; }
    }
}
