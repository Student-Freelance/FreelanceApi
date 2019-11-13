using System.Threading;
using Freelance_Api.Models;
using Freelance_Api.Models.Identity;
using Maddalena.Mongo;
using MongoDB.Driver;

namespace Freelance_Api.DatabaseContext
{
    public class MongoDbContext : IMongoDbContext
    {
        public MongoDbContext(string connectionstring, string databasename, string jobcollection)
        {
            Client = new MongoClient(connectionstring);
            DatabaseBase = Client.GetDatabase(databasename);
            Students = DatabaseBase.GetCollection<StudentModel>("Users");
            var indexModel = new CreateIndexModel<StudentModel>(Builders<StudentModel>.IndexKeys.Text(x => x.Tags));
            Students.Indexes.CreateOne(indexModel);
            Companies = DatabaseBase.GetCollection<CompanyModel>("Users");
            Jobs = DatabaseBase.GetCollection<JobModel>(jobcollection);
        }

        public MongoClient Client { get; set; }
        public IMongoDatabase DatabaseBase { get; set; }
        public IMongoCollection<CompanyModel> Companies { get; set; }
        public IMongoCollection<StudentModel> Students { get; set; }
        public IMongoCollection<JobModel> Jobs { get; set; }
    }
}