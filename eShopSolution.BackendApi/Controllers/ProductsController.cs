using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopsolution.Viewmodels.Catalog.ProductImages;
using eShopsolution.Viewmodels.Catalog.Products;
using eShopSolution.Application_.Catalog.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {

        private readonly IPublicProductService _publicProductService;
        private readonly IManagerproductService _PublicMangagerService;


        public ProductsController(IPublicProductService  publicProductService,IManagerproductService publicManagerService)
        {
            _publicProductService = publicProductService;
            _PublicMangagerService = publicManagerService;

        }


        //http://localhost:port/product?pageIndex=1&pageSize=10&categoryId=

        [HttpGet("{languageId}")]
        public async Task<IActionResult> GetAllPaging(String languageId, [FromQuery]GetPublicProductPagingRequest request)
        {
            var products = await _publicProductService.GetAllByCategoryId(languageId,request);
            return Ok(products);
        }



        [HttpGet("{ProductId}/{languageId}")]
        public async Task<IActionResult> GetById(int ProductId,string languageId)
        {
            var product = await _PublicMangagerService.GetById(ProductId,languageId);

            if (product == null) return BadRequest("cannot find Product"); 
            return Ok(product);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _PublicMangagerService.Create(request);

            if (productId == 0)
                return BadRequest();

            var product = await _PublicMangagerService.GetById(productId,request.LanguageId);

            return CreatedAtAction(nameof(GetById),new { id =productId },product) ;
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _PublicMangagerService.Update(request);
            if (affectedResult == 0)
                return BadRequest();

            return Ok();
        }

        [HttpPatch("{ProductId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice( int ProductId,decimal newPrice)
        {
            var isSuccessful = await _PublicMangagerService.UpdatePrice(ProductId,newPrice);
            if (isSuccessful )
                return Ok();

            return Ok();
        }


        [HttpDelete("{ProductId}")]
        public async Task<IActionResult> Delete(int ProductId)
        {
            var affectedResult = await _PublicMangagerService.Delete(ProductId);
            if (affectedResult == 0)
                return BadRequest();

            return Ok();
        }

        //image 

        [HttpPost("{productId}/images")]
        public async Task<IActionResult> CreateImage(int productId,[FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _PublicMangagerService.AddImage(productId,request);

            if (imageId == 0)
                return BadRequest();

            var image = await _PublicMangagerService.GetImageById(imageId);

            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm]ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _PublicMangagerService.UpdateImage(imageId, request);

            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> RemoveImage(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _PublicMangagerService.RemoveImage(imageId);

            if (result == 0)
                return BadRequest();

            return Ok();
        }


        [HttpGet("{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int ProductId, int imageId)
        {
            var image = await _PublicMangagerService.GetImageById(imageId);

            if (image == null) return BadRequest("cannot find Product");
            return Ok(image);
        }

    }
}
