namespace Microsoft.Owin.Security.DataProtection
{
    public interface IDataProtectionProvider
    {
        IDataProtector Create(params string[] purposes);
    }
}
