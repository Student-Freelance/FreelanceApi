using System;
using AspNetCore.Identity.Mongo.Model;

namespace Freelance_Api.Models.Identity
{
    public class AppUserModel: MongoUser
    {
        public LocationModel Location { get; set; }
        public string Website { get; set; }
        
        public string Logo { get; set; }
        
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}