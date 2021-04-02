using eShopsolution.Viewmodels.Catalog;
using eShopsolution.Viewmodels.Catalog.ProductImages;
using eShopsolution.Viewmodels.Catalog.Products;
using eShopsolution.Viewmodels.Comons;
using eShopSolution.Application_.Common;
using eShopSolution.data_.EF;
using eShopSolution.data_.Entities;
using eShopSolution.Utilities.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application_.Catalog.Products
{
    public class ProductService : IproductService
    {
        private readonly EShopDBContext _context;
        private readonly IStorageService _str;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        public ProductService(EShopDBContext context,IStorageService storageService)
        {
            _context = context;
            _str = storageService;
        }

        public async Task addUpdateViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
    
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name= request.Name,
                        Description= request.Description,
                        Details = request.Details,
                        SeoDescription= request.SeoDescription,
                        SeoTitle= request.SeoTitle,
                        SeoAlias=request.SeoAlias,
                        LanguageId=request.LanguageId
                 
                    }
                }
            };
            //save image 
            if(request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail image ",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1

                    }
                };
            }

            _context.Products.Add(product);
             await _context.SaveChangesAsync();
            return product.Id;
        
        }

        public async Task<int> Delete(int ProductId)
        {
            var product = await _context.Products.FindAsync(ProductId);
            if (product == null) throw new eShopException($"Can not find a product: {ProductId}");


            var images =  _context.ProductImages.Where(i => i.ProductId == ProductId);

            foreach (var image in images)
            {
                await _str.DeleteFileAsync(image.ImagePath);
            }

            _context.Products.Remove(product);

            return await _context.SaveChangesAsync();
        }


     


        public async Task<PageResult<ProductViewModel>> getAllPaging(GetManagaProductPagingRequest request)
        {
            //1.select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                      
                        select new { p,pt,pic};
            //2.filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));

            if(request.CategoryIds.Count >0)
            {
                query = query.Where(p => request.CategoryIds.Contains(p.pic.CategoryId));
            }

            //3.paging

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x=>new ProductViewModel()
                {
                    Id= x.p.Id,
                    Name = x.pt.Name,
                    DateCreated=x.p.DateCreated,
                    Description=x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId=x.pt.LanguageId,
                    OriginalPrice=x.p.OriginalPrice,
                    Price=x.p.Price,
                    SeoAlias=x.pt.SeoAlias,
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
                Items =  data,

            };
        }
        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);

            var productTranslations = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == request.Id
            && x.LanguageId == request.LanguageId);
            if (product == null || productTranslations == null) throw new eShopException($"Can not find a product id:{request.Id}");

            productTranslations.Name = request.Name;
            productTranslations.SeoAlias = request.SeoAlias;
            productTranslations.SeoDescription = request.SeoDescription;
            productTranslations.SeoTitle = request.SeoTitle;
            productTranslations.Description = request.Description;
            productTranslations.Details = request.Details;

            if (request.ThumbNailImage != null)
            {
                var thumbnailImage = await _context.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.ProductId == request.Id);
                if(thumbnailImage != null)
                {

                    thumbnailImage.FileSize = request.ThumbNailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbNailImage);
                    _context.ProductImages.Update(thumbnailImage);
                }
            }

            return await _context.SaveChangesAsync();


        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null ) throw new eShopException($"Can not find a product id:{productId}");
            product.Price = newPrice;

            return await  _context.SaveChangesAsync() > 0;


        }

        public async Task<bool> updateStock(int productId, int addedQuantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new eShopException($"Can not find a product id:{productId}");
            product.Stock += addedQuantity;
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _str.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

   

        public async Task<ProductViewModel> GetById(int ProductId,string languageId)
        {
            var product = await _context.Products.FindAsync(ProductId);
            var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == ProductId 
            &&x.LanguageId == languageId );

            var productViewModel = new ProductViewModel()
            { 
                Id = product.Id,
                DateCreated = product.DateCreated,
                Description = productTranslation != null ? productTranslation.Description : null,
                LanguageId = productTranslation.LanguageId,
                Details = productTranslation != null ? productTranslation.Details : null,
                Name = productTranslation != null ? productTranslation.Name : null,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                SeoAlias = productTranslation != null ? productTranslation.SeoAlias : null,
                SeoDescription = productTranslation != null ? productTranslation.SeoDescription : null,
                SeoTitle = productTranslation != null ? productTranslation.SeoTitle : null,
                Stock = product.Stock,
                ViewCount = product.ViewCount
            };
            return productViewModel;
        }


        public async Task<int> AddImage(int productId, ProductImageCreateRequest request)
        {
            var productImage = new ProductImage()
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,
                ProductId = productId,
                SortOrder = request.SortOrder
            };

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);

                productImage.FileSize = request.ImageFile.Length; 
  
            }
            _context.ProductImages.Add(productImage);
             await _context.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task<int> RemoveImage( int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null) throw new eShopException($"cannot find an image with id {imageId}");
            _context.ProductImages.Remove(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage( int imageId, ProductImageUpdateRequest request)
        {
            var productImage =await _context.ProductImages.FindAsync(imageId);
            if (productImage == null)
                throw new eShopException($"cannot find an image with id {imageId}");

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);

                productImage.FileSize = request.ImageFile.Length;

            }
            _context.ProductImages.Update(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ProductImageViewModel>> GetListImage(int productId)
        {
            return await  _context.ProductImages.Where(x => x.ProductId == productId)
                .Select(i => new ProductImageViewModel()
            {
                Caption = i.Caption,
                DateCreated = i.DateCreated,
                FileSize = i.FileSize,
                Id=i.Id,
                IsDefault = i.IsDefault,
                SortOrder = i.SortOrder,
                ProductId = i.ProductId,
                ImagePath = i.ImagePath
            }).ToListAsync() ;
        }

        public async Task<ProductImageViewModel> GetImageById(int imageId)
        {
                var image = await _context.ProductImages.FindAsync(imageId);
            if (image == null)
                throw new eShopException($"cannot find an image with id {imageId}");

            var viewModel =  new ProductImageViewModel()
               {
                   Caption = image.Caption,
                   DateCreated = image.DateCreated,
                   FileSize = image.FileSize,
                   Id = image.Id,
                   IsDefault = image.IsDefault,
                   SortOrder = image.SortOrder,
                   ProductId = image.ProductId,
                   ImagePath = image.ImagePath
               };
            return viewModel;
        }
        public async Task<PageResult<ProductViewModel>> GetAllByCategoryId(String languageId, GetPublicProductPagingRequest request)
        {
            //1.select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        where pt.LanguageId == languageId
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
