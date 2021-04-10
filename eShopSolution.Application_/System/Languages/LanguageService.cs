using eShopsolution.Viewmodels.Comons;
using eShopsolution.Viewmodels.System;
using eShopsolution.Viewmodels.System.Languages;
using eShopSolution.data_.EF;
using eShopSolution.data_.Entities;
using eShopSolution.Utilities.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application_.System.Languages
{
    public class LanguageService : ILanguageService
    {

        private readonly IConfiguration _config;
        private readonly EShopDBContext _context;


        public LanguageService(IConfiguration config, EShopDBContext context)
        {
            _config = config;
            _context = context; 
        }

        public async Task<ApiResult<List<LanguageVm>>> GetAll()
        {
            var languages=  await _context.Languages.Select(x => new LanguageVm()
            {
                Id= x.Id,
                Name= x.Name
            }).ToListAsync();

            return new ApiSuccessResult<List<LanguageVm>>(languages);
        }
    }
}
