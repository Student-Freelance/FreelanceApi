using System;
using System.Runtime;

namespace Freelance_Api.Models.Responses
{
    public class StudentDataResponse
    {
        public StudentDataResponse(Student student)
        {
            Firstname = student.Firstname;
            Lastname = student.Lastname;
            University = student.University;
            Semester = student.Semester;
            Ranking = student.Ranking;
            Username = student.UserName;
            Education = student.Education;
            Tags = student.Tags;
            Email = student.Email;
            Website = student.Email;
            Avatar = student.Logo;
            Availability = student.Availability;


        }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string University { get; set; }
        public int Semester { get; set; }
        public string Ranking { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string[] Tags { get; set; }
        public string Website { get; set; }
        public string Education { get; set; }
        public string Avatar { get; set; }
        public int Availability { get; set; }
        

    }
}