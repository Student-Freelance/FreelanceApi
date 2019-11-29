using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime;

namespace Freelance_Api.Models.Responses
{
    public class PublicStudentDataModel
    {
        public PublicStudentDataModel()
        {

        }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string University { get; set; }
        public int Semester { get; set; }
        public string Ranking { get; set; }
        public string Username { get; set; }
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string[] Tags { get; set; }
        public string Website { get; set; }
        public string Logo { get; set; }
        public int Availability { get; set; }
        public string[] Education { get; set; }
        public string[] Experience { get; set; }
        public string[] Competences { get; set; }
        public string Resume { get; set; }
        

    }
}