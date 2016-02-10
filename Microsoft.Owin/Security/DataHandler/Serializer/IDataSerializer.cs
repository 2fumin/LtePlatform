namespace Microsoft.Owin.Security.DataHandler.Serializer
{
    public interface IDataSerializer<TModel>
    {
        TModel Deserialize(byte[] data);

        byte[] Serialize(TModel model);
    }
}
