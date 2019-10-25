using System.Collections.Generic;
using Freelance_Api.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Freelance_Api.Services
{
    public class EmployerService
    {
        private readonly IMongoCollection<Employe> _employe;

        public EmployerService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _employe = database.GetCollection<Employe>(settings.EmployerCollectionName);
        }

        public List<Employe> Get() =>
            _employe.Find(employe => true).ToList();

        public Employe Get(string id) =>
            _employe.Find(employe => employe.Id == id).FirstOrDefault();

        public Employe Create(Employe employe)
        {
            employe.Id = ObjectId.GenerateNewId().ToString();
            _employe.InsertOne(employe);
            return employe;
        }

        public void Update(string id, Employe employein) =>
            _employe.ReplaceOne(employe => employe.Id == id, employein);
       

        public void Remove(Employe employein) =>
            _employe.DeleteOne(employe => employe.Id == employein.Id);


        public void Remove(string id) =>
            _employe.DeleteOne(employe => employe.Id == id);


    }
}