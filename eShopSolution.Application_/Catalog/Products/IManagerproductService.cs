
using eShopsolution.Viewmodels.Catalog;
using eShopsolution.Viewmodels.Catalog.Products;
using eShopsolution.Viewmodels.Comons;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.Application_.Catalog.Products
{
    public interface IManagerproductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int ProductId);

      

        Task<bool> UpdatePrice(int productId, decimal newPrice);

        Task<bool> updateStock(int productId, int addedQuantity);

        Task addUpdateViewCount(int productId);

        Task<PageResult<ProductViewModel>> getAllPaging(GetManagaProductPagingRequest request);

        Task<int> AddImage(int productId, List<IFormFile> files);

        Task<int> RemoveImage(int imageId);

        Task<int> UpdateImage(int imageId, string caption,bool IsDefault);

        Task<List<ProductImageViewModel>> GetListImage(int productId);



    }
}
