namespace Freelance_Api.Models
{
    public class DatabaseSettings: IDatabaseSettings
    {
        public string JobCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IDatabaseSettings
    {
        string JobCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}