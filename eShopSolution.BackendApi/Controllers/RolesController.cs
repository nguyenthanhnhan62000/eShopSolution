using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.Application_.Untilities.Slides;
using eShopSolution.data_.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly ISlideService _roleService;
        public RolesController(ISlideService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            var roles = await _roleService.GetAll();
            return  Ok(roles);

        }
    }
}
