using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.Application_.Catalog.Categories;
using eShopSolution.Application_.Untilities.Slides;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SlideController : ControllerBase
    {

        private readonly ISlideService _SliderService;


        public SlideController(ISlideService SliderService)
        {
            _SliderService = SliderService;

        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var slides = await _SliderService.GetAll();
            return Ok(slides);
        }

    }
}
