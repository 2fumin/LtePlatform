namespace Lte.Domain.Lz4Net.Core
{
    public interface INameTransform
    {
        string TransformDirectory(string name);
        string TransformFile(string name);
    }
}

