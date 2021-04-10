using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.Application_.Catalog.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryService _CategoryService;


        public CategoriesController(ICategoryService CategoryService)
        {
            _CategoryService = CategoryService;

        }


        [HttpGet]
        public async Task<IActionResult> GetAll(String languageId)
        {
            var products = await _CategoryService.GetAll(languageId);
            return Ok(products);
        }

    }
}
