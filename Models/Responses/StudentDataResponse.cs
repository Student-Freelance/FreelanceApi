using System;
using System.Runtime;

namespace Freelance_Api.Models.Responses
{
    public class StudentDataResponse
    {
        public string Name { get; set; }

        public DateTime Birthday  { get; set; }

        public string[] Tags { get; set; }

        public string Website { get; set; }

        public string Education { get; set; }
        
        public string Avatar { get; set; }

        public int Availability { get; set; }
    }
}