namespace Freelance_Api.Models.Responses
{
    public class PrivateStudentDataModel: PublicStudentDataModel
    {
        public LocationModel LocationModel { get; set; }
        public string PhoneNumber { get; set; }
    }
}