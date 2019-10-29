namespace Freelance_Api.Models.Responses
{
    public class CompanyDataReponse
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Logo { get; set; }

        public Job[] Jobs { get; set; }

        public string Website { get; set; }

        public Location Location { get; set; }

        public int CompanySize { get; set; }

        
    }
}