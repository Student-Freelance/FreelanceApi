using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Freelance_Api.Models.Responses
{
    public class PublicCompanyDataModel
    {
        public PublicCompanyDataModel()
        {

        }
        public string CompanyName { get; set; }

        public string About { get; set; }

        public string Logo { get; set; }

        public List<string> Jobs { get; set; }

        public string Website { get; set; }

        public LocationModel LocationModel { get; set; }

        public int CompanySize { get; set; }

        public int Vat { get; set; }
        
        public string PhoneNumber { get; set; }
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        

        
    }
}