namespace Microsoft.Owin.Security.DataHandler.Serializer
{
    public static class DataSerializers
    {
        static DataSerializers()
        {
            Properties = new PropertiesSerializer();
            Ticket = new TicketSerializer();
        }

        public static IDataSerializer<AuthenticationProperties> Properties { get; set; }

        public static IDataSerializer<AuthenticationTicket> Ticket { get; set; }
    }
}
