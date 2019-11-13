using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Freelance_Api.Models.Requests
{
    public class RegisterStudentModel
    {
        [Required]
        [BsonRequired]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        [BsonRequired]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [BsonRequired]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [BsonRequired]
        [Display(Name = "Confirm password")]
        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [BsonRequired]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [BsonRequired]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        
    }
}