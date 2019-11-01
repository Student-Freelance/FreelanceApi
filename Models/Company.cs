using Freelance_Api.Models.Identity;

namespace Freelance_Api.Models
{
    public class Company : AppUser
    {
        public Job[] Jobs { get; set; }
        public string About { get; set; }
        public int CompanySize { get; set; }
    }
}