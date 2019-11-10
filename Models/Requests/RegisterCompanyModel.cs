using System.ComponentModel.DataAnnotations;

namespace Freelance_Api.Models.Requests
{
    public class RegisterCompanyModel
    {
        [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "CompanyName")]
            public string CompanyName { get; set; }
            
            [Required]
            [Display(Name = "UserName")]
            public string UserName { get; set; }
            
            [Required]
            [Display(Name = "Vat")]
            public int Vat { get; set; }
            
        
        }
    }
    
