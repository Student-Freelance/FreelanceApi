using System.Collections.Generic;
using AspNetCore.Identity.Mongo.Model;
using Freelance_Api.Models;
using Freelance_Api.Models.Identity;
using Freelance_Api.Models.Responses;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Freelance_Api.Services
{
    public class UserService 
    {
        private readonly IMongoCollection<Student> _students;
        private readonly IMongoCollection<Company> _company;

        public UserService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _students = database.GetCollection<Student>("Users");
            _company = database.GetCollection<Company>("Users");
        }

        public List<StudentDataResponse> GetPublicStudents()
        {
            var builder = Builders<Student>.Filter;
            var filter = builder.Eq("_t", "Student");
            var publicata = new List<StudentDataResponse>();
            _students.Find(filter).ToList().ForEach(student =>  publicata.Add(new StudentDataResponse(student){} ));
            return publicata;
        }
        
        public List<CompanyDataReponse> GetPublicCompanies()
        {
            var builder = Builders<Company>.Filter;
            var filter = builder.Eq("_t", "Company");
            var publicata = new List<CompanyDataReponse>();
            _company.Find(filter).ToList().ForEach(company =>  publicata.Add(new CompanyDataReponse(company){} ));
            return publicata;
        }   

        public Student Get(string id) =>
            _students.Find(student => student.Id == id).FirstOrDefault();

        public void Update(string id, Student studentin) =>
            _students.ReplaceOne(student => student.Id == id, studentin);
        
    }
}