using AspNetCore.Identity.Mongo.Model;


namespace Freelance_Api.Models.Identity
{
    public class AppRoleModel: MongoRole
    {
        public string Student { get; set; }
        public string Company { get; set; }
    }
}