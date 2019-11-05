using System.ComponentModel.DataAnnotations;
using MongoDB.Driver.Core.Authentication;

namespace Freelance_Api.Models.Requests
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}