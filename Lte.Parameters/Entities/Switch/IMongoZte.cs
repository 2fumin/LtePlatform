namespace Lte.Parameters.Entities.Switch
{
    public interface IMongoZte
    {
        int eNodeB_Id { get; set; }

        string eNodeB_Name { get; set; }

        string lastModifedTime { get; set; }

        string iDate { get; set; }

        string parentLDN { get; set; }

        string description { get; set; }
    }
}