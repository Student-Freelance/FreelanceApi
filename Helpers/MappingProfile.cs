using AutoMapper;
using Freelance_Api.Models;
using Freelance_Api.Models.Identity;
using Freelance_Api.Models.Requests;
using Freelance_Api.Models.Responses;
using MongoDB.Driver.Linq;

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
            CreateMap<RegisterCompanyModel, CompanyModel>().ReverseMap();
            CreateMap<IMongoQueryable<StudentModel>, PublicStudentDataModel>().ReverseMap();
        }
    }
}