using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Freelance_Api.Models
{
    public class Job
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Title { get; set; }
        public int Salary { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public bool MonthlyOrHourlyPaid { get; set; } //TODO: Boolean eller enum. 
        public string Experience { get; set; }
        public DateTime JobStart { get; set; }
        public DateTime JobEnd { get; set; }
    }
}