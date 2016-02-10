namespace Microsoft.Owin.Security.DataHandler.Encoder
{
    public interface ITextEncoder
    {
        byte[] Decode(string text);

        string Encode(byte[] data);
    }
}
