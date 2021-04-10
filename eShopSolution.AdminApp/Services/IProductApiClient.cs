using eShopsolution.Viewmodels.Catalog;
using eShopsolution.Viewmodels.Catalog.Products;
using eShopsolution.Viewmodels.Comons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    
    public interface IProductApiClient
    {
        Task<PageResult<ProductVm>> GetPaging(GetManagaProductPagingRequest request);

        Task<bool> CreateProduct(ProductCreateRequest request);

        Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);
        Task<ProductVm> GetById(int id, String languageId);


    }
}
