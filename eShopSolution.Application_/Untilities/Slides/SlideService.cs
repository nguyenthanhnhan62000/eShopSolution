using eShopsolution.Viewmodels.System.Roles;
using eShopsolution.Viewmodels.Untilities.Slides;
using eShopSolution.data_.EF;
using eShopSolution.data_.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application_.Untilities.Slides
{
    public class SlideService : ISlideService
    {
        private readonly IConfiguration _config;
        private readonly EShopDBContext _context;


        public SlideService(IConfiguration config, EShopDBContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task<List<SlideVm>> GetAll()
        {
            var Slides = await _context.Slides.OrderBy(x=> x.SortOrder)
                .Select(x => new SlideVm()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Url = x.Url,
                    Image=x.Image

                }).ToListAsync();

            return Slides;
        }
    }
}