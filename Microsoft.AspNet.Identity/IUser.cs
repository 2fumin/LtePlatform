namespace Microsoft.AspNet.Identity
{
    public interface IUser : IUser<string>
    {
    }

    public interface IUser<out TKey>
    {
        TKey Id { get; }

        string UserName { get; set; }
    }
}