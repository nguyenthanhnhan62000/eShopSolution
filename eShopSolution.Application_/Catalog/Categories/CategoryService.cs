using eShopsolution.Viewmodels.Catalog.Categories;
using eShopSolution.Application_.Common;
using eShopSolution.data_.EF;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
    using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eShopSolution.Application_.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {

        private readonly EShopDBContext _context;
  
  
        public CategoryService(EShopDBContext context  )
        {
            _context = context;
       
        }

        public async Task<List<CategoryVm>> GetAll(String languageId)
        {
            var query =  from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                        //join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        //join c in _context.Categories on pic.CategoryId equals c.Id
                        where ct.LanguageId == languageId
                        select new { c, ct };
            //2.filter
            return await query.Select(x => new CategoryVm()
            {
                Id = x.c.Id,
                Name = x.ct.Name,
                ParentId= x.c.ParentId
            }).ToListAsync() ;
        }

        public async Task<CategoryVm> GetById(string languageId, int id)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                        //join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        //join c in _context.Categories on pic.CategoryId equals c.Id
                        where ct.LanguageId == languageId && c.Id== id
                        select new { c, ct };
            //2.filter
            return await query.Select(x => new CategoryVm()
            {
                Id = x.c.Id,
                Name = x.ct.Name,
                ParentId = x.c.ParentId
            }).FirstOrDefaultAsync();
        }
    }
}
