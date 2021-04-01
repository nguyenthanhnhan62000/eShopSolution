
using eShopSolution.data_.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using eShopsolution.Viewmodels.Comons;
using eShopsolution.Viewmodels.Catalog.Products;
using eShopsolution.Viewmodels.Catalog;

namespace eShopSolution.Application_.Catalog.Products
{
    public class PublicProductService : IPublicProductService
    {

        private readonly EShopDBContext _context;
        public PublicProductService(EShopDBContext context)
        {
            _context = context;
        }

    

        public async Task<PageResult<ProductViewModel>> GetAllByCategoryId(String languageId,GetPublicProductPagingRequest request)
        {
            //1.select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        where pt.LanguageId== languageId
                        select new { p, pt, pic };
            //2.filter
         

            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
            {
                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
            }

            //3.paging

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();
            ;

            //4.select and projection
            return new PageResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data,

            };
        }

       
    }
}
