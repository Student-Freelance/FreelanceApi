using Freelance_Api.Models;
using Freelance_Api.Models.Identity;
using MongoDB.Driver;

namespace Freelance_Api.DatabaseContext
{
    public interface IMongoDbContext
    {
        MongoClient Client { get; set; }
        IMongoDatabase DatabaseBase { get; set; }
        IMongoCollection<JobModel> Jobs { get; set; }
        IMongoCollection<CompanyModel> Companies { get; set; }
        
        IMongoCollection<StudentModel> Students { get; set; }
  
    }
}