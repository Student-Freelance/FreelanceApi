using Freelance_Api.Models.Identity;

namespace Freelance_Api.Models
{
    public class CompanyModel : AppUserModel
    {
        public JobModel[] Jobs { get; set; }
        public string About { get; set; }
        public int CompanySize { get; set; }
        public int Vat { get; set; }
        public string CompanyName { get; set; }
    }
}