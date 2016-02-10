namespace Microsoft.Owin.Security.DataProtection
{
    public interface IDataProtector
    {
        byte[] Protect(byte[] userData);

        byte[] Unprotect(byte[] protectedData);
    }
}
