namespace Freelance_Api.Models.Responses
{
    public struct CreateJobResponse
    {
        public CreateJobResponse(string id)
        {
            Id = id;
        }
        public string Id { get; private set; }
    }
}