using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Freelance_Api.DatabaseContext;
using Freelance_Api.Extensions;
using Freelance_Api.Models;
using Freelance_Api.Models.Identity;
using Freelance_Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Freelance_Api.Services
{
    public class StudentService
    {
        private readonly IMongoDbContext _context;
        private readonly IMapper _mapper;
        private readonly FilterDefinitionBuilder<StudentModel> _builder = Builders<StudentModel>.Filter;

        public StudentService(IMongoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PublicStudentDataModel>> GetPublicStudents()
        {
            var filter = _builder.Eq("_t", "StudentModel");
            var users = await _context.Students.Find(filter).ToListAsync();
            var returnlist =_mapper.Map<List<PublicStudentDataModel>>(users);
            return returnlist;
        }

        public async Task<List<PublicStudentDataModel>>GetStudentsByTag(string tag)
        {
            var result = _context.Students.AsQueryable().WhereText(tag).ToList();
            var model = _mapper.Map<List<PublicStudentDataModel>>(result);
            return model;
        }
    }
}