namespace Freelance_Api.Models.Responses
{
    public struct LoginResponseModel
    {
        public LoginResponseModel(string token)
        {
            Token = token;
        }

        public string Token { get; private set; }
    }
}