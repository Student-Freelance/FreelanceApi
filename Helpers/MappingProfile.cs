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
            CreateMap<AppUserModel, StudentModel>().ReverseMap().ForAllMembers(opt =>
                opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
            CreateMap<AppUserModel, CompanyModel>().ReverseMap().ForAllMembers(opt =>
                opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
            CreateMap<StudentModel, PrivateStudentDataModel>().ReverseMap().ForAllMembers(opt =>
                opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
            CreateMap<StudentModel, PublicStudentDataModel>().ReverseMap().ForAllMembers(opt =>
                opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
            CreateMap<CompanyModel, PrivateCompanyDataModel>().ReverseMap().ForAllMembers(opt =>
                opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
            CreateMap<CompanyModel, PublicCompanyDataModel>().ReverseMap().ForAllMembers(opt =>
                opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
            CreateMap<RegisterCompanyModel, CompanyModel>().ReverseMap().ForAllMembers(opt =>
                opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
            CreateMap<IMongoQueryable<StudentModel>, PublicStudentDataModel>().ReverseMap().ForAllMembers(opt =>
                opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
         
        }
    }
}