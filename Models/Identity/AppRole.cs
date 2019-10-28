using AspNetCore.Identity.Mongo.Model;

namespace Freelance_Api.Models.Identity
{
    public class AppRole: MongoRole
    {
        private MongoRole _role;
        public AppRole(MongoRole role)
        {
            _role = role;
        }
    }
}