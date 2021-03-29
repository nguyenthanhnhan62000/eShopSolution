using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.Application_.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IPublicProductService _publicProduct;


        public ProductController(IPublicProductService  publicProductService)
        {
            _publicProduct = publicProductService;
        }



        [HttpGet]
        public  async Task<IActionResult> Get()
        {
            var products = await _publicProduct.GetAll();
            return Ok(products);
        }
    }
}
