namespace Microsoft.AspNet.Identity
{
    public interface IRole : IRole<string>
    {
    }

    public interface IRole<out TKey>
    {
        TKey Id { get; }

        string Name { get; set; }
    }
}
