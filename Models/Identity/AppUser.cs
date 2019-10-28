using System;
using AspNetCore.Identity.Mongo.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Freelance_Api.Models.Identity
{
    public class AppUser: MongoUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Institutionname { get; set; }
        public string Expertisefield { get; set; }
        public int Semester { get; set; }
        public string Ranking { get; set; }
        public string Location { get; set; }
        public string Logo { get; set; } // URL
        public string Website { get; set; }
        public int CompanySize { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}