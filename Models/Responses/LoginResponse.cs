namespace Freelance_Api.Models.Responses
{
    public struct LoginResponse
    {
        public LoginResponse(string token)
        {
            Token = token;
        }

        public string Token { get; private set; }
    }
}