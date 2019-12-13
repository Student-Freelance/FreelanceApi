using System.Collections.Generic;
using Freelance_Api.DatabaseContext;
using Freelance_Api.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Freelance_Api.Services
{
    public class JobService 
    {
        private readonly IMongoDbContext _context;
        

        public JobService(IMongoDbContext context)
        {
            _context = context;
        }

        public List<JobModel> Get() =>
            _context.Jobs.Find(job => true).ToList();

        public JobModel Get(string id) =>
            _context.Jobs.Find(student => student.Id == id).FirstOrDefault();

        
        
        public string Create(JobModel jobModel)
        {
            jobModel.Id = ObjectId.GenerateNewId().ToString();
          //  _context.Jobs.InsertOne(jobModel);
            return jobModel.Id;
        }

        public void Update(string id, JobModel jobin) =>
            _context.Jobs.ReplaceOne(job => job.Id == id, jobin);

        public void Remove(JobModel jobin) =>
            _context.Jobs.DeleteOne(job => job.Id == jobin.Id);

        public void Remove(string id) =>
            _context.Jobs.DeleteOne(job => job.Id == id);
    }
}