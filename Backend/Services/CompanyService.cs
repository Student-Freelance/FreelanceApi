using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Freelance_Api.DatabaseContext;
using Freelance_Api.Models;
using Freelance_Api.Models.Responses;
using MongoDB.Driver;

namespace Freelance_Api.Services
{
    public class CompanyService
    {
        private readonly IMongoDbContext _context;
        private readonly IMapper _mapper;
        private readonly FilterDefinitionBuilder<CompanyModel> _builder = Builders<CompanyModel>.Filter;
       

        public CompanyService(IMongoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
         
        }

        public async Task<List<PublicCompanyDataModel>> GetPublicCompanies()
        {
            var filter = _builder.Eq("_t", "CompanyModel");
            var users = await _context.Companies.Find(filter).ToListAsync();
            var returnlist = _mapper.Map<List<PublicCompanyDataModel>>(users);
            return returnlist;
        }
        
        public async Task<PublicCompanyDataModel> GetCompanyById(string companyname)
        {
            var filter = _builder.Regex(e=> e.CompanyName, $"/^{companyname}$/i"); 
            var mongouser = await _context.Companies.Find(filter).FirstOrDefaultAsync();
            var returnuser = _mapper.Map<PublicCompanyDataModel>(mongouser);
            return returnuser;
        }
    }
}