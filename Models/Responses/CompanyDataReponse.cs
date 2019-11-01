namespace Freelance_Api.Models.Responses
{
    public class CompanyDataReponse
    {
        public CompanyDataReponse(Company company)
        {
            Name = company.UserName;
            About = company.About;
            Logo = company.Logo;
            Jobs = company.Jobs;
            Website = company.Website;
            Location = company.Location;
            CompanySize = company.CompanySize;

        }
        public string Name { get; set; }

        public string About { get; set; }

        public string Logo { get; set; }

        public Job[] Jobs { get; set; }

        public string Website { get; set; }

        public Location Location { get; set; }

        public int CompanySize { get; set; }
        

        
    }
}