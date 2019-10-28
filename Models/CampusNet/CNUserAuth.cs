using System.ComponentModel.DataAnnotations;

namespace Freelance_Api.Models.CampusNet
{
    public struct CnUserAuth
    {
        public CnUserAuth(string mAuthUsername, string mAuthPassword)
        {
            this.AuthUsername = mAuthUsername;
            this.AuthPassword = mAuthPassword;
        }
        
        [Required]
        public string AuthUsername { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string AuthPassword { get; set; }
    }
}