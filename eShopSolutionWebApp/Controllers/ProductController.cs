using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.ApiIntegration;
using Microsoft.AspNetCore.Mvc;
using eShopsolution.Viewmodels.Catalog.Products;
using eShopSolutionWebApp.Models;

namespace eShopSolutionWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly ICategoryApiClient _categoryApiClient;

        public ProductController(IProductApiClient productApiClient,ICategoryApiClient categoryApiClient)
        {
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }
        public async Task< IActionResult> Detail(int id,string culture)
        {

            var product = await _productApiClient.GetById(id, culture);
            return View(new ProductDetailViewModel()
            {
                Product = product,
                Category = await _categoryApiClient.GetById(culture,id)

            }) ;
        }

        public async Task<IActionResult> Category(int id, String culture, int page = 1)
        {
            var products = await _productApiClient.GetPaging(new GetManagaProductPagingRequest()
            {
                CategoryId = id,
                PageIndex = page,
                LanguageId = culture,
                PageSize=10

            });
            return View(new ProductCategoryViewModel()
            {
                category = await _categoryApiClient.GetById(culture, id),
                products = products
            }) ;
        }
    }
}
