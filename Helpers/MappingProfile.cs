using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Freelance_Api.Models;
using Freelance_Api.Models.Identity;
using Freelance_Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Freelance_Api.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUserModel, StudentModel>().ReverseMap();
            CreateMap<AppUserModel, CompanyModel>().ReverseMap();
            CreateMap<StudentModel, PrivateStudentDataModel>().ReverseMap();
            CreateMap<StudentModel, PublicStudentDataModel>().ReverseMap();
            CreateMap<CompanyModel, PrivateCompanyDataModel>().ReverseMap();
            CreateMap<CompanyModel, PublicCompanyDataModel>().ReverseMap();
           
         
        }
    }
}