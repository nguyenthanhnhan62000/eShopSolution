using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.Application_.Catalog.Products;
using eShopSolution.Application_.System.Languages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageService _languageService;


        public LanguagesController(ILanguageService languageService)
        {
            _languageService = languageService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _languageService.GetAll();
            return Ok(products);
        }


    }
}
