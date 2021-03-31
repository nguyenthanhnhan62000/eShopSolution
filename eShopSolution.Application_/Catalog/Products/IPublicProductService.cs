
using eShopsolution.Viewmodels.Catalog;
using eShopsolution.Viewmodels.Catalog.Products;

using eShopsolution.Viewmodels.Comons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application_.Catalog.Products
{
    public interface IPublicProductService
    {
        Task <PageResult<ProductViewModel>> GetAllByCategoryId(String LanguageId ,GetPublicProductPagingRequest request);

 
    
    }
}
