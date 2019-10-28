namespace Freelance_Api.Models.Responses
{
    public struct LoginResponse
    {
        public LoginResponse(string token, string firstName, string email)
        {
            Token = token;
            FirstName = firstName;
            Email = email;
        }

        public string Token { get; private set; }

        public string FirstName { get; private set; }

        public string Email { get; private set; }
    }
}