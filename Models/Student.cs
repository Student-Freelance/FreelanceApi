﻿using System;
using Freelance_Api.Models.Identity;

namespace Freelance_Api.Models
{
    public class Student : AppUser
    {
        public DateTime Birthday { get; set; }
        public string[] Tags { get; set; }
        public string Education { get; set; }
        public int Availability { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string University { get; set; }
        public int Semester { get; set; }
        public string Ranking { get; set; }
    }
}