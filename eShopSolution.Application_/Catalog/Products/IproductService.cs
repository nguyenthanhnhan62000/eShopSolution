
using eShopsolution.Viewmodels.Catalog;
using eShopsolution.Viewmodels.Catalog.ProductImages;
using eShopsolution.Viewmodels.Catalog.Products;
using eShopsolution.Viewmodels.Comons;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.Application_.Catalog.Products
{
    public interface IproductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int ProductId);


        Task<ProductViewModel> GetById(int ProductId,string languageId);


        Task<bool> UpdatePrice(int productId, decimal newPrice);

        Task<bool> updateStock(int productId, int addedQuantity);

        Task addUpdateViewCount(int productId);

        Task<PageResult<ProductViewModel>> getAllPaging(GetManagaProductPagingRequest request);

        Task<int> AddImage(int productId, ProductImageCreateRequest product);

        Task<int> RemoveImage( int imageId);

        Task<int> UpdateImage( int imageId, ProductImageUpdateRequest product);

        Task<List<ProductImageViewModel>> GetListImage(int productId);

        Task<ProductImageViewModel> GetImageById(int imageId);

        Task<PageResult<ProductViewModel>> GetAllByCategoryId(String LanguageId, GetPublicProductPagingRequest request);




    }
}
