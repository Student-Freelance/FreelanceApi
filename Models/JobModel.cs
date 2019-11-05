using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Freelance_Api.Models
{
    public class JobModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string Title { get; set; }
        public int Salary { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public bool PaidMonthly { get; set; } 
        public bool PaidHourly { get; set; } 
        public string Experience { get; set; }
        public DateTime JobStart { get; set; }
        public DateTime JobEnd { get; set; }

        
    }
}